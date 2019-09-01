using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using BackgammonProj.Events;
using BackgammonProj.GameManager;
using BackgammonProj.Tools;
using BackgammonProj.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BackgammonProj.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }
        public MainViewModel()
        {

            GlobalEvents.UserLogInEvent += UserLoggedInSucc;
            Client.Instance.StartClient();
            LoginCommand = new RelayCommand(LoginClick);
            RegisterCommand = new RelayCommand(RegisterClick);
        }

        private void RegisterClick()
        {
            if (Password != null && UserName != null)
                Client.Instance.SendPacket(PacketCreator.Register(UserName,UserName, Password));
        }

        private void LoginClick()
        {
            if (Password != null && UserName != null)
                Client.Instance.SendPacket(PacketCreator.Login(UserName, Password));
        }

        public void UserLoggedInSucc(object source, EventArgs args)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow.MainFrame.Navigate(new HomeView());
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }


    }
}