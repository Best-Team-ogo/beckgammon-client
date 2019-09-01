using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace ViewModel.Models
{
    public class TriangleUiEl
    {
        public StackPanel StackPanel { get; set; }
        public List<Ellipse> Ellipses { get; set; } = new List<Ellipse>();
        public Path Path { get; set; }
        public bool IsRed { get; set; }

    }
}
