using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace krispy_back_test.Data;

public partial class PanaderiaContext : DbContext
{
    public PanaderiaContext()
    {
    }

    public PanaderiaContext(DbContextOptions<PanaderiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Donas> Donas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    public virtual DbSet<VentaDetalle> VentaDetalle { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:MyAppCs");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
