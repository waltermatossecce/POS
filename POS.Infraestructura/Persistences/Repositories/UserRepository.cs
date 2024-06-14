using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructura.Persistences.Context;
using POS.Infraestructura.Persistences.Interfaces;

namespace POS.Infraestructura.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly POSContext _context;

        public UserRepository(POSContext context):base(context)
        {
            _context = context;
        }
        public async Task<User> AccountByUserName(string userName)
        {
            var account = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName!.Equals(userName));
            return account!;
        }
    }
}
