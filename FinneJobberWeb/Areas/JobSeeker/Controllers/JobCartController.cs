using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinneJobberWeb.Areas.JobSeeker.Controllers
{
    [Area("JobSeeker")]
    [Authorize]
    public class JobCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobCartVM JobCartVM { get; set; }
        public int AppliedJobsTotalValue { get; set; }
        public JobCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            JobCartVM = new JobCartVM()
            {
                ListJobCart = _unitOfWork.JobCart.GetAll(u=>u.ApplicationUserId==claim.Value,includeProperties: "Job"),
            };
            foreach (var cart in JobCartVM.ListJobCart) 
            {
                JobCartVM.JobCartTotal += cart.Job.Budget;
            }
            return View(JobCartVM);
        }

        public IActionResult Delete(int jobCartId)
        {
            var jobCart = _unitOfWork.JobCart.GetFirstOrDefault(u => u.Id == jobCartId);
            _unitOfWork.JobCart.Remove(jobCart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
