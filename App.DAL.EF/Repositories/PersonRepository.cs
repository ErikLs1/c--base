using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PersonRepository : BaseRepository<PersonDalDto, Person>, IPersonRepository
{
    public PersonRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new PersonUowMapper())
    {
    }
    
    public override async Task<IEnumerable<PersonDalDto>> AllAsync(Guid userId = default)
    {
        var query = GetQuery(userId);
        query = query.Include(p => p.User);
        return (await query.ToListAsync()).Select(e => Mapper.Map(e)!);
    }

    public async Task<int> GetPersonCountByNameAsync(string name, Guid userId)
    {
        var query = GetQuery(userId);
        return await query
            .CountAsync(p => p.PersonFirstName == name);
    }

    public async Task<PersonDalDto> FindByUserIdAsync(Guid userId)
    {
        var query = GetQuery(userId);
        var res = await query.FirstOrDefaultAsync(p => p.UserId == userId);
        return Mapper.Map(res)!;
    }

    public async Task<Guid> FindPersonIdByUserIdAsync(Guid userId)
    {
        var person = await RepositoryDbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId);

        return person!.Id;
    }
}