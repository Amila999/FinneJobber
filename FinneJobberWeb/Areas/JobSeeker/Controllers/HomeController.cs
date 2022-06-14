using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace FinneJobberWeb.Areas.JobSeeker.Controllers;
[Area("JobSeeker")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
    public IActionResult Index()
        {
            IEnumerable<Job> jobList = _unitOfWork.Job.GetAll(includeProperties:"Category");
            return View(jobList);
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