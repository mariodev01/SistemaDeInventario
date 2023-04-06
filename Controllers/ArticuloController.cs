using Microsoft.AspNetCore.Mvc;
using SistemaInventario.Models;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly SistemaDbContext context;

        public ArticuloController(SistemaDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var articulos = context.GetArticulos().ToList();
            return View(articulos);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Articulo articulo)
        {
            if(ModelState.IsValid)
            {
                context.CreateArticulo(articulo.Descripcion, articulo.FechaIngreso, articulo.Estado, articulo.FechaVencimiento, articulo.Cantidad, articulo.Costo);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            var articulo = context.GetArticulo(id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        [HttpPost]
        public IActionResult Edit(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                context.ActualizarArticulo(articulo.Id, articulo.Descripcion, articulo.FechaIngreso, articulo.Estado, articulo.FechaVencimiento, articulo.Cantidad, articulo.Costo);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var articulo = context.GetArticulo(id);
            return View(articulo);
        }

        [HttpPost]
        public IActionResult Delete(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                context.EliminarArticulo(articulo.Id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
