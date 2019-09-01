using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonProj.Headers
{
    public class ServerHeaders
    {
        //USER
        public const short LOGIN_RESPONSE = 1;
        public const short REGISTER_RESPONSE = 2;
        public const short UPDATE_USERS = 3;
        public const short GET_ALL_ONLINE_USERS = 4;
        //CHAT
        public const short CHAT_REQUEST_RESPONS = 5;
        public const short SEND_MESSAGE_RESPONE = 7;
    }
}
