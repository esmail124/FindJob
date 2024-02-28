using FindJob.Core;
using FindJob.Data.Entities;
using FindJob.Data.Repositories.JobsRepository;
using FindJob.Data.Repository;
using MongoDB.Driver;

namespace FindJob.Data.Repositories.RoleRepository
{
    public class RoleRepository : MongoRepository<AppRole>, IRoleRepository
    {
        public RoleRepository(FindJobDatabaseSettings findJobDatabaseSettings)
            : base(new MongoClient(findJobDatabaseSettings.ConnectionString).GetDatabase(findJobDatabaseSettings.DatabaseName), nameof(AppRole))
        {
        }
    }
}
