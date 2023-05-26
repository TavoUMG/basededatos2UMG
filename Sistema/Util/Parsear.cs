using Sistema.Class;
using Sistema.Models.Sistema;
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
    }
}
