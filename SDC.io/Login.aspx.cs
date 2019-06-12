using System;
using System.Data;
using System.Data.SqlClient;

namespace SDC.io
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection SqlConnect = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SDC.io.DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.FindControl("LoginAlertMessage").Visible = false;
        }

        protected void LoginSubmit(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            SqlConnect.Open();
            SqlCommand cmd = SqlConnect.CreateCommand();
            cmd.CommandText = "select * from Users where Email='" + LoginMail.Text + "';";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            // this will query the database and return the result of the datatable
            dataAdapter.Fill(dataTable);
            SqlConnect.Close();
            dataAdapter.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                if (LoginMail.Text.Equals(row["Email"]) && LoginPassword.Text.Equals(row["Password"]))
                {
                    Session["user"] = LoginMail.Text;
                    Response.Redirect("Default.aspx");
                    this.Master.FindControl("LoginAlertMessage").Visible = false;
                    return;
                }
            }

            this.Master.FindControl("LoginAlertMessage").Visible = true;
        }

        protected void Register(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}