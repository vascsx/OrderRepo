using System.ComponentModel.DataAnnotations;

namespace OrderAPI.DTOs
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public int ClienteId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal ValorTotal { get; set; }
    }
}