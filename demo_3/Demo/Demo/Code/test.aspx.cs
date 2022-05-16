using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;

namespace Demo.Code
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.TextBox1.Text = "11111";
            Show();
            this.TextBox1.Text = Fun();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Response.Write("<script>alert(111);</script>");
        }

        public void Show() {
            Response.Write("<script>alert('"+Fun()+ "');</script>");
        }

        public string Fun() {
            return "58.88%";
        }

        [WebMethod]
        public static string ffff(string name, string age) {
            return JsonConvert.SerializeObject("name:" + name + " age:" + age);
        }

    }
}