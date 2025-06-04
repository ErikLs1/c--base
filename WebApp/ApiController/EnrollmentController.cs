using System.Security.Claims;
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
public class EnrollmentController : ControllerBase
{
    private readonly AppDbContext _db;
   
    public EnrollmentController(AppDbContext db)
    {
        _db = db;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RequestEnrollment([FromBody] RequestEnrollmentDto dto)
    {
        var appUserId = User.GetUserId();
        var student = await _db.Persons.FirstOrDefaultAsync(p => p.UserId == appUserId);
        if (student == null)
            return NotFound("Student profile not found.");

        var ss = await _db.SemesterSubjects.FindAsync(dto.SemesterSubjectId);
        if (ss == null)
            return NotFound("SemesterSubject not found.");

        var already = await _db.Enrollments
            .AnyAsync(e => e.StudentId == student.Id
                           && e.SemesterSubjectId == dto.SemesterSubjectId);
        if (already) 
            return BadRequest("You already requested enrollment for this subject.");

        var enroll = new App.Domain.Enrollment
        {
            Id = Guid.NewGuid(),
            StudentId  = student.Id,
            SchoolId = ss.SchoolId,
            SemesterSubjectId  = ss.Id,
            EnrollmentDate = DateTime.UtcNow,
            StudentAccepted = false,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            CreatedBy = User.FindFirst(ClaimTypes.Email)?.Value ?? "system"
        };
        _db.Enrollments.Add(enroll);
        await _db.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetEnrollmentStatus([FromBody] SetEnrollmentStatusDto dto)
    {
        var enroll = await _db.Enrollments.FindAsync(dto.EnrollmentId);
        if (enroll == null)
            return NotFound("Enrollment not found.");

        enroll.StudentAccepted = dto.Accepted;
        enroll.Status = dto.Accepted ? "Accepted" : "Rejected";
        await _db.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<EnrollmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EnrollmentDto>>> GetMyEnrollments()
    {
        var appUserId = User.GetUserId();
        var student = await _db.Persons.FirstOrDefaultAsync(p => p.UserId == appUserId);
        if (student == null)
            return NotFound("Student profile not found.");

        var list = await _db.Enrollments
            .AsNoTracking()
            .Include(e => e.SemesterSubject).ThenInclude(ss => ss!.Subject)
            .Where(e => e.StudentId == student.Id)
            .Select(e => new EnrollmentDto {
                Id = e.Id,
                SubjectName = e.SemesterSubject!.Subject!.SubjectName,
                SubjectCode = e.SemesterSubject.Subject.SubjectCode,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status!,
                StudentAccepted = e.StudentAccepted
            })
            .ToListAsync();

        return Ok(list);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<EnrollmentForTeacherDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EnrollmentForTeacherDto>>> GetEnrollmentsForTeacher()
    {
        var appUserId = User.GetUserId();
        var teacher = await _db.Persons.FirstOrDefaultAsync(p => p.UserId == appUserId);
        if (teacher == null)
            return NotFound("Teacher profile not found.");

        var list = await _db.Enrollments
            .AsNoTracking()
            .Include(e => e.Student)
            .Include(e => e.SemesterSubject).ThenInclude(ss => ss!.Subject)
            .Where(e => e.SemesterSubject!.TeacherId == teacher.Id)
            .Select(e => new EnrollmentForTeacherDto {
                EnrollmentId = e.Id,
                SubjectName = e.SemesterSubject!.Subject!.SubjectName,
                StudentId = e.StudentId,
                StudentName = e.Student!.PersonFirstName + " " + e.Student.PersonLastName,
                EnrollmentDate = e.EnrollmentDate,
                Status = e.Status!,
                StudentAccepted = e.StudentAccepted
            })
            .ToListAsync();

        return Ok(list);
    }
}