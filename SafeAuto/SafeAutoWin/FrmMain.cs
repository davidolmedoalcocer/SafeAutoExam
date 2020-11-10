using SafeAuto.Core.Entities;
using SafeAuto.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SafeAutoWin
{
    public partial class FrmMain : Form
    {
        private readonly IDriverService _driverService;
        public FrmMain(IDriverService driverService)
        {
            InitializeComponent();

            this._driverService = driverService;
        }



        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.opdOpenFile.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string fileName = this.opdOpenFile.FileName;
                string[] lines = File.ReadAllLines(fileName);

                this.ProcessLines(lines);

            }
        }

        private void ProcessLines(string[] lines)
        {
            try
            {
                //First clear the textbox
                this.txtResult.Text = string.Empty;

                StringBuilder resultPrint = new StringBuilder();
                List<Driver> driversOrder;
                string displayLine = string.Empty;

                foreach (string line in lines)
                {
                    this._driverService.ProcessComand(line);
                }


                driversOrder = this._driverService.GetDriversSorted();

                foreach (Driver driver in driversOrder)
                {
                    // Discard any trips that average a speed of less than 5 mph or greater than 100 mph
                    if (driver.SpeedAverage < 5 || driver.SpeedAverage > 100)
                    {
                        displayLine = string.Format("{0}: {1} miles", driver.Name,
                            Math.Round(driver.Distance, 0));
                    }
                    else
                    {
                        displayLine = string.Format("{0}: {1} miles @ {2} mph",
                            driver.Name, Math.Round(driver.Distance, 0), Math.Round(driver.SpeedAverage, 0));
                    }

                    resultPrint.Append(displayLine + Environment.NewLine);
                }



                this.txtResult.Text = resultPrint.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exists a problem with the file. " + ex.Message);
            }

        }
    }
}
