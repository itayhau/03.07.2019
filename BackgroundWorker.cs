using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            // Check if the backgroundWorker is already busy running the asynchronous operation
            if (!backgroundWorker1.IsBusy)
            {
                // This method will start the execution asynchronously in the background
                backgroundWorker1.RunWorkerAsync();
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int sum = 0;
            for (int i = 1; i <= 100; i++)
            {
                Thread.Sleep(100);
                sum = sum + i;
                // Calling ReportProgress() method raises ProgressChanged event
                // To this method pass the percentage of processing that is complete
                backgroundWorker1.ReportProgress(i);

                // Check if the cancellation is requested
                if (backgroundWorker1.CancellationPending)
                {
                    // Set Cancel property of DoWorkEventArgs object to true
                    e.Cancel = true;
                    // Reset progress percentage to ZERO and return
                    backgroundWorker1.ReportProgress(0);
                    return;
                }
            }

            // Store the result in Result property of DoWorkEventArgs object
            e.Result = sum;
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "Processing cancelled";
            }
            else if (e.Error != null)
            {
                label1.Text = e.Error.Message;
            }
            else
            {
                label1.Text = e.Result.ToString();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                // Cancel the asynchronous operation if still in progress
                backgroundWorker1.CancelAsync();
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString() + "%";
            // ------------ this could be done only on UI Thread
            button1.Text = "aaaaaaaaaaaa";

        }
    }
}
