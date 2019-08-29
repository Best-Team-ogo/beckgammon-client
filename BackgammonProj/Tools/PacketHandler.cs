using BackgammonProj.Handlers;
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
                case ClientHeaders.LOGIN:
                    UserHandler.Login(reader);
                    break;
                case ClientHeaders.REGISTER:
                    UserHandler.Register(reader);
                    break;


                default:
                    System.Windows.Forms.MessageBox.Show("Handler cannot be found!");
                    break;
            }
        }
    }
}
