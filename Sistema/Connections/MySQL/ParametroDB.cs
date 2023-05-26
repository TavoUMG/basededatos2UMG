using MySql.Data.MySqlClient;
using System.Data;

namespace Sistema.Connections.MySQL
{
    public class ParametroDB
    {
        public enum EParameterDirection
        {
            input = 0,
            output = 1
        }

        public enum SType
        {
            //
            // Resumen:
            //     MySql.Data.MySqlClient.MySqlDbType.Decimal
            //     A fixed precision and scale numeric value between -1038 -1 and 10 38 -1.
            Decimal = 0,
            //
            // Resumen:
            //     MySql.Data.MySqlClient.MySqlDbType.Byte
            //     The signed range is -128 to 127. The unsigned range is 0 to 255.
            Byte = 1,
            //
            // Resumen:
            //     MySql.Data.MySqlClient.MySqlDbType.Int16
            //     A 16-bit signed integer. The signed range is -32768 to 32767. The unsigned range
            //     is 0 to 65535
            Int16 = 2,
            //
            // Resumen:
            //     Specifies a 24 (3 byte) signed or unsigned value.
            Int24 = 9,
            //
            // Resumen:
            //     MySql.Data.MySqlClient.MySqlDbType.Int32
            //     A 32-bit signed integer
            Int32 = 3,
            //
            // Resumen:
            //     MySql.Data.MySqlClient.MySqlDbType.Int64
            //     A 64-bit signed integer.
            Int64 = 8,
            //
            // Resumen:
            //     System.Single
            //     A small (single-precision) floating-point number. Allowable values are -3.402823466E+38
            //     to -1.175494351E-38, 0, and 1.175494351E-38 to 3.402823466E+38.
            Float = 4,
            //
            // Resumen:
            //     MySql.Data.MySqlClient.MySqlDbType.Double
            //     A normal-size (double-precision) floating-point number. Allowable values are
            //     -1.7976931348623157E+308 to -2.2250738585072014E-308, 0, and 2.2250738585072014E-308
            //     to 1.7976931348623157E+308.
            Double = 5,
            //
            // Resumen:
            //     A timestamp. The range is '1970-01-01 00:00:00' to sometime in the year 2037
            Timestamp = 7,
            //
            // Resumen:
            //     Date The supported range is '1000-01-01' to '9999-12-31'.
            Date = 10,
            //
            // Resumen:
            //     Time
            //     The range is '-838:59:59' to '838:59:59'.
            Time = 11,
            //
            // Resumen:
            //     DateTime The supported range is '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.
            DateTime = 12,
            //
            // Resumen:
            //     Datetime The supported range is '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.
            [Obsolete("The Datetime enum value is obsolete.  Please use DateTime.")]
            Datetime = 12,
            //
            // Resumen:
            //     A year in 2- or 4-digit format (default is 4-digit). The allowable values are
            //     1901 to 2155, 0000 in the 4-digit year format, and 1970-2069 if you use the 2-digit
            //     format (70-69).
            Year = 13,
            //
            // Resumen:
            //     Obsolete Use Datetime or Date type
            Newdate = 14,
            //
            // Resumen:
            //     A variable-length string containing 0 to 65535 characters
            VarString = 0xF,
            //
            // Resumen:
            //     Bit-field data type
            Bit = 0x10,
            //
            // Resumen:
            //     JSON
            JSON = 245,
            //
            // Resumen:
            //     New Decimal
            NewDecimal = 246,
            //
            // Resumen:
            //     An enumeration. A string object that can have only one value, chosen from the
            //     list of values 'value1', 'value2', ..., NULL or the special "" error value. An
            //     ENUM can have a maximum of 65535 distinct values
            Enum = 247,
            //
            // Resumen:
            //     A set. A string object that can have zero or more values, each of which must
            //     be chosen from the list of values 'value1', 'value2', ... A SET can have a maximum
            //     of 64 members.
            Set = 248,
            //
            // Resumen:
            //     A binary column with a maximum length of 255 (2^8 - 1) characters
            TinyBlob = 249,
            //
            // Resumen:
            //     A binary column with a maximum length of 16777215 (2^24 - 1) bytes.
            MediumBlob = 250,
            //
            // Resumen:
            //     A binary column with a maximum length of 4294967295 or 4G (2^32 - 1) bytes.
            LongBlob = 251,
            //
            // Resumen:
            //     A binary column with a maximum length of 65535 (2^16 - 1) bytes.
            Blob = 252,
            //
            // Resumen:
            //     A variable-length string containing 0 to 255 bytes.
            VarChar = 253,
            //
            // Resumen:
            //     A fixed-length string.
            String = 254,
            //
            // Resumen:
            //     Geometric (GIS) data type.
            Geometry = 0xFF,
            //
            // Resumen:
            //     Unsigned 8-bit value.
            UByte = 501,
            //
            // Resumen:
            //     Unsigned 16-bit value.
            UInt16 = 502,
            //
            // Resumen:
            //     Unsigned 24-bit value.
            UInt24 = 509,
            //
            // Resumen:
            //     Unsigned 32-bit value.
            UInt32 = 503,
            //
            // Resumen:
            //     Unsigned 64-bit value.
            UInt64 = 508,
            //
            // Resumen:
            //     Fixed length binary string.
            Binary = 754,
            //
            // Resumen:
            //     Variable length binary string.
            VarBinary = 753,
            //
            // Resumen:
            //     A text column with a maximum length of 255 (2^8 - 1) characters.
            TinyText = 749,
            //
            // Resumen:
            //     A text column with a maximum length of 16777215 (2^24 - 1) characters.
            MediumText = 750,
            //
            // Resumen:
            //     A text column with a maximum length of 4294967295 or 4G (2^32 - 1) characters.
            LongText = 751,
            //
            // Resumen:
            //     A text column with a maximum length of 65535 (2^16 - 1) characters.
            Text = 752,
            //
            // Resumen:
            //     A guid column.
            Guid = 854
        }

