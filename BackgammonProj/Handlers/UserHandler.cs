using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackgammonProj.DatabaseModel;
using BackgammonProj.Events;
using BackgammonProj.GameManager;
using BackgammonProj.Tools;
using ZeroFormatter;

namespace BackgammonProj.Handlers
{
    class UserHandler
    {
        internal static void Login(PacketReader reader)
        {
            byte res = reader.ReadByte();
            switch (res)
            {
                case 0: //Wrong ID
                    System.Windows.Forms.MessageBox.Show("You have entered a wrong id/pass","Login");
                    break;
                case 1: //Succ
                    GlobalEvents.UserLogInEvent?.Invoke(null, EventArgs.Empty);
                    break;
                case 2: //Already Connected
                    System.Windows.Forms.MessageBox.Show("Account already logged in!", "Login");
                    break;

            }
        }

        internal static void Register(PacketReader reader)
        {
            byte res = reader.ReadByte();
            switch (res)
            {
                case 0: //Already Exist
                    System.Windows.Forms.MessageBox.Show("Account already exist!", "Register");
                    break;
                case 1: //Registerd
                    GlobalEvents.UserLogInEvent?.Invoke(null, EventArgs.Empty);
                    break;

            }
        }

        internal static void GetAllUsersName(PacketReader reader)
        {
            int amount = reader.ReadInt();
            List<string> users = new List<string>();
            for (int i = 0; i < amount; i++)
            {
                users.Add(reader.ReadCommonString());
            }


            GlobalEvents.OnGetUserEvent?.Invoke(null
                , new OnGetUserEventArgs { Update = false
                , Users = users });
        }


        internal static void UpdateUserOnline(PacketReader reader)
        {
            
            byte action = reader.ReadByte();
            string userName = reader.ReadCommonString();

            GlobalEvents.OnGetUserEvent?.Invoke(null,
                new OnGetUserEventArgs { Update = true
                , User =userName
                ,AddUser = action==1? true:false});
        }


    }
}
