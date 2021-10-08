using System.Windows;
using System.Management;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System;
using System.Windows.Input;

namespace RyzenTDPUnlocker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public int language = 1;
        public int helphide = 1;
        public int abouthide = 1;
        public string abouttxt = "关于";
        public string helptxt = "帮助";
        public string hidetxt = "隐藏";

        public enum WMIPath
        {
            Win32_Processor,     // CPU 处理器
        }

        public MainWindow()
        {
            InitializeComponent();
            GetReg();
            t_cpuinfo.Content = GetCpuInfo();
            setprofile();
            vrmslider.Maximum = maxtdpslider.Maximum * 2000;



        }
        public void  GetReg()
        {
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if ((string)rkey.GetValue("RyzenLaptopTDPUnlocker") != null)
            {
                t_autorun.IsChecked = true;
            }
        }

        public string GetCpuInfo()
        {
            string cpuinfo = "unkown";
            try
            {
                ManagementClass mc = new ManagementClass(WMIPath.Win32_Processor.ToString());
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuinfo = ("" + mo.Properties["Name"].Value);
                }
            }
            catch
            {
                cpuinfo = ("unkown");
                t_apply.IsEnabled = false;
            }
            return cpuinfo;
        }

        public void RunExe()
        {

            CreateBat();
            string propath = System.IO.Directory.GetCurrentDirectory();

            using (Process proc = new Process())
            {
                string command = $"{propath}\\autorun.bat";
                proc.StartInfo.FileName = command;
                proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(command);

                //run as admin
                // proc.StartInfo.Verb = "runas";

                proc.Start();
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
            }


            if (t_autorun.IsChecked == true)
            {
                AutoRun();
            }
            else
            {
                ClearAutoRun();
            }

        }

        public void AutoRun()
        {
            var batFile = CreateBat();
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rkey.SetValue("RyzenLaptopTDPUnlocker", batFile);
        }

        public void ClearAutoRun()
        {
            
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if ((string)rkey.GetValue("RyzenLaptopTDPUnlocker") != null)
            {
                rkey.DeleteValue("RyzenLaptopTDPUnlocker");
            }
        }

        public string CreateBat()
        {
            int spm = 15; int sfl = 30; int ssl = 25; int tmp = 80; int vrm = 30000;
            spm = int.Parse(maxtdpslider.Value.ToString());
            sfl = int.Parse(fastocslider.Value.ToString());
            ssl = int.Parse(slowocslider.Value.ToString());
            tmp = int.Parse(tempslider.Value.ToString());
            vrm = int.Parse(vrmslider.Value.ToString());
            string propath = System.IO.Directory.GetCurrentDirectory();

            string vrmhex = vrm.ToString("X");

            string batFilePath = $"{propath}\\autorun.bat";
            string bat = $"ryzenadj.exe --stapm-limit={spm}000 --fast-limit={sfl}000 --slow-limit={ssl}000 --tctl-temp={tmp} --vrmmax-current=0x{vrmhex}";

            //string bat = $"%~dp0\\ryzenadj.exe --stapm-limit={spm}000 --fast-limit={sfl}000 --slow-limit={ssl}000 --tctl-temp={tmp} --vrmmax-current=0x{vrmhex}";

            if (!File.Exists(batFilePath))
            {
                using (FileStream fs = File.Create(batFilePath))
                {
                    fs.Close();
                }
            }

            using (StreamWriter sw = new StreamWriter(batFilePath))
            {
                sw.WriteLine(bat);
            }

            return batFilePath;


        }

        private void T_eng_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            t_chs.Opacity = 0.5;
            t_eng.Opacity = 1;

            hidetxt = "Hide";
            abouttxt = "About";
            helptxt = "Help";

            t_tilite.Content = "RyzenLaptopTDPUnlocker";
            t_urcpu.Content = "Processor";
            t_apply.Content = "Apply";
            t_reset.Content = "Reset";
            t_pro1.Content = "Custom";
            t_pro2.Content = "Green";
            t_pro3.Content = "Balance";
            t_pro4.Content = "Performance";
            t_pro5.Content = "Gaming";
            t_maxtdp.Content = "STAMP Limit";
            t_fastoctdp.Content = "PPT Fast Limit";
            t_slowoctdp.Content = "PPT Slow Limit";
            t_tempc.Content = "Temp Limit";
            t_vrm.Content = "VRM Current";
            t_about.Content = "About";
            t_help.Content = "Help";
            t_autorun.Content = "Autorun";

            t_aboutinfo.Text = "About this software\r\n\r\nRyzenLaptopTDPUnlocker is built on the RyzenADJ project and is \r\npermanently free of charge\r\nGitHub of RyzenADJ: https://github.com/flygoat/ryzenadj \r\n\r\nIf you have any questions or suggestions, you can contact us\r\nDeveloper's mailbox: encvstin@qq.com\r\n\r\nIf you think this software is helpful to you, you are welcome to\r\n encourage developers to continue optimizing this software.\r\n \r\n version 1.6.0";
            t_helptinfo.Text = "HelpInfo\r\n\r\nSTAMP Limit: Power Wall Triggered by Temperature Limit\r\nPPT Fast Limit: Short - time processor power consumption without triggering temperature limitn\r\nPPT Slow Limit: Long - time processor power consumption without triggering temperature limit\r\nTemp Limit: Temperature control of the processor to run at a limited maximum power consumption\r\nVRM Current: Processor current control, you can adjust the current to reduce or increase power consumption\r\n\r\nReminder: Unlocking power consumption may pose unknown risks. Keep good heat dissipation to avoid processor damage.";

        }

        private void T_chs_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            t_chs.Opacity = 1;
            t_eng.Opacity = 0.5;

            hidetxt = "隐藏";
            abouttxt = "关于";
            helptxt = "帮助";

            t_tilite.Content = "锐龙笔记本功耗解锁工具";
            t_urcpu.Content = "你的处理器";
            t_apply.Content = "应用";
            t_reset.Content = "重置";
            t_pro1.Content = "自定义";
            t_pro2.Content = "节  能";
            t_pro3.Content = "平  衡";
            t_pro4.Content = "高性能";
            t_pro5.Content = "极致游戏";
            t_maxtdp.Content = "最大功耗限制";
            t_fastoctdp.Content = "短时间功耗";
            t_slowoctdp.Content = "长时间功耗";
            t_tempc.Content = "温度墙控制";
            t_vrm.Content = "处理器电源控制";
            t_about.Content = "关于";
            t_help.Content = "帮助";
            t_autorun.Content = "自动启动";
            t_aboutinfo.Text = "关于此软件\r\n\r\nRyzenLaptopTDPUnlocker基于RyzenADJ构建，永久免费使用\r\nRyzenADJ的GitHub：https://github.com/flygoat/ryzenadj\r\n\r\n如果你有什么问题或建议可以联系\r\n开发者邮箱: encvstin@qq.com\r\n\r\n如你觉得此软件对你有所帮助，欢迎鼓励开发者继续优化此软件\r\n \r\n version 1.6.0";
            t_helptinfo.Text = "帮助信息\r\n\r\n最大功耗限制：触发温度限制后的功耗墙\r\n短时间功耗：未触发温度限制时，短时间处理器功耗\r\n长时间功耗：未触发温度限制时，长时间处理器功耗\r\n温度墙控制：处理器的温度控制，达到此温度时以限制的最大功耗运行\r\n处理器电流控制：处理器电流控制，你可以调整电压来降低或提升功耗\r\n\r\n提醒: 解锁功耗可能带来未知的风险，请保持良好的散热以免造成处理器损坏";

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Aboutinfo_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void T_about_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (abouthide == 1)
            {
                seting.Visibility = Visibility.Hidden;
                help.Visibility = Visibility.Hidden;
                t_chs.Visibility = Visibility.Hidden;
                t_eng.Visibility = Visibility.Hidden;
                about.Visibility = Visibility.Visible;
                t_help.Content = helptxt;
                t_about.Content = hidetxt;
                abouthide = 0;


            }
            else
            {

                help.Visibility = Visibility.Hidden;
                about.Visibility = Visibility.Hidden;
                seting.Visibility = Visibility.Visible;
                t_chs.Visibility = Visibility.Visible;
                t_eng.Visibility = Visibility.Visible;
                t_help.Content = helptxt;
                t_about.Content = abouttxt;
                abouthide = 1;
            }
        }

        private void T_help_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (helphide == 1)
            {
                seting.Visibility = Visibility.Hidden;
                t_chs.Visibility = Visibility.Hidden;
                t_eng.Visibility = Visibility.Hidden;
                about.Visibility = Visibility.Hidden;
                help.Visibility = Visibility.Visible;
                t_about.Content = abouttxt;
                t_help.Content = hidetxt;
                helphide = 0;
            }
            else
            {

                help.Visibility = Visibility.Hidden;
                about.Visibility = Visibility.Hidden;
                seting.Visibility = Visibility.Visible;
                t_chs.Visibility = Visibility.Visible;
                t_eng.Visibility = Visibility.Visible;
                t_about.Content = abouttxt;
                t_help.Content = helptxt;
                helphide = 1;
            }

        }

        private void B_pro1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            b_pro1.Opacity = 1;
            b_pro2.Opacity = 0.5;
            b_pro3.Opacity = 0.5;
            b_pro4.Opacity = 0.5;
            b_pro5.Opacity = 0.5;
        }

        private void B_pro2_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            maxtdpslider.Value = Math.Round(maxtdpslider.Maximum * 0.25, 0);
            tempslider.Value = 60;
            vrmslider.Value = maxtdpslider.Value * 2000;
            fastocslider.Value = Math.Round(maxtdpslider.Maximum * 0.25, 0);
            slowocslider.Value = Math.Round(maxtdpslider.Maximum * 0.25, 0);


            b_pro1.Opacity = 0.5;
            b_pro2.Opacity = 1;
            b_pro3.Opacity = 0.5;
            b_pro4.Opacity = 0.5;
            b_pro5.Opacity = 0.5;
        }

        private void B_pro3_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            maxtdpslider.Value = Math.Round(maxtdpslider.Maximum * 0.5, 0);
            tempslider.Value = 65;
            vrmslider.Value = maxtdpslider.Value * 2000;
            fastocslider.Value = Math.Round(maxtdpslider.Maximum * 0.5, 0);
            slowocslider.Value = Math.Round(maxtdpslider.Maximum * 0.5, 0);


            b_pro1.Opacity = 0.5;
            b_pro2.Opacity = 0.5;
            b_pro3.Opacity = 1;
            b_pro4.Opacity = 0.5;
            b_pro5.Opacity = 0.5;
        }

        private void B_pro4_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            maxtdpslider.Value = Math.Round(maxtdpslider.Maximum * 0.8, 0);
            tempslider.Value = 70;
            vrmslider.Value = maxtdpslider.Value * 2000;
            fastocslider.Value = Math.Round(maxtdpslider.Maximum * 0.8, 0);
            slowocslider.Value = Math.Round(maxtdpslider.Maximum * 0.8, 0);

            b_pro1.Opacity = 0.5;
            b_pro2.Opacity = 0.5;
            b_pro3.Opacity = 0.5;
            b_pro4.Opacity = 1;
            b_pro5.Opacity = 0.5;
        }

        private void B_pro5_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            maxtdpslider.Value = Math.Round(maxtdpslider.Maximum * 1, 0);
            tempslider.Value = 80;
            vrmslider.Value = maxtdpslider.Value * 2000;
            fastocslider.Value = Math.Round(maxtdpslider.Maximum * 1, 0);
            slowocslider.Value = Math.Round(maxtdpslider.Maximum * 1, 0);

            b_pro1.Opacity = 0.5;
            b_pro2.Opacity = 0.5;
            b_pro3.Opacity = 0.5;
            b_pro4.Opacity = 0.5;
            b_pro5.Opacity = 1;
        }

        private void T_apply_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RunExe();
        }

        private void T_reset_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            setprofile();
            vrmslider.Value = 30000;
        }

        private void Maxtdpslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            b_pro1.Opacity = 1;
            b_pro2.Opacity = 0.5;
            b_pro3.Opacity = 0.5;
            b_pro4.Opacity = 0.5;
            b_pro5.Opacity = 0.5;
        }

        private void b_hide_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void b_close_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void RyzenTDPUnlocker_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)

            {

                this.DragMove();

            }
        }
    }
}
