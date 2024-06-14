using POS.Domain.Entities;

namespace POS.Infraestructura.Persistences.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> AccountByUserName(string userName);
    }
}
