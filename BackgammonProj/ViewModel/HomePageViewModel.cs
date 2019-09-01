using BackgammonProj.DatabaseModel;
using BackgammonProj.Events;
using BackgammonProj.GameManager;
using BackgammonProj.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BackgammonProj.ViewModel
{
    public class HomePageViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public Dispatcher MainDispatcher { get; set; }
        public ObservableCollection<User> OnlineUsers { get; set; }

        public RelayCommand CreateChat { get; set; }
        public RelayCommand CreateGame { get; set; }
        public RelayCommand MouseDoubleClickCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private User _selectedUser;
        public User SelectedUser { get { return _selectedUser; } set { _selectedUser = value; IsChatEnable = _selectedUser != null; } }

        private bool _isChatEnable;
        public bool IsChatEnable { get { return _isChatEnable; } set { _isChatEnable = value; Notify(nameof(IsChatEnable)); } }



        public HomePageViewModel()
        {
            OnlineUsers = new ObservableCollection<User>();
            GlobalEvents.OnGetUserEvent += GetAllUser;
            MainDispatcher = Application.Current.Dispatcher;
            CreateChat = new RelayCommand(StartNewChat);
            CreateGame = new RelayCommand(StartNewGame);
            MouseDoubleClickCommand = new RelayCommand(StartNewChat);
        }
        

        public void StartNewChat()
        {
            if (SelectedUser != null)
            {

            }
        }

        public void StartNewGame()
        {

        }

        private void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void GetAllUser(object source, OnGetUserEventArgs args)
        {
            if (!args.Update)
            {
                foreach (string name in args.Users)
                {
                    if (!OnlineUsers.Any(u => u.Name == name))
                        MainDispatcher.Invoke(()=> { OnlineUsers.Add(new User { Name =name}); });
                }
            }
            else
            {
                if (args.AddUser)
                    MainDispatcher.Invoke(() => { OnlineUsers.Add(new User {Name =args.User }); });
                else
                {
                    try
                    {
                        var user = OnlineUsers.FirstOrDefault(u => u.Name == args.User);
                        MainDispatcher.Invoke(() => { OnlineUsers.Remove(user); });

                    }
                    catch
                    {

                    }
                }
            }
            
        }
    }
}
