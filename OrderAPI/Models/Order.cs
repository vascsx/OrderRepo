using OrderAPI.Enum;

namespace OrderAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public decimal Value { get; set; }
        public OrderStatus Status { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    }
}