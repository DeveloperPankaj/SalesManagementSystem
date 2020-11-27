using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace SalesManagementSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Request.Cookies.Remove("user");
                Session.RemoveAll();
            }catch(Exception ex)
            {
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if(inputEmail.Value.ToUpper() == "ADMIN" && inputPassword.Value.ToUpper() =="ADMIN@123")
            {
                Session["userid"] = "1";
                Session["role"] = "Admin";
                Session["username"] = "Admin";
                Session["IsAuth"] = "true";
                Response.Redirect("Home.aspx");
            }
            else
            {
                LoginDetails log = ValidateUser(inputEmail.Value, inputPassword.Value);
                if(log.IsAuthUser)
                {
                    Session["userid"] = log.UserId;
                    Session["role"] = log.Role;
                    Session["username"] = log.UserName;
                    Session["IsAuth"] = log.IsAuthUser;
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private LoginDetails ValidateUser(string username, string password)
        {
            LoginDetails obj = new LoginDetails();
            obj.IsAuthUser = false;

            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
                SqlDataAdapter da;
                DataSet ds = new DataSet();
                string qry = "select * from users_table where username='" + username.Trim() + "' and pwd='" + password.Trim() + "'";
                da = new SqlDataAdapter(qry, connection);
                connection.Open();
                da.Fill(ds);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    obj.IsAuthUser = true;
                    obj.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                    obj.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                    obj.Role = ds.Tables[0].Rows[0]["Role"].ToString();
                }

            }catch(Exception ex)
            {
                obj.IsAuthUser = false;
            }
            return obj;
        }
    }

    public struct LoginDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsAuthUser { get; set; }
    }
}