using EMS.Models;
using EMS201724112145;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace EMS.View
{
    public partial class showEqp : System.Web.UI.Page
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
                }
                else
                {
                    gv1.FooterRow.Visible = true;
                    gv1.Columns[col - 1].Visible = true;
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
                var query = from eqp in context.Equipment
                            join emp in context.Employee
                            on eqp.EmpId equals emp.EmpId
                            select new
                            {
                                eqp.EqpId,
                                eqp.EqpName,
                                eqp.EqpSpecification,
                                eqp.EqpImg,
                                eqp.EqpPrice,
                                eqp.DateOfPurchase,
                                eqp.Position,
                                emp.EmpName,
                                emp.EmpId
                            };
                //将结果填入GirdView中
                gv1.DataSource = query.ToList();
                gv1.DataBind();

                //如果没有数据，显示NotDataFound
                if (query.ToList() == null || query.ToList().Count <= 0)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("EqpId");
                    dataTable.Columns.Add("EqpName");
                    dataTable.Columns.Add("EqpSpecification");
                    dataTable.Columns.Add("EqpImg");
                    dataTable.Columns.Add("EqpPrice");
                    dataTable.Columns.Add("DateOfPurchase");
                    dataTable.Columns.Add("Position");
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
                        bool bok = false; //默认为false
                        FileUpload fileUp = gv1.FooterRow.FindControl("imgEqpFooter") as FileUpload;
                        string path = Server.MapPath("~/UploadImg");//储存文件夹路径
                        string fileName = fileUp.FileName;//获取文件名
                        if (fileUp.HasFile)//检测是否有上传文件
                        {
                            string file = fileName.Substring(fileName.LastIndexOf(".")).ToLower(); //获取文件夹下的文件路径
                            string[] allow = new string[] { ".png", ".jpg", ".gif", ".bmp", ".jpeg" }; //图片的后缀名数组
                            foreach(string s in allow)
                            {
                                if(s == file)
                                {
                                    bok = true;
                                }
                            }
                            //修改文件名
                            fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + fileUp.FileName;
                            if (bok)
                            {
                                try
                                {
                                    fileUp.PostedFile.SaveAs(path + "/" + fileName); //上传文件
                                    lblSuccessMessage.Text = "文件" + fileName + "上传成功！";
                                }catch(Exception ex)
                                {
                                    lblErrorMessage.Text = "文件上传失败！" + ex.Message;
                                }
                            }
                            else
                            {
                                lblErrorMessage.Text = "上传的图片格式不正确：只能上传.png,jpg,.gif,.bmp,.jpeg";
                            }
                        }
                        string URL = "../UploadImg/" + fileName; //文件路径名

                        Equipment eqp = new Equipment
                        {
                            EqpId = int.Parse((gv1.FooterRow.FindControl("txtIdFooter") as TextBox).Text.Trim()),
                            EqpName = (gv1.FooterRow.FindControl("txtEqpNameFooter") as TextBox).Text.Trim(),
                            EqpSpecification = (gv1.FooterRow.FindControl("txtEqpSpeFooter") as TextBox).Text.Trim(),
                            EqpImg = URL,
                            EqpPrice = float.Parse((gv1.FooterRow.FindControl("txtEqpPriceFooter") as TextBox).Text.Trim()),
                            DateOfPurchase = DateTime.Parse((gv1.FooterRow.FindControl("txtDateOfPurFooter") as TextBox).Text.Trim()),
                            Position = (gv1.FooterRow.FindControl("txtPositionFooter") as TextBox).Text.Trim(),
                            EmpId = int.Parse((gv1.FooterRow.FindControl("txtEmpIdFooter") as TextBox).Text.Trim())
                        };
                        //告诉上下文要进行添加操作
                        context.Entry<Equipment>(eqp).State = System.Data.Entity.EntityState.Added;
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
                SqlCommand sqlCmdNoCheck = new SqlCommand("alter table Equipment NOCHECK constraint all", sqlconn);
                sqlCmdNoCheck.ExecuteNonQuery();
                //实例化上下文对象
                using(EMSEntities context = new EMSEntities())
                {
                    //先删除后新增
                    int EqpId = int.Parse(gv1.DataKeys[e.RowIndex].Values[0].ToString());
                    Equipment eqp = context.Equipment.Where<Equipment>(equip => equip.EqpId == EqpId).FirstOrDefault<Equipment>();
                    //告诉上下文要删除
                    context.Entry<Equipment>(eqp).State = System.Data.Entity.EntityState.Deleted;
                    //执行删除操作
                    context.SaveChanges();

                    string URL;
                    if((gv1.Rows[e.RowIndex].FindControl("imgEqp") as FileUpload).HasFile)
                    {
                        bool bok = false; //默认为false
                        FileUpload fileUp = gv1.Rows[e.RowIndex].FindControl("imgEqp") as FileUpload;
                        string path = Server.MapPath("~/UploadImg");//获取服务器文件夹路径
                        string fileName = fileUp.FileName;//获取文件名
                        if (fileUp.HasFile)//检测是否有上传文件
                        {
                            string file = fileName.Substring(fileName.LastIndexOf(".")).ToLower(); //获取文件夹下的文件路径
                            string[] allow = new string[] { ".png", ".jpg", ".gif", ".bmp", ".jpeg" }; //图片的后缀名数组
                            foreach (string s in allow)
                            {
                                if (s == file)
                                {
                                    bok = true;
                                }
                            }
                            //修改文件名
                            fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + fileUp.FileName;
                            if (bok)
                            {
                                try
                                {
                                    fileUp.PostedFile.SaveAs(path + "/" + fileName); //上传文件
                                    lblSuccessMessage.Text = "文件" + fileName + "上传成功！";
                                }
                                catch (Exception ex)
                                {
                                    lblErrorMessage.Text = "文件上传失败！" + ex.Message;
                                }
                            }
                            else
                            {
                                lblErrorMessage.Text = "上传的图片格式不正确：只能上传.png,jpg,.gif,.bmp,.jpeg";
                            }
                        }
                        URL = "../UploadImg/" + fileName; //文件路径名
                    }
                    else
                    {
                        URL = (gv1.Rows[e.RowIndex].FindControl("showEqpImg") as Image).ImageUrl;
                    }                 

                    Equipment eqpEdit = new Equipment
                    {
                        EqpId = int.Parse((gv1.Rows[e.RowIndex].FindControl("txtId") as TextBox).Text.Trim()),
                        EqpName = (gv1.Rows[e.RowIndex].FindControl("txtEqpName") as TextBox).Text.Trim(),
                        EqpSpecification = (gv1.Rows[e.RowIndex].FindControl("txtEqpSpe") as TextBox).Text.Trim(),
                        EqpImg = URL,
                        EqpPrice = float.Parse((gv1.Rows[e.RowIndex].FindControl("txtEqpPrice") as TextBox).Text.Trim()),
                        DateOfPurchase = DateTime.Parse((gv1.Rows[e.RowIndex].FindControl("txtDateOfPurchase") as TextBox).Text.Trim()),
                        Position = (gv1.Rows[e.RowIndex].FindControl("txtPosition") as TextBox).Text.Trim(),
                        EmpId = int.Parse((gv1.Rows[e.RowIndex].FindControl("txtEmpId") as TextBox).Text.Trim())
                    };
                    //告诉上下文要进行添加操作
                    context.Entry<Equipment>(eqpEdit).State = System.Data.Entity.EntityState.Added;
                    //执行添加操作
                    context.SaveChanges();
                }
                //恢复外键关系
                SqlCommand sqlComdCheck = new SqlCommand("alter table Equipment CHECK constraint all", sqlconn);
                sqlComdCheck.ExecuteNonQuery();

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
                SqlCommand sqlCmdNoCheck = new SqlCommand("alter table Equipment NOCHECK constraint all", sqlconn);
                sqlCmdNoCheck.ExecuteNonQuery();
                using (EMSEntities context = new EMSEntities()) {
                    //找到要删除的id
                    int EqpId = int.Parse(gv1.DataKeys[e.RowIndex].Values[0].ToString());
                    Equipment eqp = context.Equipment.Where<Equipment>(equip => equip.EqpId == EqpId).FirstOrDefault<Equipment>();
                    //告诉上下文要删除
                    context.Entry<Equipment>(eqp).State = System.Data.Entity.EntityState.Deleted;
                    //执行删除操作
                    context.SaveChanges();
                }
                //恢复外键关系
                SqlCommand sqlComdCheck = new SqlCommand("alter table Equipment CHECK constraint all", sqlconn);
                sqlComdCheck.ExecuteNonQuery();

                PopulateGridView();

                lblSuccessMessage.Text = "删除成功!";
                lblErrorMessage.Text = "";
            }
            catch(Exception ex)
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
            using(EMSEntities context = new EMSEntities())
            {
                try
                {
                    var query = from eqp in context.Equipment
                            join emp in context.Employee
                            on eqp.EmpId equals emp.EmpId
                            select new
                            {
                                eqp.EqpId,
                                eqp.EqpName,
                                eqp.EqpSpecification,
                                eqp.EqpImg,
                                eqp.EqpPrice,
                                eqp.DateOfPurchase,
                                eqp.Position,
                                emp.EmpName,
                                emp.DptId
                            };

                    int equipId = 0;
                    string dateT = null;
                    if (!string.IsNullOrEmpty(txtBox1.Text))
                    {
                        equipId = int.Parse(txtBox1.Text);
                    }
                    string equipName = txtBox2.Text;
                    if (!string.IsNullOrEmpty(txtBox3.Text))
                    {
                        if (txtBox3.Text.Length > 4)
                        {
                            DateTime dateTime = DateTime.Parse(txtBox3.Text);
                            dateT = dateTime.Year.ToString();
                        }
                        else
                        {
                            dateT = txtBox3.Text;
                        }                       
                    }                                    
                    string posit = txtBox4.Text;
                    string emplName = txtBox5.Text;
                    string deptName = txtBox6.Text;

                    /*下列代码不会立即执行查询，而是生成查询计划
                 * 若参数不存在则不添加查询条件，从而可以无限制的添加查询条件
                 */
                    if (equipId != 0) { query = query.Where(eqp => eqp.EqpId == equipId); }
                    if (!string.IsNullOrEmpty(equipName)) { query = query.Where(eqp => eqp.EqpName.Contains(equipName)); }
                    if (!string.IsNullOrEmpty(dateT)) { query = query.Where(eqp => eqp.DateOfPurchase.ToString().Contains(dateT)); }
                    if (!string.IsNullOrEmpty(posit)) { query = query.Where(eqp => eqp.Position.Contains(posit)); }
                    if (!string.IsNullOrEmpty(emplName)) { query = query.Where(eqp => eqp.EmpName.Equals(emplName)); }
                    if (!string.IsNullOrEmpty(deptName))
                    {
                        Department dept = context.Department.Where<Department>(dep => dep.DptName.Equals(deptName)).FirstOrDefault<Department>();
                        query = query.Where(eqp => eqp.DptId == dept.DptId);
                    }

                    //将结果填入GridView中
                    gv1.DataSource = query.ToList();
                    gv1.DataBind();

                    //如果没有数据，显示NotDataFound
                    if (query.ToList() == null || query.ToList().Count <= 0)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("EqpId");
                        dataTable.Columns.Add("EqpName");
                        dataTable.Columns.Add("EqpSpecification");
                        dataTable.Columns.Add("EqpImg");
                        dataTable.Columns.Add("EqpPrice");
                        dataTable.Columns.Add("DateOfPurchase");
                        dataTable.Columns.Add("Position");
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
            txtBox4.Text = "";
            txtBox5.Text = "";
            txtBox6.Text = "";
            PopulateGridView();
        }


        /*protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Attributes["style"] = "width:50px;"; //设置第一列宽度
        }*/
            }
        }