using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Datos;

namespace Presentacion.Controllers
{
    public class DetallesController : Controller
    {
        private FacturaModel db = new FacturaModel();

        // GET: Detalles
        public async Task<ActionResult> Index()
        {
            var detalles = db.Detalles.Include(d => d.Facturas).Include(d => d.Productos);
            return View(await detalles.ToListAsync());
        }

        // GET: Detalles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalles detalles = await db.Detalles.FindAsync(id);
            if (detalles == null)
            {
                return HttpNotFound();
            }
            return View(detalles);
        }

        // GET: Detalles/Create
        public ActionResult Create()
        {
            ViewBag.IdFactura = new SelectList(db.Facturas, "IdFactura", "NumeroFactura");
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Producto");
            return View();
        }

        // POST: Detalles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdDetalle,IdFactura,IdProducto,Cantidad,PrecioUnitario")] Detalles detalles)
        {
            if (ModelState.IsValid)
            {
                db.Detalles.Add(detalles);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdFactura = new SelectList(db.Facturas, "IdFactura", "NumeroFactura", detalles.IdFactura);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Producto", detalles.IdProducto);
            return View(detalles);
        }

        // GET: Detalles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalles detalles = await db.Detalles.FindAsync(id);
            if (detalles == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFactura = new SelectList(db.Facturas, "IdFactura", "NumeroFactura", detalles.IdFactura);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Producto", detalles.IdProducto);
            return View(detalles);
        }

        // POST: Detalles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdDetalle,IdFactura,IdProducto,Cantidad,PrecioUnitario")] Detalles detalles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalles).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdFactura = new SelectList(db.Facturas, "IdFactura", "NumeroFactura", detalles.IdFactura);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Producto", detalles.IdProducto);
            return View(detalles);
        }

        // GET: Detalles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalles detalles = await db.Detalles.FindAsync(id);
            if (detalles == null)
            {
                return HttpNotFound();
            }
            return View(detalles);
        }

        // POST: Detalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Detalles detalles = await db.Detalles.FindAsync(id);
            db.Detalles.Remove(detalles);
            await db.SaveChangesAsync();
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
