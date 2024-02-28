namespace FindJob.Core
{
    public class SiteSettings
    {
    }

    public class FindJobDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        //implement in evry repos=> public string CollectionName { get; set; } = null!;
    }

    public interface IFindJobDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        //implement in evry repos=> public string CollectionName { get; set; } = null!;
    }
}
