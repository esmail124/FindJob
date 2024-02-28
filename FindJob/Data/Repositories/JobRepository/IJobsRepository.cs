using FindJob.Data.Entities;
using FindJob.Data.Repository;
using FindJob.Models;
using MongoDB.Bson;

namespace FindJob.Data.Repositories.JobsRepository
{
    public interface IJobsRepository : IRepository<Job>
    {
    }

}