using OrderAPI.Enum;

namespace OrderAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public OrderStatus Status { get; set; } 
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}