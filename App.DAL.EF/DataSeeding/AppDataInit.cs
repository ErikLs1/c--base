using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.DataSeeding;

public class AppDataInit
{
    public static void SeedAppData(AppDbContext context)
    {
        foreach (var s in InitialData.Schools)
        {
            var already = context.Schools.Any(x => x.SchoolName == s.schoolName);
            if (already) continue;

            var school = new School
            {
                Id = s.id ?? Guid.NewGuid(),
                SchoolName = s.schoolName,
                SchoolAddress = s.schoolAddress,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            var result = context.Schools.Add(school);
            if (result.State != EntityState.Added) 
                throw new ApplicationException("School creation failed!");
        }
        context.SaveChanges();

        
        var schoolMap = context.Schools
                               .ToDictionary(k => k.SchoolName, v => v.Id);

        foreach (var subj in InitialData.Subjects)
        {
            var entity = new Subject
            {
                Id                = subj.id ?? Guid.NewGuid(),
                SubjectName       = subj.subjectName,
                SubjectCode       = subj.subjectCode,
                Eap               = subj.eap,
                SchoolId          = schoolMap[subj.schoolName],
                CreatedAt         = DateTime.UtcNow,
                CreatedBy         = "system"
            };

            context.Subjects.Add(entity);
        }
        context.SaveChanges();
        
        foreach (var sem in InitialData.Semesters)
        {
            var entity = new Semester
            {
                Id = sem.id ?? Guid.NewGuid(),
                SemesterName  = sem.semesterName,
                SemesterYear  = new DateOnly(sem.semesterYear, 1, 1),
                CreatedAt     = DateTime.UtcNow,
                CreatedBy     = "system"
            };

            context.Semesters.Add(entity);
        }
        context.SaveChanges();
    }

    public static void MigrateDatabase(AppDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DeleteDatabase(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        foreach (var (roleName, id) in InitialData.Roles)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;

            if (role != null) continue;

            role = new AppRole()
            {
                Id = id ?? Guid.NewGuid(),
                Name = roleName,
            };

            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException("Role creation failed!");
            }
        }

        foreach (var userInfo in InitialData.Users)
        {
            var user = userManager.FindByEmailAsync(userInfo.name).Result;
            if (user == null)
            {
                user = new AppUser()
                {
                    Id = userInfo.id ?? Guid.NewGuid(),
                    Email = userInfo.name,
                    UserName = userInfo.name,
                    EmailConfirmed = true,
                    FirstName = userInfo.firstName,
                    LastName = userInfo.lastName,
                };
                var result = userManager.CreateAsync(user, userInfo.password).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }
            }

            foreach (var role in userInfo.roles)
            {
                if (userManager.IsInRoleAsync(user, role).Result)
                {
                    Console.WriteLine($"User {user.UserName} already in role {role}");
                    continue;
                }

                var roleResult = userManager.AddToRoleAsync(user, role).Result;
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                else
                {
                    Console.WriteLine($"User {user.UserName} added to role {role}");
                }
            }
        }
    }
}