using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Telecomunica.Models;

namespace Telecomunica.Controllers
{
    public class PersonasController : Controller
    {
        private Entities db = new Entities();

        // GET: Personas
        public ActionResult Index()
        {
            var persona = db.Persona.Include(p => p.Role);
            return View(persona.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.idrole = new SelectList(db.Role, "idrole", "role1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idpersona,nombre,apellido,dpi,fecha_nacimiento,username,password,idrole")] Persona persona, Role role)
        {
            if (ModelState.IsValid)
            {
                db.insertar_persona(persona.nombre, persona.apellido, Convert.ToInt64(persona.dpi), persona.fecha_nacimiento, persona.username, persona.password, persona.idrole);
                return RedirectToAction("Index");
            }

            ViewBag.idrole = new SelectList(db.Role, "idrole", "role1", persona.idrole);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.idrole = new SelectList(db.Role, "idrole", "role1", persona.idrole);
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idpersona,nombre,apellido,dpi,fecha_nacimiento,estado,username,password,idrole")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idrole = new SelectList(db.Role, "idrole", "role1", persona.idrole);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
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
