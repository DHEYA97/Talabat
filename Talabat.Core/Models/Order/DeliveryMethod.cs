using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order
{
	public class DeliveryMethod : BaseEntity
	{
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, string deliveryTime, decimal deliveryAmount)
		{
			ShortName = shortName;
			Description = description;
			DeliveryTime = deliveryTime;
			DeliveryAmount = deliveryAmount;
		}

		public string ShortName {  get; set; }
		public string Description {  get; set; }
		public string DeliveryTime {  get; set; }
		public decimal DeliveryAmount { get; set; }
	}
}
