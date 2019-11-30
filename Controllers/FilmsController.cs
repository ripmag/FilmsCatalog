using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilmsCatalog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;


namespace FilmsCatalog.Controllers
{
    public class FilmsController : Controller
    {
        private FilmContext db = new FilmContext();
        private ApplicationUserManager _userManager;
        private ApplicationUser currentUser;

        // GET: Films
        public ActionResult Index(int? page)
        {
            _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (_userManager != null)
            {
                currentUser = _userManager.FindById(User.Identity.GetUserId());
                if (currentUser != null)
                    ViewBag.currentUser = currentUser.Email;
            }
            var films = db.Films.OrderBy(f => f.Title);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(films.ToPagedList(pageNumber, pageSize));
        }

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Year,Director,user,Poster")] Film film, HttpPostedFileBase file)
        {
            _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = _userManager.FindById(User.Identity.GetUserId());
            film.user = currentUser.Email;

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //сохраняем файл на сервере
                    file.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/") + "Content\\Images\\" + file.FileName);
                    film.Poster = file.FileName;
                }
                db.Films.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Year,Director,user,Poster")] Film film, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //сохраняем файл на сервере
                    file.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/") + "Content\\Images\\" + file.FileName);
                    film.Poster = file.FileName;
                }
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
