using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrFixIt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MrFixIt.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MrFixIt.Controllers
{
    public class JobsController : Controller
    {
        private MrFixItContext db = new MrFixItContext();

        //Basic User Account Info here...
        private readonly MrFixItContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public JobsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, MrFixItContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }


        // GET: /<controller>/
        //Connecting user to index
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            Worker currentwrkr = _db.Workers.FirstOrDefault(w=> w.UserName == currentUser.UserName);
            var inclWorkerWithJob = _db.Jobs.Include(i => i.Worker).ToList();

            return View(inclWorkerWithJob);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Job job)
        {
            db.Jobs.Add(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public IActionResult Claim(int id)
        //{
        //    var thisItem = db.Jobs.FirstOrDefault(items => items.JobId == id);
        //    return View(thisItem);
        //}

        //[HttpPost]
        //public IActionResult Claim(Job job)
        //{
        //    job.Worker = db.Workers.FirstOrDefault(i => i.UserName == User.Identity.Name);
        //    db.Entry(job).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> ClaimJobCtrl(int id)
        {

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            Worker currentwrkr = _db.Workers.FirstOrDefault(w => w.UserName == currentUser.UserName);
            var inclWorkerWithJob = _db.Jobs.Include(i => i.Worker).ToList();
            Job selJob = _db.Jobs.FirstOrDefault(j => j.JobId == id);
            //var thisJob = db.Jobs.FirstOrDefault(m => m.JobId == id);
            ////var getWorker = db.Jobs.Include(i => i.Worker).ToList();
            ////Worker worker = db.Workers.FirstOrDefault(i => i.UserName == User.Identity.Name);
            ////thisJob.Add(worker);
            ////db.Entry(worker).State = EntityState.Modified;
            //db.Entry(thisJob).State = EntityState.Modified;
            //db.SaveChanges();
            return Json(selJob);
        }
    }
}
