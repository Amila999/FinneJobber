using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models;
using FinneJobber.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

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
        var jobList = _unitOfWork.Job.GetAll(includeProperties: "Category");
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

    public IActionResult Details(int jobId)
    {
        JobCart cartObj = new()
        {
            IsApplied = false,
            JobId = jobId,
            Job = _unitOfWork.Job.GetFirstOrDefault(u => u.Id == jobId, includeProperties: "Category"),
        };
        return View(cartObj);
    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(JobCart jobCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        jobCart.ApplicationUserId = claim.Value;

        JobCart cartFromDb = _unitOfWork.JobCart.GetFirstOrDefault(
            u => u.ApplicationUserId == claim.Value && u.JobId == jobCart.JobId
            );
        if (cartFromDb == null)
        {
            jobCart.IsApplied=true;
            _unitOfWork.JobCart.Add(jobCart);
        }
        else 
        {
            jobCart.IsApplied = true;
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }
}