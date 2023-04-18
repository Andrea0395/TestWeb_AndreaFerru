using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWeb.DbContexts;
using TestWeb.Dtos;
using TestWeb.Entities;

namespace TestWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _db;

    public EmployeesController(AppDbContext db) {
        _db = db;
    }

    public async Task<IEnumerable<EmployeeRowDto>> List(string filter) {
        return await _db.Employees
            .Select(e => new EmployeeRowDto {
                Id = e.Id,
            })
            .ToListAsync();
    }

    [HttpPost]
    public async Task AddVacation(AddVacationDto dto) {
        var employee = await _db.Employees
            .SingleOrDefaultAsync(x => true);
        employee.Vacations.Add(new VacationEntity { Start = dto.Start, End = dto.End });
        await _db.SaveChangesAsync();
    }

    [HttpGet]
    public async Task<IEnumerable<EmployeeVacationDto>> GetVacationDays()
    {
        var employees = await _db.Employees
            .Include(x => x.Vacations)
            .OrderBy(e => e.Surname)
            .ThenBy(e => e.Name)
            .ToListAsync();
        var result = employees.Select(employee => new EmployeeVacationDto
        {
            NameSurname = $"{employee.Name} + {" "} + {employee.Surname}",
            TotalVacationDays = 0
        });
        return result;
    }
}
