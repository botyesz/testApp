using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LynxTestApp.Models;

namespace LynxTestApp.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuildUsersTableWithSearch(string SearchValue)
        {
            IEnumerable<User> myUsers = db.Users.Where(
                x => x.Username.Contains(SearchValue) ||
                x.Last_name.Contains(SearchValue) ||
                x.First_name.Contains(SearchValue) ||
                x.Email.Contains(SearchValue) ||
                SearchValue == null).ToList();
            return PartialView("_UsersTable", myUsers);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,ConfirmPassword,First_name,Last_name,Email,Phone_number")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/?
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,ConfirmPassword,First_name,Last_name,Email,Phone_number")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult AJAXDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Users.Remove(user);
                db.SaveChanges();
                IEnumerable<User> myUsers = db.Users.ToList();
                return PartialView("_UsersTable", myUsers);
            }
        }
    }
}