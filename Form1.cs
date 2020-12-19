using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChristmasWallpaper
{
    public partial class Form1 : Form
    {
        const string IconPath = @"..\..\icon.ico";
        NotifyIcon SysTrayIcon;
        public Form1()
        {
            InitializeComponent();
        }

        private void RemoveStartupBtn_Click(object sender, EventArgs e)
        {
            // Stop program running on startup
            Program.RemoveFromStartup();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set default values for date pickers, and prevent dates from other years being used
            startDatePicker.Value = State.StartDate;
            startDatePicker.MinDate = new DateTime(State.StartDate.Year, 1, 1);
            startDatePicker.MaxDate = new DateTime(State.StartDate.Year, 12, 31);
            endDatePicker.Value = State.EndDate;
            endDatePicker.MinDate = new DateTime(State.EndDate.Year, 1, 1);
            endDatePicker.MaxDate = new DateTime(State.EndDate.Year, 12, 31);

            // Minimise to system tray
            SysTrayIcon = new NotifyIcon();
            SysTrayIcon.Icon = new Icon(IconPath);
            SysTrayIcon.Text = "ChristmasWallpaper settings";
            SysTrayIcon.Visible = true;
            // Register event handler to show settings when clicked
            SysTrayIcon.Click += new EventHandler(SysTrayIcon_Click);
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            Visible = false;
        }

        private void SysTrayIcon_Click(object sender, EventArgs e)
        {
            // Show settings
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Visible = true;
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            // Save changes to start and end date
            State.StartDate = startDatePicker.Value;
            State.EndDate = endDatePicker.Value;
            State.SaveConfig();
            MessageBox.Show("Saved changes");
        }

        private void CancelChangesButton_Click(object sender, EventArgs e)
        {
            // Cancel changes to start and end dates
            startDatePicker.Value = State.StartDate;
            endDatePicker.Value = State.EndDate;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Minimise to system tray when X button pressed
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Only override if UserClosing to avoid interfering with shutdown
                e.Cancel = true;
                ShowInTaskbar = false;
                WindowState = FormWindowState.Minimized;
                Visible = false;

            }
        }
    }
}
