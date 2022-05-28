using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formLogin_Register
{
    class DataAccess
    {
        string connectStr = @"Data Source=KHANHHI\SQLKHANH_2801;Initial Catalog=QLY_VLXD;Integrated Security=True";
        SqlConnection sqlConn = null;

        void OpenConnect()
        {
            sqlConn = new SqlConnection(connectStr);
            if (sqlConn.State != ConnectionState.Open)
                sqlConn.Open();
        }

        void CloseConnect()
        {
            if (sqlConn.State != ConnectionState.Closed)
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }
        }

        public void UpdateData(string sql)
        {
            OpenConnect();
            SqlCommand sqlComand = new SqlCommand();
            sqlComand.Connection = sqlConn;
            sqlComand.CommandText = sql;

            sqlComand.ExecuteNonQuery();
            CloseConnect();
            sqlComand.Dispose();
        }

        public DataTable DataReader(string sqlSelect)
        {
            DataTable dtTable = new DataTable();
            OpenConnect();
            SqlDataAdapter sqlData = new SqlDataAdapter(sqlSelect, sqlConn);
            sqlData.Fill(dtTable);
            CloseConnect();
            sqlData.Dispose();
            return dtTable;
        }
    }
}
