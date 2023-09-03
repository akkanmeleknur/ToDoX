using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using to_do_List.Models;

namespace to_do_List.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        private TodoContext db = new TodoContext();


        public ActionResult TaskList(bool? isCompleted)
        {
            var tasks = db.Tasks.ToList();

            if (isCompleted.HasValue)
            {
                tasks = tasks.Where(t => t.IsCompleted == isCompleted).ToList();
            }



            return View("Index", tasks);
        }



        public ActionResult Index()
        {
            var tasks = db.Tasks.ToList();
            return View(tasks);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,DueDate,IsCompleted")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Model doğrulama hatası varsa, Create sayfasını yeniden görüntüleyin.
            ViewBag.ErrorMessage = "Görev oluşturulurken bir hata oluştu. Lütfen tekrar deneyin.";
            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,DueDate,IsCompleted")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public ActionResult Delete(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            // Silme işlemi
            db.Tasks.Remove(task);
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

        public ActionResult SetCompleted(int id)
        {
            var task = db.Tasks.Find(id);
            if (task != null)
            {
                task.IsCompleted = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}