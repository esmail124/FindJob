using AutoMapper;
using FindJob.Data.Entities;
using FindJob.Data.Repositories.JobsRepository;
using FindJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FindJob.Controllers
{
    //[Authorize(Policy = "CustomRolePolicy")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IJobsRepository jobsRepository;

        public HomeController(ILogger<HomeController> logger, IMapper mapper,IJobsRepository jobsRepository)
        {

            _logger = logger;
            _mapper = mapper;
            this.jobsRepository = jobsRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IList<Job> jobs = await jobsRepository.GetAllAsync();
                IList<JobDTO> jobDtos = _mapper.Map<IList<JobDTO>>(jobs);
                return View(jobDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching jobs.");
                return View(new List<JobDTO>());
            }
        }

        public async Task<IActionResult> Index1()
        {
            try
            {
                IList<Job> jobs = await jobsRepository.GetAllAsync();
                IList<JobDTO> jobDtos = _mapper.Map<IList<JobDTO>>(jobs);
                return View(jobDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching jobs.");
                return View(new List<JobDTO>());
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
