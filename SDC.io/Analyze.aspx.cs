﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SDC.io
{
    public partial class Analyze : Page
    {
        protected string ResultBefore { get; set; }
        protected string ResultAfter { get; set; }

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

        private string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try {
                byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(strIn);

                // Convert utf-8 bytes to a string.
                return System.Text.Encoding.UTF8.GetString(utf8Bytes);
                //return Regex.Replace(strIn, @"[^\w\.@-,]", " ",
                //                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (Exception) {
                return " ";
            }
        }
        private void ClearFile(string inptuFileName)
        {
            var tempFileName = Path.GetTempFileName();
            try {
                using (var streamReader = new StreamReader(inptuFileName))
                using (var streamWriter = new StreamWriter(tempFileName)) {
                    string line;
                    while ((line = streamReader.ReadLine()) != null) {
                        if (!string.IsNullOrWhiteSpace(line))
                            streamWriter.WriteLine(CleanInput(line));
                    }
                }
                File.Copy(tempFileName, inptuFileName, true);
            }
            finally {
                File.Delete(tempFileName);
            }
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if ((TextFileUpload1.PostedFile != null) && (TextFileUpload1.PostedFile.ContentLength > 0)) {
                string SaveLocation = Server.MapPath("PythonScripts") + "\\in1.txt";
                TextFileUpload1.PostedFile.SaveAs(SaveLocation);
                ClearFile(SaveLocation);
            }/*End of --> if ((TextFileUpload1.PostedFile != null) && (TextFileUpload1.PostedFile.ContentLength > 0))*/

            if ((TextFileUpload2.PostedFile != null) && (TextFileUpload2.PostedFile.ContentLength > 0)) {
                string SaveLocation = Server.MapPath("PythonScripts") + "\\in2.txt";
                TextFileUpload2.PostedFile.SaveAs(SaveLocation);
                ClearFile(SaveLocation);
            }/*End of --> if ((TextFileUpload1.PostedFile != null) && (TextFileUpload1.PostedFile.ContentLength > 0))*/

            string strErr;
            string res;

            /*Run first ZV analysis on original texts*/
            var args = $"-fi \"{AppContext.BaseDirectory + @"PythonScripts\in1.txt"}\" -si \"{AppContext.BaseDirectory + @"PythonScripts\in2.txt"}\" -dir {Server.MapPath("PythonScripts")} -name first_result";
            res = RunCmd(@"C:\Users\shpun\Anaconda3\python.exe", AppContext.BaseDirectory + @"PythonScripts\zv.py", args, out strErr);

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "25%");

            /*Run translation on original texts*/
            args = $"-model \"{AppContext.BaseDirectory + @"Models\" + ModelDetails.SelectedValue + @".pt"}\" -src \"{AppContext.BaseDirectory + @"PythonScripts\in1.txt"}\" -output \"{AppContext.BaseDirectory + @"PythonScripts\pred1.txt"}\" -replace_unk";
            res = RunCmd(@"C:\Users\shpun\Anaconda3\python.exe", AppContext.BaseDirectory + @"PythonScripts\translate.py", args, out strErr);

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "50%");

            args = $"-model \"{AppContext.BaseDirectory + @"Models\" + ModelDetails.SelectedValue + @".pt"}\" -src \"{AppContext.BaseDirectory + @"PythonScripts\in2.txt"}\" -output \"{AppContext.BaseDirectory + @"PythonScripts\pred2.txt"}\" -replace_unk";
            res = RunCmd(@"C:\Users\shpun\Anaconda3\python.exe", AppContext.BaseDirectory + @"PythonScripts\translate.py", args, out strErr);

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "75%");

            /*Run second ZV analysis on translated texts*/
            args = $"-fi \"{AppContext.BaseDirectory + @"PythonScripts\pred1.txt"}\" -si \"{AppContext.BaseDirectory + @"PythonScripts\pred2.txt"}\" -dir {Server.MapPath("PythonScripts")} -name second_result";
            res = RunCmd(@"C:\Users\shpun\Anaconda3\python.exe", AppContext.BaseDirectory + @"PythonScripts\zv.py", args, out strErr);

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "100%");
        }/*End of --> private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)*/

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null) {
                return;
            }/*End of --> if (e.Cancelled || e.Error != null)*/

            MoveToResultsButton.Visible = true;

            string imagepath = AppContext.BaseDirectory + @"PythonScripts\first_result.png";
            FileStream fs = new FileStream(imagepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            ResultBefore = ConvertByteArrayToString(byData);
            fs.Close();

            imagepath = AppContext.BaseDirectory + @"PythonScripts\second_result.png";
            fs = new FileStream(imagepath, FileMode.Open);
            byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            ResultAfter = ConvertByteArrayToString(byData);
            fs.Close();

            AfterFirstText.Text = File.ReadAllText(AppContext.BaseDirectory + @"PythonScripts\pred1.txt");
            AfterSecondText.Text = File.ReadAllText(AppContext.BaseDirectory + @"PythonScripts\pred2.txt");
        }/*End of --> private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)*/

        protected void StartAnalyze(object sender, EventArgs e)
        {
            MoveToResultsButton.Visible = false;

            /*Modify the percentage of the progress bar.*/
            ProgressPercentage.Style.Add("width", "10%");


            /**********************************************************
             * Analyze the texts.
             *********************************************************/
            m_backgroundWorker = new BackgroundWorker();
            m_backgroundWorker.WorkerSupportsCancellation = true;
            m_backgroundWorker.DoWork += new DoWorkEventHandler(OnBackgroundWorkerDoWork);
            m_backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnBackgroundWorkerRunWorkerCompleted);
            m_backgroundWorker.RunWorkerAsync();
            /*********************************************************/
            /**
               * Trigger the modal to show.
               * MUST be last.
               */
            ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#myModal').modal('toggle');</script>");
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
