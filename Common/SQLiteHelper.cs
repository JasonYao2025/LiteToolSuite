using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class SQLiteHelper
    {
        public static Dictionary<string, SQLiteHelper> DataBaseList = new Dictionary<string, SQLiteHelper>();
        public string DataSource { get; set; }

        public SQLiteHelper(string filename = null)
        {
            DataSource = filename;
        }

        public void CreateDataBase()
        {
            string path = Path.GetDirectoryName(DataSource);
            if (!string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(DataSource))
                SQLiteConnection.CreateFile(DataSource);
        }

        public SQLiteConnection GetSQLiteConnection()
        {
            string connStr = string.Format("Data Source={0}", DataSource);
            var con = new SQLiteConnection(connStr);
            return con;
        }

        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, Dictionary<string, string> data)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (data != null && data.Count >= 1)
            {
                foreach (var val in data)
                {
                    cmd.Parameters.AddWithValue(val.Key, val.Value);
                }
            }
        }

        public DataSet ExecuteDataset(string cmdText, Dictionary<string, string> data = null)
        {
            var ds = new DataSet();
            using (var connection = GetSQLiteConnection())
            {
                var command = new SQLiteCommand();
                PrepareCommand(command, connection, cmdText, data);
                var da = new SQLiteDataAdapter(command);
                da.Fill(ds);
            }
            return ds;
        }

        public DataTable ExecuteDataTable(string cmdText, Dictionary<string, string> data = null)
        {
            var dt = new DataTable();
            using (var connection = GetSQLiteConnection())
            {
                var command = new SQLiteCommand();
                PrepareCommand(command, connection, cmdText, data);
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            return dt;
        }

        public DataRow ExecuteDataRow(string cmdText, Dictionary<string, string> data = null)
        {
            DataSet ds = ExecuteDataset(cmdText, data);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0];
            return null;
        }

        public int ExecuteNonQuery(string cmdText, Dictionary<string, string> data = null)
        {
            using (var connection = GetSQLiteConnection())
            {
                var command = new SQLiteCommand();
                PrepareCommand(command, connection, cmdText, data);
                return command.ExecuteNonQuery();
            }
        }

        public SQLiteDataReader ExecuteReader(string cmdText, Dictionary<string, string> data = null)
        {
            var command = new SQLiteCommand();
            var connection = GetSQLiteConnection();
            try
            {
                PrepareCommand(command, connection, cmdText, data);
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                connection.Close();
                command.Dispose();
                throw;
            }
        }

        public object ExecuteScalar(string cmdText, Dictionary<string, string> data = null)
        {
            using (var connection = GetSQLiteConnection())
            {
                var cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, cmdText, data);
                return cmd.ExecuteScalar();
            }
        }

        public DataSet ExecutePager(ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, Dictionary<string, string> data = null)
        {
            if (recordCount < 0)
                recordCount = int.Parse(ExecuteScalar(countText, data).ToString());
            var ds = new DataSet();
            using (var connection = GetSQLiteConnection())
            {
                var command = new SQLiteCommand();
                PrepareCommand(command, connection, cmdText, data);
                var da = new SQLiteDataAdapter(command);
                da.Fill(ds, (pageIndex - 1) * pageSize, pageSize, "result");
            }
            return ds;
        }

        public void ResetDataBase()
        {
            using (var conn = GetSQLiteConnection())
            {
                var cmd = new SQLiteCommand();
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.Parameters.Clear();
                cmd.Connection = conn;
                cmd.CommandText = "vacuum";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 30;
                cmd.ExecuteNonQuery();
            }
        }



        #region 调用样例代码

        //SQLiteHelper testDb = new SQLiteHelper("test.db");
        //SQLiteHelper.DataBaseList.Add("TEST", testDb);

        //    // 创建数据库
        //    testDb.CreateDataBase();

        //    //// 创建表
        //    StringBuilder sbr = new StringBuilder();
        //sbr.AppendLine("CREATE TABLE IF NOT EXISTS `test_table`(");
        //    sbr.AppendLine("`id` INTEGER PRIMARY KEY AUTOINCREMENT,");
        //    sbr.AppendLine("`name` VARCHAR(100) NOT NULL,");
        //    sbr.AppendLine("`password` VARCHAR(40) NOT NULL,");
        //    sbr.AppendLine("`create_time` datetime DEFAULT CURRENT_TIMESTAMP,");
        //    sbr.AppendLine("`update_time` datetime DEFAULT CURRENT_TIMESTAMP );");
        //    sbr.AppendLine();
        //    sbr.AppendLine("CREATE TRIGGER IF NOT EXISTS `trigger_test_table_update_time` ");
        //    sbr.AppendLine("AFTER UPDATE ON `test_table` ");
        //    sbr.AppendLine("FOR EACH ROW ");
        //    sbr.AppendLine("BEGIN ");
        //    sbr.AppendLine("UPDATE `test_table` SET `update_time` = CURRENT_TIMESTAMP WHERE id = old.id; ");
        //    sbr.AppendLine("END;");
        //    string cmdText = sbr.ToString();
        //int val = testDb.ExecuteNonQuery(cmdText);
        ////Console.WriteLine("影响行数：" + val);

        ////// 插入数据
        //sbr.Clear();
        //    sbr.Append("INSERT INTO test_table (name,password) VALUES ");
        //    sbr.Append("(11,111), ");
        //    sbr.Append("(12,222); ");
        //    cmdText = sbr.ToString();
        //    val = testDb.ExecuteNonQuery(cmdText);
            //Console.WriteLine("影响行数：" + val);

            //// 删除数据
            //sbr.Clear();
            //sbr.Append("DELETE FROM test_table ");
            //sbr.Append("WHERE id=1;");
            //cmdText = sbr.ToString();
            //val = testDb.ExecuteNonQuery(cmdText);
            //Console.WriteLine("影响行数：" + val);

            //// 更新数据
            //sbr.Clear();
            //sbr.Append("UPDATE test_table SET ");
            //sbr.Append("name='13', ");
            //sbr.Append("password='333' ");
            //sbr.Append("WHERE id=@id;");
            //cmdText = sbr.ToString();
            //Dictionary<string, string> data = new Dictionary<string, string>();
            //data.Add("@id", "2");
            //val = testDb.ExecuteNonQuery(cmdText, data);
            //Console.WriteLine("影响行数：" + val);

            //// 查询数据
            //sbr.Clear();
            //sbr.Append("SELECT name,password FROM test_table ");
            //sbr.Append("WHERE id=@id;");
            //cmdText = sbr.ToString();
            //DataTable dt = testDb.ExecuteDataTable(cmdText, data);
            //Console.WriteLine("结果行数：" + dt.Rows.Count);

            //// 删除表
            //sbr.Clear();
            //sbr.Append("DROP TABLE test_table;");
            //cmdText = sbr.ToString();
            //val = SQLiteHelper.DataBaseList["TEST"].ExecuteNonQuery(cmdText);
            //Console.WriteLine("影响行数：" + val);

            //// 重组数据库
            //SQLiteHelper.DataBaseList["TEST"].ResetDataBase();
            //Console.ReadKey();

        #endregion
    }
}
