using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SDC.io
{
    public partial class Analyze : Page
    {
        protected string ResultDzv { get; set; }
        protected string ResultZv { get; set; }

        //CancellationTokenSource cancellationTokenSource;

        private BackgroundWorker m_backgroundWorker;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null) {
                Response.Redirect("Login.aspx");
            }/*End of --> if (Session["User"] == null)*/

            if (IsPostBack) {
                return;
            }/*End of --> if (IsPostBack)*/

            MoveToResultsButton.Visible = false;

            var ModelFiles = Directory.GetFiles(AppContext.BaseDirectory + @"Models/", "*.pt",
                                                SearchOption.AllDirectories).Select(f => Path.GetFileName(f));
            var JsonFiles = Directory.GetFiles(AppContext.BaseDirectory + @"Models/", "*.json",
                                               SearchOption.AllDirectories).Select(f => Path.GetFileName(f));

            ModelDetails.Items.Clear();

            foreach (string model in ModelFiles) {
                string modelName = model.Split('.')[0];
                bool isValidModel = false;

                foreach (string json in JsonFiles) {
                    string jsonName = json.Split('.')[0];

                    if (jsonName.Equals(modelName)) {
                        isValidModel = true;
                        break;
                    }/*End of --> if (jsonName.Equals(modelName))*/
                }/*End of --> foreach (json in JsonFiles)*/

                if (isValidModel) {
                    ModelDetails.Items.Add(modelName);
                }/*End of --> if (isValidModel)*/
            }/*End of --> foreach (string model in ModelFiles)*/
        }/*End of --> protected void Page_Load(object sender, EventArgs e)*/

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if ((TextFileUpload1.PostedFile != null) && (TextFileUpload1.PostedFile.ContentLength > 0)) {
                string SaveLocation = Server.MapPath("PythonScripts") + "\\in1.txt";
                TextFileUpload1.PostedFile.SaveAs(SaveLocation);
            }/*End of --> if ((TextFileUpload1.PostedFile != null) && (TextFileUpload1.PostedFile.ContentLength > 0))*/

            if ((TextFileUpload2.PostedFile != null) && (TextFileUpload2.PostedFile.ContentLength > 0)) {
                string SaveLocation = Server.MapPath("PythonScripts") + "\\in2.txt";
                TextFileUpload2.PostedFile.SaveAs(SaveLocation);
            }/*End of --> if ((TextFileUpload1.PostedFile != null) && (TextFileUpload1.PostedFile.ContentLength > 0))*/

            string strErr;
            string res;
            
            /*Run first ZV analysis on original texts*/
            var args = $"-fi \"{AppContext.BaseDirectory + @"PythonScripts\in1.txt"}\" -si \"{AppContext.BaseDirectory + @"PythonScripts\in2.txt"}\" -dir {Server.MapPath("PythonScripts")} -name first_result";
            //res = RunCmd(@"C:\Users\shpun\Anaconda3\python.exe", AppContext.BaseDirectory + @"PythonScripts\zv.py", args, out var strErr);

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "25%");

            /*Run translation on original texts*/
            args = $"-fi \"{AppContext.BaseDirectory + @"Models\" + ModelDetails.SelectedValue + @".pt"}\" -src \"{AppContext.BaseDirectory + @"PythonScripts\in1.txt"}\" -output \"{AppContext.BaseDirectory + @"PythonScripts\pred1.txt"}\" -replace_unk";
            res = RunCmd(@"C:\Users\shpun\Anaconda3\python.exe", AppContext.BaseDirectory + @"PythonScripts\translate.py", args, out strErr);

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "50%");

            args = $"-fi \"{AppContext.BaseDirectory + @"Models\" + ModelDetails.SelectedValue + @".pt"}\" -src \"{AppContext.BaseDirectory + @"PythonScripts\in2.txt"}\" -output \"{AppContext.BaseDirectory + @"PythonScripts\pred2.txt"}\" -replace_unk";
            res = RunCmd(@"C:\Users\shpun\Anaconda3\python.exe", AppContext.BaseDirectory + @"PythonScripts\translate.py", args, out strErr);

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "75%");

            /*Run second ZV analysis on translated texts*/
            args = $"-fi \"{AppContext.BaseDirectory + @"PythonScripts\pred1.txt"}\" -si \"{AppContext.BaseDirectory + @"PythonScripts\pred2.txt"}\" -dir {Server.MapPath("PythonScripts")} -name second_result";
            //res = RunCmd(@"C:\Users\shpun\Anaconda3\python.exe", AppContext.BaseDirectory + @"PythonScripts\zv.py", args, out var strErr);

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "100%");
        }/*End of --> private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)*/

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null) {
                return;
            }/*End of --> if (e.Cancelled || e.Error != null)*/

            MoveToResultsButton.Visible = true;
        }/*End of --> private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)*/

        protected void StartAnalyze(object sender, EventArgs e)
        {
            MoveToResultsButton.Visible = false;

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

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "10%");


            /**********************************************************
             * Analyze the texts.
             * use the above line to modify the percentage
             *********************************************************/

            /**
              * Trigger the modal to show.
              * MUST be last.
              */
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#myModal').modal('toggle');</script>");

            m_backgroundWorker = new BackgroundWorker();
            m_backgroundWorker.WorkerSupportsCancellation = true;
            m_backgroundWorker.DoWork += new DoWorkEventHandler(OnBackgroundWorkerDoWork);
            m_backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnBackgroundWorkerRunWorkerCompleted);
            m_backgroundWorker.RunWorkerAsync();
            /*********************************************************/
        }/*End of --> protected void StartAnalyze(object sender, EventArgs e)*/

        protected void StopProcess(object sender, EventArgs e)
        {
            /**********************************************************
             * Stop the process
             *********************************************************/
            m_backgroundWorker.CancelAsync();
            /*********************************************************/
            ClientScript.RegisterStartupScript(GetType(), "Hide", "<script> $('#myModal').modal('dismiss');</script>");
        }/*End of --> protected void StopProcess(object sender, EventArgs e)*/

        private string ConvertByteArrayToString(byte[] arr)
        {
            var base64 = Convert.ToBase64String(arr);
            return String.Format("data:image/jpg;base64,{0}", base64);
        }/*End of --> private string ConvertByteArrayToString(byte[] arr)*/

        public static string RunCmd(string file, string cmd, string args, out string stderr)
        {

            var start = new ProcessStartInfo {
                FileName = file,
                Arguments = $"\"{cmd}\"" + (string.IsNullOrEmpty(args) ? string.Empty : " " + args),
                UseShellExecute = false, // Do not use OS shell
                CreateNoWindow = true, // We don't need new window
                RedirectStandardOutput = true, // Any output, generated by application will be redirected back
                RedirectStandardError = true // Any error in standard output will be redirected back (for example exceptions)
            };

            using (var process = Process.Start(start)) {
                if (process == null) {
                    stderr = "Failed on try to start process!";
                    return string.Empty;
                }/*End of --> if (process == null)*/

                using (var reader = process.StandardOutput) {
                    stderr =
                        process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    return reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                }/*End of --> using (var reader = process.StandardOutput)*/
            }/*End of --> using (var process = Process.Start(start))*/
        }/*End of --> public static string RunCmd(string file, string cmd, string args, out string stderr)*/
    }/*End of --> public partial class Analyze : System.Web.UI.Page*/
}/*End of --> namespace SDC.io*/
