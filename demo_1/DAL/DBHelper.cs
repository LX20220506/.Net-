using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public static class DBHelper
    {
        static private string SqlStr = "server=.;database=GoodsDB;uid=sa;pwd=Lx141238792.";
        static private SqlConnection conn = null;

        //初始化
        static public void Init() {
            if (conn==null)
            {
                conn = new SqlConnection(SqlStr);
            }

            if (conn.State!=ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
        }

        //查询
        static public DataTable GetDataTable(string sql) {
            try
            {
                Init();
                SqlDataAdapter sda = new SqlDataAdapter(sql,conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //增删改
        static public bool GetExecuteNonQuery(string sql) {
            try
            {
                Init();
                SqlCommand comm = new SqlCommand(sql,conn);
                int i = comm.ExecuteNonQuery();
                conn.Close();
                return i > 0;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //調用存儲過程
        static public void II(string sql) {
            try
            {
                Init();
                SqlCommand comm = new SqlCommand(sql,conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("Name","tom"));
                string str= comm.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
