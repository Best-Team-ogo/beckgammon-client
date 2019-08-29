using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackgammonProj.Tools;

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
                    System.Windows.Forms.MessageBox.Show("You have logged in OK!", "Login");
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
                    System.Windows.Forms.MessageBox.Show("You have Registered!", "Register");
                    break;
               

            }
        }
    }
}
