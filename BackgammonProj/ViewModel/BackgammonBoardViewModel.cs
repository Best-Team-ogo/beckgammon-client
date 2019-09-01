using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ViewModel.Models;
using BackgammonLogic;
using ViewModel.ViewModels;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace BackgammonProj.ViewModel
{
    public class BackgammonBoardViewModel
    {
        public Grid BoardGame { get; set; }

        public List<TriangleUiEl> List { get; set; }

        public BarViewModel Bar { get; set; }
        public Grid OutBarRedPlayer { get; set; }
        public Grid OutBarBlackPlayerar { get; set; }

        public bool MovingChecker { get; set; }
        public bool MovingFromBar { get; set; }

        public TriangleUiEl Current { get; set; }

        public BackgammonController BackgammonLogic { get; set; }

        public BackgammonBoardViewModel()
        {
            StartNewGame();
        }

        public void StartNewGame()
        {
            BoardGame = new Grid();
            List = new List<TriangleUiEl>(new TriangleUiEl[24]);
            BackgammonLogic = BackgammonController.Singlton
;
            Bar = new BarViewModel();
            BackgammonLogic.GetDiceRolls();

            InitializeList();
            SetGridRowAndColumns();
            SetBoardLayout();
            SetTriangle();
            InitializeBoard();
        }

        #region Click Events
        private void OutBarBlackPlayerar_Click(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OutBarRedPlayer_Click(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Bar_Click(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Triangle_Click(object sender, MouseButtonEventArgs e)
        {
            if (MovingFromBar)
            {
                TriangleUiEl triangle = null;
                if (sender is Ellipse ellipse)
                    triangle = List.FirstOrDefault(s => s.Ellipses.Any(c => c.Equals(ellipse)));

                if (sender is Path path)
                    triangle = List.FirstOrDefault(s => s.Path.Equals(path));


                var clickedTriangle = GetIndexForTriangle(triangle);
                if (BackgammonLogic.IsLegalFinalMove(clickedTriangle))      // trying to move to his color or empty slot
                {
                    BackgammonLogic.GetOptionsForInitialSet().ForEach(num => { List[num].Path.Fill = List[num].IsRed ? GetColor(Colors.Red) : GetColor(Colors.Gray); });
                    BackgammonLogic.SetPlayerFinalMove(clickedTriangle, false);
                    try
                    {
                        var pack = BackgammonLogic._RedPlayer.IsMyTurn ? Bar.RemoveRedChecker() : Bar.RemoveBlackChecker();
                        AddPack(triangle, pack);
                    }
                    catch
                    { }
                }
                else if (BackgammonLogic.IsLegalFinalMoveEat(clickedTriangle))   // trying to eat.
                {
                    BackgammonLogic.GetOptionsForInitialSet().ForEach(num => { List[num].Path.Fill = List[num].IsRed ? GetColor(Colors.Red) : GetColor(Colors.Gray); });
                    BackgammonLogic.SetPlayerFinalMoveEat(clickedTriangle);
                    try
                    {
                        var pack = BackgammonLogic._RedPlayer.IsMyTurn ? Bar.RemoveRedChecker() : Bar.RemoveBlackChecker();
                        var packEaten = triangle.Ellipses.Last();
                        var ind = GetIndexForTriangle(triangle);
                        RemovePack(packEaten, triangle);
                        if (BackgammonLogic.GameBoard.Triangles[ind].CheckersColor == CheckerColor.Red)
                            Bar.AddRedChecker(packEaten);
                        else
                            Bar.AddBlackChecker(packEaten);
                        AddPack(triangle, pack);
                    }
                    catch
                    { }

                }
                else
                    return;

                MovingFromBar = false;
            }
            if (!MovingChecker)
            {
                TriangleUiEl triangle = null;
                if (sender is Path p)
                    triangle = List.FirstOrDefault(t => t.Path.Equals(p));
                if (sender is Ellipse ellipse)
                    triangle = List.FirstOrDefault(t => t.Ellipses.Any(c => c.Equals(ellipse)));

                if (triangle == null)
                    return;
                Current = triangle;


                int index = GetIndexForTriangle(triangle);

                if ((BackgammonLogic._RedPlayer.IsMyTurn && BackgammonLogic.GameBoard.GameBar.NumOfRedCheckers > 0) ||
                    (BackgammonLogic._BlackPlayer.IsMyTurn && BackgammonLogic.GameBoard.GameBar.NumOfBlackCheckers > 0))
                {
                    var ind = BackgammonLogic._RedPlayer.IsMyTurn ? -1 : 24;
                    BackgammonLogic.SetPlayerInitialMove(ind);
                    MovingFromBar = true;
                    //BackgammonLogic.GetOptionsForBarOff().ForEach(num => { List[num].Path.Style = Styles.triangleCheck; });
                    return;
                }
                else if (BackgammonLogic.PlayerHasAvailableMoves())
                {
                    if (BackgammonLogic.GetOptionsForInitialMove(index).Count > 0)
                    {
                        BackgammonLogic.SetPlayerInitialMove(index);
                        MovingChecker = true;
                        BackgammonLogic.GetOptionsForInitialSet().ForEach(num => { List[num].Path.Fill = GetColor(Colors.Green); });
                        return;
                    }
                }

            }
            else
            {
                TriangleUiEl triangle = null;
                if (sender is Ellipse ellipse)
                    triangle = List.FirstOrDefault(s => s.Ellipses.Any(c => c.Equals(ellipse)));

                if (sender is Path path)
                    triangle = List.FirstOrDefault(s => s.Path.Equals(path));


                var clickedTriangle = GetIndexForTriangle(triangle);
                if (BackgammonLogic.PlayerInitialTriangleChoice == clickedTriangle)
                {
                    MovingChecker = false;
                    BackgammonLogic.GetOptionsForInitialSet().ForEach(num => { List[num].Path.Fill = List[num].IsRed ? GetColor(Colors.Red) : GetColor(Colors.Gray); });
                    return;
                }
                else if (BackgammonLogic.IsLegalFinalMove(clickedTriangle))      // trying to move to his color or empty slot
                {
                    BackgammonLogic.GetOptionsForInitialSet().ForEach(num => { List[num].Path.Fill = List[num].IsRed ? GetColor(Colors.Red) : GetColor(Colors.Gray); });
                    BackgammonLogic.SetPlayerFinalMove(clickedTriangle, false);
                    try
                    {
                        var pack = Current.Ellipses.Last();
                        RemovePack(pack, Current);
                        AddPack(triangle, pack);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                else if (BackgammonLogic.IsLegalFinalMoveEat(clickedTriangle))   // trying to eat.
                {
                    BackgammonLogic.GetOptionsForInitialSet().ForEach(num => { List[num].Path.Fill = List[num].IsRed ? GetColor(Colors.Red) : GetColor(Colors.Gray); });
                    BackgammonLogic.SetPlayerFinalMoveEat(clickedTriangle);
                    try
                    {
                        var pack = Current.Ellipses.Last();
                        RemovePack(pack, Current);
                        var packEaten = triangle.Ellipses.Last();
                        RemovePack(packEaten, triangle);
                        if (BackgammonLogic._BlackPlayer.IsMyTurn)
                            Bar.AddRedChecker(packEaten);
                        else
                            Bar.AddBlackChecker(packEaten);
                        AddPack(triangle, pack);
                    }
                    catch
                    { }

                }
                else
                    return;

                MovingChecker = false;
            }
        }
        #endregion

        #region Tools For Ui Elments

        private int GetIndexForTriangle(TriangleUiEl triangle)
        {
            return List.FindIndex((TriangleUiEl tr) => { return tr.Equals(triangle); });
        }
         
        private static void AddPack(TriangleUiEl triangle, Ellipse pack)
        {
            if (triangle.Ellipses.Count < 5)
            {
                triangle.StackPanel.Children.Add(pack);
            }
            else if (triangle.Ellipses.Count == 5)
            {
                if (triangle.StackPanel.VerticalAlignment == VerticalAlignment.Bottom)
                    AddTextForBottomTri(triangle, 6);
                else
                    triangle.StackPanel.Children.Add(new TextBlock { Text = "6", TextAlignment = TextAlignment.Center, FontSize = 15 });
            }
            else
            {
                var textAmount = ImplementOptionsForGrid.GetChildOfType<TextBlock>(triangle.StackPanel);
                var amount = textAmount.Text;
                textAmount.Text = (int.Parse(amount) + 1).ToString();
            }
            triangle.Ellipses.Add(pack);
        }

        private static void AddTextForBottomTri(TriangleUiEl triangle, int amount)
        {
            var chidren = triangle.StackPanel.Children;
            triangle.StackPanel.Children.Clear();
            triangle.StackPanel.Children.Add(new TextBlock { Text = "6",TextAlignment = TextAlignment.Center,FontSize =15 });
            for (int i = 0; i < chidren.Count; i++)
            {
                triangle.StackPanel.Children.Add(chidren[i]);
            }
        }

        private void RemovePack(UIElement pack, TriangleUiEl triangle)
        {
            triangle.Ellipses.Remove((Ellipse)pack);
            if (triangle.Ellipses.Count < 5)
            {
                triangle.StackPanel.Children.Remove(pack);

            }
            else if (triangle.Ellipses.Count == 5)
            {
                var textAmount = ImplementOptionsForGrid.GetChildOfType<TextBlock>(triangle.StackPanel);
                triangle.StackPanel.Children.Remove(textAmount);
            }
            else
            {
                var textAmount = ImplementOptionsForGrid.GetChildOfType<TextBlock>(triangle.StackPanel);
                var amount = textAmount.Text;
                textAmount.Text = (int.Parse(amount) - 1).ToString();
            }
        }

        private SolidColorBrush GetColor(Color color)
        {
            return new SolidColorBrush() { Color = color };
        }

        #endregion

        #region SetBoard

        private Path CreateUpTriangle(SolidColorBrush triangleColor)
        {
            Path p = new Path();
            PathSegmentCollection col = new PathSegmentCollection();
            col.Add(new LineSegment() { Point = new Point() { X = 0, Y = 0 } });
            col.Add(new LineSegment() { Point = new Point() { X = 10, Y = 100 } });
            p.Data = new PathGeometry()
            {
                Figures = new PathFigureCollection()
                {
                    new PathFigure()
                    {
                        StartPoint= new Point(){X=20,Y=0 },
                        Segments = col
                    }
                }

            };
            p.Fill = triangleColor;
            p.Stretch = Stretch.Fill;


            return p;
        }

        private Path CreateDownTriangle(SolidColorBrush triangleColor)
        {
            Path p = new Path();
            PathSegmentCollection col = new PathSegmentCollection();

            col.Add(new LineSegment() { Point = new Point() { X = 10, Y = 0 } });
            col.Add(new LineSegment() { Point = new Point() { X = 20, Y = 100 } });
            p.Data = new PathGeometry()
            {
                Figures = new PathFigureCollection()
                {
                    new PathFigure()
                    {
                        StartPoint= new Point(){X=0,Y=100 },
                        Segments = col
                    }
                }

            };
            p.Fill = triangleColor;
            p.Stretch = Stretch.Fill;


            return p;
        }

        private void InitializeList()
        {
            for (int i = 0; i < List.Count; i++)
            {
                List[i] = new TriangleUiEl();
                if (i < List.Count / 2)
                {
                    List[i].Path = i % 2 == 0 ? CreateDownTriangle(new SolidColorBrush() { Color = Colors.Gray }) : CreateDownTriangle(new SolidColorBrush() { Color = Colors.Red });

                }
                else
                {
                    List[i].Path = i % 2 == 0 ? CreateUpTriangle(new SolidColorBrush() { Color = Colors.Gray }) : CreateUpTriangle(new SolidColorBrush() { Color = Colors.Red });

                }
                List[i].StackPanel = i < List.Count / 2 ? new StackPanel() { VerticalAlignment = VerticalAlignment.Bottom } : new StackPanel() { };
                List[i].Path.MouseLeftButtonDown += Triangle_Click;
                List[i].IsRed = i % 2 == 1;
            }
        }

        public Rectangle GetNewTriangleBoard()
        {
            var board = new Rectangle() { Fill = new SolidColorBrush() { Color = Colors.LightSalmon } };
            Grid.SetRowSpan(board, 3);
            Grid.SetColumnSpan(board, 6);
            return board;
        }

        private void SetGridRowAndColumns()
        {
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        BoardGame.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
                        break;
                    case 1:
                        BoardGame.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16.0, GridUnitType.Star) });
                        break;
                    case 2:
                        BoardGame.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(4.0, GridUnitType.Star) });
                        break;
                    case 3:
                        BoardGame.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(16.0, GridUnitType.Star) });
                        break;
                    case 4:
                        BoardGame.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
                        break;
                }
            }

            for (int i = 0; i < 15; i++)
            {
                switch (i)
                {
                    case 0:
                        BoardGame.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
                        break;
                    case 7:
                        BoardGame.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.2, GridUnitType.Star) });
                        break;
                    case 14:
                        BoardGame.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.5, GridUnitType.Star) });
                        break;
                    default:
                        BoardGame.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                        break;
                }
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 24; i++)
            {
                switch (i)
                {
                    case 0:
                        AddCheckers(2, new SolidColorBrush() { Color = Colors.White }, List[i]);
                        break;

                    case 5:
                        AddCheckers(5, new SolidColorBrush() { Color = Colors.Black }, List[i]);
                        break;

                    case 7:
                        AddCheckers(3, new SolidColorBrush() { Color = Colors.Black }, List[i]);
                        break;

                    case 11:
                        AddCheckers(5, new SolidColorBrush() { Color = Colors.White }, List[i]);

                        break;

                    case 12:
                        AddCheckers(5, new SolidColorBrush() { Color = Colors.Black }, List[i]);
                        break;

                    case 16:
                        AddCheckers(3, new SolidColorBrush() { Color = Colors.White }, List[i]);
                        break;

                    case 18:
                        AddCheckers(5, new SolidColorBrush() { Color = Colors.White }, List[i]);

                        break;

                    case 23:
                        AddCheckers(2, new SolidColorBrush() { Color = Colors.Black }, List[i]);
                        break;

                    default:
                        break;
                }
            }
        }

        private void SetBoardLayout()
        {
            var board = new Rectangle()
            {
                Fill = new SolidColorBrush() { Color = Colors.RosyBrown },
                StrokeThickness = 10
            };
            Grid.SetRowSpan(board, 5);
            Grid.SetColumnSpan(board, 15);
            ImplementOptionsForGrid.AddToGrid(board, 0, 0, BoardGame);

            OutBarRedPlayer = new Grid()
            {
                Background = new SolidColorBrush() { Color = Colors.Brown },
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 20,
                Height = 150

            };
            ImplementOptionsForGrid.CreateRowsForGrid(15, OutBarRedPlayer, null);
            ImplementOptionsForGrid.AddToGrid(OutBarRedPlayer, 1, 14, BoardGame);
            OutBarRedPlayer.MouseLeftButtonDown += OutBarRedPlayer_Click;

            OutBarBlackPlayerar = new Grid()
            {
                Background = new SolidColorBrush() { Color = Colors.Brown },
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 20,
                Height = 150

            };
            ImplementOptionsForGrid.CreateRowsForGrid(15, OutBarBlackPlayerar, null);
            ImplementOptionsForGrid.AddToGrid(OutBarBlackPlayerar, 3, 14, BoardGame);
            OutBarBlackPlayerar.MouseLeftButtonDown += OutBarBlackPlayerar_Click;

            Bar.Grid.MouseLeftButtonDown += Bar_Click;
            ImplementOptionsForGrid.AddToGrid(Bar.Grid, 2, 7, BoardGame);

            ImplementOptionsForGrid.AddToGrid(GetNewTriangleBoard(), 1, 1, BoardGame);
            ImplementOptionsForGrid.AddToGrid(GetNewTriangleBoard(), 1, 8, BoardGame);
        }

        private void SetTriangle()
        {
            int index = 0;
            for (int j = 14; j >= 0; j--)
            {

                switch (j)
                {
                    case 0:
                        break;
                    case 7:
                        break;
                    case 14:
                        break;
                    default:
                        ImplementOptionsForGrid.AddToGrid(List[index].Path, 3, j, BoardGame);
                        ImplementOptionsForGrid.AddToGrid(List[index++].StackPanel, 3, j, BoardGame);
                        break;
                }
            }
            for (int j = 0; j < 15; j++)
            {

                switch (j)
                {
                    case 0:
                        break;
                    case 7:
                        break;
                    case 14:
                        break;
                    default:
                        ImplementOptionsForGrid.AddToGrid(List[index].Path, 1, j, BoardGame);
                        ImplementOptionsForGrid.AddToGrid(List[index++].StackPanel, 1, j, BoardGame);
                        break;
                }
            }
        }

        private void AddCheckers(int amount, SolidColorBrush checkerStyle, TriangleUiEl tr)
        {
            for (int i = 0; i < amount; i++)
            {
                var pack = new Ellipse
                {
                    Fill = checkerStyle,
                    MaxWidth = 40,
                    MaxHeight = 40,
                    Stretch = Stretch.UniformToFill
                };
                pack.MouseLeftButtonDown += Triangle_Click; ;
                tr.Ellipses.Add(pack);
                tr.StackPanel.Children.Add(pack);
            }
        }


        #endregion
    }
}
