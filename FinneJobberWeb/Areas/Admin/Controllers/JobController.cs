using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinneJobber.DataAccess;
using FinneJobber.Models;
using FinneJobber.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinneJobber.Models.ViewModels;

namespace FinneJobberWeb.Areas.Admin.Controllers;
[Area("Admin")]

public class JobController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public JobController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }

    //Get
    public IActionResult Upsert(int? id)
    {
        JobVM jobVM = new()
        {
            Job = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            }),
        };
        
        if (id == null || id == 0)
        {
            //Create job
            return View(jobVM);
        }
        else 
        {
            jobVM.Job = _unitOfWork.Job.GetFirstOrDefault(u => u.Id == id);
            return View(jobVM);
            //update job
        }
        
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(JobVM obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.Job.Id == 0)
            {
                _unitOfWork.Job.Add(obj.Job);
                TempData["success"] = "Job created successfully";
            }
            else
            {
                _unitOfWork.Job.Update(obj.Job);
                TempData["success"] = "Job updated successfully";
            }
            
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var jobList = _unitOfWork.Job.GetAll(includeProperties: "Category");
        return Json(new { data = jobList });
    }
    //Post
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Job.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        _unitOfWork.Job.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
}
