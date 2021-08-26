using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFAssignment1.Models;

namespace EFAssignment1.Controllers
{
    [Authorize(Roles = "Admin,Supervisor")]
    public class EnrollmentsController : Controller
    {
        private EF_schoolEntities1 db = new EF_schoolEntities1();

        // GET: Enrollments
        public async Task<ActionResult> Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Cours).Include(e => e.Staff).Include(e => e.Student);
            return View(await enrollments.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseTitle");
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "StaffName");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollmentID,Grade,CourseId,StudentId,StaffId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Enrollments.Add(enrollment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {

                }
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseTitle", enrollment.CourseId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "StaffName", enrollment.StaffId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseTitle", enrollment.CourseId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "StaffName", enrollment.StaffId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollmentID,Grade,CourseId,StudentId,StaffId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseTitle", enrollment.CourseId);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "StaffName", enrollment.StaffId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            db.Enrollments.Remove(enrollment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetStudents(string term)
        {
            var students = db.Students.Select(q => new { 
                Name = q.StudentName,
                Id = q.StudentId
            }).Where(q => q.Name.Contains(term));
            return Json(students,JsonRequestBehavior.AllowGet);
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
