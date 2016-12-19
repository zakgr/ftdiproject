using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using IziR.Glue;

namespace FtdiProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        #region dps
        public ObservableCollection<IRelayBoard> Boards
        {
            get { return (ObservableCollection<IRelayBoard>)GetValue(BoardsProperty); }
            set { SetValue(BoardsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoardsProperty =
            DependencyProperty.Register("Boards", typeof(ObservableCollection<IRelayBoard>), typeof(MainWindow), new PropertyMetadata(null));



        public ObservableCollection<RelayStatus> RelayStatuses
        {
            get { return (ObservableCollection<RelayStatus>)GetValue(RelayStatusesProperty); }
            set { SetValue(RelayStatusesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RelayStatuses.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RelayStatusesProperty =
            DependencyProperty.Register("RelayStatuses", typeof(ObservableCollection<RelayStatus>), typeof(MainWindow), new PropertyMetadata(null));
        #endregion



        public MainWindow()
        {
            InitializeComponent();
            Boards = new ObservableCollection<IRelayBoard>();
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

            /*
            var res =SerialPort.GetPortNames();
            foreach (var re in res)
            {
                SerialPort port = new SerialPort(re);

            }
            SerialPort serialPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();
            var data = new byte[] {0xA0, 0x01, 0x01, 0xA2};
            serialPort.Write(data,0,data.Length);
            //serialPort.Close();
            var data2 = new byte[] { 0xA0, 0x01, 0x00, 0xA1 };
            serialPort.Write(data2, 0, data2.Length);
            */
            foreach (var dll in Directory.GetFiles("Plugins", "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    var providerTypes =
                        assembly.GetTypes().Where(type => typeof(IRelayDeviceProvider).IsAssignableFrom(type));
                    foreach (var providerType in providerTypes)
                    {


                        if (providerType != null)
                        {
                            var provider = (IRelayDeviceProvider)Activator.CreateInstance(providerType);
                            foreach (var board in provider.GetBoards)
                            {
                                Boards.Add(board);

                            }

                        }
                    }
                }
                catch
                {
                }
            }

        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            if (button == null) return;
            var relay = button.Tag as IRelay;
            relay.IsOpen = !relay.IsOpen;
        }

    }

}
