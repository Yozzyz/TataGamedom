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
    public class BackendMembersListController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: BackendMembersList
        public ActionResult Index()
        {
            var backendMembers = db.BackendMembers.Include(b => b.BackendMembersRolesCode);
            return View(backendMembers.ToList());
        }

        // GET: BackendMembersList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackendMember backendMember = db.BackendMembers.Find(id);
            if (backendMember == null)
            {
                return HttpNotFound();
            }
            return View(backendMember);
        }

        // GET: BackendMembersList/Create
        public ActionResult Create()
        {
            ViewBag.BackendMembersRoleId = new SelectList(db.BackendMembersRolesCodes, "Id", "Name");
            return View();
        }

        // POST: BackendMembersList/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Account,Password,Birthday,Email,Phone,BackendMembersRoleId,RegistrationDate,ActiveFlag")] BackendMember backendMember)
        {
            if (ModelState.IsValid)
            {
                db.BackendMembers.Add(backendMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BackendMembersRoleId = new SelectList(db.BackendMembersRolesCodes, "Id", "Name", backendMember.BackendMembersRoleId);
            return View(backendMember);
        }

        // GET: BackendMembersList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackendMember backendMember = db.BackendMembers.Find(id);
            if (backendMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.BackendMembersRoleId = new SelectList(db.BackendMembersRolesCodes, "Id", "Name", backendMember.BackendMembersRoleId);
            return View(backendMember);
        }

        // POST: BackendMembersList/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Account,Password,Birthday,Email,Phone,BackendMembersRoleId,RegistrationDate,ActiveFlag")] BackendMember backendMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(backendMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BackendMembersRoleId = new SelectList(db.BackendMembersRolesCodes, "Id", "Name", backendMember.BackendMembersRoleId);
            return View(backendMember);
        }

        // GET: BackendMembersList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackendMember backendMember = db.BackendMembers.Find(id);
            if (backendMember == null)
            {
                return HttpNotFound();
            }
            return View(backendMember);
        }

        // POST: BackendMembersList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BackendMember backendMember = db.BackendMembers.Find(id);
            db.BackendMembers.Remove(backendMember);
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
