using krispy_back_test.Data;
using Microsoft.AspNetCore.Mvc;

namespace krispy_back_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DonasController : ControllerBase
    {
        private readonly PanaderiaContext _context;
        private IConfiguration _config;

        public DonasController(PanaderiaContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet]
        public async Task<List<Donas>> GetDonas()
        {
            try
            {
                var donas = _context.Donas.ToList();

                return donas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
