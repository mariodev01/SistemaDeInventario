using Microsoft.EntityFrameworkCore;
using SistemaInventario.Models;

namespace SistemaInventario.Utilidades
{
    public class SistemaDbContext:DbContext
    {
        public SistemaDbContext(DbContextOptions<SistemaDbContext> options):base(options)
        {
        }
        public DbSet<Articulo> Articulos { get; set; }

        public List<Articulo> GetArticulos()
        {
            return Articulos.FromSqlRaw("exec sp_VerArticulos").ToList();
        }

        public Articulo GetArticulo(int id)
        {
            var articulo = Articulos.FromSqlInterpolated($"exec sp_VerArticulo {id}").AsEnumerable().FirstOrDefault();
            return articulo;
        }

        public void CreateArticulo(string Descripcion,DateTime fechaIngreso,bool estado,DateTime fechavencimiento,int cantidad,decimal costo)
        {
            Database.ExecuteSqlRaw("exec sp_CrearArticulo {0},{1},{2},{3},{4},{5}", Descripcion, fechaIngreso, estado, fechavencimiento, cantidad, costo);
        }

        public void ActualizarArticulo(int id, string Descripcion, DateTime fechaIngreso, bool estado, DateTime fechavencimiento, int cantidad, decimal costo)
        {
            Database.ExecuteSqlRaw("exec sp_ActualizarArticulo {0},{1},{2},{3},{4},{5},{6}", id, Descripcion, fechaIngreso, estado, fechavencimiento, cantidad, costo);
        }

        public void EliminarArticulo(int id)
        {
            Database.ExecuteSqlRaw("exec sp_EliminarArticulo {0}", id);
        }
    }
}
