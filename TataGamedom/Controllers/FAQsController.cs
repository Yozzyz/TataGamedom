using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.Infra.DapperRepositories;
using TataGamedom.Models.Infra.EFRepositories;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.Services;
using TataGamedom.Models.ViewModels.CustomerServices;
using TataGamedom.Models.ViewModels.Games;

namespace TataGamedom.Controllers
{
    public class FAQsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: FAQs
        [Authorize]
        public ActionResult Index()
        {
            var fAQs = db.FAQs.Include(f => f.IssueTypesCode);
            return View(fAQs.ToList());
        }
        //public ActionResult Index()
        //{
        //    IEnumerable<FAQIndexVM> FAQs = GetFAQs();
        //    return View();
        //}
        //private IEnumerable<FAQIndexVM> GetFAQs()
        //{
        //    IFAQRepository repo = new FAQsEFRepository();
        //    FAQService service = new FAQService(repo);
        //    return (IEnumerable<FAQIndexVM>)service.Search();
        //}

        // GET: FAQs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ fAQ = db.FAQs.Find(id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // GET: FAQs/Create
        public ActionResult Create()
        {
            ViewBag.IssueTypeId = new SelectList(db.IssueTypesCodes, "Id", "TypeName");
            return View();
        }

        // POST: FAQs/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Question,Answer,CreatedAt,IssueTypeId")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                db.FAQs.Add(fAQ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IssueTypeId = new SelectList(db.IssueTypesCodes, "Id", "TypeName", fAQ.IssueTypeId);
            return View(fAQ);
        }

        // GET: FAQs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ fAQ = db.FAQs.Find(id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            ViewBag.IssueTypeId = new SelectList(db.IssueTypesCodes, "Id", "TypeName", fAQ.IssueTypeId);
            return View(fAQ);
        }

        // POST: FAQs/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Question,Answer,CreatedAt,IssueTypeId")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fAQ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IssueTypeId = new SelectList(db.IssueTypesCodes, "Id", "TypeName", fAQ.IssueTypeId);
            return View(fAQ);
        }

        // GET: FAQs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ fAQ = db.FAQs.Find(id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FAQ fAQ = db.FAQs.Find(id);
            db.FAQs.Remove(fAQ);
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
