using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace krispy_back_test.Data;

[Table("donas")]
public partial class Donas
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("descripcion")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [Column("precio", TypeName = "decimal(18, 2)")]
    public decimal? Precio { get; set; }

    [Column("imagen", TypeName = "text")]
    public string? Imagen { get; set; }
}
