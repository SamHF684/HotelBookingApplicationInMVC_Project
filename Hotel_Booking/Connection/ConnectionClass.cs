using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using Hotel_Booking;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;

namespace DB_con
{
    public enum DBTrans
    #region "enum"
    {
        Insert,
        Update
    }
    #endregion

    public class ConnectionClass : IDisposable
    {
        #region "variable"
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        SqlTransaction trans;
        #endregion

        public string ConnectionString { get; set; }


        public ConnectionClass()
        {
            //
            // TODO: Add constructor logic here
            //
            createConnection();
            setconnection();
        }

        public void closeConnection()
        {
            try
            {
                if (trans != null)
                {
                    try
                    {
                        trans.Commit();
                        trans = null;
                    }
                    catch (Exception)
                    { }
                    conn.Close();
                }
                else
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void createConnection()
        {
            try
            {


                string str = "Data Source=DESKTOP-DPKUC31\\MSSQLSERVER2022;Initial Catalog=SAM;Integrated Security=True;Trusted_Connection = SSPI; Encrypt = false; TrustServerCertificate = true";
                conn = new SqlConnection(str);
                cmd = new SqlCommand();

                cmd.CommandTimeout = 60 * 10;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void createConnection(Boolean mybool)
        {
            try
            {
                string str = ConnectionString;
                conn = new SqlConnection(str);
                cmd = new SqlCommand();
                cmd.CommandTimeout = 60 * 10;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void setconnection()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void clearParameter()
        {
            try
            {
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addParameter(string name, object value)
        {
            try
            {
                cmd.Parameters.AddWithValue(name, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addParameter(string name, long value, DB_con.DBTrans trans)
        {
            try
            {
                if (trans == DBTrans.Insert)
                {
                    cmd.Parameters.AddWithValue(name, value);
                    cmd.Parameters[name].Direction = ParameterDirection.Output;
                }
                else if (trans == DBTrans.Update)
                    cmd.Parameters.AddWithValue(name, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void addParameter(string name, string value, DB_con.DBTrans trans)
        {
            try
            {
                if (trans == DBTrans.Insert)
                {
                    cmd.Parameters.AddWithValue(name, value);
                    //cmd.Parameters[name].Direction = ParameterDirection.Output;
                }
                else if (trans == DBTrans.Update)
                    cmd.Parameters.AddWithValue(name, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getValue(string name)
        {
            object parameter = null;
            try
            {
                parameter = cmd.Parameters[name].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return parameter;
        }

        public void ExecuteNoneQuery(string commandText, CommandType commandType)
        {
            try
            {
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                setconnection();
                cmd.Connection = conn;

                //cmd.Parameters.Add("@RETVAL", SqlDbType.BigInt).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                //if (cmd.Parameters["@RETVAL"].Value.ToString() != "0" || cmd.Parameters["@RETVAL"].Value.ToString() != string.Empty)
                //{
                //    return Convert.ToUInt64(cmd.Parameters["@RETVAL"].Value.ToString());
                //}
                //else
                //{
                //    return 0;
                //}
            }
            catch (SqlException ex)
            {
                RollbackTransaction();
                if (ex.Number == 1205 || ex.Message.ToLower().Contains("was deadlocked on lock resources") || ex.Message.ToLower().Contains("the size property has an invalid size"))
                {
                    //System.Threading.Thread.Sleep(1000);
                    ExecuteNoneQuery(commandText, commandType);
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex1)
            {
                RollbackTransaction();
                if (!ex1.Message.ToLower().Contains("was deadlocked on lock resources") && ex1.Message.ToLower().Contains("the size property has an invalid size"))
                {
                    //System.Threading.Thread.Sleep(1000);
                    ExecuteNoneQuery(commandText, commandType);
                }
                else
                {
                    throw ex1;
                }
            }
        }

        public IDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            try
            {
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                setconnection();
                cmd.Connection = conn;
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                RollbackTransaction();
                if (ex.Number == 1205 || ex.Message.ToLower().Contains("was deadlocked on lock resources") || ex.Message.ToLower().Contains("the size property has an invalid size"))
                {
                    return ExecuteReader(commandText, commandType);
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex1)
            {
                RollbackTransaction();
                if (!ex1.Message.ToLower().Contains("was deadlocked on lock resources") && ex1.Message.ToLower().Contains("the size property has an invalid size"))
                {
                    //System.Threading.Thread.Sleep(1000);
                    return ExecuteReader(commandText, commandType);
                }
                else
                {
                    throw ex1;
                }
            }
        }
        public DataTable ExecuteReaderTable(string commandText, CommandType commandType)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(commandText, commandType));
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// starts Transaction
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    cmd.Transaction = trans;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void StartTransaction()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                else
                {
                    EndTransaction();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Commits Transaction
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (trans != null)
                {
                    trans.Commit();
                    conn.Close();
                    trans = null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// RollBacks Transaction
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (trans != null)
                {
                    trans.Rollback();
                    conn.Close();
                    trans = null;
                }
            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans = null;
                    conn.Close();
                }
                throw ex;
            }

        }

        public void EndTransaction()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    trans = null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public void Dispose()
        {
            closeConnection();
        }

        internal void addParameter(string v)
        {
            throw new NotImplementedException();
        }
    }
}
