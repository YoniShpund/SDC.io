using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDC.io
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginSubmit(object sender, EventArgs e)
        {
            if (LoginMail.Text.Equals("y@n.i") && LoginPassword.Text.Equals("123"))
            {
                Session["user"] = LoginMail.Text;
                Response.Redirect("Default.aspx");
                this.Master.FindControl("LoginAlertMessage").Visible = false;
            }
            else
            {
                this.Master.FindControl("LoginAlertMessage").Visible = true;
            }
        }

        protected void Register(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}