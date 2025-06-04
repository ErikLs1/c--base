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
public class SemesterSubjectController : ControllerBase
{
    private readonly AppDbContext _db;
    
   
    public SemesterSubjectController(AppDbContext db)
    {
        _db = db;
    }
    
    
    [HttpGet]
    [ProducesResponseType(typeof(List<SemesterSubjectDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SemesterSubjectDto>>> GetSemesterSubjectsForTeacher()
    {
        var teacherId = User.GetUserId();

        var list = await _db.SemesterSubjects
            .AsNoTracking()
            .Include(ss => ss.Subject)
            .Select(ss => new SemesterSubjectDto
            {
                Id = ss.Id,
                Name = ss.Subject!.SubjectName,
                Description = ss.Subject.SubjectDescription,
                Eap = ss.Subject.Eap,
                Assigned = false
            })
            .ToListAsync();

        return Ok(list);
    }
    [HttpGet]
    [ProducesResponseType(typeof(List<SemesterSubjectDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SemesterSubjectDto>>> GetSemesterSubjectForCurrentTeacher()
    {
        var appUserId = User.GetUserId();
        var teacherPerson = await _db.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == appUserId);

        if (teacherPerson == null)
            return NotFound("Teacher profile not found.");

        var list = await _db.SemesterSubjects
            .AsNoTracking()
            .Include(ss => ss.Subject)
            .Where(ss => ss.TeacherId == teacherPerson.Id)
            .Select(ss => new SemesterSubjectDto {
                Id = ss.Id,
                Name = ss.Subject!.SubjectName,
                Description = ss.Subject.SubjectDescription,
                Eap = ss.Subject.Eap,
                Assigned = true
            })
            .ToListAsync();

        return Ok(list);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<IActionResult> AssignSemesterSubject([FromBody] AssignSemesterSubjectDto dto)
    {
        var appUserId = User.GetUserId();

        var teacherPerson = await _db.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == appUserId);

        if (teacherPerson == null)
            return NotFound(new { Message = "Teacher profile not found." });

        var ss = await _db.SemesterSubjects.FindAsync(dto.SemesterSubjectId);
        if (ss == null)
            return NotFound(new { Message = "SemesterSubject not found." });

        ss.TeacherId = teacherPerson.Id;
        await _db.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<SemesterSubjectDto>>> GetAvailableSubjectsForEnrollment()
    {
        var appUserId = User.GetUserId();
        var student = await _db.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == appUserId);
        if (student == null)
        {
            return NotFound("Student profile not found.");
        }

        var list = await _db.SemesterSubjects
            .AsNoTracking()
            .Include(ss => ss.Subject)
            .Where(ss =>
                ss.TeacherId != null
                && !_db.Enrollments.Any(e =>
                    e.StudentId == student.Id
                    && e.SemesterSubjectId == ss.Id
                )
            )
            .Select(ss => new SemesterSubjectDto {
                Id = ss.Id,
                Name = ss.Subject!.SubjectName,
                Description = ss.Subject.SubjectDescription,
                Eap = ss.Subject.Eap,
                Assigned = false  
            })
            .ToListAsync();

        return Ok(list);
    }
}