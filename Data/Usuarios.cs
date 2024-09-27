using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace krispy_back_test.Data;

public partial class Usuarios 
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("usuario")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Usuario { get; set; }

    [Column("correo")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Correo { get; set; }

    [Column("contraseña")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Contraseña { get; set; }

    [Column("nombre")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("direccion")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Direccion { get; set; }
}

public class AuthUser
{
    public int Id { get; set; }
    public string? Usuario { get; set; }
    public string? Correo { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Token { get; set; }
}
