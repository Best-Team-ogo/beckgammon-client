using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ViewModel.Models;

namespace ViewModel.ViewModels
{
    public class BarViewModel
    {
        public Grid Grid { get; set; }

        public TextBlock RedAmount { get; set; }

        public TextBlock BlackAmount { get; set; }
        public List<Ellipse> EllipsesRed { get; set; } = new List<Ellipse>();
        public List<Ellipse> EllipsesBlack { get; set; } = new List<Ellipse>();



        public BarViewModel()
        {
            Grid = new Grid(){Background = new SolidColorBrush { Color = Colors.Brown }};

            RedAmount = new TextBlock { Foreground = new SolidColorBrush { Color = Colors.White }, TextAlignment = TextAlignment.Center };
            BlackAmount = new TextBlock { Foreground = new SolidColorBrush { Color = Colors.Black }, TextAlignment = TextAlignment.Center };

            ImplementOptionsForGrid.CreateCoulmnsForGrid(2, Grid);
            ImplementOptionsForGrid.AddToGrid(RedAmount, 0, 0, Grid);
            ImplementOptionsForGrid.AddToGrid(BlackAmount, 0, 1, Grid);

        }

        public void AddBlackChecker(Ellipse ellipse)
        {
            EllipsesBlack.Add(ellipse);
            BlackAmount.Text = EllipsesBlack.Count.ToString();
        }

        public Ellipse RemoveBlackChecker()
        {
            var checker = EllipsesBlack.FirstOrDefault();
            EllipsesBlack.Remove(checker);
            BlackAmount.Text = EllipsesBlack.Count == 0 ? "" : EllipsesBlack.Count.ToString();
            return checker;
        }

        public void AddRedChecker(Ellipse ellipse)
        {
            EllipsesRed.Add(ellipse);
            RedAmount.Text = EllipsesRed.Count.ToString();
        }

        public Ellipse RemoveRedChecker()
        {
            var checker = EllipsesRed.LastOrDefault();
            EllipsesRed.Remove(checker);
            RedAmount.Text = EllipsesRed.Count == 0 ? "" : EllipsesRed.Count.ToString();
            return checker;
        }
    }
}
