using FindJob.Core;
using FindJob.Data.Entities;
using FindJob.Data.Repositories.RoleRepository;
using FindJob.Data.Repository;
using MongoDB.Driver;

namespace FindJob.Data.Repositories.PermissionRepository
{
    public class PermissionRepository : MongoRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(FindJobDatabaseSettings findJobDatabaseSettings)
            : base(new MongoClient(findJobDatabaseSettings.ConnectionString).GetDatabase(findJobDatabaseSettings.DatabaseName), nameof(Permission))
        {

        }
    }
}
