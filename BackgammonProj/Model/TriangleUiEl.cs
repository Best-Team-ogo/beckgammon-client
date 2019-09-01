using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

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
