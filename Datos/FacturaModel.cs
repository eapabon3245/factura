using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Datos
{
    public partial class FacturaModel : DbContext
    {
        public FacturaModel()
            : base("name=FacturaModel")
        {
        }

        public virtual DbSet<Detalles> Detalles { get; set; }
        public virtual DbSet<Facturas> Facturas { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facturas>()
                .Property(e => e.NumeroFactura)
                .IsUnicode(false);

            modelBuilder.Entity<Facturas>()
                .Property(e => e.TipodePago)
                .IsUnicode(false);

            modelBuilder.Entity<Facturas>()
                .Property(e => e.Documentocliente)
                .IsUnicode(false);

            modelBuilder.Entity<Facturas>()
                .HasMany(e => e.Detalles)
                .WithRequired(e => e.Facturas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.Producto)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .HasMany(e => e.Detalles)
                .WithRequired(e => e.Productos)
                .WillCascadeOnDelete(false);
        }
    }
}
