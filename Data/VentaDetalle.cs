using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace krispy_back_test.Data;

[Table("venta_detalle")]
public partial class VentaDetalle
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_venta")]
    public int? IdVenta { get; set; }

    [Column("id_dona")]
    public int? IdDona { get; set; }

    [Column("cantidad")]
    public int? Cantidad { get; set; }
}
public class VentaDetalleRegister
{
    public int? IdDona { get; set; }
    public int? Cantidad { get; set; }
}

public class VentaDetalleFull
{
    public Donas Dona { get; set; }
    public int? Cantidad { get; set; }
}