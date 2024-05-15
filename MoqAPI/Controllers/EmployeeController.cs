using Microsoft.AspNetCore.Mvc;
using MoqAPI.Data.Interfaces;
using MoqAPI.Model;
using System;
using System.Threading.Tasks;

namespace MoqAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _employeeRepository.Get();
            return result != null ? Ok(result) : NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _employeeRepository.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("")]
    public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
    {
        try
        {
            var result = await _employeeRepository.Post(employee);
            return result != null ? Ok(result) : NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

