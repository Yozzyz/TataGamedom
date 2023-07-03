using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Models.EFModels;

namespace TataGamedom.Controllers
{
    public class IssuesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Issues
        public ActionResult Index()
        {
            var issues = db.Issues.Include(i => i.IssueStatusCode).Include(i => i.IssueTypesCode).Include(i => i.Member);
            return View(issues.ToList());
        }

        // GET: Issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // GET: Issues/Create
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(db.IssueStatusCodes, "Id", "Name");
            ViewBag.IssueTypeId = new SelectList(db.IssueTypesCodes, "Id", "TypeName");
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name");
            return View();
        }

        // POST: Issues/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,IssueTypeId,Content,File,CreatedAt,Status")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Status = new SelectList(db.IssueStatusCodes, "Id", "Name", issue.Status);
            ViewBag.IssueTypeId = new SelectList(db.IssueTypesCodes, "Id", "TypeName", issue.IssueTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", issue.MemberId);
            return View(issue);
        }

        // GET: Issues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            ViewBag.Status = new SelectList(db.IssueStatusCodes, "Id", "Name", issue.Status);
            ViewBag.IssueTypeId = new SelectList(db.IssueTypesCodes, "Id", "TypeName", issue.IssueTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", issue.MemberId);
            return View(issue);
        }

        // POST: Issues/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,IssueTypeId,Content,File,CreatedAt,Status")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Status = new SelectList(db.IssueStatusCodes, "Id", "Name", issue.Status);
            ViewBag.IssueTypeId = new SelectList(db.IssueTypesCodes, "Id", "TypeName", issue.IssueTypeId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Name", issue.MemberId);
            return View(issue);
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
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
