using System.ComponentModel.DataAnnotations;
namespace FindJob.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
