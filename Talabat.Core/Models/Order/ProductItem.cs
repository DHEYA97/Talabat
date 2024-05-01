using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order
{
	public class ProductItem
	{
        public ProductItem()
        {
            
        }
        public ProductItem(int productId, int productName, int picturUrl)
		{
			ProductId = productId;
			ProductName = productName;
			PicturUrl = picturUrl;
		}

		public int ProductId { get; set; }
		public int ProductName { get; set; }
		public int PicturUrl { get; set; }
	}
}
