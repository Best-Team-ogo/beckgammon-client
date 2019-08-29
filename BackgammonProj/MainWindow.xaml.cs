using BackgammonProj.GameManager;
using BackgammonProj.Tools;
using System;
using System.Collections.Generic;
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

namespace BackgammonProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Client.Instance.StartClient();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            Client.Instance.SendPacket(PacketCreator.Login(nameTxt.Text,passTxt.Text));
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Client.Instance.SendPacket(PacketCreator.Register(nameTxt.Text, passTxt.Text));
        }
    }
}
