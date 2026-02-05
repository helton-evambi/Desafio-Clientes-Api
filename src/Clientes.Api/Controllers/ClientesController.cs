using Clientes.Api.Data;
using Clientes.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetClientes()
    {
        var clientes = await _context.Clientes.ToListAsync();
        return Ok(clientes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
    {
        if (DublicatedEmail(cliente.Email))
        {
            return Conflict(new { message = "Já existe um cliente com este email." });
        }

        cliente.Id = Guid.NewGuid();
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetClientes), new { id = cliente.Id }, cliente);
    }

    private bool DublicatedEmail(string email)
    {
        return _context.Clientes.Any(e => e.Email == email);  
    }
}