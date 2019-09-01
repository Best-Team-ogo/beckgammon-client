using BackgammonProj.GameManager;
using BackgammonProj.Tools;
using BackgammonProj.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgammonProj.Handlers
{
    public class ChatHandler
    {
        internal static void ChatResponse(PacketReader reader)
        {
            var action = reader.ReadByte();
            string recep = "";
            int chatid = 0;
            switch (action)
            {
                //action 0 is no client found request
                case 0:
                    recep = reader.ReadCommonString();
                    System.Windows.Forms.MessageBox.Show($"User {recep} Could not be found!");
                    break;

                //send request chat
                case 1:
                    chatid = reader.ReadInt();
                    recep = reader.ReadCommonString();
                    DialogResult result = MessageBox.Show($"User {recep} wants to open a chat with you\r\nPress yes to open, no to decline", "Chat Request", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        //send request Accept
                        Client.Instance.SendPacket(PacketCreator.AnswerRequestChat(true, chatid, recep));
                        //Open ChatRome

                        App.Current.Dispatcher.Invoke(() =>
                        {
                            var chatRoom = new ChatRoomWindow(chatid);
                            Client.Instance.ChatRooms.Add(chatRoom);
                            chatRoom.Show();
                        });

                    }
                    else if (result == DialogResult.No)
                    {
                        //send request Decline
                        Client.Instance.SendPacket(PacketCreator.AnswerRequestChat(false, chatid, recep));

                    }

                    break;

                case 2:
                    bool ans = reader.ReadBool();
                    if (!ans)
                    {
                        // ChatRome false
                        recep = reader.ReadCommonString();
                        MessageBox.Show($"User {recep} has decilne you request for chat");
                    }
                    else
                    {
                        chatid = reader.ReadInt();


                        App.Current.Dispatcher.Invoke(() =>
                        {
                            var chatRoom = new ChatRoomWindow(chatid);
                            Client.Instance.ChatRooms.Add(chatRoom);
                            chatRoom.Show();
                        });



                    }
                    break;
            }

        }

        internal static void ChatMessage(PacketReader reader)
        {
            string msg = reader.ReadCommonString();
            int id = reader.ReadInt();
            var cc = Client.Instance.ChatRooms.FirstOrDefault(c => c._chatID == id);
            if (cc != null)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    cc.allMessages.Text += msg + Environment.NewLine;
                });
            }
        }
    }
}
