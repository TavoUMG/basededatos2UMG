using Sistema.Class;
using Sistema.Connections.SQLServer;
using Sistema.Models.Formulario;
using Sistema.Models.Sistema;
using Sistema.Util;
using System.Data;
using System.Text.Json;

namespace Sistema.Services
{
    public class ServiceSQLServer
    {
        private string _conexion = String.Empty;
        private BaseDatos _baseDatos;
        private List<ParametroDB> parametroDB;
        private string _sp_reportes;

        public ServiceSQLServer()
        {
            _conexion = Environment.GetEnvironmentVariable("CONEXION_STRING");
            _sp_reportes = "SP_REPORTERIA_SISTEMA";
        }

        public (bool respuesta, string mensaje, List<UsuarioModel> modelo) ServiceUsuario(OptionUsuario option, UsuarioModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<UsuarioModel> modelo) = (false, "", new List<UsuarioModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@CUI", data.CUI, ParametroDB.SType.NVarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Nombre", data.Nombre, ParametroDB.SType.NVarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Apellido", data.Apellido, ParametroDB.SType.VarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Password", data.Password, ParametroDB.SType.VarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.VarChar, ParametroDB.EParameterDirection.input));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_USUARIO", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch(option)
                {
                    case OptionUsuario.TODOS:
                    case OptionUsuario.SELECCIONAR:
                    case OptionUsuario.INICIO_SESION:
                    case OptionUsuario.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataUsuarioModel(dr));
                        }
                    break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Usuario: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<CajaModel> modelo) ServiceCaja(OptionCaja option, CajaModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<CajaModel> modelo) = (false, "", new List<CajaModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion",(int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id",data.Id,ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@UsuarioId",data.UsuarioId,ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@efectivoApertura",data.efectivoApertura,ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@efectivoCierre",data.efectivoCierre,ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@Usuario",UsuarioAudita,ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_CAJA", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option) { 
                    case OptionCaja.TODOS:
                    case OptionCaja.CAJA_ABIERTA:
                    case OptionCaja.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataCajaModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Caja: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<CategoriaModel> modelo) ServiceCategoria(OptionCategoria option, CategoriaModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<CategoriaModel> modelo) = (false, "", new List<CategoriaModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Nombre", data.Nombre, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_CATEGORIA", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {
                    case OptionCategoria.TODOS:
                    case OptionCategoria.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataCategoriaModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Categoria: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<ClienteModel> modelo) ServiceCliente(OptionCliente option, ClienteModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<ClienteModel> modelo) = (false, "", new List<ClienteModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Nombre", data.Nombre, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Apellido", data.Apellido, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Telefono", data.Telefono, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Direccion", data.Direccion, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_CLIENTE", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionCliente.TODOS:
                    case OptionCliente.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataClienteModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Cliente: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<CompraModel> modelo) ServiceCompra(OptionCompra option, CompraModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<CompraModel> modelo) = (false, "", new List<CompraModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Cantidad", data.Cantidad, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@ProductoId", data.ProductoId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@ProveedorId", data.ProveedorId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@PrecioCosto", data.PrecioCosto, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@CajaId", data.CajaId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_COMPRA", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionCompra.TODOS:
                    case OptionCompra.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataCompraModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Compra: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<DetalleModel> modelo) ServiceDetalle(OptionDetalle option, DetalleModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<DetalleModel> modelo) = (false, "", new List<DetalleModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@FacturaId", data.FacturaId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@ProductoId", data.ProductoId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Cantidad", data.Cantidad, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Precio", data.Precio, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@SubTotal", data.SubTotal, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_DETALLE", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionDetalle.TODOS:
                    case OptionDetalle.SELECCIONAR_ID:
                    case OptionDetalle.SELECCIONAR_FACTURA:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataDetalleModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Detalle: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<DevolucionModel> modelo) ServiceDevolucion(OptionDevolucion option, DevolucionModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<DevolucionModel> modelo) = (false, "", new List<DevolucionModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Cantidad", data.Cantidad, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@FacturaId", data.FacturaId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@DetalleId", data.DetalleId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@ProductoId", data.ProductoId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Fecha", data.Fecha, ParametroDB.SType.DateTime));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_DEVOLUCION", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionDevolucion.TODOS:
                    case OptionDevolucion.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataDevolucionModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Devolucion: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<FacturaModel> modelo) ServiceFactura(OptionFactura option, FacturaModel data, String UsuarioAudita = "Sistema", bool llenarDetalle = true)
        {
            (bool respuesta, string mensaje, List<FacturaModel> modelo) = (false, "", new List<FacturaModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@ClienteId", data.ClienteId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Cui_Nit", data.CUI_NIT, ParametroDB.SType.VarChar));
                parametroDB.Add(new ParametroDB("@Direccion", data.Direccion, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Fecha", data.Fecha, ParametroDB.SType.DateTime));
                parametroDB.Add(new ParametroDB("@Total", data.Total, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@CajaId", data.CajaId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@TipoPago", String.IsNullOrEmpty(data.TipoPago) ? "" : data.TipoPago, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_FACTURA", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionFactura.TODOS:
                    case OptionFactura.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataFacturaModel(dr, llenarDetalle));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Factura: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<InventarioModel> modelo) ServiceInventario(OptionInventario option, InventarioModel data, String UsuarioAudita = "Sistema", bool excluirInactivo = false)
        {
            (bool respuesta, string mensaje, List<InventarioModel> modelo) = (false, "", new List<InventarioModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@ProductoId", data.ProductoId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@PrecioVenta", data.PrecioVenta, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@Stock", data.Stock, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_INVENTARIO", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {
                    case OptionInventario.TODOS:
                    case OptionInventario.SELECCIONAR_ID:
                    case OptionInventario.PRODUCTO_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (excluirInactivo)
                            {
                                if(ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero > 0)
                                {
                                    modelo.Add(Parsear.DataInventarioModel(dr));
                                }
                            }
                            else
                            {
                                modelo.Add(Parsear.DataInventarioModel(dr));
                            }
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Inventario: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<ProductoModel> modelo) ServiceProducto(OptionProducto option, ProductoModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<ProductoModel> modelo) = (false, "", new List<ProductoModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@CategoriaId", data.CategoriaId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Nombre", data.Nombre, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Vencimiento", data.Vencimiento, ParametroDB.SType.DateTime));
                parametroDB.Add(new ParametroDB("@Imagen", String.IsNullOrEmpty(data.Imagen) ? null : data.Imagen, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_PRODUCTO", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionProducto.TODOS:
                    case OptionProducto.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataProductoModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Producto: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<ProveedorModel> modelo) ServiceProveedor(OptionProveedor option, ProveedorModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<ProveedorModel> modelo) = (false, "", new List<ProveedorModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Nombre", data.Nombre, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Direccion", data.Direccion, ParametroDB.SType.NVarChar));                
                parametroDB.Add(new ParametroDB("@Telefono", data.Telefono, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_PROVEEDOR", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {
                    case OptionProveedor.TODOS:
                    case OptionProveedor.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataProveedorModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Proveedor: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<FacturaModel> factura, List<DetalleModel> detalle) ServiceTransactionFactura(FacturaForm data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<FacturaModel> factura, List<DetalleModel> detalle) = (false, "", new List<FacturaModel>(), new List<DetalleModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();             

                parametroDB.Add(new ParametroDB("@ClienteId", data.ClienteId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Cui_Nit", data.CUI_NIT, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Direccion", String.IsNullOrEmpty(data.Direccion) ? "" : data.Direccion, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Fecha", data.Fecha, ParametroDB.SType.DateTime));
                parametroDB.Add(new ParametroDB("@Total", data.Total, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@Detalle", JsonSerializer.Serialize(data), ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Monto", data.Monto, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@CajaId", data.CajaId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@TipoPago", data.Pago, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_TRANSACCION_FACTURA", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);

                if (_baseDatos.getDataset().Tables.Count == 2) {
                    DataTable dt = _baseDatos.getDataset().Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        factura.Add(Parsear.DataFacturaModel(dr, false));
                    }

                    dt = _baseDatos.getDataset().Tables[1];

                    foreach (DataRow dr in dt.Rows)
                    {
                        detalle.Add(Parsear.DataDetalleModel(dr));
                    }
                }
                else
                {
                    throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                }  
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"TransactionFactura: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, factura, detalle);
        }

        public (bool respuesta, string mensaje, List<TipoPromocionModel> modelo) ServiceTipoPromocion(OptionTipoPromocion option, TipoPromocionModel data)
        {
            (bool respuesta, string mensaje, List<TipoPromocionModel> modelo) = (false, "", new List<TipoPromocionModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();
                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_TIPO_PROMOCION", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                DataTable dt = _baseDatos.getDataset().Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    modelo.Add(Parsear.DataTipoPromocionModel(dr));
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"TipoPromocion: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<CobroModel> cobro, List<FacturaModel> factura) ServiceCobro(OptionCobro option, CobroModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<CobroModel> cobro, List<FacturaModel> factura) = (false, "", new List<CobroModel>(), new List<FacturaModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Monto", data.Monto, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@FacturaId", data.FacturaId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_COBRO", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                DataTable dt = new DataTable();

                switch (option)
                {
                    case OptionCobro.TODOS:
                    case OptionCobro.SELECCIONAR_ID:
                    case OptionCobro.MONTO_RESTANTE:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            cobro.Add(Parsear.DataCobroModel(dr));
                        }
                        break;
                    case OptionCobro.FACTURA_PENDIENTE_PAGO:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            factura.Add(Parsear.DataFacturaModel(dr, false));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Cobro: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, cobro, factura);
        }

        public (bool respuesta, string mensaje, List<PromocionModel> modelo) ServicePromocion(OptionPromocion option, PromocionModel data, String UsuarioAudita = "Sistema")
        {
            (bool respuesta, string mensaje, List<PromocionModel> modelo) = (false, "", new List<PromocionModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)option, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Id", data.Id, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Descripcion", String.IsNullOrEmpty(data.Descripcion) ? "" : data.Descripcion, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@ProductoId", data.ProductoId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@TipoPromocionId", data.TipoPromocionId, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@ProductosId", String.IsNullOrEmpty(data.ProductosId) ? "" : data.ProductosId, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_PROMOCION", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {
                    case OptionPromocion.TODOS:
                    case OptionPromocion.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataPromocionModel(dr));
                        }
                        break;
                    default:
                        if (_baseDatos.getDataset().Tables.Count != 0 && _baseDatos.getDataset().Tables[0].Rows.Count == 1) throw new Exception(_baseDatos.getDataset().Tables[0].Rows[0].ItemArray[0].ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Promocion: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<ReporteVentaModel> modelo) ServiceReporteVenta(ReporteForm data)
        {
            (bool respuesta, string mensaje, List<ReporteVentaModel> modelo) = (false, "", new List<ReporteVentaModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)OptionReporte.VENTA, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Fecha", data.Fecha, ParametroDB.SType.DateTime));

                respuesta = _baseDatos.executeSP(_sp_reportes, parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                DataTable dt = _baseDatos.getDataset().Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    modelo.Add(new ReporteVentaModel
                    {
                        Caja = $"Caja #{dr.ItemArray[0].ToString()}",
                        Producto = dr.ItemArray[1].ToString(),
                        Apertura = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                        Cierre = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                        Venta = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                    });
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"ReporteVenta: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<ReporteCajaModel> modelo) ServiceReporteCaja(ReporteForm data)
        {
            (bool respuesta, string mensaje, List<ReporteCajaModel> modelo) = (false, "", new List<ReporteCajaModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)OptionReporte.VENTA, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Fecha", data.Fecha, ParametroDB.SType.DateTime));

                respuesta = _baseDatos.executeSP(_sp_reportes, parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                DataTable dt = _baseDatos.getDataset().Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    modelo.Add(new ReporteCajaModel
                    {
                        Caja = $"Caja #{dr.ItemArray[0].ToString()}",
                        Apertura = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                        Cierre = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                        UsuarioAbre = dr.ItemArray[3].ToString(),
                        UsuarioCierra = dr.ItemArray[4].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"ReporteCaja: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }

        public (bool respuesta, string mensaje, List<ReporteCompraModel> modelo) ServiceReporteCompra(ReporteForm data)
        {
            (bool respuesta, string mensaje, List<ReporteCompraModel> modelo) = (false, "", new List<ReporteCompraModel>());
            _baseDatos = new BaseDatos(_conexion);

            try
            {
                string? info = String.Empty;
                parametroDB = new List<ParametroDB>();

                parametroDB.Add(new ParametroDB("@Opcion", (int)OptionReporte.VENTA, ParametroDB.SType.Int));
                parametroDB.Add(new ParametroDB("@Fecha", data.Fecha, ParametroDB.SType.DateTime));

                respuesta = _baseDatos.executeSP(_sp_reportes, parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                if (_baseDatos.getDataset().Tables.Count == 0) throw new Exception(info);
                DataTable dt = _baseDatos.getDataset().Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    modelo.Add(new ReporteCompraModel
                    {
                        Producto = dr.ItemArray[0].ToString(),
                        Proveedor = dr.ItemArray[1].ToString(),
                        Compra = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                        Venta = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                        Inventario = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.Integer).numero
                    });
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"ReporteCaja: {ex.Message}";
            }
            finally
            {
                _baseDatos.closeConnection();
            }

            return (respuesta, mensaje, modelo);
        }
    }
}
