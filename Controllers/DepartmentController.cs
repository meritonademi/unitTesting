using ItemManagementSystem1.Models;
using ItemManagementSystem1.Services.DepartmentService;
using Microsoft.AspNetCore.Mvc;

namespace ItemManagementSystem1.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
    {
        var items = await _departmentService.GetAllDepartment();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartmentById(int id)
    {
        var item = await _departmentService.GetDepartmentById(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Department>> CreateDepartment(Department item)
    {
        await _departmentService.CreateDepartment(item);
        return Ok(item);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(int id, Department department)
    {
        if (id != department.Id)
        {
            return BadRequest();
        }

        try
        {
            var message = await _departmentService.UpdateDepartmentAsync(id, department);
            return Ok(message);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDepartment(int id)
    {
        var item = await _departmentService.GetDepartmentById(id);
        if (item == null)
        {
            return NotFound();
        }

        var message = await _departmentService.DeleteDepartment(item.Id);
        return Ok(message);
    }
}