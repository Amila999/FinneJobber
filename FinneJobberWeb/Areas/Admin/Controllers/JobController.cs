using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinneJobber.DataAccess;
using FinneJobber.Models;
using FinneJobber.DataAccess.Repository.IRepository;

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
        IEnumerable<Job> objJobList = _unitOfWork.Job.GetAll();
        return View(objJobList);
    }
    //Get
    public IActionResult Create()
    {
        return View();
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Job obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Job.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Job created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //Get
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var jobFromDbFirst = _unitOfWork.Job.GetFirstOrDefault(u => u.Id == id);
        if (jobFromDbFirst == null)
        {
            return NotFound();
        }
        return View(jobFromDbFirst);
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Job obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Job.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Job updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //Get
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var jobFromDbFirst = _unitOfWork.Job.GetFirstOrDefault(u => u.Id == id);
        if (jobFromDbFirst == null)
        {
            return NotFound();
        }
        return View(jobFromDbFirst);
    }
    //Post
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.Job.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Job.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Job deleted successfully";
        return RedirectToAction("Index");
    }
}
