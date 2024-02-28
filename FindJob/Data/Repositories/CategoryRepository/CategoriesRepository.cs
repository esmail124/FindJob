using FindJob.Core;
using FindJob.Data.Entities;
using FindJob.Data.Repositories.JobsRepository;
using FindJob.Data.Repository;
using MongoDB.Driver;

namespace FindJob.Data.Repositories.CategoryRepository
{
    public class CategoriesRepository : MongoRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(FindJobDatabaseSettings findJobDatabaseSettings)
         : base(new MongoClient(findJobDatabaseSettings.ConnectionString).GetDatabase(findJobDatabaseSettings.DatabaseName), nameof(Category))
        {
        }
    }
}
