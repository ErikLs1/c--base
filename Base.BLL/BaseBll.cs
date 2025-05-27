using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace Base.BLL;

public class BaseBll<TUow> : IBaseBll
    where TUow : IBaseUow
{
    protected readonly TUow BllUow;

    public BaseBll(TUow bllUow)
    {
        BllUow = bllUow;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await BllUow.SaveChangesAsync();
    }
}