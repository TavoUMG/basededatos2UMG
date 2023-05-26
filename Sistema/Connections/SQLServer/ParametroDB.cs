using System.Data;

namespace Sistema.Connections.SQLServer
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
            BigInt = 0,
            Binary = 1,
            Bit = 2,
            Char = 3,
            DateTime = 4,
            Decimal = 5,
            Float = 6,
            Image = 7,
            Int = 8,
            Money = 9,
            NChar = 10,
            NText = 11,
            NVarChar = 12,
            Real = 13,
            SmallDateTime = 0xF,
            SmallInt = 0x10,
            SmallMoney = 17,
            Structured = 30,
            Text = 18,
            Timestamp = 19,
            TinyInt = 20,
            Udt = 29,
            UniqueIdentifier = 14,
            VarBinary = 21,
            VarChar = 22,
            Variant = 23,
            Xml = 25
        }

        public string Name;

        public object Value;

        public SqlDbType Type;

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
                case (SType)24:
                case (SType)26:
                case (SType)27:
                case (SType)28:
                    break;
                case SType.BigInt:
                    Type = SqlDbType.BigInt;
                    break;
                case SType.Binary:
                    Type = SqlDbType.Binary;
                    break;
                case SType.Bit:
                    Type = SqlDbType.Bit;
                    break;
                case SType.Char:
                    Type = SqlDbType.Char;
                    break;
                case SType.DateTime:
                    Type = SqlDbType.DateTime;
                    break;
                case SType.Decimal:
                    Type = SqlDbType.Decimal;
                    break;
                case SType.Float:
                    Type = SqlDbType.Float;
                    break;
                case SType.Image:
                    Type = SqlDbType.Image;
                    break;
                case SType.Int:
                    Type = SqlDbType.Int;
                    break;
                case SType.Money:
                    Type = SqlDbType.Money;
                    break;
                case SType.NChar:
                    Type = SqlDbType.NChar;
                    break;
                case SType.NText:
                    Type = SqlDbType.NText;
                    break;
                case SType.NVarChar:
                    Type = SqlDbType.NVarChar;
                    break;
                case SType.Real:
                    Type = SqlDbType.Real;
                    break;
                case SType.SmallDateTime:
                    Type = SqlDbType.SmallDateTime;
                    break;
                case SType.SmallInt:
                    Type = SqlDbType.SmallInt;
                    break;
                case SType.SmallMoney:
                    Type = SqlDbType.SmallMoney;
                    break;
                case SType.Structured:
                    Type = SqlDbType.Structured;
                    break;
                case SType.Text:
                    Type = SqlDbType.Text;
                    break;
                case SType.Timestamp:
                    Type = SqlDbType.Timestamp;
                    break;
                case SType.TinyInt:
                    Type = SqlDbType.TinyInt;
                    break;
                case SType.Udt:
                    Type = SqlDbType.Udt;
                    break;
                case SType.UniqueIdentifier:
                    Type = SqlDbType.UniqueIdentifier;
                    break;
                case SType.VarBinary:
                    Type = SqlDbType.VarBinary;
                    break;
                case SType.VarChar:
                    Type = SqlDbType.VarChar;
                    break;
                case SType.Variant:
                    Type = SqlDbType.Variant;
                    break;
                case SType.Xml:
                    Type = SqlDbType.Xml;
                    break;
            }
        }
    }
}
