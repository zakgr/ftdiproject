using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FtdiLib;

namespace FtdiProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var device = new FtdiBoard();
            foreach (var relay in device.Relays)
            {
                var result = relay.IsOpen = true;
                Thread.Sleep(1000);
            }
            foreach (var relay in device.Relays)
            {
                var result = relay.IsOpen = false;
                Thread.Sleep(1000);
            }

        }

        /*
        private event EventHandler DeviceSelected;
        private FTDI _ftdi = new FTDI();
        private FTDI.FT_DEVICE_INFO_NODE[] _devicelist = new FTDI.FT_DEVICE_INFO_NODE[200];
        public MainWindow()
        {
            InitializeComponent();
            DeviceSelected += Device_Selected;   
        }

        

        private void Device_Selected(object sender, EventArgs e)
        {
            var deviceSerial = sender as string;
            _ftdi.Close();
            _ftdi.OpenBySerialNumber(deviceSerial);
            if (_ftdi.IsOpen)
            {
                _ftdi.SetBaudRate(921600);
                _ftdi.SetBitMode(255, 4);
                uint t=0;
                _ftdi.Write(new byte[1] { 0 }, 1, ref t);

                for (var i = 0; i < 4; ++i)
                {
                    var button = new Button()
                    {
                        Content = $"Relay {i + 1}",
                        Background = Brushes.Crimson,
                        Tag = i
                    };
                    button.Click += (button_Click);
                    this.grid.Children.Add(button);
                }
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            Relay relayDevice = new Relay();
            byte[] pinStatus;
            relayDevice.ReadDevice(_ftdi, out pinStatus);

            var relayButton = (sender as Button);
            byte pinPosition = relayDevice.PositionNo((int) relayButton.Tag);
            
            if (Equals(relayButton.Background, Brushes.Crimson))
            {
                pinPosition |= pinStatus[0];
                relayButton.Background = Brushes.ForestGreen;
            }
            else
            {
                pinPosition ^= pinStatus[0];
                relayButton.Background = Brushes.Crimson;
            }
            relayDevice.WriteDevice(_ftdi, pinPosition);
        }

        private void CmbPorts_OnLoaded(object sender, RoutedEventArgs e)
        {
            var deviList = _devicelist.Select(deviceInfo => deviceInfo.SerialNumber).ToList();
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
            string value = comboBox?.SelectedItem as string;
            this.Title = "Selected: " + value;
            DeviceSelected?.Invoke(value,e);
        }
        */
    }

}
