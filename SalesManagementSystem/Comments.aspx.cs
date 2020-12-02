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
    public partial class Comments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Userid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            LibraryDb.SelectCommand = "Select Id,Message,convert(nvarchar, Datetime, 121) as Datetime,CreatedUserId from [Announcement_table] where [CreatedUserId]='"+Session["UserId"].ToString()+"'";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("insert into Announcement_table (Message,datetime,CreatedUserId) values (@name,@datetime,@lUserId)", connection);
                cmd.Parameters.AddWithValue("@name", txtStudentId.Text);
                cmd.Parameters.AddWithValue("@datetime", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@lUserId", Session["UserId"].ToString());

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    GridView1.DataBind();
                    txtStudentId.Text = "";
                    txtStudentName.Text = "";
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}