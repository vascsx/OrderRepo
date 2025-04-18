using System.ComponentModel.DataAnnotations;

namespace OrderAPI.DTOs
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "O valor do pedido é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal Value { get; set; }

    }
}