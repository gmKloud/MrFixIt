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

        public IActionResult ClaimJobCtrl(int id)
        {

            Job thisJobId = db.Jobs.FirstOrDefault(m => m.JobId == id);
            Worker worker = db.Workers.FirstOrDefault(i => i.UserName == User.Identity.Name);
            thisJobId.Worker = worker;
            db.Entry(worker).State = EntityState.Modified;
            db.Entry(thisJobId).State = EntityState.Modified;
            db.SaveChanges();
            var nameWroker = worker.FirstName + worker.LastName; 
            return Json(nameWroker);
        }
    }
}
