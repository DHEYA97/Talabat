using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Filter;
using Talabat.APIs.Hellper;
using Talabat.Core.Models.Idintity;
using Talabat.Core.Service.Interfaces;

namespace Talabat.APIs.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_mapper = mapper;
		}
		[HttpPost("SingUp")]
		public async Task<ActionResult<UserDto>> SingUp(RegisterDto registerDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ApiResponseError(StatusCodes.Status400BadRequest, "Enter Corect Data"));
			}
			var user = await _userManager.FindByEmailAsync(registerDto.Email);
			if(user is not null)
			{
				return  BadRequest(new ApiResponseError(StatusCodes.Status400BadRequest, "Email Already exists "));
			}
			user = new ApplicationUser
			{
				Email = registerDto.Email,
				DisplayName = registerDto.DisplayName,
				PhoneNumber = registerDto.PhoneNumber,
				UserName = registerDto.Email.Split("@")[0]
			};
			var result = await _userManager.CreateAsync(user,registerDto.Password);
			if(!result.Succeeded)
			{
				return BadRequest(new ApiResponseError(StatusCodes.Status400BadRequest));
			}
			result = await _userManager.AddToRolesAsync(user, registerDto.Roles);
			if(!result.Succeeded)
			{
				return BadRequest(new ApiResponseError(StatusCodes.Status400BadRequest));
			}
			var token = await _tokenService.GenerateToken(user, _userManager);
			var userDto = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = token
			};
			Response.Cookies.Append("token", token);
			return Ok(userDto);
		}

		[HttpPost("SingIn")]
		public async Task<ActionResult<UserDto>> SingIn(LogInDto LogInDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ApiResponseError(StatusCodes.Status400BadRequest, "Enter Corect Data"));
			}
			var user = await _userManager.FindByEmailAsync(LogInDto.Email);
			if (user is  null)
			{
				return Unauthorized(new ApiResponseError(StatusCodes.Status401Unauthorized, "Unauthorized"));
			}
			var result = await _signInManager.CheckPasswordSignInAsync(user, LogInDto.Password,false);
			if (!result.Succeeded)
			{
				return Unauthorized(new ApiResponseError(StatusCodes.Status401Unauthorized, "Unauthorized"));
			}
			var token = await _tokenService.GenerateToken(user, _userManager);
			var userDto = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = token
			};
			Response.Cookies.Append("token", token);
			return Ok(userDto);
		}
		[Jwt]
		[HttpGet("GetCurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(userEmail);
			return Ok (new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = Request.Cookies["token"] ?? await _tokenService.GenerateToken(user, _userManager)
		});
		}
		[Jwt]
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto>> Addres()
		{
			var user = await _userManager.FirstOrDefaultUserWithAddress(User);
			var address = _mapper.Map<AddressDto>(user!.Addres);
			return Ok(address);
		}

		[Jwt]
		[HttpPut("Address")]
		public async Task<ActionResult<AddressDto>> Address(AddressDto model)
		{
			var user = await _userManager.FirstOrDefaultUserWithAddress(User);
			var address = _mapper.Map<Addres>(model);
			user.Addres = address;
			var result = await _userManager.UpdateAsync(user);
			if(!result.Succeeded)
				return BadRequest(new ApiResponseError(StatusCodes.Status400BadRequest, "Updated Failed"));
			return Ok(model);
		}
	}
}