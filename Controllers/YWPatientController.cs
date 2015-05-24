using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YWPatient.Models;

namespace YWPatient.Controllers
{
    //Generate a controller called YWPatientController with full CRGU support and views
    public class YWPatientController : Controller
    {
        private PatientSQLContext db = new PatientSQLContext();

        // GET: YWPatient
        //Order the patients by last name and then by first name on index view
        public ActionResult Index()
        {
            var patients = db.patients.OrderBy(a=>a.lastName).ThenBy(a=>a.firstName).Include(p => p.province);
            return View(patients.ToList());
        }

        // GET: YWPatient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: YWPatient/Create
        //Create get action, retrieve fields value
        public ActionResult Create()
        {
            //Order the province drop-down by province name
            ViewBag.provinceCode = new SelectList(db.provinces.OrderBy(a=>a.name), "provinceCode", "name");
            return View();
        }

        // POST: YWPatient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Create set action, post-back(save) fields value or throw a exception with a related message say so
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "patientId,firstName,lastName,address,city,provinceCode,postalCode,OHIP,dateOfBirth,deceased,dateOfDeath,homePhone,gender")] patient patient)
        {
            if (ModelState.IsValid)
            {
                //Happy path of the Create action , save new record with a message(Tempdata) say so and return to the Member listing
                try
                {
                    db.patients.Add(patient);
                    db.SaveChanges();
                    TempData["message"] = "There is a new record have been added";
                    return RedirectToAction("Index");
                }
                //Sad path of the Create action, there is a innermost error message show when catch any exception by the database
                // and also redisply the user's data 
                catch(Exception ex)
                {
                    while (ex.InnerException != null) ex = ex.InnerException;
                    ModelState.AddModelError("", "error on insert: " + ex.Message);

                }
                
            }
            //Order the province drop-down by province name
            ViewBag.provinceCode = new SelectList(db.provinces.OrderBy(a=>a.name), "provinceCode", "name", patient.provinceCode);
            return View(patient);
        }

        // GET: YWPatient/Edit/5
        //Edit get action, retrieve fields value of a selected record
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            //Order the province drop-down by province name
            ViewBag.provinceCode = new SelectList(db.provinces.OrderBy(a => a.name), "provinceCode", "name", patient.provinceCode);
            return View(patient);
        }

        // POST: YWPatient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Edit set action, post-back(save) fields value or throw a exception with a related message say so
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patientId,firstName,lastName,address,city,provinceCode,postalCode,OHIP,dateOfBirth,deceased,dateOfDeath,homePhone,gender")] patient patient)
        {
            if (ModelState.IsValid)
            {
                //Happy path of the Edit action , save the changed record  with a message(Tempdata) say so and return to the Member listing
                try
                {
                    db.Entry(patient).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "There is a record has been updated";
                    return RedirectToAction("Index");
                }
                //Sad path of the Edit action, there is a innermost error message show when catch any exception by the database
                // and also redisply the user's data 
                catch (Exception ex)
                {
                    while (ex.InnerException != null) ex = ex.InnerException;
                    ModelState.AddModelError("", "error on update: " + ex.Message);

                }
               
            }
            //Order the province drop-down by province name
            ViewBag.provinceCode = new SelectList(db.provinces.OrderBy(a => a.name), "provinceCode", "name", patient.provinceCode);
            return View(patient);
        }

        // GET: YWPatient/Delete/5
        //Delete get action, retrieve fields value of a select record
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: YWPatient/Delete/5
        //Delete set action, delete the selected record or throw a exception with a related message say so
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Happy path of the Delete action , delete selected record with a message(Tempdata) say so and return to the Member listing
            try
            {
                patient patient = db.patients.Find(id);
                db.patients.Remove(patient);
                db.SaveChanges();
                TempData["message"] = "There is a record has been deleted";
                return RedirectToAction("Index");
            }
            //Sad path of the delete action, there is a innermost error message show when catch any exception by the database
            // and also return to the delete page
            catch (Exception ex)
            {
                TempData["message"] = "error on delete:" + ex.GetBaseException().Message;
            }
            return RedirectToAction("Delete", new { ID = id });
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
