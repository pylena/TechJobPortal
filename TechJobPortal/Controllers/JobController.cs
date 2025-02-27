using Microsoft.AspNetCore.Mvc;
using TechJobPortal.Models;

namespace TechJobPortal.Controllers
{
    public class JobController : Controller
    {
        private static List<JobListing> _jobs = new()
        {
           new JobListing { Id = 1, Title= "Softwere Engineer", CompanyName= "STC", jobType= Models.Enum.JobTypes.Remote, Location="RUH"},
           new JobListing { Id = 2, Title= "Softwere Engineer", CompanyName= "A", jobType= Models.Enum.JobTypes.Remote, Location="RUH"},
           new JobListing { Id = 3, Title= "Engineer", CompanyName= "Elm", jobType= Models.Enum.JobTypes.Remote, Location="RUH"},
       };


        // Displays a list of job listings.
        //GET Job/Index
        public IActionResult Index(string searchTerm, string jobType, DateTime? startDate, DateTime? endDate, string sortOrder)
        {
            var jobs = _jobs;

            // Search by Title or Company Name
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                jobs = jobs.Where(j => j.Title.ToLower().Contains(searchTerm) || j.CompanyName.ToLower().Contains(searchTerm)).ToList();
            }

            // Filter by Job Type
            if (!string.IsNullOrEmpty(jobType) && Enum.TryParse(jobType, out Models.Enum.JobTypes selectedJobType))
            {
                jobs = jobs.Where(j => j.jobType == selectedJobType).ToList();
            }

            // Filter by Date Range
            if (startDate.HasValue)
            {
                jobs = jobs.Where(j => j.PostedDate >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                jobs = jobs.Where(j => j.PostedDate <= endDate.Value).ToList();
            }

            // Sorting by Date
            jobs = sortOrder switch
            {
                "oldest" => jobs.OrderBy(j => j.PostedDate).ToList(),
                _ => jobs.OrderByDescending(j => j.PostedDate).ToList(), // Default: Newest First
            };

            // Store filter values in ViewBag
            ViewBag.SearchTerm = searchTerm;
            ViewBag.SelectedJobType = jobType;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.SortOrder = sortOrder;

            return View(jobs);
        }



        public IActionResult Details(int id)
        {
            foreach (var job in _jobs)
            {
                if (job.Id == id)
                {
                    return View(job);
                }
     
            }
            return NotFound();
        }
    }
}
