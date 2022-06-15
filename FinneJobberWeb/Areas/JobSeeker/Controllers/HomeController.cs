using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models;
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
    #region API CALLS
    //Post
    [HttpPost]
    public IActionResult Apply(int? id)
    {
        var obj = _unitOfWork.Job.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return Json(new { success = false, message = "Error while applying" });
        }

        var claimsIdentity = User.Identity as ClaimsIdentity;
        if (claimsIdentity != null)
        {
            // the principal identity is a claims identity.
            // now we need to find the NameIdentifier claim
            var userIdClaim = claimsIdentity.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                var userIdValue = userIdClaim.Value;
                _unitOfWork.Job.Update(obj);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete successful" });
            }
            else
            {
                return Json(new { success = false, message = "Error while applying" });
            }
        }
        else {
            return Json(new { success = false, message = "Please Loggin" });
        }
    }
    #endregion
}