using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services; 


namespace Demo.Code
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static DataTable GetData() {
            string connSql = "server=.;database=GoodsDB;uid=sa;pwd=Lx141238792.";
            SqlConnection conn = new SqlConnection(connSql);
            conn.Open();
            string sql = "select * from goods";
            SqlDataAdapter sda = new SqlDataAdapter(sql,conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            return dt;
        }
    }
}