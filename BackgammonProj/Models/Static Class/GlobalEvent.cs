using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{

    public class GlobalEvent
    {
        public static EventHandler<IsEnabledEventArgs> IsChatEnabledEvent;
        public static EventHandler<RollDiceEventArgs> RollDiceEvent;
        public static EventHandler<EventArgs> StartGameEvent;
        public static EventHandler<EventArgs> StartChatEvent;
    }

    public class RollDiceEventArgs: EventArgs
    {
        public int FirstRoll { get; set; }

        public int SecondRoll { get; set; }
    }

    public class IsEnabledEventArgs : EventArgs
    {
        public bool IsEnabled { get; set; }
    }
}
