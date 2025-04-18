using Microsoft.AspNetCore.Mvc;
using OrderAPI.Data;
using OrderAPI.Models;
using OrderAPI.Dtos;
using System;
using OrderAPI.DTOs;
using OrderAPI.Enum;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        // Valida o modelo recebido
        if (!ModelState.IsValid)
        {
            return BadRequest("Dados inválidos. Verifique os campos enviados.");
        }

        // Mapeia o DTO para a entidade Order
        var order = new Order
        {
            ClienteId = orderDto.ClienteId,
            ValorTotal = orderDto.ValorTotal,
            Status = OrderStatus.Created
        };

        try
        {
            // Salva o pedido no banco de dados
            _context.Orders.Add(order); // Corrigido para usar o DbSet correto
            await _context.SaveChangesAsync();

            // Retorna uma resposta com o ID do pedido criado
            return CreatedAtAction(nameof(CreateOrder), new { id = order.Id }, new
            {
                Message = "Pedido criado com sucesso!",
                OrderId = order.Id
            });
        }
        catch (Exception ex)
        {
            // Tratamento de exceções
            return StatusCode(500, $"Ocorreu um erro ao criar o pedido: {ex.Message}");
        }
    }
}