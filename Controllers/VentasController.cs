using krispy_back_test.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace krispy_back_test.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VentasController : ControllerBase
    {
        private readonly PanaderiaContext _context;
        private IConfiguration _config;

        public VentasController(PanaderiaContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        private async Task<List<VentaDetalleFull>> ParseVentasDetalles(int idVenta)
        {
            try
            {
                List<VentaDetalleFull> details = new List<VentaDetalleFull>();

                var detalles = _context.VentaDetalle.Where(x=>x.IdVenta == idVenta).ToList();

                foreach (var detalle in detalles)
                {
                    VentaDetalleFull detail = new VentaDetalleFull();

                    detail.Dona = _context.Donas.FirstOrDefault(x => x.Id == detalle.IdDona);
                    detail.Cantidad = detalle.Cantidad;

                    details.Add(detail);
                }

                return details;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<List<VentaFull>> ParseVentas(List<Venta> ventas)
        {
            try
            {
                List<VentaFull> sales = new List<VentaFull>();

                foreach(var venta in ventas)
                {
                    VentaFull sale = new VentaFull();

                    sale.Id = venta.Id;
                    sale.Total = venta.Total;
                    sale.Usuario = _context.Usuarios.FirstOrDefault(x => x.Id == venta.IdUsuario);
                    sale.VentaDetalles = await ParseVentasDetalles(venta.Id);
                    sales.Add(sale);
                }

                return sales;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("totales")]
        public async Task<List<VentaFull>> GetVentas()
        {
            try
            {
                var venta = _context.Venta.ToList();

                var ventas = await ParseVentas(venta);

                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        public async Task<Venta> GetVenta(int idVenta)
        {
            try
            {
                var venta = _context.Venta.FirstOrDefault(x => x.Id == idVenta);

                return venta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private async Task<ActionResult<int>> SaveVentasDetalles(List<VentaDetalleRegister> ventaDetalles, int IdVenta)
        {
            try
            {
               foreach(var ventaDetalle in ventaDetalles)
                {
                    VentaDetalle detail = new VentaDetalle();

                    detail.IdVenta = IdVenta;
                    detail.Cantidad = ventaDetalle.Cantidad;
                    detail.IdDona = ventaDetalle.IdDona;
                    var save = _context.VentaDetalle.Add(detail);
                }
                var response = await _context.SaveChangesAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<bool> SaveVentas(VentaRegister venta)
        {
            try
            {
                Venta sale = new Venta();

                sale.Id = 0;
                sale.Total = venta.Total;
                sale.Direccion = venta.Direccion;
                sale.IdUsuario = venta.IdUsuario;

                var save = _context.Venta.Add(sale);
                var response = await _context.SaveChangesAsync();

                if(response > 0)
                {
                    var lastSale = _context.Venta.OrderByDescending(x => x.Id).FirstOrDefault();
                    var responseDetails = await SaveVentasDetalles(venta.VentaDetalles, lastSale.Id);
                }

                return (response > 0 ? true : false);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> GetVenta(VentaRegister venta)
        {
            try
            {
                var response = await SaveVentas(venta);

                if (!response)
                {
                    return new BadRequestObjectResult(false);
                }
                else
                {
                    return new OkObjectResult("Venta registrada con éxito");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
