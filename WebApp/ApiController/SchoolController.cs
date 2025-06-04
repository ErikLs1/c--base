using App.DAL.EF;
using App.DTO.V1.DTO;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiController;

[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SchoolController : ControllerBase
{

    private readonly AppDbContext _db;
    
   
    public SchoolController(AppDbContext db)
    {
        _db = db;
    }

    [ProducesResponseType(typeof(SchoolDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<List<SchoolDto>>> GetAllSchools()
    {
        var list = await _db.Schools
            .AsNoTracking()
            .Select(s => new SchoolDto {
                Id   = s.Id,
                SchoolName = s.SchoolName
            })
            .ToListAsync();

        return list;
    }
    
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AssignSchool([FromBody] AssignSchoolDto dto)
    {
        var userId = User.GetUserId();

        var person = await _db.Persons
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if (person == null)
            return NotFound(new { Message = "User profile not found." });

        var exists = await _db.Schools
            .AnyAsync(s => s.Id == dto.SchoolId);

        if (!exists)
            return NotFound(new { Message = "School not found." });

        person.SchoolId = dto.SchoolId;
        await _db.SaveChangesAsync();
        return NoContent();
    }
}