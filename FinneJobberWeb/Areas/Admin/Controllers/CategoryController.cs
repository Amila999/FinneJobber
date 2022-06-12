using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinneJobber.DataAccess;
using FinneJobber.Models;
using FinneJobber.DataAccess.Repository.IRepository;

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
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
        return View(objCategoryList);
    }
    //Get
    public IActionResult Create()
    {
        return View();
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null) 
            {
                string fileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(wwwRootPath, @"images\category");
                var extention = Path.GetExtension(file.FileName);
                using (var fileStreams = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create)) 
                {
                    file.CopyTo(fileStreams);
                }
                obj.ImageUrl = @"\images\category\"+fileName+extention;
            }

            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
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
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj, IFormFile file)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order can't exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
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
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }
    //Post
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
