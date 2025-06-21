using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _authDbContext;

        #region Ctor
        public UserRepository(AuthDbContext context)
        {
            _authDbContext = context;
        }
        #endregion

        #region Public Functions
        public async Task<User?> GetAsync(string email)
        {
            return await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task AddAsync(User user)
        {
            await _authDbContext.Users.AddAsync(user);
            await _authDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
