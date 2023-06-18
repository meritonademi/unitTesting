using ItemManagementSystem1.DTOs;
using ItemManagementSystem1.Models;
using ItemManagementSystem1.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace ItemManagementSystem1.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployee()
    {
        var items = await _employeeService.GetAllEmployee();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var item = await _employeeService.GetEmployeeById(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDTO item)
    {
        Employee employee = new Employee()
        {
            Name = item.Name,
            SurName = item.SurName,
            Tel = item.Tel,
            DepartmentId = item.DepartmentId
        };
        await _employeeService.CreateEmployee(employee);
        return Ok(item);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmployeeAsync(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }

        try
        {
            var message = await _employeeService.UpdateEmployeeAsync(id, employee);
            return Ok(message);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployee(int id)
    {
        var item = await _employeeService.GetEmployeeById(id);
        if (item == null)
        {
            return NotFound();
        }

        var message = await _employeeService.DeleteEmployee(item.Id);
        return Ok(message);
    }
}