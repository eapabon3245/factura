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
using Microsoft.Ajax.Utilities;

namespace Presentacion.Controllers
{
    public class FacturasController : Controller
    {
        private FacturaModel db = new FacturaModel();

        // GET: Facturas
        public async Task<ActionResult> Index()
        {
            return View(await db.Facturas.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Producto");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NumeroFactura,Fecha,TipodePago,Documentocliente,Nombrecliente,Descuento,IVA,Cantidad,PrecioUnitario,IdProducto")] DetalleFactura facturas)
        {
            if (ModelState.IsValid)
            {
                var precio = facturas.PrecioUnitario * facturas.Cantidad;
                Facturas facturas1 = new Facturas()
                {
                    Descuento = facturas.Descuento,
                    Documentocliente = facturas.Documentocliente,
                    Fecha = DateTime.ParseExact(facturas.Fecha, "yyyy-MM-dd", null),
                    IVA = facturas.IVA,
                    Nombrecliente = facturas.Nombrecliente,
                    NumeroFactura = facturas.NumeroFactura,
                    Detalles = new List<Detalles>() { new Detalles {
                        PrecioUnitario= facturas.PrecioUnitario,
                        IdProducto = facturas.IdProducto,
                        Cantidad = facturas.Cantidad
                    } },
                    TipodePago = facturas.TipodePago,
                    TotalDescuento = precio * (facturas.Descuento/100),
                    TotalImpuesto = precio * (facturas.IVA / 100),
                    Subtotal = precio
                };
                facturas1.Total = precio - facturas1.TotalDescuento + facturas1.TotalImpuesto;
                db.Facturas.Add(facturas1);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(facturas);
        }

        // GET: Facturas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            DetalleFactura detalle = new DetalleFactura()
            {
                IdFactura = facturas.IdFactura,
                Descuento = facturas.Descuento,
                Documentocliente = facturas.Documentocliente,
                Fecha = facturas.Fecha.ToString("yyyy-MM-dd"),
                IVA = facturas.IVA,
                Nombrecliente = facturas.Nombrecliente,
                NumeroFactura = facturas.NumeroFactura,
                TipodePago = facturas.TipodePago,
                PrecioUnitario = facturas.Detalles.FirstOrDefault()?.PrecioUnitario??0,
                IdProducto = facturas.Detalles.FirstOrDefault()?.IdProducto??0,
                Cantidad = facturas.Detalles.FirstOrDefault()?.Cantidad??0
            };
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Producto", detalle.IdProducto);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdFactura,NumeroFactura,Fecha,TipodePago,Documentocliente,Nombrecliente,Descuento,IVA,Cantidad,PrecioUnitario,IdProducto")] DetalleFactura facturas)
        {
            if (ModelState.IsValid)
            {
                var precio = facturas.PrecioUnitario * facturas.Cantidad;
                Facturas facturas1 = new Facturas()
                {
                    IdFactura = facturas.IdFactura,
                    Descuento = facturas.Descuento,
                    Documentocliente = facturas.Documentocliente,
                    Fecha = DateTime.ParseExact(facturas.Fecha, "yyyy-MM-dd", null),
                    IVA = facturas.IVA,
                    Nombrecliente = facturas.Nombrecliente,
                    NumeroFactura = facturas.NumeroFactura,
                    Detalles = new List<Detalles>() { new Detalles {
                        IdFactura = facturas.IdFactura,
                        PrecioUnitario= facturas.PrecioUnitario,
                        IdProducto = facturas.IdProducto,
                        Cantidad = facturas.Cantidad
                    } },
                    TipodePago = facturas.TipodePago,
                    TotalDescuento = precio * (facturas.Descuento / 100),
                    TotalImpuesto = precio * (facturas.IVA / 100),
                    Subtotal = precio
                };
                facturas1.Total = precio - facturas1.TotalDescuento + facturas1.TotalImpuesto;
                db.Entry(facturas1).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Producto", facturas.IdProducto);
            return View(facturas);
        }

        // GET: Facturas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas.Detalles.Any()) {
                db.Detalles.Remove(facturas.Detalles.FirstOrDefault());
            }
            db.Facturas.Remove(facturas);
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
