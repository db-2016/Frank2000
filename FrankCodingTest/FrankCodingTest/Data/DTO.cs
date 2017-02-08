using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace FrankCodingTest.Data
{
    public class DTO : IDisposable, IDTO
    {
        #region V A R I A B L E S 
        private readonly string _dbConnectionString = string.Empty;
        public MySqlConnection _conn;
        private bool _disposed = false;
        #endregion

        #region C O N S T R U T O R S
        public DTO(string connectionString)
        {
            _dbConnectionString = connectionString;
            _conn = DbConnect(_dbConnectionString);
        }
        #endregion

        #region M E T H O D S
        private MySqlConnection DbConnect(string connectionString)
        {
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }

            return conn;
        }

        public List<T> Select(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, _conn);
            List<T> dataset = new List<T>();

            try
            {
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            T dataRecord = new T();
                            dataRecord.ID = rdr["ID"].ToString();
                            dataRecord.Att1 = rdr["Att1"].ToString();
                            dataRecord.Att2 = rdr["Att2"].ToString();

                            dataset.Add(dataRecord);
                        }
                    }
                }
            }
            catch { return null; }

            return dataset;
        }

        public bool InsertRecord(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                cmd.CommandText = query;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@ID", "1");
                cmd.Parameters.AddWithValue("@ATT1", "Trollsen");
                cmd.Parameters.AddWithValue("@ATT2", "Trollsen");
                cmd.ExecuteNonQuery();
            }
            catch { return false; }

            return true;
        }

        public bool DeleteRecord(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                cmd.CommandText = query;
            }
            catch { return false; }

            return true;
        }

        public bool UpdateRecord(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                cmd.CommandText = query;

                cmd.ExecuteNonQuery();
            }
            catch { return false; }

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.


                    if (_conn != null)
                    {
                        _conn.Close();
                    }
                }

                // Dispose unmanaged managed resources.

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
