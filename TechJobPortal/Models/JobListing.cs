using System.ComponentModel.DataAnnotations;
using System.Globalization;
using TechJobPortal.Models.Enum;

namespace TechJobPortal.Models
{
    public class JobListing
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required String CompanyName { get; set; }
        public string Location { get; set; }

        public JobTypes jobType { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Now;


    }
    
}
