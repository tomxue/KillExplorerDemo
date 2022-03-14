using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp36
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Process newProc;
        string executable = "C:\\Windows\\System32\\notepad.exe";

        private void ShowExistingProcess()
        {
            Process[] processes = Process.GetProcessesByName("explorer");
            foreach (Process p in processes)
            {
                Debug.WriteLine($"exsitig process (id = {p.Id})");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            ShowExistingProcess();
            StartProcess(executable);
            ShowExistingProcess();
        }

        public void StartProcess(string path)
        {
            newProc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = path,
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Normal
                }
            };
            _ = newProc.Start();

            Debug.WriteLine($"start my process (id = {newProc.Id})");
        }

        private void KillProcess(Process p)
        {
            if (!p.HasExited)
            {
                p.Kill();
                Debug.WriteLine($"kill my process (id = {p.Id})");
            }
            else
            {
                Debug.WriteLine($"my process had quit already (id = {p.Id})");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KillProcess(newProc);
            ShowExistingProcess();
        }
    }
}
