using MySql.Data.MySqlClient;
using System.Data;

namespace Sistema.Connections.MySQL
{
    public class BaseDatos
    {
        public enum ReturnIsolationLevel
        {
            ReadCommitted = 1,
            ReadUncommitted,
            RepeatableRead,
            Serializable
        }

        public enum ReturnTypes
        {
            Reader = 1,
            Dataset,
            Xml,
            Caracter
        }

        private string _connectionString;

        private string _defaultConnection;

        private MySqlConnection _sqlConn;

        private MySqlCommand _sqlCommand;

        private MySqlDataAdapter _sqlDataAdapter;

        private DataSet _rows;

        private MySqlDataReader _sqlReader;

        private string _message;

        private MySqlTransaction _sqlTransac;

        const int _timeOutBD = 18000;

        private const string prefix = "SQL";

        public BaseDatos(string defaultConnection)
        {
            try
            {
                if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("CONEXION_STRING")))
                {
                    // Equivalent connection string:
                    // "Uid=<DB_USER>;Pwd=<DB_PASS>;Host=<INSTANCE_HOST>;Database=<DB_NAME>;"
                    var connectionString = new MySqlConnectionStringBuilder()
                    {
                        // Note: Saving credentials in environment variables is convenient, but not
                        // secure - consider a more secure solution such as
                        // Cloud Secret Manager (https://cloud.google.com/secret-manager) to help
                        // keep secrets safe.
                        Server = Environment.GetEnvironmentVariable("DB_HOST"),
                        UserID = Environment.GetEnvironmentVariable("DB_USER"),
                        Password = Environment.GetEnvironmentVariable("DB_PASS"),
                        Database = Environment.GetEnvironmentVariable("DB_NAME"),
                        SslMode = MySqlSslMode.Disabled,
                    };
                    connectionString.Pooling = true;

                    // Specify additional properties here.
                    defaultConnection = connectionString.ConnectionString;
                }

                if (string.IsNullOrEmpty(defaultConnection))
                {
                    throw new ArgumentException($"'{nameof(defaultConnection)}' no puede ser nulo ni estar vacío.", nameof(defaultConnection));
                }

                _defaultConnection = defaultConnection;
                _message = "";
                _rows = new DataSet();
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }

        ~BaseDatos()
        {
            closeConnection();
        }

        public string getMessage()
        {
            return _message;
        }

        public DataSet getDataset()
        {
            return _rows;
        }

        public MySqlDataReader getReader()
        {
            return _sqlReader;
        }

        public MySqlCommand getCommand()
        {
            return _sqlCommand;
        }

        private bool loadConnectionString()
        {
            bool result = false;
            _connectionString = string.Empty;

            try
            {
                _connectionString = _defaultConnection;
            }
            catch (Exception ex2)
            {
                _message = ex2.Message;
                _connectionString = string.Empty;
            }

            if (!(_connectionString == string.Empty))
            {
                result = true;
            }

            return result;
        }

        public string loadConnectionString(string key)
        {
            string empty = string.Empty;
            _connectionString = string.Empty;

            try
            {
                _connectionString = key;
            }
            catch (Exception ex2)
            {
                _message = ex2.Message;
                _connectionString = string.Empty;
            }

            return _connectionString;
        }

        public bool openConnection()
        {
            bool result = false;

            if (loadConnectionString())
            {
                try
                {
                    if (_sqlConn != null)
                    {
                        if (_sqlConn.State != 0)
                        {
                            if (_sqlConn.State != ConnectionState.Open)
                            {
                                return result;
                            }

                            result = true;
                            return result;
                        }

                        _sqlConn.Open();
                        result = true;
                        return result;
                    }

                    _sqlConn = new MySqlConnection(_connectionString);
                    _sqlConn.Open();
                    result = true;
                    return result;
                }
                catch (MySqlException ex)
                {
                    _message = $"{prefix} {ex.Message}";
                    return result;
                }
                catch (Exception ex2)
                {
                    _message = "Exception. Message=" + ex2.Message;
                    return result;
                }
            }

            if (string.IsNullOrEmpty(_message))
            {
                _message = "Database Connection String cannot be Null or Blank";
            }

            return result;
        }

        public bool closeConnection()
        {
            bool result = false;

            try
            {
                if (_sqlReader != null)
                {
                    _sqlReader.Close();
                }

                if (_sqlConn != null)
                {
                    _sqlConn.Close();
                }

                result = true;
                return result;
            }
            catch (Exception ex)
            {
                _message = "Exception. Message=" + ex.Message;
                return result;
            }
        }

