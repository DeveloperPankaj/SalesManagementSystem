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
    public partial class OppType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Userid"] == null)
            {
                Response.Redirect("Login.aspx");
            }

           LibraryDb.SelectCommand = "SELECT * FROM [OppType_Table] where [CreatedByUserId]='" + Session["UserId"].ToString() + "'";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
                {
                    string query = "INSERT INTO [OppType_Table] (Name,CreatedbyUserId) VALUES (@name,@CreatedByUser)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtOpportunity.Text);
                    cmd.Parameters.AddWithValue("@CreatedByUser", Session["UserId"].ToString());
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        GridView1.DataBind();
                        txtOpportunity.Text = "";
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}