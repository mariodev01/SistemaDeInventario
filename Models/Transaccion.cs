using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaInventario.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("Id")]
        public int TipoTransaccionId { get; set; }
        [ForeignKey("Id")]
        public int ArticuloId { get; set; }
        [Column(TypeName= "date")]
        public DateTime FechaDocumento { get; set; }
        [ForeignKey("Id")]
        public int EstadoId { get; set; }
        public int Cantidad { get; set; }
        [Precision(precision:8,scale:2)]
        public decimal CostoTotal { get; set; }
        public virtual TipoTransaccion TipoTransaccion { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
