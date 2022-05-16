using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;
using System.Web.Services;

namespace UI
{
    public partial class Index : System.Web.UI.Page
    {
        GoodsBLL GBLL = new GoodsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RepeaterBind();
            }
        }

        private void RepeaterBind() {
            this.Repeater1.DataSource = GBLL.GetGoodsInfo();
            this.Repeater1.DataBind();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandName);
            if (GBLL.DeleteGoodsInfo(id))
            {
                Response.Write("<script>alert('刪除成功')</script>");
            }
            else
            {
                Response.Write("<script>alert('刪除失敗')</script>");
            }
        }

        [WebMethod]
        public static string Show(int InventoryID) {
            return "1";
        }

        [WebMethod]
        public static string Page(int size,int index) {
            return "jj";
        }
    }
}