using Demo.Redis.Entity;

namespace Demo.Redis.IRepository
{
    public interface IUserRepository:IRepositoryBase<Account> 
    {
        Account FindAccountById(int id);
    }
}
