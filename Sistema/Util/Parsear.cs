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

       
        public static CajaModel DataCajaModel(DataRow dr)
        {
            
            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,

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
            };
        }

        public static CompraModel DataCompraModel(DataRow dr)
        {

            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,

            return new CompraModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Cantidad = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ProductoId = ClassUtilidad.parseMultiple(dr.ItemArray[2].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ProveedorId = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                PrecioCosto = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[5].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[6].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[7].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[7].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[8].ToString(),
                },
            };
        }

        public static DetalleModel DataDetalleModel(DataRow dr)
        {

            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,

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
            };
        }

        public static DevolucionModel DataDevolucionModel(DataRow dr)
        {

            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,

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
            };
        }

        public static FacturaModel DataFacturaModel(DataRow dr)
        {

            //ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,

            return new FacturaModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                ClienteId = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                CUI_NIT = dr.ItemArray[2].ToString(),
                Direccion = dr.ItemArray[3].ToString(),
                Fecha = ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,
                Total = ClassUtilidad.parseMultiple(dr.ItemArray[5].ToString(), ClassUtilidad.TipoDato.Decimal).flotante,                
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[7].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[8].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[8].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[9].ToString(),
                },
            };
        }

            public static InventarioModel DataInventarioModel(DataRow dr)
            {
                return new InventarioModel
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
                    },
                };
            }

        public static ProductoModel DataProductoModel(DataRow dr)
        {
            return new ProductoModel
            {
                Id = ClassUtilidad.parseMultiple(dr.ItemArray[0].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                CategoriaId = ClassUtilidad.parseMultiple(dr.ItemArray[1].ToString(), ClassUtilidad.TipoDato.Integer).numero,
                Nombre = dr.ItemArray[2].ToString(),
                Vencimiento = ClassUtilidad.parseMultiple(dr.ItemArray[3].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora,                
                Auditoria = new AuditoriaModel
                {
                    AuditFechaCreacion = String.IsNullOrEmpty(dr.ItemArray[4].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[4].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioCreacion = dr.ItemArray[5].ToString(),
                    AuditFechaModificacion = String.IsNullOrEmpty(dr.ItemArray[6].ToString()) ? null : ClassUtilidad.parseMultiple(dr.ItemArray[6].ToString(), ClassUtilidad.TipoDato.DateTime).fechahora.ToString("dd/MM/yyyy HH:mm:ss"),
                    AuditUsuarioModificacion = dr.ItemArray[7].ToString(),
                },
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
    }
    }
