using MongoDB.Bson;

namespace FindJob.Models
{
    public class JobDTO
    {
        public ObjectId Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    //public class JobDetailDTO
    //{
    //    public string Id { get; set; }
    //    public string JobTitle { get; set; }
    //    public string CompanyName { get; set; }
    //    public string Description { get; set; }
    //}
}