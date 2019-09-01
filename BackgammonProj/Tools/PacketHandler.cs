using BackgammonProj.Handlers;
using BackgammonProj.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonProj.Tools
{
    class PacketHandler
    {
        public PacketHandler(PacketReader reader)
        {
            int header = reader.ReadShort(); // Header
            switch (header)
            {
                case ServerHeaders.LOGIN_RESPONSE:
                    UserHandler.Login(reader);
                    break;
                case ServerHeaders.REGISTER_RESPONSE:
                    UserHandler.Register(reader);
                    break;
                case ServerHeaders.UPDATE_USERS:
                    UserHandler.UpdateUserOnline(reader);
                    break;
                case ServerHeaders.GET_ALL_ONLINE_USERS:
                    UserHandler.GetAllUsersName(reader);
                    break;
                case ServerHeaders.CHAT_REQUEST_RESPONS:
                    ChatHandler.ChatResponse(reader);
                    break;


                default:
                    System.Windows.Forms.MessageBox.Show("Handler cannot be found!");
                    break;
            }
        }
    }
}
