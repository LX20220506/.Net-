using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;


namespace UI
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public static int[] arr = new int[] {1,2,3,4,5 };
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static string Show(string name) {
            //return name;
            return JsonConvert.SerializeObject(name);
        }


    }
}