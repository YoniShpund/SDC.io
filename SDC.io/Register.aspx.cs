using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SDC.io
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection SqlConnect = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SDC.io.DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private static bool VerifyEmailID(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected void RegisterSubmit(object sender, EventArgs e)
        {
            bool isValid = true;

            do
            {
                if (!VerifyEmailID(RegisterEmail.Text))
                {
                    RegisterEmail.CssClass = "form-control  is-invalid";
                    isValid = false;
                }

                if (RegisterPassword.Text.Length == 0)
                {
                    RegisterPassword.CssClass = "form-control  is-invalid";
                    isValid = false;
                }

                if (!RegisterPassword.Text.Equals(RegisterPasswordCheck.Text))
                {
                    RegisterPasswordCheck.CssClass = "form-control  is-invalid";
                    isValid = false;
                }

                if (!isValid)
                {
                    break;
                }

                /********************************************************
                 * Update database with new user.
                 ********************************************************/
                SqlConnect.Open();
                SqlCommand cmd = SqlConnect.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into Users values('" + RegisterEmail.Text + "', '" + RegisterPassword.Text + "')";
                cmd.ExecuteNonQuery();
                SqlConnect.Close();

                /*On Success move to login page.*/
                Response.Redirect("Login.aspx");
            } while (false);
        }
    }
}