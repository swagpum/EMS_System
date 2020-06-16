using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.View
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Write("<script>alert('请先登录！登录后才有权访问此页面！');window.location.href='Default.aspx';</script>");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
            
        }
    }
}