using Microsoft.AspNetCore.Mvc;
using OrderAPI.Data;
using OrderAPI.Models;
using OrderAPI.DTOs;
using OrderAPI.Enum;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Cria um novo pedido.
    /// </summary>
    /// <param name="orderDto">Dados do pedido enviados pelo cliente.</param>
    /// <returns>Retorna o status da criação do pedido.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        // Validação de entrada
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retorna erros detalhados de validação
        }

        // Mapeamento do DTO para a entidade Order
        var order = new Order
        {
            ClientName = orderDto.ClientName,
            Value = orderDto.Value,
            Status = OrderStatus.Created
        };

        try
        {
            // Adiciona o pedido ao banco de dados
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Retorna sucesso com CreatedAtAction e link para o recurso criado
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, new
            {
                Message = "Pedido criado com sucesso!",
                OrderId = order.Id
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                ErrorMessage = "Ocorreu um erro interno ao criar o pedido.",
                ExceptionMessage = ex.Message
            });
        }
    }

    /// <summary>
    /// Retorna os detalhes de um pedido pelo ID.
    /// </summary>
    /// <param name="id">Identificador do pedido.</param>
    /// <returns>Os detalhes do pedido.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(new { Message = $"Pedido com ID {id} não encontrado." });
            }

            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                ErrorMessage = "Ocorreu um erro interno ao buscar o pedido.",
                ExceptionMessage = ex.Message
            });
        }
    }
}