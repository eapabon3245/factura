namespace Datos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Facturas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Facturas()
        {
            Detalles = new HashSet<Detalles>();
        }

        [Key]
        public int IdFactura { get; set; }

        [Required]
        [StringLength(800)]
        public string NumeroFactura { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(800)]
        public string TipodePago { get; set; }

        [Required]
        [StringLength(800)]
        public string Documentocliente { get; set; }

        [Required]
        [StringLength(800)]

        public string Nombrecliente { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? Descuento { get; set; }

        public decimal? IVA { get; set; }

        public decimal? TotalDescuento { get; set; }

        public decimal? TotalImpuesto { get; set; }

        public decimal? Total { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalles> Detalles { get; set; }
    }
}
