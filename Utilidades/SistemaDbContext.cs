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
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TipoTransaccion> TipoTransaccions { get; set; }
        public DbSet<Transaccion> Transaccions { get; set; }

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

        public List<Transaccion> GetTransacciones()
        {
            return Transaccions.FromSqlRaw("exec sp_VerTransacciones").ToList();
        }

        public Transaccion GetTransaccion(int id)
        {
            var transaccion = Transaccions.FromSqlInterpolated($"exec sp_VerTransaccion {id}").AsEnumerable().FirstOrDefault();
            return transaccion;
        }

        public void CreateTransaccion(string desc, int tipotr, int arti,DateTime fecha,int estado,int canti,decimal costo)
        {
            Database.ExecuteSqlRaw("exec sp_CrearTransaccion {0},{1},{2},{3},{4},{5},{6}", desc, tipotr, arti, fecha, estado, canti, costo);
        }

        public void ActualizarTransaccion(int id, string desc, int tipotr, int arti, DateTime fecha, int estado, int canti, decimal costo)
        {
            Database.ExecuteSqlRaw("exec sp_ActualizarTransaccion {0},{1},{2},{3},{4},{5},{6},{7}", id, desc, tipotr, arti, fecha, estado, canti, costo);
        }

        public void EliminarTransaccion(int id)
        {
            Database.ExecuteSqlRaw("exec sp_EliminarTransaccion {0}",id);
        }

        public void RegistrarSalida(int id,int cant)
        {
            Database.ExecuteSqlRaw("exec sp_RegistrarSalida {0},{1}",id,cant);
        }
        public void RegistrarEntrada(int id, int cant)
        {
            Database.ExecuteSqlRaw("exec sp_RegistrarEntrada {0},{1}", id, cant);
        }
        public void ActualizarCosto(decimal costo,int id)
        {
            Database.ExecuteSqlRaw("exec sp_CostoTotal {0},{1}", costo, id);
        }
    }
}
