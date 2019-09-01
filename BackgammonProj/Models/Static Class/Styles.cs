using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ViewModel.Models
{
    public class Styles
    {
        public static Style windowHeader  = (Style)Application.Current.Resources["WindowHeader"];
        public static Style userName  = (Style)Application.Current.Resources["UserNameTxt"];
        public static Style mainHeader  = (Style)Application.Current.Resources["mainHeader"];
        public static Style triangleRed  = (Style)Application.Current.Resources["triangleRed"];
        public static Style triangleBlack  = (Style)Application.Current.Resources["triangleBlack"];
        public static Style triangleCheck = (Style)Application.Current.Resources["triangleChack"];
        public static Style PackBlack = (Style)Application.Current.Resources["PackBlack"];
        public static Style PackWhite = (Style)Application.Current.Resources["PackWhite"];
        public static Style smallTxt  = (Style)Application.Current.Resources["smallTxt"];
        public static Style IconButton  = (Style)Application.Current.Resources["IconButton"];
    }
}
