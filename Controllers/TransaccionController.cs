using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.Models;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly SistemaDbContext context;

        public TransaccionController(SistemaDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var tr = context.GetTransacciones().ToList();
            return View(tr);
        }
        public IActionResult Create()
        {
            ViewData["tipotr"] = new SelectList(context.TipoTransaccions, "Id", "Descripcion");
            ViewData["articulos"] = new SelectList(context.Articulos, "Id", "Descripcion");
            ViewData["estado"] = new SelectList(context.Estados, "Id", "Descripcion");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                context.CreateTransaccion(transaccion.Descripcion, transaccion.TipoTransaccionId, transaccion.ArticuloId, transaccion.FechaDocumento, transaccion.EstadoId, transaccion.Cantidad, transaccion.CostoTotal);
                var Tultimo = context.Transaccions.OrderByDescending(t => t.Id).Take(1).FirstOrDefault();
                if (Tultimo.TipoTransaccionId == 2)
                {
                    context.RegistrarSalida(Tultimo.ArticuloId, Tultimo.Cantidad);
                    return RedirectToAction("Index");
                }else if (Tultimo.TipoTransaccionId == 1)
                {
                    context.RegistrarEntrada(Tultimo.ArticuloId, Tultimo.Cantidad);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            var tr = context.GetTransaccion(id);
            if(tr == null)
            {
                return NotFound();
            }
            ViewData["tipotr"] = new SelectList(context.TipoTransaccions, "Id", "Descripcion");
            ViewData["articulos"] = new SelectList(context.Articulos, "Id", "Descripcion");
            ViewData["estado"] = new SelectList(context.Estados, "Id", "Descripcion");
            return View(tr);
        }

        [HttpPost]
        public IActionResult Edit(Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                context.ActualizarTransaccion(transaccion.Id, transaccion.Descripcion, transaccion.TipoTransaccionId, transaccion.ArticuloId, transaccion.FechaDocumento, transaccion.EstadoId, transaccion.Cantidad, transaccion.CostoTotal);
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Delete(int id)
        {
            var tr = context.GetTransaccion(id);
            if (tr == null)
            {
                return NotFound();
            }
            ViewData["tipotr"] = new SelectList(context.TipoTransaccions, "Id", "Descripcion");
            ViewData["articulos"] = new SelectList(context.Articulos, "Id", "Descripcion");
            ViewData["estado"] = new SelectList(context.Estados, "Id", "Descripcion");
            return View(tr);
        }

        [HttpPost]
        public IActionResult Delete(Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                context.EliminarTransaccion(transaccion.Id);
                return RedirectToAction("Index");
            }

            return View();
        }

        /*primero trata de registrar una salida, enfocate en hacer esto primero*/
    }
}
