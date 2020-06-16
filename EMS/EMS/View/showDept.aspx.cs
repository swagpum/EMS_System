using EMS.Models;
using EMS201724112145;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace EMS.View
{
    public partial class showDept : System.Web.UI.Page
    {
        int isAdmin;
        //连接数据库字符串
        SqlConnection sqlconn;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] == null)
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
                    Panel1.Visible = false;
                }
                /*else if(Session["DptAdmin"].Equals("true"))
                {
                    gv1.FooterRow.Visible = false;
                    gv1.Columns[col - 1].Visible = false;
                    Panel1.Visible = true;
                }*/
                else
                {
                    gv1.FooterRow.Visible = true;
                    gv1.Columns[col - 1].Visible = true;
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
                var query = from dpt in context.Department
                            join emp in context.Employee
                            on dpt.DptAdmin equals emp.EmpId into temp
                            from tt in temp.DefaultIfEmpty()
                            select new
                            {
                                dpt.DptId,
                                dpt.DptName,
                                dpt.DptAdmin,
                                tt.EmpName
                            };
                //将结果填入GirdView中
                gv1.DataSource = query.ToList();
                gv1.DataBind();

                //如果没有数据，显示NotDataFound
                if (query.ToList() == null || query.ToList().Count <= 0)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("DptId");
                    dataTable.Columns.Add("DptName");
                    dataTable.Columns.Add("EmpName");
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

            if (isAdmin == 0)
            {
                gv1.FooterRow.Visible = false;
            }
            SQLHelper.Close(sqlconn);
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
                        Department dpt = new Department
                        {
                            DptId = int.Parse((gv1.FooterRow.FindControl("txtIdFooter") as TextBox).Text.Trim()),
                            DptName = (gv1.FooterRow.FindControl("txtEqpNameFooter") as TextBox).Text.Trim(),                           
                            DptAdmin = int.Parse((gv1.FooterRow.FindControl("txtDptAdminFooter") as TextBox).Text.Trim())
                        };
                        //告诉上下文要进行添加操作
                        context.Entry<Department>(dpt).State = System.Data.Entity.EntityState.Added;
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
            /*try
            {*/
                //暂时禁用外键关系
                SqlCommand sqlCmdNoCheck = new SqlCommand("alter table Department NOCHECK constraint all", sqlconn);
                SqlCommand sqlCmd2NoCheck = new SqlCommand("alter table Employee NOCHECK constraint all", sqlconn);
                sqlCmdNoCheck.ExecuteNonQuery();
                sqlCmd2NoCheck.ExecuteNonQuery();
                //实例化上下文对象
                using (EMSEntities context = new EMSEntities())
                    {
                    //先删除后新增
                    int DptId = int.Parse(gv1.DataKeys[e.RowIndex].Values[0].ToString());
                    Department eqp = context.Department.Where<Department>(dpt => dpt.DptId == DptId).FirstOrDefault<Department>();
                    //告诉上下文要删除
                    context.Entry<Department>(eqp).State = System.Data.Entity.EntityState.Deleted;
                    //执行删除操作
                    context.SaveChanges();

                    Department depart = new Department
                    {
                        DptId = int.Parse((gv1.Rows[e.RowIndex].FindControl("txtId") as TextBox).Text.Trim()),
                        DptName = (gv1.Rows[e.RowIndex].FindControl("txtEqpName") as TextBox).Text.Trim(),
                        DptAdmin = int.Parse((gv1.Rows[e.RowIndex].FindControl("txtDptAdmin") as TextBox).Text.Trim())
                    };
                    //告诉上下文要进行添加操作
                    context.Entry<Department>(depart).State = System.Data.Entity.EntityState.Added;
                    //执行添加操作
                    context.SaveChanges();
                }
                //恢复外键关系
                SqlCommand sqlComdCheck = new SqlCommand("alter table Department CHECK constraint all", sqlconn);
                sqlComdCheck.ExecuteNonQuery();
                SqlCommand sqlComd2Check = new SqlCommand("alter table Employee CHECK constraint all", sqlconn);
                sqlComd2Check.ExecuteNonQuery();

                gv1.EditIndex = -1;
                PopulateGridView();

                lblSuccessMessage.Text = "修改成功!";
                lblErrorMessage.Text = "";
            /*}
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
                SQLHelper.Close(sqlconn);
            }
            SQLHelper.Close(sqlconn);*/
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
                    int dptId = int.Parse(gv1.DataKeys[e.RowIndex].Values[0].ToString());
                    Department dpt = context.Department.Where<Department>(dept => dept.DptId == dptId).FirstOrDefault<Department>();
                    //告诉上下文要删除
                    context.Entry<Department>(dpt).State = System.Data.Entity.EntityState.Deleted;
                    //执行删除操作
                    context.SaveChanges();
                }
                //恢复外键关系
                SqlCommand sqlComdCheck = new SqlCommand("alter table Department CHECK constraint all", sqlconn);
                sqlComdCheck.ExecuteNonQuery();
                SqlCommand sqlComd2Check = new SqlCommand("alter table Employee CHECK constraint all", sqlconn);
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
                try
                {
                    var query = from dpt in context.Department
                                join emp in context.Employee
                                on dpt.DptAdmin equals emp.EmpId
                                select new
                                {
                                    dpt.DptId,
                                    dpt.DptName,
                                    emp.EmpName
                                };

                    int dptId = 0;
                    if (!string.IsNullOrEmpty(txtBox1.Text))
                    {
                        dptId = int.Parse(txtBox1.Text);
                    }
                    string dptName = txtBox2.Text;
                    string empName = txtBox3.Text;

                    /*下列代码不会立即执行查询，而是生成查询计划
                 * 若参数不存在则不添加查询条件，从而可以无限制的添加查询条件
                 */
                    if (dptId != 0) { query = query.Where(dpt => dpt.DptId == dptId); }
                    if (!string.IsNullOrEmpty(dptName)) { query = query.Where(dpt => dpt.DptName.Equals(dptName)); }                   
                    if (!string.IsNullOrEmpty(empName)) { query = query.Where(dpt => dpt.EmpName.Equals(empName)); }

                    //将结果填入GridView中
                    gv1.DataSource = query.ToList();
                    gv1.DataBind();

                    //如果没有数据，显示NotDataFound
                    if (query.ToList() == null || query.ToList().Count <= 0)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("DptId");
                        dataTable.Columns.Add("DptName");
                        dataTable.Columns.Add("EmpName");
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
                catch (Exception ex)
                {
                    lblSuccessMessage.Text = "";
                    lblErrorMessage.Text = ex.Message;
                    SQLHelper.Close(sqlconn);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            txtBox1.Text = "";
            txtBox2.Text = "";
            txtBox3.Text = "";
            PopulateGridView();
        }


        /*protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Attributes["style"] = "width:50px;"; //设置第一列宽度
        }*/

    }
}