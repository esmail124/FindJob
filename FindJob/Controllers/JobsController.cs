using FindJob.Data.Repositories.JobsRepository;
using Microsoft.AspNetCore.Mvc;
using FindJob.Data.Entities;
using MongoDB.Bson;
using FindJob.Models;
using AutoMapper;
using FindJob.Core.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace FindJob.Controllers
{
    //[Authorize(Policy = "CustomRolePolicy")]
    public class JobsController : Controller
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IMapper mapper;

        public JobsController(IJobsRepository jobsRepository, IMapper mapper)
        {
            _jobsRepository = jobsRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IList<Job> jobs = await _jobsRepository.GetAllAsync();
            IList<JobDTO> jobDtos = mapper.Map<IList<JobDTO>>(jobs);
            return View(jobDtos);
        }

        public async Task<IActionResult> Details(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _jobsRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobDTO jobDto)
        {
            if (ModelState.IsValid)
            {
                Job job = mapper.Map<Job>(jobDto);
                await _jobsRepository.CreateAsync(job);
                return RedirectToAction(nameof(Index));
            }
            return View(jobDto);
        }

        public async Task<IActionResult> Edit(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _jobsRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ObjectId id, Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        public async Task<IActionResult> Delete(ObjectId id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _jobsRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            await _jobsRepository.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}