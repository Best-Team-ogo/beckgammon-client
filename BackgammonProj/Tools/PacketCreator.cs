using BackgammonProj.GameManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonProj.Tools
{
    class PacketCreator
    {
        internal static byte[] Login(string username, string pass)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ClientHeaders.LOGIN);
            writer.WriteCommonString(username);
            writer.WriteCommonString(pass);
            return writer.ToArray();
        }

        internal static byte[] Register(string username,string name, string pass)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ClientHeaders.REGISTER);
            writer.WriteCommonString(username);
            writer.WriteCommonString(name);
            writer.WriteCommonString(pass);
            return writer.ToArray();
        }

        internal static byte[] SendMessage(string text,int id)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ClientHeaders.SEND_MESSAGE);
            writer.WriteCommonString(text);
            writer.WriteInt(id);
            return writer.ToArray();
        }
        //test

        internal static byte[] LogOut()
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ClientHeaders.LOGOUT);
            return writer.ToArray();
        }

        internal static byte[] RequestChat(string userChat)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ClientHeaders.CHAT_REQUEST);
            writer.WriteByte(0); //request a chat from the server
            writer.WriteCommonString(userChat);
            return writer.ToArray();
        }

        internal static byte[] AnswerRequestChat(bool ans,int chatid,string requester)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ClientHeaders.CHAT_REQUEST);
            writer.WriteByte(1); //answer request a chat from the server
            writer.WriteBool(ans);
            writer.WriteCommonString(requester);
            writer.WriteInt(chatid);

            return writer.ToArray();
        }




    }
}
