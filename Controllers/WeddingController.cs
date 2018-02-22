using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;

        public WeddingController(WeddingContext context)
        {
            _context = context;
        }

        private RegisterUser ActiveUser
        {
            get{ return _context.User.Where( u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();}
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Index()
        {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");

            RegisterUser thisUser = _context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();
            ViewBag.UserInfo = thisUser;

            List<Wedding> Wedding = _context.Wedding.Include(w => w.UserWedding).ToList();
            return View(Wedding);
        }

        [HttpGet]
        [Route("newwedding")]
        public IActionResult newWedding() {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");

            ViewBag.UserInfo = ActiveUser;
            return View("NewWedding");
        }

        [HttpPost]
        [Route("create")]
        public IActionResult createWedding(WeddingViewModel model)
        {
            if(ModelState.IsValid)
            {
                Wedding newWedding = new Wedding
                {
                    CreatedBy = ActiveUser.UserId,
                    Wedder1 = model.Wedder1,
                    Wedder2 = model.Wedder2,
                    Date = model.Date,
                    Address = model.Address,
                };
                _context.Wedding.Add(newWedding);
                _context.SaveChanges();
                Wedding wedding = _context.Wedding.Where(w => w.Wedder1 == model.Wedder1 && w.Wedder2 == model.Wedder2).SingleOrDefault();
                return RedirectToAction("ShowWedding", new {WeddingId = wedding.WeddingId});
            }
            ViewBag.UserInfo = ActiveUser;
            return View("NewWedding");
        }

        [HttpGet]
        [Route("wedding/{WeddingId}")]
        public IActionResult showWedding(int WeddingId) {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");
            
            ViewBag.wedding = _context.Wedding.Where(w => w.WeddingId == WeddingId)
                                                    .Include(w => w.UserWedding)
                                                        .ThenInclude(u => u.User)
                                                            .SingleOrDefault();
            return View("ShowWedding");
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int WeddingId)
        {
            Wedding toDelete = _context.Wedding.SingleOrDefault(d => d.WeddingId == WeddingId);
            _context.Wedding.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        } 

        [HttpPost]
        [Route("rsvp")]
        public IActionResult Rsvp(int WeddingId)
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            UserWedding newRsvp = new UserWedding
            {
                UserId = ActiveUser.UserId,
                WeddingId = WeddingId
            };
            _context.UserWedding.Add(newRsvp);
            _context.SaveChanges();

            ViewBag.UserInfo = ActiveUser;
            List<Wedding> Wedding = _context.Wedding.ToList();
            return RedirectToAction("Index", Wedding);
        }

        [HttpPost]
        [Route("unrsvp")]
        public IActionResult UnRsvp(int WeddingId)
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            UserWedding toDelete = _context.UserWedding.SingleOrDefault(r => r.WeddingId == WeddingId && r.UserId == ActiveUser.UserId);
            _context.UserWedding.Remove(toDelete);
            _context.SaveChanges();

            ViewBag.UserInfo = ActiveUser;
            List<Wedding> Wedding = _context.Wedding.ToList();
            return RedirectToAction("Index", Wedding);
        }     

        [HttpGet]
        [Route ("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }


    }
}