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
    public class RepliesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Replies
        public ActionResult Index()
        {
            var replies = db.Replies.Include(r => r.BackendMember).Include(r => r.Issue);
            return View(replies.ToList());
        }

        // GET: Replies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }

        // GET: Replies/Create
        public ActionResult Create()
        {
            ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name");
            ViewBag.IssueId = new SelectList(db.Issues, "Id", "Content");
            return View();
        }

        // POST: Replies/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IssueId,BackendMemberId,CreatedAt,Content")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                db.Replies.Add(reply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", reply.BackendMemberId);
            ViewBag.IssueId = new SelectList(db.Issues, "Id", "Content", reply.IssueId);
            return View(reply);
        }

        // GET: Replies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", reply.BackendMemberId);
            ViewBag.IssueId = new SelectList(db.Issues, "Id", "Content", reply.IssueId);
            return View(reply);
        }

        // POST: Replies/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IssueId,BackendMemberId,CreatedAt,Content")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BackendMemberId = new SelectList(db.BackendMembers, "Id", "Name", reply.BackendMemberId);
            ViewBag.IssueId = new SelectList(db.Issues, "Id", "Content", reply.IssueId);
            return View(reply);
        }

        // GET: Replies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reply reply = db.Replies.Find(id);
            db.Replies.Remove(reply);
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
