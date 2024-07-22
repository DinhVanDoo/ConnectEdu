using ConnectEduV2.Interfaces;
using ConnectEduV2.Models;

namespace ConnectEduV2.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        int getTotalUsers();
    }
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly ConnectEduV1Context _dbContext;
        public UserRepository(ConnectEduV1Context dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int getTotalUsers()
        {
            return _dbContext.Users.Count();
        }
    }
}
