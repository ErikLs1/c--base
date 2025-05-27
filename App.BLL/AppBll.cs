using App.BLL.Contracts;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.Contracts;
using Base.BLL;

namespace App.BLL;

public class AppBll : BaseBll<IAppUow>, IAppBll
{
    public AppBll(IAppUow uow) : base(uow)
    {
    }

   
    private IPersonService? _personService;
    public IPersonService PersonService =>
        _personService ??= new PersonService(
            BllUow, 
            new PersonBllMapper());
    
}