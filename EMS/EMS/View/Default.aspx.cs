using EMS.Models;
using EMS201724112145;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace EMS.View
{
    public partial class Default : System.Web.UI.Page
    {
        public int uname;
        public string pwd;
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                username.Text = Request.Cookies["username"].Value.ToString();
                password.Text = Request.Cookies["password"].Value.ToString();//将键名为ID的Cookie的值读出，并在文本框TextBox2中显示出来 
            }*/
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["username"] = null;
            Session["DptAdmin"] = null;

            /*if (CheckBox1.Checked)
            {
                Response.Cookies["username"].Expires = DateTime.Now.AddDays(3);
                Response.Cookies["password"].Expires = DateTime.Now.AddDays(3);
                Response.Cookies["username"].Value = username.Text;
                Response.Cookies["password"].Value = password.Text;
                
            }*/

            if (string.IsNullOrEmpty(this.username.Text))
            {
                Response.Write("<script>alert('用户名不能为空！')</script>");
                return;
            }
            if(string.IsNullOrEmpty(this.password.Text))
            {
                Response.Write("<script>alert('密码不能为空！')</script>");
                return;
            }
            //连接数据库
            SqlConnection sqlconn = SQLHelper.Connect();
            uname = int.Parse(this.username.Text);
            pwd = this.password.Text;

            using (EMSEntities context = new EMSEntities())
            {
                try
                {
                    var query = from emp in context.Employee
                                select new
                                {
                                    emp.EmpId,
                                    emp.EmpName,
                                    emp.Password,
                                    emp.IsAdmin
                                };
                    var query2 = from dpt in context.Department
                                 select new
                                 {
                                     dpt.DptAdmin
                                 };
                    if(query != null)
                    {
                        foreach (var q in query)
                        {
                            if (q.EmpId == uname && q.Password.Equals(pwd))
                            {
                                Session["EmpId"] = q.EmpId;
                                Session["username"] = q.EmpName;
                                Session["password"] = q.Password;
                                Session["isAdmin"] = q.IsAdmin;

                                /*Response.Write(Session["username"]);*/
                                Response.Redirect("home.aspx");                          
                            }
                            else
                            {
                                Response.Write("<script>alert('用户名或密码错误！');window.location.href='Default.aspx';</script>");
                            }
                        }
                    }                 
                }
                catch(Exception ex)
                {
                    Response.Write(ex);
                }
            }
            SQLHelper.Close(sqlconn);
        }
    }
}