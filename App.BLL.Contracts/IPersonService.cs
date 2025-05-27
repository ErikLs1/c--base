using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IPersonService : IBaseService<PersonBllDto>, IPersonRepositoryCustom
{
    Task<Guid> GetPersonIdByUserIdAsync(Guid userId);
}