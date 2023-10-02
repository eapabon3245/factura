using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Datos
{
    public class DetalleFactura
    {
        public int IdFactura { get; set; }
        [Required]
        public string NumeroFactura { get; set; }
        [Required]
        public string Fecha { get; set; }

        public string TipodePago { get; set; }
        [Required]
        public string Documentocliente { get; set; }
        [Required]
        public string Nombrecliente { get; set; }
        [Range(0, 100)]
        public decimal? Descuento { get; set; }
        [Range(0, 100)]
        public decimal? IVA { get; set; }
        [Required]
        [Range(1, long.MaxValue)]
        public long Cantidad { get; set; }
        [Required]
        [Range(1, long.MaxValue)]
        public long PrecioUnitario { get; set; }
        [Required]
        public int IdProducto { get; set; }
    }
}
