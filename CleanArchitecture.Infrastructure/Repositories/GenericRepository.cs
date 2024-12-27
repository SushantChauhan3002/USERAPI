using CleanArchitecture.Application.GenericRepository;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Response;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitecture.Infrastructure.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UserDbContext _UserDbContext;
        private readonly DbSet<T> _dbSet;

        

        public GenericRepository(UserDbContext context)
        {
            _UserDbContext = context;
            _dbSet = _UserDbContext.Set<T>();

        }

        public async Task<AdminUser> GetByUsernameAsync(string username)
        {
            return await _UserDbContext.Set<AdminUser>().FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<Result<AdminUser>> GetByEmailAsync(string email)
        {
            try
            {
                var user = await _UserDbContext.Set<AdminUser>().FirstOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    return Result<AdminUser>.Success(user); // Return success if user found
                }

                return Result<AdminUser>.Fail("User not found."); // Return failure if no user is found

            }
            catch (Exception ex)
            {
               
                return Result<AdminUser>.Fail(ex.Message, 500);
            }
        }



        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _UserDbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _UserDbContext.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _UserDbContext.SaveChangesAsync();
            }
        }
    }
}

