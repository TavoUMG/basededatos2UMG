using System.Text;

namespace Sistema.Class
{
    public static class ClassUtilidad
    {
        public enum TipoDato
        {
            String = 0,
            Integer = 1,
            Boolean = 2,
            Decimal = 3,
            DateTime = 4,
            CalculateTime = 5,
            CalculateTimeReverse = 6,
            Time = 7
        }

        public static string GUID()
        {
            string guid = String.Empty;

            try
            {
                StringBuilder sb = new StringBuilder();
                DateTime dateTime = fechaSistema();
                string hash = String.Empty;
                while (hash.Length < 10)
                {
                    hash += Guid.NewGuid().ToString().GetHashCode().ToString("x");
                }
                sb.Append(dateTime.Year.ToString());
                sb.Append(dateTime.Month.ToString());
                sb.Append(dateTime.Day.ToString());
                sb.Append(dateTime.Hour.ToString());
                sb.Append(dateTime.Minute.ToString());
                sb.Append(dateTime.Second.ToString());
                sb.Append(dateTime.Millisecond.ToString());
                sb.Append(hash);
                guid = sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear GUID único:  {ex.Message}");
            }

            return guid;
        }

        public static (string letra, int numero, bool logico, decimal flotante, DateTime fechahora, TimeSpan tiempo) parseMultiple(string? value, TipoDato tipo)
        {
            (string str, int i, bool logic, decimal dec, DateTime dt, TimeSpan time) = ("", 0, false, 0, fechaSistema(), fechaSistema().TimeOfDay);

            switch ((int)tipo)
            {
                case 0:
                    str = value;
                    break;
                case 1:
                    int.TryParse(value, out i);
                    break;
                case 2:
                    bool.TryParse(value, out logic);
                    break;
                case 3:
                    decimal.TryParse(value, out dec);
                    break;
                case 4:
                    if (!String.IsNullOrEmpty(value))
                    {
                        value = value.Split(".").Length > 1 ? value.Split(".")[0] : value;

                        List<string> formats = new List<string>()
                        {
                            "d-M-yyyy",
                            "dd-MM-yyyy",
                            "dd-MM-yyyy HH:mm:ss",
                            "HH:mm:ss",
                            "MM-dd-yyyy HH:mm:ss",
                            "yyyy-MM-ddTHH:mm:sszzzz",
                            "yyyy-MM-ddTHH:mm:ss.ffzzzz",
                            "yyyy-MM-ddTHH:mm:ss.fzzzz",
                            "yyyy-MM-ddTHH:mm:ss.fffzzzz",
                            "yyyy-MM-ddTHH:mm",
                            "yyyy-MM-ddTHH:mm:ss",
                            "dd-MM-yyyy HH:mm:ssz",
                        };

                        List<string> newFormats = new List<string>();

                        foreach (var format in formats)
                            newFormats.Add(format.Replace('-', '/'));

                        formats.AddRange(newFormats);

                        dt = DateTime.ParseExact(value, formats.ToArray(), null);
                    }
                    break;
                case 5:
                    int.TryParse(value, out i);
                    str = ((i / 1000) / 60).ToString();
                    break;
                case 6:
                    int.TryParse(value, out i);
                    str = ((i * 1000) * 60).ToString();
                    break;
                case 7:
                    if (!String.IsNullOrEmpty(value))
                    {
                        TimeSpan.TryParse(value, out time);
                    }
                    break;
            }

            return (str, i, logic, dec, dt, time);
        }

        public static DateTime fechaSistema()
        {
            DateTime fecha = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second, DateTime.UtcNow.Millisecond);
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            fecha = TimeZoneInfo.ConvertTimeFromUtc(fecha, cstZone);
            return fecha;
        }
    }
}
