using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UmaMahesh_BackApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : Controller
{
    private  readonly MongoDBRepositoryContext _mongoDbRepositoryContext;
    private readonly ILogger<StudentsController> _logger;


    public StudentsController(MongoDBRepositoryContext mongoDbRepositoryContext, ILogger<StudentsController> logger)
    {
        _mongoDbRepositoryContext = mongoDbRepositoryContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _mongoDbRepositoryContext.students.Select(stud => stud).ToListAsync();
        return Ok(students);
    }
}
