using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;

namespace UI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private GoodsTypeBLL GTBLL = new GoodsTypeBLL();
        private GoodsBLL GBLL = new GoodsBLL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropBind();
            }
        }

        public void DropBind() {
            this.DropDownList1.DataSource = GTBLL.GetGoodsInfo();
            this.DropDownList1.DataTextField = "TypeName";
            this.DropDownList1.DataValueField = "TypeID";
            this.DropDownList1.DataBind();
        }

        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            string name = this.name.Value;
            decimal price = decimal.Parse(this.price.Value);
            int typeid = int.Parse(this.DropDownList1.SelectedValue);
            int KuCun = int.Parse(this.kuncun.Value);
            Goods goods = new Goods()
            {
                GoodsName = name,
                Price = price,
                KunCun = KuCun,
                TypeID = typeid
            };

            if (GBLL.AddGoodsInfo(goods))
            {
                Response.Write("<script>alert('添加成功');location.href='index.aspx'</script>");
            }
            else
            {
                Response.Write("<script>alert('添加失敗');</script>");
            }
        }
    }
}