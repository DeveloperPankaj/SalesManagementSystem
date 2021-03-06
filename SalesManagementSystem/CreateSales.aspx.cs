﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace SalesManagementSystem
{
    public partial class CreateSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserId"] ==null)
            {
                Response.Redirect("Login.aspx");
            }

            LibraryDb.SelectCommand = "select * from users_table where role='Sales' and CreatedUserId='" + Session["UserId"].ToString() + "'";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("insert into users_table (UserName,Pwd,Role,CreatedUserId) values (@name,@pwd,@role,@lUserId)", connection);
                cmd.Parameters.AddWithValue("@name", txtStudentId.Text);
                cmd.Parameters.AddWithValue("@pwd", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@role", "Sales");
                cmd.Parameters.AddWithValue("@lUserId", Session["UserId"].ToString());

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    GridView1.DataBind();
                    txtStudentId.Text = "";
                    txtStudentName.Text = "";
                }
                catch(Exception ex)
                {

                }finally
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