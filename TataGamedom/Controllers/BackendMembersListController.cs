using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TataGamedom.Filters;
using TataGamedom.Models.EFModels;
using TataGamedom.Models.ViewModels.Members;
using TataGamedom.Models.ViewModels.News;

namespace TataGamedom.Controllers
{
    public class BackendMembersListController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private string _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
		// GET: BackendMembersList
        
		public ActionResult Index()
        {
            using (var con = new SqlConnection(_connstr))
            {
                string sql = @"SELECT bm.Id ,bm.Name,bm.Account,bm.Email,bm.Phone,
bmr.Name as BackendMembersRoleName ,bm.ActiveFlag
FROM BackendMembers AS bm
LEFT JOIN BackendMembersRolesCodes AS bmr ON bmr.Id = bm.BackendMembersRoleId";

				var list = con.Query<BackendMembersListVM>(sql);

				return View(list);

			}
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

        // GET: BackendMembersList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var con = new SqlConnection(_connstr))
            {
                string sql = @"SELECT bm.Id, bm.Name, bm.Account, bm.Email, bm.Phone,
                bmr.Name AS BackendMembersRoleName, bm.ActiveFlag
                FROM BackendMembers AS bm
                LEFT JOIN BackendMembersRolesCodes AS bmr ON bmr.Id = bm.BackendMembersRoleId
                WHERE bm.Id = @Id";


                var list = con.Query<BackendMembersListVM>(sql, new { Id = id }).SingleOrDefault();

				if (list == null)
				{
					return HttpNotFound();
				}

				ViewBag.BackendMembersRoleId = new SelectList(db.BackendMembersRolesCodes, "Id", "Name", list.BackendMembersRoleId);
				return View(list);
			}
        }

        // POST: BackendMembersList/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BackendMembersListVM list)
        {
            if (ModelState.IsValid)
            {
                using (var con = new SqlConnection(_connstr))
                {
                    string sql = @"UPDATE BackendMembers
                   SET 
                       BackendMembersRoleId = @BackendMembersRoleId
                   WHERE Id = @Id";

                    con.Execute(sql, list);
                }
                return RedirectToAction("Index");
            }

            ViewBag.BackendMembersRoleId = new SelectList(db.BackendMembersRolesCodes, "Id", "Name", list.BackendMembersRoleId);
            return View(list);
        }
			//public ActionResult Edit([Bind(Include = "Id,Name,Account,Password,Birthday,Email,Phone,BackendMembersRoleId,RegistrationDate,ActiveFlag")] BackendMember backendMember)
			//{
			//    if (ModelState.IsValid)
			//    {
			//        db.Entry(backendMember).State = EntityState.Modified;
			//        db.SaveChanges();
			//        return RedirectToAction("Index");
			//    }
			//    ViewBag.BackendMembersRoleId = new SelectList(db.BackendMembersRolesCodes, "Id", "Name", backendMember.BackendMembersRoleId);
			//    return View(backendMember);
			//}


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