        public string Name;

        public object Value;

        public MySqlDbType Type;

        public ParameterDirection Direction;

        public ParametroDB(string pName, object pValue, SType pType, EParameterDirection pDirection = 0)
        {
            Name = pName;
            Value = pValue;

            switch (pDirection)
            {
                case EParameterDirection.input:
                    Direction = ParameterDirection.Input;
                    break;
                case EParameterDirection.output:
                    Direction = ParameterDirection.InputOutput;
                    break;
            }

            switch (pType)
            {
                case SType.Decimal:
                    Type = MySqlDbType.Decimal;
                    break;
                case SType.Byte:
                    Type = MySqlDbType.Byte;
                    break;
                case SType.Int16:
                    Type = MySqlDbType.Int16;
                    break;
                case SType.Int24:
                    Type = MySqlDbType.Int24;
                    break;
                case SType.Int32:
                    Type = MySqlDbType.Int32;
                    break;
                case SType.Int64:
                    Type = MySqlDbType.Int64;
                    break;
                case SType.Float:
                    Type = MySqlDbType.Float;
                    break;
                case SType.Double:
                    Type = MySqlDbType.Double;
                    break;
                case SType.Timestamp:
                    Type = MySqlDbType.Timestamp;
                    break;
                case SType.Date:
                    Type = MySqlDbType.Date;
                    break;
                case SType.Time:
                    Type = MySqlDbType.Time;
                    break;
                case SType.DateTime:
                    Type = MySqlDbType.DateTime;
                    break;
                case SType.Year:
                    Type = MySqlDbType.Year;
                    break;
                case SType.Newdate:
                    Type = MySqlDbType.Newdate;
                    break;
                case SType.Bit:
                    Type = MySqlDbType.Bit;
                    break;
                case SType.JSON:
                    Type = MySqlDbType.JSON;
                    break;
                case SType.NewDecimal:
                    Type = MySqlDbType.NewDecimal;
                    break;
                case SType.Enum:
                    Type = MySqlDbType.Enum;
                    break;
                case SType.Set:
                    Type = MySqlDbType.Set;
                    break;
                case SType.TinyBlob:
                    Type = MySqlDbType.TinyBlob;
                    break;
                case SType.MediumBlob:
                    Type = MySqlDbType.MediumBlob;
                    break;
                case SType.LongBlob:
                    Type = MySqlDbType.LongBlob;
                    break;
                case SType.Blob:
                    Type = MySqlDbType.Blob;
                    break;
                case SType.VarChar:
                    Type = MySqlDbType.VarChar;
                    break;
                case SType.String:
                    Type = MySqlDbType.String;
                    break;
                case SType.Geometry:
                    Type = MySqlDbType.Geometry;
                    break;
                case SType.UByte:
                    Type = MySqlDbType.UByte;
                    break;
                case SType.UInt16:
                    Type = MySqlDbType.UInt16;
                    break;
                case SType.UInt24:
                    Type = MySqlDbType.UInt24;
                    break;
                case SType.UInt32:
                    Type = MySqlDbType.UInt32;
                    break;
                case SType.UInt64:
                    Type = MySqlDbType.UInt64;
                    break;
                case SType.Binary:
                    Type = MySqlDbType.Binary;
                    break;
                case SType.VarBinary:
                    Type = MySqlDbType.VarBinary;
                    break;
                case SType.TinyText:
                    Type = MySqlDbType.TinyText;
                    break;
                case SType.MediumText:
                    Type = MySqlDbType.MediumText;
                    break;
                case SType.LongText:
                    Type = MySqlDbType.LongText;
                    break;
                case SType.Text:
                    Type = MySqlDbType.Text;
                    break;
                case SType.Guid:
                    Type = MySqlDbType.Guid;
                    break;
            }
        }
    }
}
