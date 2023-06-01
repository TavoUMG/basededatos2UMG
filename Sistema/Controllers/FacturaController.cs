using Microsoft.AspNetCore.Mvc;
using Sistema.Class;
using Sistema.Models.Formulario;
using Sistema.Models.Sistema;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class FacturaController : NotificadorController
    {
        private readonly ILogger<FacturaController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        protected const string _controller = "FacturaController";
        protected const string _dataTable = "DataTable/_FacturaTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private FacturaForm _form;
        private int _UsuarioId;

        public FacturaController(ILogger<FacturaController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new FacturaForm();
            _hostEnvironment = hostEnvironment;
            _UsuarioId = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).ID;
        }

        // GET: Factura/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                List<CajaModel> cajas = _service.ServiceCaja(Models.Sistema.OptionCaja.CAJA_ABIERTA, new Models.Sistema.CajaModel { UsuarioId = _UsuarioId }, _usuario).modelo;
                if (cajas.Count == 0)
                {
                    AlertSuperior("Este usuario no tiene caja abierta, cree una caja por favor.", NotificationType.info);
                    return RedirectToAction($"Index", "Caja");
                }

                CargarCatalogos();

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Factura/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(FacturaForm form)
        {
            try
            {
                List<CajaModel> cajas = _service.ServiceCaja(Models.Sistema.OptionCaja.CAJA_ABIERTA, new Models.Sistema.CajaModel { UsuarioId = _UsuarioId }, _usuario).modelo;
                if (cajas.Count == 0) throw new Exception("Este usuario aún no a abierto una caja, vaya a la caja a crear una por favor.");


                List<UsuarioModel> usuarios = _service.ServiceUsuario(Models.Sistema.OptionUsuario.SELECCIONAR_ID, new Models.Sistema.UsuarioModel { Id = _UsuarioId }, _usuario).modelo;
                List<ClienteModel> clientes = _service.ServiceCliente(Models.Sistema.OptionCliente.SELECCIONAR_ID, new Models.Sistema.ClienteModel { Id = form.ClienteId }, _usuario).modelo;

                form.Numero = "1".PadLeft(totalWidth: 5, paddingChar: '0');
                form.Archivo = $"Ticket_{form.Numero}.pdf";

                string dirPathImage = Path.Combine(_hostEnvironment.WebRootPath, "images");

                TicketPDF ticket = new TicketPDF();
                ticket.Path = Path.Combine(_hostEnvironment.WebRootPath, "Tickets", form.Archivo);
                ticket.HeaderImage = Path.Combine(_hostEnvironment.WebRootPath, "images", "logo.png");
                ticket.FooterQR = WebUtil.generarQR(_hostEnvironment.WebRootPath, form.Numero, "gracias por comprar");

                ticket.AddHeaderLine("NIT: 1002827029");
                ticket.AddHeaderLine($"FACTURA Nro.: {form.Numero}");
                ticket.AddHeaderLine($"AUTORIZACION Nro.: {ClassUtilidad.fechaSistema().ToString("fff/ss/HH/mm/MM/yyyy").Replace("/","")}");

                ticket.AddSubHeaderLine($"Fecha: {form.Fecha.ToString("dd/mm/yyyy")}");
                ticket.AddSubHeaderLine($"NIT/CUI: {form.CUI_NIT}");
                ticket.AddSubHeaderLine($"Cliente: {clientes[0].NombreCompleto}");

                form.Total = 0;
                for (int i = 0; i < form.Detalle.Count; i++)
                {
                    if(form.Detalle[i].Producto.Split("-")[0].Length > 11)
                    {
                        ticket.AddItem(
                            cantidad: form.Detalle[i].Cantidad.ToString(),
                            item: form.Detalle[i].Producto.Split("-")[0].Substring(0, 11),
                            price: String.Format("{0:0,0.00}", form.Detalle[i].Precio),
                            total: String.Format("{0:0,0.00}", form.Detalle[i].SubTotal)
                        ) ;
                    }
                    else
                    {
                        ticket.AddItem(
                            cantidad: form.Detalle[i].Cantidad.ToString(),
                            item: form.Detalle[i].Producto.Split("-")[0],
                            price: String.Format("{0:0,0.00}", form.Detalle[i].Precio),
                            total: String.Format("{0:0,0.00}", form.Detalle[i].SubTotal)
                        );
                    }

                    form.Total += form.Detalle[i].SubTotal;
                }

                //El metodo AddTotal requiere 2 parametros, 
                //la descripcion del total, y el precio 
                ticket.AddTotal("TOTAL", String.Format("{0:0,0.00}", form.Total));

                //El metodo AddFooterLine funciona igual que la cabecera 
                ticket.AddFooterLine($"Son: {ConversoresNumeroLetras.Parse(form.Total)}");
                ticket.AddFooterLine($"\n");
                ticket.AddFooterLine($"Código de Control: {ClassUtilidad.GUID()}");
                ticket.AddFooterLine($"Fecha Límite de Emisión: {form.Fecha.AddMonths(2).Date.ToString("dd/MM/yyyy")}");

                //Generamos
                if(ticket.Print())
                {
                    _form = form;
                    CargarCatalogos(imprimir: true);
                    return View(viewName: "Index", model: _form);
                }
                else
                {
                    throw new Exception("El comprobante no pudo crearse.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                Alert("¡Error!", ex.Message, NotificationType.error);

                return RedirectToAction(actionName: "Index", controllerName: "Factura");
            }
        }

        private void CargarCatalogos(bool imprimir = false)
        { 
            if(!imprimir)
            {
                _form.Detalle = new List<DatelleForm>();
            }

            _form.clientes = _service.ServiceCliente(Models.Sistema.OptionCliente.TODOS, new Models.Sistema.ClienteModel { }, _usuario).modelo;
            _form.productos = _service.ServiceInventario(Models.Sistema.OptionInventario.TODOS, new Models.Sistema.InventarioModel { }, _usuario, true).modelo;
            _form.Imprimir = imprimir;
        }
    }
}
