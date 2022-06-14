using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinneJobber.DataAccess;
using FinneJobber.Models;
using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models.ViewModels;

namespace FinneJobberWeb.Areas.Admin.Controllers;
[Area("Admin")]

public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }
    //Get
    public IActionResult Upsert(int? id)
    {
        CategoryVM categoryVM = new() 
        {
            Category = new(),
        };
        if (id == null || id == 0)
        {
            //Create Category
            return View(categoryVM);
        }
        else
        {
            categoryVM.Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            return View(categoryVM);
            //update job
        }
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(CategoryVM obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null) 
            {
                string fileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(wwwRootPath, @"images\category");
                var extention = Path.GetExtension(file.FileName);

                if (obj.Category.ImageUrl != null) 
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.Category.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath)) 
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create)) 
                {
                    file.CopyTo(fileStreams);
                }
                obj.Category.ImageUrl = @"\images\category\"+fileName+extention;
            }
            if (obj.Category.Id == 0)
            {
                _unitOfWork.Category.Add(obj.Category);
                TempData["success"] = "Category created successfully";
            }
            else 
            {
                _unitOfWork.Category.Update(obj.Category);
                TempData["success"] = "Category updated successfully";
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
        var categoryList = _unitOfWork.Category.GetAll();
        return Json(new { data = categoryList });
    }
    //Post
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return Json(new { success=false,message="Error while deleting"});
        }

        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
}
