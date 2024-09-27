using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace krispy_back_test.Data;

public partial class Venta
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("direccion")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [Column("total", TypeName = "decimal(18, 2)")]
    public decimal? Total { get; set; }
}

public class VentaRegister
{
    public int? IdUsuario { get; set; }
    public string? Direccion { get; set; }
    public decimal? Total { get; set; }
    public List<VentaDetalleRegister>? VentaDetalles { get; set; }
}
public class VentaFull
{
    public int Id { get; set; }
    public Usuarios Usuario { get; set; }
    public decimal? Total { get; set; }
    public List<VentaDetalleFull>? VentaDetalles { get; set; }
}
