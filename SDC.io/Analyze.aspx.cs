using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDC.io
{
    public partial class Analyze : Page
    {

        protected string ResultDzv { get; set; }
        protected string ResultZv { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
            }/*End of --> if (Session["User"] == null)*/

            ((Control)MoveToResultsButton).Visible = false;
        }/*End of --> protected void Page_Load(object sender, EventArgs e)*/

        protected void StartAnalyze(object sender, EventArgs e)
        {
            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "10%");

            /**********************************************************
             * TODO: Analyze the texts.
             * use the above line to modify the percentage
             * to get the texts use:  Textarea1.Text and Textarea2.Text
             *********************************************************/

            /*When process is finished show the button*/
            ((Control)MoveToResultsButton).Visible = true;
            /*********************************************************/


            /******************Just for debug*************************/
            string imagepath = @"D:\SDC.io\SDC.io\images\SDC.io.logo.png";
            FileStream fs = new FileStream(imagepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            /******************Just for debug*************************/


            /*Use this function to convert bytes to string*/
            ResultDzv = ConvertByteArrayToString(byData);
            ResultZv = ConvertByteArrayToString(byData);

            /**
             * Trigger the modal to show.
             * MUST be last.
             */
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#myModal').modal('toggle');</script>");
        }/*End of --> */

        protected void StopProcess(object sender, EventArgs e)
        {
            /**********************************************************
             * TODO: Stop the process
             *********************************************************/

            /*********************************************************/
            ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#myModal').modal('dismiss');</script>");
        }/*End of --> protected void StopProcess(object sender, EventArgs e)*/

        private string ConvertByteArrayToString(byte[] arr)
        {
            var base64 = Convert.ToBase64String(arr);
            return String.Format("data:image/jpg;base64,{0}", base64);
        }/*End of --> private string ConvertByteArrayToString(byte[] arr)*/
    }/*End of --> public partial class Analyze : System.Web.UI.Page*/
}/*End of --> */