/*++
DB Helper to connect mysql and some function to inert/update/delete data

Copyright © 2022  VIA Technologies, Inc. All Rights Reserved.

This PROPRIETARY SOFTWARE is the property of VIA Technologies, Inc. and may contain trade secrets and/or 
other confidential information of VIA Technologies, Inc. This file shall not be disclosed to any third party, 
in whole or in part, without prior written consent of VIA. 

THIS PROPRIETARY SOFTWARE AND ANY RELATED DOCUMENTATION ARE PROVIDED AS IS, WITH ALL FAULTS, AND WITHOUT 
WARRANTY OF ANY KIND EITHER EXPRESS OR IMPLIED, AND VIA TECHNOLOGIES, INC. DISCLAIMS ALL EXPRESS OR IMPLIED
WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, QUIET ENJOYMENT OR NON-INFRINGEMENT. 
--*/

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// MySqlHelper Operation
    /// </summary>
    public sealed partial class MySQLHelper
    {   
        /// <summary>
        /// batch size
        /// </summary>
        public static int BatchSize = 2000;

        /// <summary>
        /// time out
        /// </summary>
        public static int CommandTimeOut = 600;

        /// <summary>
        ///initialize MySqlHelper object
        /// </summary>
        /// <param name="connectionString">connection string</param>
        public MySQLHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// db connection string
        /// </summary>
        public string ConnectionString { get; set; }
              

        #region ExecuteDataSet

        /// <summary>
        /// execute SQL statement and return dataset
        /// </summary>
        /// <param name="commandText">SQL statement</param>
        /// <param name="parms">query parameter</param>
        /// <returns>return dataset</returns>
        public DataSet ExecuteDataSet(string commandText, params MySqlParameter[] parms)
        {
            return ExecuteDataSet(ConnectionString, CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// execute SQL statement and return dataset
        /// </summary>
        /// <param name="commandType">SQL command type</param>
        /// <param name="commandText">SQL statement</param>
        /// <param name="parms">query parameter</param>
        /// <returns>return dataset</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            return ExecuteDataSet(ConnectionString, commandType, commandText, parms);
        }

        #endregion ExecuteDataSet
   
        private static void PrepareCommand(MySqlCommand command, MySqlConnection connection, MySqlTransaction transaction, CommandType commandType, string commandText, MySqlParameter[] parms)
        {
            if (connection.State != ConnectionState.Open) connection.Open();

            command.Connection = connection;
            command.CommandTimeout = CommandTimeOut;
            // set command text (SQL statement or Stored Procedure)
            command.CommandText = commandText;
            // assign transaction
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            // set command type.
            command.CommandType = commandType;
            if (parms != null && parms.Length > 0)
            {
                //preproccess for MySqlParameter array, assign DBNull.Value to the null parameter;
                foreach (MySqlParameter parameter in parms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                command.Parameters.AddRange(parms);
            }
        }

        #region ExecuteDataSet static method

        /// <summary>
        /// execute SQL statement and return dataset
        /// </summary>
        ///  /// <param name="connectionString">db connection string</param>
        /// <param name="commandType">SQL command type</param>
        /// <param name="commandText">SQL statement</param>
        /// <param name="parms">query parameter</param>
        /// <returns>return dataset</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                return ExecuteDataSet(connection, commandType, commandText, parms);
            }
        }

        /// <summary>
        /// execute SQL statement and return dataset
        /// </summary>
        ///  /// <param name="connectionString">db connection string</param>
        /// <param name="commandType">SQL command type</param>
        /// <param name="commandText">SQL statement</param>
        /// <param name="parms">query parameter</param>
        /// <returns>return dataset</returns>
        public static DataSet ExecuteDataSet(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            return ExecuteDataSet(connection, null, commandType, commandText, parms);
        }

        
        /// <summary>
        /// execute SQL statement and return dataset
        /// </summary>
        ///  /// <param name="connectionString">db connection string</param>
        ///  /// <param name="transaction">transaction</param>
        /// <param name="commandType">SQL command type</param>
        /// <param name="commandText">SQL statement</param>
        /// <param name="parms">query parameter</param>
        /// <returns>return dataset</returns>
        private static DataSet ExecuteDataSet(MySqlConnection connection, MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            MySqlCommand command = new MySqlCommand();

            PrepareCommand(command, connection, transaction, commandType, commandText, parms);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            if (commandText.IndexOf("@") > 0)
            {
                commandText = commandText.ToLower();
                int index = commandText.IndexOf("where ");
                if (index < 0)
                {
                    index = commandText.IndexOf("\nwhere");
                }
                if (index > 0)
                {
                    //store SQL statement for CommandBuilder
                    ds.ExtendedProperties.Add("SQL", commandText.Substring(0, index - 1)); 
                }
                else
                {
                    ds.ExtendedProperties.Add("SQL", commandText); 
                }
            }
            else
            {
                ds.ExtendedProperties.Add("SQL", commandText);  
            }

            foreach (DataTable dt in ds.Tables)
            {
                dt.ExtendedProperties.Add("SQL", ds.ExtendedProperties["SQL"]);
            }

            command.Parameters.Clear();
            return ds;
        }

        #endregion ExecuteDataSet static method
    }

}
