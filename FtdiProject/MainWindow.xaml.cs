using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using FtdiLib;

namespace FtdiProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private FtdiBoard _device;


        public ObservableCollection<RelayStatus> RelayStatuses
        {
            get { return (ObservableCollection<RelayStatus>)GetValue(RelayStatusesProperty); }
            set { SetValue(RelayStatusesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RelayStatuses.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RelayStatusesProperty =
            DependencyProperty.Register("RelayStatuses", typeof(ObservableCollection<RelayStatus>), typeof(MainWindow), new PropertyMetadata(null));


        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var device = new FtdiBoard();
            _device = device;
            RelayStatuses = new ObservableCollection<RelayStatus>();
            foreach (var relay in device.Relays)
            {
                RelayStatuses.Add(new RelayStatus()
                {
                    State = relay.IsOpen,
                    Index = relay.Index
                });
            }

        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            if (button == null) return;
            var relay = _device.Relays.First(r => r.Index == (int)button.Tag);
            if (button.IsChecked != null)
                RelayStatuses.First(r => r.Index == (int)button.Tag).State = relay.IsOpen = (bool)button.IsChecked;
        }
        /*
        
        private void Device_Selected(object sender, EventArgs e)
        {
            var deviceSerial = sender as string;
            _ftdi.Close();
            _ftdi.OpenBySerialNumber(deviceSerial);
            
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
