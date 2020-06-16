using EMS.Models;
using EMS201724112145;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace EMS.View
{
    public partial class personal : System.Web.UI.Page
    {
        //连接数据库字符串
        SqlConnection sqlconn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Write("<script>alert('请先登录！登录后才有权访问此页面！');window.location.href='Default.aspx';</script>");
            }
            else
            {
                Label1.Visible = false;
                int id = int.Parse(Session["EmpId"].ToString());

                //连接数据库
                sqlconn = SQLHelper.Connect();

                using (EMSEntities context = new EMSEntities())
                {
                    var query = from emp in context.Employee
                                join dpt in context.Department
                                on emp.DptId equals dpt.DptId
                                where emp.EmpId == id
                                select new
                                {
                                    emp.EmpId,
                                    emp.EmpName,
                                    emp.Password,
                                    emp.EmpPhone,
                                    emp.DptId,
                                    emp.IsAdmin,
                                    dpt.DptName,
                                };
                    if (query != null)
                    {
                        foreach (var q in query)
                        {
                            if (q.IsAdmin == 0) { TextBox1.ReadOnly = true; TextBox5.ReadOnly = true; }
                            if (TextBox1.Text.Equals(""))
                            {
                                TextBox1.Text = q.EmpId.ToString();
                                TextBox2.Text = q.EmpName.ToString();
                                TextBox3.Text = q.Password.ToString();
                                TextBox4.Text = q.EmpPhone.ToString();
                                TextBox5.Text = q.DptName.ToString();
                                TextBox6.Text = q.DptId.ToString();
                                TextBox7.Text = q.IsAdmin.ToString();
                            }
                        }
                    }
                }
                SQLHelper.Close(sqlconn);
            }            
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            sqlconn = SQLHelper.Connect();
            try
            {
                //暂时禁用外键关系
                SqlCommand sqlCmdNoCheck = new SqlCommand("alter table Department NOCHECK constraint all", sqlconn);
                SqlCommand sqlCmd2NoCheck = new SqlCommand("alter table Employee NOCHECK constraint all", sqlconn);
                sqlCmdNoCheck.ExecuteNonQuery();
                sqlCmd2NoCheck.ExecuteNonQuery();
                //实例化上下文对象
                using (EMSEntities context = new EMSEntities())
                {
                    //先删除后新增
                    int EmpId = int.Parse(TextBox1.Text.ToString());
                    Employee emp = context.Employee.Where<Employee>(empl => empl.EmpId == EmpId).FirstOrDefault<Employee>();
                    //告诉上下文要删除
                    context.Entry<Employee>(emp).State = System.Data.Entity.EntityState.Deleted;
                    //执行删除操作
                    context.SaveChanges();

                    Employee empEdit = new Employee
                    {
                        EmpId = int.Parse(TextBox1.Text.Trim()),
                        EmpName = TextBox2.Text.Trim(),
                        Password = TextBox3.Text.Trim(),
                        EmpPhone = TextBox4.Text.Trim(),
                        DptId = int.Parse(TextBox6.Text.Trim()),
                        IsAdmin = int.Parse(TextBox7.Text.Trim())
                    };
                    //告诉上下文要进行添加操作
                    context.Entry<Employee>(empEdit).State = System.Data.Entity.EntityState.Added;
                    //执行添加操作
                    context.SaveChanges();
                }
                //恢复外键关系
                SqlCommand sqlComdCheck = new SqlCommand("alter table Department CHECK constraint all", sqlconn);
                SqlCommand sqlComd2Check = new SqlCommand("alter table Employee CHECK constraint all", sqlconn);
                sqlComdCheck.ExecuteNonQuery();
                sqlComd2Check.ExecuteNonQuery();

                Label1.Visible = true;
                Label1.Text = "修改成功！";
            }
            catch (Exception ex)
            {
                Label1.Visible = true;
                Label1.Text = ex.Message;
                SQLHelper.Close(sqlconn);
            }

            SQLHelper.Close(sqlconn);
            
        }
    }
}