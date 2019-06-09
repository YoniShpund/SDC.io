using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDC.io
{
    public partial class Register : System.Web.UI.Page
    {
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
                 * TODO: Update database with new user.
                 ********************************************************/


                /********************************************************/

                /*On Success move to login page.*/
                Response.Redirect("Login.aspx");
            } while (false);
        }

        protected void RegisterPasswordTextChanged(object sender, EventArgs e)
        {
            if (RegisterPassword.Text.Equals(RegisterPasswordCheck.Text))
            {
                RegisterPasswordCheck.CssClass = "form-control is-valid";
            }
            else
            {
                RegisterPasswordCheck.CssClass = "form-control is-invalid";
            }
        }
    }
}