        public bool BeginTransaction()
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        _sqlTransac = _sqlConn.BeginTransaction();
                        result = true;
                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public bool BeginTransaction(string TransactionName)
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.Serializable, TransactionName);
                        result = true;
                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public bool BeginTransaction(ReturnIsolationLevel returns, string TransactionName)
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        switch (returns)
                        {
                            case ReturnIsolationLevel.ReadCommitted:
                                _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.ReadCommitted, TransactionName);
                                break;
                            case ReturnIsolationLevel.ReadUncommitted:
                                _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.ReadUncommitted, TransactionName);
                                break;
                            case ReturnIsolationLevel.RepeatableRead:
                                _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.RepeatableRead, TransactionName);
                                break;
                            case ReturnIsolationLevel.Serializable:
                                _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.Serializable, TransactionName);
                                break;
                        }

                        result = true;
                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public bool BeginTransaction(ReturnIsolationLevel returns)
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        switch (returns)
                        {
                            case ReturnIsolationLevel.ReadCommitted:
                                _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.ReadCommitted);
                                break;
                            case ReturnIsolationLevel.ReadUncommitted:
                                _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.ReadUncommitted);
                                break;
                            case ReturnIsolationLevel.RepeatableRead:
                                _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.RepeatableRead);
                                break;
                            case ReturnIsolationLevel.Serializable:
                                _sqlTransac = _sqlConn.BeginTransaction(IsolationLevel.Serializable);
                                break;
                        }

                        result = true;
                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public bool CommitTransaction()
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        _sqlTransac.Commit();
                        result = true;
                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public bool RollBackTransaction()
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        _sqlTransac.Rollback();
                        result = true;
                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public bool executeNonQuery(string sqlStatement)
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        if (_sqlCommand == null)
                        {
                            _sqlCommand = new MySqlCommand(sqlStatement, _sqlConn);
                        }
                        else
                        {
                            _sqlCommand.CommandText = sqlStatement;
                        }

                        _sqlCommand.Transaction = _sqlTransac;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.ExecuteNonQuery();
                        result = true;
                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public object? executeScalar(string sqlStatement)
        {
            object? result = null;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        if (_sqlCommand == null)
                        {
                            _sqlCommand = new MySqlCommand(sqlStatement, _sqlConn);
                        }
                        else
                        {
                            _sqlCommand.CommandText = sqlStatement;
                        }

                        _sqlCommand.Transaction = _sqlTransac;
                        _sqlCommand.CommandType = CommandType.Text;
                        result = _sqlCommand.ExecuteScalar();
                        return result;
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public bool executeQuery(string query, ReturnTypes returns)
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        if (_sqlCommand == null)
                        {
                            _sqlCommand = new MySqlCommand(query, _sqlConn);
                        }
                        else
                        {
                            _sqlCommand.CommandText = query;
                        }

                        _sqlCommand.Transaction = _sqlTransac;
                        _sqlCommand.CommandType = CommandType.Text;
                        switch (returns)
                        {
                            case ReturnTypes.Reader:
                                _sqlReader = _sqlCommand.ExecuteReader();
                                result = true;
                                return result;
                            case ReturnTypes.Dataset:
                                _sqlDataAdapter = new MySqlDataAdapter();
                                _sqlDataAdapter.SelectCommand = _sqlCommand;
                                _rows = new DataSet();
                                _sqlDataAdapter.Fill(_rows);
                                closeConnection();
                                result = true;
                                return result;
                            default:
                                return result;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }

        public bool executeSP(string sp, List<ParametroDB> pParametros, ReturnTypes returns, int timeOutBD = _timeOutBD)
        {
            bool result = false;
            _message = "";

            try
            {
                if (openConnection())
                {
                    try
                    {
                        if (_sqlCommand == null)
                        {
                            _sqlCommand = new MySqlCommand(sp, _sqlConn);
                        }
                        else
                        {
                            _sqlCommand.CommandText = sp;
                            _sqlCommand.Parameters.Clear();
                        }

                        _sqlCommand.Transaction = _sqlTransac;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = timeOutBD;
                        foreach (ParametroDB pParametro in pParametros)
                        {
                            MySqlParameter pCommand = new MySqlParameter();
                            pCommand.ParameterName = pParametro.Name;
                            pCommand.MySqlDbType = pParametro.Type;
                            pCommand.Value = pParametro.Value;
                            pCommand.Direction = pParametro.Direction;
                            _sqlCommand.Parameters.Add(pCommand);
                        }

                        switch (returns)
                        {
                            case ReturnTypes.Reader:
                                _sqlReader = _sqlCommand.ExecuteReader();
                                result = true;
                                return result;
                            case ReturnTypes.Dataset:
                                _sqlDataAdapter = new MySqlDataAdapter();
                                _sqlDataAdapter.SelectCommand = _sqlCommand;
                                _rows = new DataSet();
                                _sqlDataAdapter.Fill(_rows);
                                result = true;
                                return result;
                            case ReturnTypes.Caracter:
                                _sqlCommand.ExecuteNonQuery();
                                result = true;
                                return result;
                            default:
                                return result;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _message = $"{prefix} {ex.Message}";
                        return result;
                    }
                    catch (Exception ex2)
                    {
                        _message = "Exception. Message = " + ex2.Message;
                        return result;
                    }
                }

                _message = "Impossible to connect to Database. " + _message;
                return result;
            }
            catch (Exception ex3)
            {
                _message = ex3.Message;
                return result;
            }
        }
    }
}
