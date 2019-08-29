using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonProj.Tools
{
    class PacketCreator
    {
        internal static byte[] Login(string name, string pass)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ClientHeaders.LOGIN);
            writer.WriteCommonString(name);
            writer.WriteCommonString(pass);
            return writer.ToArray();
        }
        internal static byte[] Register(string name, string pass)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ClientHeaders.REGISTER);
            writer.WriteCommonString(name);
            writer.WriteCommonString(pass);
            return writer.ToArray();
        }
    }
}
