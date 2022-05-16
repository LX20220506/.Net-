using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dmeo.Model;

namespace dmeo
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (GoodsDBEntities db=new GoodsDBEntities())
            {
                var na="";
                int id = 11;
                List<GoodsType> list= db.GoodsType.Where(g=>g.TypeID==id).ToList();
                db.Goods.Add(new Goods());
                db.SaveChanges();
            }
        }
    }
}