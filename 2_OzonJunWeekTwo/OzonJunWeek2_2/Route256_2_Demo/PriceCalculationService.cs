using System.Collections.Generic;
using System.Linq;

namespace Route256_2_Demo
{
    public class PriceCalculationService
    {
        private readonly IDeliveryCalculator _deliveryCalculator;

        public PriceCalculationService(IDeliveryCalculator deliveryCalculator)
        {
            _deliveryCalculator = deliveryCalculator;
        }

        public decimal CalculatePrice(ICollection<GoodItem> goods)
        {
            var itemPrice = goods
                .Select(x => x.Price)
                .Sum();
            var deliveryPrice = goods
                .Select(x => _deliveryCalculator.GetPrice(x))
                .Sum();
            var totalPrice = itemPrice + deliveryPrice;
            return totalPrice;
        }
    }

    public interface IDeliveryCalculator
    {
        decimal GetPrice(GoodItem goodItem);
    }
    
    public class DeliveryCalculator : IDeliveryCalculator
    {
        public DeliveryCalculator(
            string dataBaseConnectionString,
            int deliveryRatio,
            string otherServiceUrl
            )
        {
            
        }

        public decimal GetPrice(GoodItem goodItem)
        {
            return 0;
        }
    }

    public class GoodItem
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
    }
}