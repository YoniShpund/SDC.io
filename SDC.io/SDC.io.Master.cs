using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDC.io
{
    public partial class SDCio : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Control)LoginAlertMessage).Visible = false;

            if (Page.Title.Equals("Login") || Page.Title.Equals("Register"))
            {
                LogoutButton.Visible = false;

                if (Session["User"] == null)
                {
                    ((Control)navbarHeader).Visible = false;
                }
                else
                {
                    Response.Redirect("Default.aspx");
                    ((Control)navbarHeader).Visible = true;
                }
            }
            else
            {
                LogoutButton.Visible = true;
            }
        }

        protected void Logout(object sender, EventArgs e)
        {
            ((Control)navbarHeader).Visible = false;
            Session["User"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}