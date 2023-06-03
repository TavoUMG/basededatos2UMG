using Sistema.Class;
using Sistema.Models.Sistema;
using Sistema.Services;
using System.Data;

namespace Sistema.Util
{
    public static class Parsear
    {
        public static UsuarioModel DataUsuarioModel(DataRow dr)
        {
            return new UsuarioModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                CUI = dr.ItemArray[1].ToString(),
                Nombre = dr.ItemArray[2].ToString(),
                Apellido = dr.ItemArray[3].ToString(),
                Password = dr.ItemArray[4].ToString(),
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[5].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[6].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[7].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[7].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[8].ToString(),
                },
            };
        }

       
        public static CajaModel DataCajaModel(DataRow dr)
        {
            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,
            ServiceSQLServer servicio = new ServiceSQLServer();

            return new CajaModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                UsuarioId = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                efectivoApertura = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                efectivoCierre = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[4].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[5].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[7].ToString(),
                },
                Usuario = servicio.ServiceUsuario(OptionUsuario.SELECCIONAR_ID, new UsuarioModel { Id = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero }).modelo[0]
            };
        }

        public static CategoriaModel DataCategoriaModel(DataRow dr)
        {

            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,

            return new CategoriaModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Nombre = dr.ItemArray[1].ToString(),                
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[2].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[3].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[4].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[5].ToString(),
                },
            };
        }

        public static ClienteModel DataClienteModel(DataRow dr)
        {
            return new ClienteModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Nombre = dr.ItemArray[1].ToString(),
                Apellido = dr.ItemArray[2].ToString(),
                Telefono = dr.ItemArray[3].ToString(),
                Direccion = dr.ItemArray[4].ToString(),
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[5].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[6].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[7].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[7].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[8].ToString(),
                },
                NombreCompleto = $"{dr.ItemArray[1].ToString()} {dr.ItemArray[2].ToString()}"
            };
        }

        public static CompraModel DataCompraModel(DataRow dr)
        {
            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,
            ServiceSQLServer servicio = new ServiceSQLServer();
            return new CompraModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Cantidad = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ProductoId = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ProveedorId = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                PrecioCosto = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                CajaId = ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[7].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[8].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[8].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[9].ToString(),
                },
                Producto = servicio.ServiceProducto(OptionProducto.SELECCIONAR_ID, new ProductoModel { Id = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero }).modelo[0],
                Proveedor = servicio.ServiceProveedor(OptionProveedor.SELECCIONAR_ID, new ProveedorModel { Id = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero }).modelo[0]
            };
        }

        public static DetalleModel DataDetalleModel(DataRow dr)
        {
            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,
            ServiceSQLServer servicio = new ServiceSQLServer();
            return new DetalleModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                FacturaId = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ProductoId = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Cantidad = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero,                
                Precio = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                SubTotal = ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[7].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[8].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[8].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[9].ToString(),
                },
                Producto = servicio.ServiceProducto(OptionProducto.SELECCIONAR_ID, new ProductoModel { Id = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero }).modelo[0],
            };
        }

        public static DevolucionModel DataDevolucionModel(DataRow dr)
        {
            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,
            ServiceSQLServer servicio = new ServiceSQLServer();
            return new DevolucionModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Cantidad = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                FacturaId = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                DetalleId = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ProductoId = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.Integer).numero,                
                Fecha = ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[7].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[8].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[8].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[9].ToString(),
                },
                Factura = servicio.ServiceFactura(OptionFactura.SELECCIONAR_ID, new FacturaModel { Id = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero }).modelo[0],
                Detalle = servicio.ServiceDetalle(OptionDetalle.SELECCIONAR_ID, new DetalleModel { Id = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero }).modelo[0],
                Producto = servicio.ServiceProducto(OptionProducto.SELECCIONAR_ID, new ProductoModel { Id = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.Integer).numero }).modelo[0],
            };
        }

        public static FacturaModel DataFacturaModel(DataRow dr, bool llenarDetalle = true)
        {
            ServiceSQLServer servicio = new ServiceSQLServer();
            FacturaModel factura = new FacturaModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ClienteId = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                CUI_NIT = dr.ItemArray[2].ToString(),
                Direccion = dr.ItemArray[3].ToString(),
                Fecha = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,
                Total = ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                CajaId = ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Pagado = ClassUtilidad.parseMultiple(dr.ItemArray[7].ToString(), ClassUtilidad.TipoDato.Boolean).logico,
                TipoPago = dr.ItemArray[8].ToString(),
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[9].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[9].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[10].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[11].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[11].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[12].ToString(),
                },
            };

            factura.Cliente = servicio.ServiceCliente(OptionCliente.SELECCIONAR_ID, new ClienteModel { Id = factura.ClienteId }).modelo[0];
            factura.Numero = factura.Id.ToString().PadLeft(totalWidth: 5, paddingChar: '0');
            factura.Archivo = $"Ticket_{factura.Numero}.pdf";

            if (llenarDetalle)
                factura.Detalle = servicio.ServiceDetalle(OptionDetalle.SELECCIONAR_FACTURA, new DetalleModel { FacturaId = factura.Id }).modelo;

            return factura;
        }

        public static InventarioModel DataInventarioModel(DataRow dr)
        {
            ServiceSQLServer servicio = new ServiceSQLServer();

            InventarioModel inventario = new InventarioModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ProductoId = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                PrecioVenta = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                Stock = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[4].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[5].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[7].ToString(),
                }
            };

            inventario.Producto = servicio.ServiceProducto(OptionProducto.SELECCIONAR_ID, new ProductoModel { Id = inventario.ProductoId }).modelo[0];
            inventario.Activo = inventario.Stock > 0;
            inventario.ValorSelect = $"{inventario.ProductoId}|{inventario.Stock}|{inventario.PrecioVenta}";
            inventario.ValorFlied = $"{inventario.Producto.Nombre} - Q {inventario.PrecioVenta}";

            return inventario;
        }

        public static ProductoModel DataProductoModel(DataRow dr)
        {
            ServiceSQLServer servicio = new ServiceSQLServer();
            return new ProductoModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                CategoriaId = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Nombre = dr.ItemArray[2].ToString(),
                Vencimiento = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,
                Imagen = dr.ItemArray[4].ToString(),
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[5].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[6].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[7].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[7].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[8].ToString(),
                },
                Categoria = servicio.ServiceCategoria(OptionCategoria.SELECCIONAR_ID, new CategoriaModel { Id = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero }).modelo[0],
            };
        }

        public static ProveedorModel DataProveedorModel(DataRow dr)
        {
            return new ProveedorModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Nombre = dr.ItemArray[1].ToString(),
                Direccion = dr.ItemArray[2].ToString(),
                Telefono = dr.ItemArray[3].ToString(),
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[4].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[5].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[7].ToString(),
                },
            };
        }

        public static TipoPromocionModel DataTipoPromocionModel(DataRow dr)
        {
            return new TipoPromocionModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Nombre = dr.ItemArray[1].ToString()
            };
        }

        public static CobroModel DataCobroModel(DataRow dr)
        {
            ServiceSQLServer servicio = new ServiceSQLServer();

            CobroModel cobro = new CobroModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                MontoReal = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                Monto = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                MontoRestante = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                FacturaId = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                UsuarioId = ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[7].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[8].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[8].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[9].ToString(),
                },
            };

            cobro.Usuario = servicio.ServiceUsuario(OptionUsuario.SELECCIONAR_ID, new UsuarioModel { Id = cobro.UsuarioId }).modelo[0];
            cobro.Factura = servicio.ServiceFactura(OptionFactura.SELECCIONAR_ID, new FacturaModel { Id = cobro.FacturaId }, "Sistema", false).modelo[0];

            return cobro;
        }

        public static PromocionModel DataPromocionModel(DataRow dr)
        {
            ServiceSQLServer servicio = new ServiceSQLServer();

            PromocionModel promocion = new PromocionModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Descripcion = dr.ItemArray[1].ToString(),
                ProductoId = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                TipoPromocionId = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[4].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[5].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[7].ToString(),
                },
            };

            promocion.Producto = servicio.ServiceProducto(OptionProducto.SELECCIONAR_ID, new ProductoModel { Id = promocion.ProductoId }).modelo[0];
            promocion.TipoPromocion = servicio.ServiceTipoPromocion(OptionTipoPromocion.SELECCIONAR_ID, new TipoPromocionModel { Id = promocion.TipoPromocionId }).modelo[0];

            return promocion;
        }
    }
}
