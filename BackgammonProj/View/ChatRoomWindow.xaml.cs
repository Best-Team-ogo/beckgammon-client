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
using System.Windows.Shapes;

namespace BackgammonProj.View
{
    /// <summary>
    /// Interaction logic for ChatRoomWindow.xaml
    /// </summary>
    public partial class ChatRoomWindow : Window
    {
        private int _chatID;
        public ChatRoomWindow(int id)
        {
            InitializeComponent();
            _chatID = id;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            Client.Instance.SendPacket(PacketCreator.SendMessage(message.Text));
        }
    }
}
