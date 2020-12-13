using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SalesManagementSystem
{
    public partial class Proposals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserId"] == null)
            {
                Response.Redirect("login.aspx");
            }

           LibraryDb.SelectCommand = "SELECT * FROM [Proposal_Table] WHERE CreatedUserId='"+Session["UserId"].ToString()+"'";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int i=0;
                string query = string.Empty;
                using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
                {
                     query = "INSERT INTO Proposal_Table (Name,CreatedUserId,timestamp,ChanceToClose,EBudget,Duration," +
                        "ContactName,ContactMobile,Description,Notes,Amount,Revenue,status,RejectionReason) VALUES (@Name," +
                        "@CreatedUserId,@timestamp,@ChanceToClose,@EBudget,@Duration,@ContactName,@ContactMobile,@Description," +
                        "@Notes,@Amount,@Revenue,@status,@RejectionReason)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@CreatedUserId", Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@ChanceToClose", txtChance.Text);
                    cmd.Parameters.AddWithValue("@EBudget", txtBudget.Text);
                    cmd.Parameters.AddWithValue("@Duration", txtDuration.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);                    cmd.Parameters.AddWithValue("@ContactName", txtContactName.Text);
                    cmd.Parameters.AddWithValue("@ContactMobile", txtContactNumber.Text);

                    cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@status", chkStatus.Checked);
                    cmd.Parameters.AddWithValue("@Revenue", txtRevenue.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@RejectionReason", txtReason.Text);
                    try
                    {
                        con.Open();
                        i =  cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch(Exception ex)
                    {
                        string msg = ex.Message;    
                    }

                    if(i>0 && chkStatus.Checked)
                    {
                        query = "INSERT INTO dbo.[Project_Table]([ProjectName],[ProjectManager],[timestamp],[Amount],[Revenue]," +
                            "[Description],[status],[LaunchDate],[Duration],[ContactName],[ContactMobile],[Notes]) Values (@ProjectName," +
                            "@ProjectManager,@timestamp,@Amount,@Revenue,@Description,@status,@LaunchDate,@Duration,@ContactName,@ContactMobile,@Notes)";
                        cmd = new SqlCommand(query, con);

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ProjectName", txtName.Text);
                        cmd.Parameters.AddWithValue("@ProjectManager", Session["UserId"].ToString());
                        cmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                        cmd.Parameters.AddWithValue("@status", chkStatus.Checked);
                        cmd.Parameters.AddWithValue("@Revenue", txtRevenue.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@Duration", txtDuration.Text);
                        cmd.Parameters.AddWithValue("@ContactName", txtContactName.Text);
                        cmd.Parameters.AddWithValue("@ContactMobile", txtContactNumber.Text);
                        cmd.Parameters.AddWithValue("@LaunchDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                        try
                        {
                            con.Open();
                            i = cmd.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        finally
                        {
                            con.Close();
                        }
                        GridView1.DataBind();
                        txtName.Text = "";
                        txtChance.Text = "";
                        txtBudget.Text = "";
                        txtAmount.Text = "";
                        txtDuration.Text = "";
                        txtRevenue.Text = "";
                        txtContactName.Text = "";
                        txtContactNumber.Text = "";
                        txtDescription.Text = "";
                        txtNotes.Text = "";
                        txtReason.Text = "";
                        chkStatus.Checked = false;
                    }
                }
            }catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}