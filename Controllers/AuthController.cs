using Microsoft.AspNetCore.Mvc;
using MiniShop.Dtos;
using MiniShop.Services;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly CustomerService _service;
    
    public AuthController(CustomerService service) => _service = service;
    
    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(CustomerRegisterDto dto)
    {
        try
        {
            var customer = await _service.RegisterAsync(dto);
            return Ok(new { message = "Kayıt başarılı", customer });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        try
        {
            var customer = await _service.LoginAsync(dto.Email, dto.Password);
            return Ok(new { message = "Giriş başarılı", customer });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }
    
    // POST: api/auth/change-password
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        try
        {
            await _service.ChangePasswordAsync(dto.CustomerId, dto.OldPassword, dto.NewPassword);
            return Ok(new { message = "Şifre başarıyla değiştirildi" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}