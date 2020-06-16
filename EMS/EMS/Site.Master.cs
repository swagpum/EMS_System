using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS201724112145.View
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Path; //获取地址栏的地址

            if (url.Contains("showEqp.aspx"))
            {
                B.Attributes.Add("class", "active");
            }
            else if (url.Contains("showDept.aspx"))
            {
                C.Attributes.Add("class", "active");
            }
            else if (url.Contains("showEmp.aspx"))
            {
                D.Attributes.Add("class", "active");
            }
            else if (url.Contains("personal.aspx"))
            {
                E.Attributes.Add("class", "active");
            }
            else if (url.Contains("logout.aspx"))
            {
                F.Attributes.Add("class", "active");
            }
            else
            {
                A.Attributes.Add("class", "active");
            }
        }
    }
}