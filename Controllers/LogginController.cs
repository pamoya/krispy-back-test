using krispy_back_test.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace krispy_back_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogginController : ControllerBase
    {
        private readonly PanaderiaContext _context;
        private IConfiguration _config;

        public LogginController(PanaderiaContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet(Name = "login")]
        public async Task<ActionResult<AuthUser>> GetLogin(string user, string password)
        {
            try
            {
                var response = _context.Usuarios.FirstOrDefault(x => (x.Correo == user || x.Usuario == user) && x.Contraseña == password);


                if (response == null)
                {

                    return new BadRequestObjectResult("Los datos del usuario no son correctos o no se encuentra registrado");
                }
                else
                {
                    AuthUser userAuth = new AuthUser();
                    userAuth.Id = response.Id;
                    userAuth.Nombre = response.Nombre;
                    userAuth.Correo = response.Correo;
                    userAuth.Direccion = response.Direccion;
                    userAuth.Usuario = response.Usuario;
                    userAuth.Token = GenerateToken(response);
                    
                    return userAuth;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPost(Name = "login")]
        public async Task<ActionResult<bool>> Register(Usuarios user)
        {
            try
            {
                var usuarioRegistered = _context.Usuarios.FirstOrDefault(x => x.Correo == user.Correo);

                if (usuarioRegistered == null)
                {
                    var save = _context.Usuarios.Add(user);

                    var response = await _context.SaveChangesAsync();

                    if (response == 0)
                    {
                        return new BadRequestObjectResult("El usuario no pudo registrarse");
                    }
                    else
                    {
                        return new OkObjectResult("Usuario registrado con éxito");
                    }
                }
                else
                {
                    return new BadRequestObjectResult("El usuario ya se encuentra registrado");
                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private string GenerateToken(Usuarios user)
        {
            try
            {
                var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Correo)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:key").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var securityToken = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: creds);

                string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
