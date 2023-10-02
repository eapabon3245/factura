namespace Datos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Detalles
    {
        [Key]
        public int IdDetalle { get; set; }

        public int IdFactura { get; set; }

        public int IdProducto { get; set; }

        public long Cantidad { get; set; }

        public long? PrecioUnitario { get; set; }

        public virtual Facturas Facturas { get; set; }

        public virtual Productos Productos { get; set; }
    }
}
