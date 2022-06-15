using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models;
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

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Apply(int id)
    {
        JobCart jobCart = new() { };

        if (jobCart == null)
        {
            return Json(new { success = false, message = "Error while applying" });
        }
        jobCart.JobId = id;
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        jobCart.ApplicationUserId = claim.Value;

        _unitOfWork.JobCart.Add(jobCart);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
}