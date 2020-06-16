using EMS.Models;
using EMS201724112145;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace EMS.View
{
    public partial class showEmp : System.Web.UI.Page
    {
        //连接数据库字符串
        SqlConnection sqlconn;
        int isAdmin;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Write("<script>alert('请先登录！登录后才有权访问此页面！');window.location.href='Default.aspx';</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    PopulateGridView();

                    lblErrorMessage.Text = "";
                    lblSuccessMessage.Text = "";
                }

                isAdmin = int.Parse(Session["isAdmin"].ToString());
                int col = gv1.Columns.Count;

                if (isAdmin == 0)
                {
                    gv1.FooterRow.Visible = false;
                    gv1.Columns[col - 1].Visible = false;
                    gv1.Columns[2].Visible = false;
                    Panel1.Visible = false;
                }
                else
                {
                    gv1.FooterRow.Visible = true;
                    gv1.Columns[col - 1].Visible = true;
                    gv1.Columns[2].Visible = true;
                    Panel1.Visible = true;
                }
            }
        }

        /// <summary>
        /// 刷新GirdView
        /// </summary>
        private void PopulateGridView()
        {
            //连接数据库
            sqlconn = SQLHelper.Connect();

            using (EMSEntities context = new EMSEntities())
            {
                var query = from emp in context.Employee
                            join dpt in context.Department
                            on emp.DptId equals dpt.DptId
                            select new
                            {
                                emp.EmpId,
                                emp.EmpName,
                                emp.Password,
                                emp.EmpPhone,
                                emp.IsAdmin,
                                emp.DptId,
                                dpt.DptName,
                                dpt.DptAdmin
                            };
                //将结果填入GirdView中
                gv1.DataSource = query.ToList();
                gv1.DataBind();

                //如果没有数据，显示NotDataFound
                if (query.ToList() == null || query.ToList().Count <= 0)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("EmpId");
                    dataTable.Columns.Add("EmpName");
                    dataTable.Columns.Add("Password");
                    dataTable.Columns.Add("EmpPhone");
                    dataTable.Columns.Add("IsAdmin");
                    dataTable.Columns.Add("DptId");
                    dataTable.Columns.Add("DptName");
                    dataTable.Columns.Add("DptAdmin");
                    dataTable.Rows.Add(dataTable.NewRow());
                    gv1.DataSource = dataTable;
                    gv1.DataBind();
                    gv1.Rows[0].Cells.Clear();
                    gv1.Rows[0].Cells.Add(new TableCell());
                    gv1.Rows[0].Cells[0].ColumnSpan = dataTable.Columns.Count;
                    gv1.Rows[0].Cells[0].Text = "Not Data Found...";
                    gv1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }

            SQLHelper.Close(sqlconn);

            if (isAdmin == 0)
            {
                gv1.FooterRow.Visible = false;
            }
        }

        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        protected void gv1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //连接数据库
            sqlconn = SQLHelper.Connect();
            try
            {
                if (e.CommandName.Equals("add"))
                {
                    //实例化上下文对象
                    using (EMSEntities context = new EMSEntities())
                    {
                        Employee emp = new Employee
                        {
                            EmpId = int.Parse((gv1.FooterRow.FindControl("txtIdFooter") as TextBox).Text.Trim()),
                            EmpName = (gv1.FooterRow.FindControl("txtEmpNameFooter") as TextBox).Text.Trim(),
                            Password = (gv1.FooterRow.FindControl("txtPwdFooter") as TextBox).Text.Trim(),                           
                            EmpPhone = (gv1.FooterRow.FindControl("txtEmpPhoneFooter") as TextBox).Text.Trim(),
                            DptId = int.Parse((gv1.FooterRow.FindControl("txtDptIdFooter") as TextBox).Text.Trim()),
                            IsAdmin = int.Parse((gv1.FooterRow.FindControl("txtIsAdminFooter") as TextBox).Text.Trim())
                        };
                        //告诉上下文要进行添加操作
                        context.Entry<Employee>(emp).State = System.Data.Entity.EntityState.Added;
                        //执行添加操作
                        context.SaveChanges();
                    }
                    PopulateGridView();

                    lblSuccessMessage.Text = "添加成功!";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
                SQLHelper.Close(sqlconn);
            }
            SQLHelper.Close(sqlconn);
        }

        protected void gv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv1.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }

        protected void gv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv1.EditIndex = -1;
            PopulateGridView();
        }

        /// <summary>
        /// 编辑按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
                    int EmpId = int.Parse(gv1.DataKeys[e.RowIndex].Values[0].ToString());
                    Employee emp = context.Employee.Where<Employee>(empl => empl.EmpId == EmpId).FirstOrDefault<Employee>();
                    //告诉上下文要删除
                    context.Entry<Employee>(emp).State = System.Data.Entity.EntityState.Deleted;
                    //执行删除操作
                    context.SaveChanges();
                    
                    Employee empEdit = new Employee
                    {
                        EmpId = int.Parse((gv1.Rows[e.RowIndex].FindControl("txtId") as TextBox).Text.Trim()),
                        EmpName = (gv1.Rows[e.RowIndex].FindControl("txtEmpName") as TextBox).Text.Trim(),
                        Password = (gv1.Rows[e.RowIndex].FindControl("txtPwd") as TextBox).Text.Trim(),
                        EmpPhone = (gv1.Rows[e.RowIndex].FindControl("txtEmpPhone") as TextBox).Text.Trim(),                      
                        DptId = int.Parse((gv1.Rows[e.RowIndex].FindControl("txtDptId") as TextBox).Text.Trim()),
                        IsAdmin = int.Parse((gv1.Rows[e.RowIndex].FindControl("txtIsAdmin") as TextBox).Text.Trim())
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

                gv1.EditIndex = -1;
                PopulateGridView();

                lblSuccessMessage.Text = "修改成功!";
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
                SQLHelper.Close(sqlconn);
            }

            SQLHelper.Close(sqlconn);
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            sqlconn = SQLHelper.Connect();
            try
            {
                //暂时禁用外键关系
                SqlCommand sqlCmdNoCheck = new SqlCommand("alter table Department NOCHECK constraint all", sqlconn);
                SqlCommand sqlCmd2NoCheck = new SqlCommand("alter table Employee NOCHECK constraint all", sqlconn);
                sqlCmdNoCheck.ExecuteNonQuery();
                sqlCmd2NoCheck.ExecuteNonQuery();
                using (EMSEntities context = new EMSEntities())
                {
                    //找到要删除的id
                    int EmpId = int.Parse(gv1.DataKeys[e.RowIndex].Values[0].ToString());
                    Employee emp = context.Employee.Where<Employee>(empl => empl.EmpId == EmpId).FirstOrDefault<Employee>();
                    //告诉上下文要删除
                    context.Entry<Employee>(emp).State = System.Data.Entity.EntityState.Deleted;
                    //执行删除操作
                    context.SaveChanges();
                }
                //恢复外键关系
                SqlCommand sqlComdCheck = new SqlCommand("alter table Department CHECK constraint all", sqlconn);
                SqlCommand sqlComd2Check = new SqlCommand("alter table Employee CHECK constraint all", sqlconn);
                sqlComdCheck.ExecuteNonQuery();
                sqlComd2Check.ExecuteNonQuery();

                PopulateGridView();

                lblSuccessMessage.Text = "删除成功!";
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
                SQLHelper.Close(sqlconn);
            }
            SQLHelper.Close(sqlconn);
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQury_Click(object sender, EventArgs e)
        {
            //连接数据库
            sqlconn = SQLHelper.Connect();
            using (EMSEntities context = new EMSEntities())
            {
                /*try
                {*/
                    var query = from emp in context.Employee
                                join dpt in context.Department
                                on emp.DptId equals dpt.DptId into temp
                                from tt in temp.DefaultIfEmpty()
                                select new
                                {
                                    emp.EmpId,
                                    emp.EmpName,
                                    emp.Password,
                                    emp.EmpPhone,
                                    tt.DptName,
                                    emp.IsAdmin,
                                };

                    int emplId = 0;
                    /*int isDptAdmin = 0;*/
                    int isAdmin = 0;
                    if (!string.IsNullOrEmpty(txtBox1.Text))
                    {
                        emplId = int.Parse(txtBox1.Text);
                    }
                    string emplName = txtBox2.Text;
                    string dptName = txtBox3.Text;
                    string empPhone = txtBox4.Text;
                    /*if (!string.IsNullOrEmpty(txtBox5.Text))
                    {
                        isDptAdmin = int.Parse(txtBox5.Text);
                    }*/
                    if (!string.IsNullOrEmpty(txtBox6.Text))
                    {
                        isAdmin = int.Parse(txtBox6.Text);
                        if(isAdmin >= 0)
                        {
                            query = query.Where(emp => emp.IsAdmin == isAdmin);
                        }
                    }

                    /*下列代码不会立即执行查询，而是生成查询计划
                     * 若参数不存在则不添加查询条件，从而可以无限制的添加查询条件
                     */
                    if (emplId != 0) { query = query.Where(emp => emp.EmpId == emplId); }
                    if (!string.IsNullOrEmpty(emplName)) { query = query.Where(emp => emp.EmpName.Equals(emplName)); }
                    if (!string.IsNullOrEmpty(dptName)) { query = query.Where(emp => emp.DptName.Contains(dptName)); }
                    if (!string.IsNullOrEmpty(empPhone)) { query = query.Where(emp => emp.EmpPhone.Contains(empPhone)); }
                    /*if (isDptAdmin != 0)
                    {
                        var depts = from dpt in context.Department
                                    select new
                                    {
                                        dpt.DptAdmin
                                    };
                        foreach(var d in depts)
                        {
                            query = query.Where(emp => emp.EmpId == d.DptAdmin);
                        }                       
                    }*/

                    //将结果填入GridView中
                    gv1.DataSource = query.ToList();
                    gv1.DataBind();

                    //如果没有数据，显示NotDataFound
                    if (query.ToList() == null || query.ToList().Count <= 0)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("EmpId");
                        dataTable.Columns.Add("EmpName");
                        dataTable.Columns.Add("Password");
                        dataTable.Columns.Add("EmpPhone");
                        dataTable.Columns.Add("DptName");
                        dataTable.Columns.Add("IsAdmin");
                        dataTable.Rows.Add(dataTable.NewRow());
                        gv1.DataSource = dataTable;
                        gv1.DataBind();
                        gv1.Rows[0].Cells.Clear();
                        gv1.Rows[0].Cells.Add(new TableCell());
                        gv1.Rows[0].Cells[0].ColumnSpan = dataTable.Columns.Count;
                        gv1.Rows[0].Cells[0].Text = "Not Data Found...";
                        gv1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    }
                /*}
                catch (Exception ex)
                {
                    lblSuccessMessage.Text = "";
                    lblErrorMessage.Text = ex.Message;
                    SQLHelper.Close(sqlconn);
                }*/
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            txtBox1.Text = "";
            txtBox2.Text = "";
            txtBox3.Text = "";
            txtBox4.Text = "";
            txtBox5.Text = "";
            txtBox6.Text = "";
            PopulateGridView();
        }

    }
}