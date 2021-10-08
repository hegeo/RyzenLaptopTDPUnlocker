using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace RyzenTDPUnlocker
{
    public partial class MainWindow : Window
    {
        public void setprofile()
        {
            /**这是C#7.0 case when示例
            switch (t_cpuinfo.Content.ToString())
            {
                //LAPTOP CPU
                case string a when a.Contains("2200U"):
                    maxtdpslider.Maximum = 25;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
            }
            **/
            string message = t_cpuinfo.Content.ToString();
            string[] switchStrings = { "2200U", "2300U","2500U","2700U","3200U","3300U","3500U", "3550U", "3700U", "3750H"};
            switch (switchStrings.FirstOrDefault<string>(s => message.ToUpper().Contains(s)))
            {  //LAPTOP CPU
                case ("2200U"):
                    maxtdpslider.Maximum = 25;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
                case ("2300U"):
                    maxtdpslider.Maximum = 25;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
                case  ("2500U"):
                    maxtdpslider.Maximum = 25;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
                case  ("2700U"):
                    maxtdpslider.Maximum = 25;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
                case  ("3200U"):
                    maxtdpslider.Maximum = 25;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
                case  ("3300U"):
                    maxtdpslider.Maximum = 35;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
                case  ("3500U"):
                    maxtdpslider.Maximum = 35;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
                case  ("3700U"):
                    maxtdpslider.Maximum = 25;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 15;
                    fastocslider.Value = 15;
                    slowocslider.Value = 15;
                    break;
                case  ("3750H"):
                    maxtdpslider.Maximum = 35;
                    maxtdpslider.Minimum = 10;
                    tempslider.Value = 80;
                    maxtdpslider.Value = 35;
                    fastocslider.Value = 35;
                    slowocslider.Value = 35;
                    break;
                default:
                    MessageBox.Show("很抱歉，不支持您的处理器\nUnsupported Processor!");
                    Application.Current.Shutdown();
                    break;
            }
        }
    }
}
