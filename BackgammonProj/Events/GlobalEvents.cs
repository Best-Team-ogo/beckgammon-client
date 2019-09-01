using BackgammonProj.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonProj.Events
{
    public class GlobalEvents
    {
        public static EventHandler UserLogInEvent { get; set; }
        public static EventHandler<OnGetUserEventArgs> OnGetUserEvent { get; set; }
    }

    public class OnGetUserEventArgs:EventArgs
    {
        public bool Update { get; set; }
        public List<string> Users { get; set; }


        public bool AddUser { get; set; }
        public string User { get; set; }

    }
}
