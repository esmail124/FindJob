using FindJob.Core;
using FindJob.Data.Entities;
using FindJob.Data.Repositories.JobsRepository;
using FindJob.Data.Repository;
using MongoDB.Driver;

namespace FindJob.Data.Repositories.JobRepository
{
    public class JobsRepository : MongoRepository<Job>, IJobsRepository
    {
        public JobsRepository(FindJobDatabaseSettings findJobDatabaseSettings)
            : base(new MongoClient(findJobDatabaseSettings.ConnectionString).GetDatabase(findJobDatabaseSettings.DatabaseName), nameof(Job))
        {
        }
    }
}