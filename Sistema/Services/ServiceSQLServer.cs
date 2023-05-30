using Sistema.Class;
using Sistema.Connections.SQLServer;
using Sistema.Models.Sistema;
using Sistema.Util;
using System.Data;

namespace Sistema.Services
{
    public class ServiceSQLServer
    {
        private string _conexion = String.Empty;
        private BaseDatos _baseDatos;
        private List<ParametroDB> parametroDB;

        public ServiceSQLServer()
        {
            _conexion = Environment.GetEnvironmentVariable("CONEXION_STRING");
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
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_COMPRA", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionCompra.TODOS:
                    case OptionCompra.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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

        public (bool respuesta, string mensaje, List<FacturaModel> modelo) ServiceFactura(OptionFactura option, FacturaModel data, String UsuarioAudita = "Sistema")
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
                parametroDB.Add(new ParametroDB("Direccion", data.Direccion, ParametroDB.SType.NVarChar));
                parametroDB.Add(new ParametroDB("Fecha", data.Fecha, ParametroDB.SType.DateTime));
                parametroDB.Add(new ParametroDB("Total", data.Total, ParametroDB.SType.Decimal));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_FACTURA", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionFactura.TODOS:
                    case OptionFactura.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataFacturaModel(dr));
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

        public (bool respuesta, string mensaje, List<InventarioModel> modelo) ServiceInventario(OptionInventario option, InventarioModel data, String UsuarioAudita = "Sistema")
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
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
                        DataTable dt = _baseDatos.getDataset().Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            modelo.Add(Parsear.DataInventarioModel(dr));
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
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.NVarChar));

                respuesta = _baseDatos.executeSP("SP_MATENIMIENTO_PRODUCTO", parametroDB, BaseDatos.ReturnTypes.Dataset);
                if (!respuesta) throw new Exception(_baseDatos.getMessage());
                info = String.IsNullOrEmpty(info) ? $"No pudimos realizar el proceso seleccionado." : info;

                switch (option)
                {

                    case OptionProducto.TODOS:
                    case OptionProducto.SELECCIONAR_ID:
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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
                        if (_baseDatos.getDataset().Tables.Count == 0 || _baseDatos.getDataset().Tables[0].Rows.Count == 0) throw new Exception(info);
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

    }
}
