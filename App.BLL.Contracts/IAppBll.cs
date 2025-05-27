using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBll : IBaseBll
{
    IPersonService PersonService { get; }
}