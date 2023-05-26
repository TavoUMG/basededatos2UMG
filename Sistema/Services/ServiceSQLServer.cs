using Sistema.Class;
using Sistema.Connections.SQLServer;
using Sistema.Models.Sistema;
using Sistema.Util;
using System.Data;

namespace Sistema.Services
{
    public class ServiceSQLServer
    {
        private ClassSeguridad _seguridad;
        private string _conexion = String.Empty;
        private BaseDatos _baseDatos;
        private List<ParametroDB> parametroDB;

        public ServiceSQLServer()
        {
            _seguridad = new ClassSeguridad();
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
                parametroDB.Add(new ParametroDB("@Id", 0, ParametroDB.SType.Int, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@CUI", data.CUI, ParametroDB.SType.NVarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Nombre", data.Nombre, ParametroDB.SType.NVarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Apellido", data.Apellido, ParametroDB.SType.VarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Password", data.Password, ParametroDB.SType.VarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Usuario", UsuarioAudita, ParametroDB.SType.VarChar, ParametroDB.EParameterDirection.input));
                parametroDB.Add(new ParametroDB("@Respuesta", info, ParametroDB.SType.VarChar, ParametroDB.EParameterDirection.input));
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
    }
}
