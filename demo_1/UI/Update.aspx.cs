using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;
using System.Data;

namespace UI
{
    public partial class Update : System.Web.UI.Page
    {
        GoodsBLL GBLL = new GoodsBLL();
        GoodsTypeBLL GTBLL = new GoodsTypeBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropBind();
                Init();
            }
        }

        private void DropBind() {
            this.DropDownList1.DataSource = GTBLL.GetGoodsInfo();
            this.DropDownList1.DataTextField = "TypeName";
            this.DropDownList1.DataValueField = "TypeID";
            this.DropDownList1.DataBind();
        }

        public void Init() {
            var dd = Request["id"];
            if (Request["id"]==null)
            {
                Response.Write("<script>alert('您不能直接訪問該網頁');location.href='index.aspx'</script>");
                return;
            }
            int id=int.Parse(Request["id"]);
            DataTable dt = GBLL.GetGoodsInfoByID(id);
            this.id.Value = dt.Rows[0]["ID"].ToString();
            this.name.Value = dt.Rows[0]["Name"].ToString();
            this.price.Value = dt.Rows[0]["Price"].ToString();
            this.kuncun.Value = dt.Rows[0]["KuCun"].ToString();
            this.DropDownList1.SelectedValue = dt.Rows[0]["TypeID"].ToString();
        }


        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            Goods goods = new Goods()
            {
                GoodsName = this.name.Value,
                ID = int.Parse(this.id.Value),
                Price = decimal.Parse(this.price.Value),
                TypeID = int.Parse(this.DropDownList1.DataValueField),
                KunCun = int.Parse(this.kuncun.Value)
            };

            if (GBLL.UpdateGoodsInfo(goods))
            {
                Response.Write("<script>alert('修改成功');location.href='index.aspx';</script>");
               
            }
            else
            {
                Response.Write("<script>alert('修改失敗')</script>");
            }
        }
    }
}