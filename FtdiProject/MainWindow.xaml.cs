using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
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
using FTD2XX_NET;

namespace FtdiProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //string[] ports = SerialPort.GetPortNames();

        }

        private void CmbPorts_OnLoaded(object sender, RoutedEventArgs e)
        {
            FTDI ftdi = new FTDI();
            FTDI.FT_DEVICE_INFO_NODE[] devicelist = new FTDI.FT_DEVICE_INFO_NODE[100];
            ftdi.GetDeviceList(devicelist);
            List<object> deviList = new List<object>();
            foreach (var deviceInfo in devicelist)
            {
                try
                {
                    deviList.Add(deviceInfo.Description);
                }
                catch (Exception){}
                
            }
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.ItemsSource = deviList;
                comboBox.SelectedIndex = 0;
            }

        }

        private void CmbPorts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            string value = comboBox.SelectedItem as string;
            this.Title = "Selected: " + value; 
        }
    }
}
