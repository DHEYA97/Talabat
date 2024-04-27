using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
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

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
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
			var userDto = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokenService.GenerateToken(user, _userManager)
			};
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
			var userDto = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokenService.GenerateToken(user, _userManager)
			};
			return Ok(userDto);
		}
	}

}