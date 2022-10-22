using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Draw.Lib;
using static Draw.Lib.DrawChaosGame;
using Point = Avalonia.Point;

namespace ChaosGame
{
    public partial class MainWindow : Window
    {
        #region Properties
        public List<Point> Vertices { get; set; }
        public int Tick { get; set; } = 1;
        public static double SliderValue { get; set; }
        public Canvas Canvas { get; set; }
        public Window Window { get; set; }
        public DispatcherTimer Timer = new DispatcherTimer();
        public static int _Iterations { get; set; }
        public static string Shape { get; set; }
        public static int _mode { get; set; } = 0;
        #endregion
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            Shape = "Triangle";
            Vertices = new List<Point>
            {
                new Point(200, 0),
                new Point(0, 400),
                new Point(400, 400),
            };
            Window = this;
            Canvas = this.FindControl<Canvas>("canvas");
            SliderValue = this.FindControl<Slider>("Slider").Value;
            DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
            _Iterations = Convert.ToInt32(this.FindControl<TextBox>("Iterations").Text);
            BasePoints();
        }
        #endregion

        #region Methods
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        /// <summary>
        /// starts the Timer
        /// </summary>
        /// <param name="shape"></param>
        private void DispatchTimer(string shape)
        {
            DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
            Timer = new DispatcherTimer();

            Timer.Interval = TimeSpan.FromMilliseconds(SliderValue);
            Timer.Start();
            Timer.Tick += (sender, args) =>
            {
                if (shape == "Triangle")
                {
                    cg.GenerateChaosGame();
                }
                else if (shape == "Pentagon")
                {
                    cg.GenerateChaosGamePentagon();
                }
                else if (shape == "Square")
                {
                    cg.GenerateChaosGamePentagon();
                }
                else if (shape == "Square2")
                {
                    cg.GenerateChaosGameSquareAntiClockwise();
                }
                else if (shape == "Square3")
                {
                    cg.GenerateChaosGameSquare();
                }
                else if (shape == "Hexagon")
                {
                    cg.GenerateChaosGamePentagon();
                }
                else if (shape == "Octagon")
                {
                    cg.GenerateChaosGamePentagon();
                }
                Tick++;
                if (_mode == 1)
                {
                    if (Tick == _Iterations / 2)
                    {
                        SaveCanvas();
                    }
                }
                if (Tick == _Iterations)
                {
                    Timer.Stop();
                    Tick = 1;
                }
            };
        }
        /// <summary>
        /// Clears the canvas and draws the base points
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_ClearCanvas(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        /// <summary>
        /// Stops the Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_Stop(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
        }
        /// <summary>
        /// when slider value changes it updates the Timer interval and restarts the Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void Slider_OnPointerPressed(object? sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                SliderValue = this.FindControl<Slider>("Slider").Value;
                this.FindControl<TextBlock>("TextBlock").Text = SliderValue.ToString(CultureInfo.CurrentCulture);
                Tick = 1;
                Timer.Stop();
                Clear();
                Window.Background = null;
                
                DispatchTimer(Shape);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        /// <summary>
        /// when text changes it changes the iterations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Iterations_OnLostFocus(object? sender, RoutedEventArgs e)
        {
            _Iterations = Convert.ToInt32(this.FindControl<TextBox>("Iterations").Text);
        }
        /// <summary>
        /// switches the shapes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            //triangle
            if (this.FindControl<ComboBox>("ComboBox").SelectedIndex == 0)
            {
                Shape = "Triangle";
                Vertices.Clear();
                Vertices = new List<Point>
                {
                    new Point(200, 0),
                    new Point(0, 400),
                    new Point(400, 400),
                };
                DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
                Clear();
            }
            //pentagon
            if (this.FindControl<ComboBox>("ComboBox").SelectedIndex == 1)
            {
                Shape = "Pentagon";
                Vertices.Clear();
                Vertices = new List<Point>
                {
                    new Point(200, 0),
                    new Point(0, 150),
                    new Point(400, 150),
                    new Point(100, 400),
                    new Point(300, 400),
                };
                DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
                Clear();
            }
            //hexagon
            if (this.FindControl<ComboBox>("ComboBox").SelectedIndex == 2)
            {
                Shape = "Hexagon";
                Vertices.Clear();
                Vertices = new List<Point>
                {
                    new Point(200, 0),
                    new Point(0, 100),
                    new Point(0, 300),
                    new Point(200, 400),
                    new Point(400, 300),
                    new Point(400, 100),
                };
                DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
                Clear();
            }
            //octagon
            if (this.FindControl<ComboBox>("ComboBox").SelectedIndex == 3)
            {
                Shape = "Octagon";
                Vertices.Clear();
                Vertices = new List<Point>
                {
                    new Point(200, 0),
                    new Point(0, 100),
                    new Point(0, 300),
                    new Point(200, 400),
                    new Point(400, 300),
                    new Point(400, 100),
                    new Point(300, 0),
                    new Point(100, 0),
                };
                DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
                Clear();
            }
            //square
            if (this.FindControl<ComboBox>("ComboBox").SelectedIndex == 4)
            {
                Shape = "Square";
                Vertices.Clear();
                Vertices = new List<Point>
                {
                    new Point(0, 0),
                    new Point(0, 400),
                    new Point(400, 400),
                    new Point(400, 0),
                };
                DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
                Clear();
            }
            //square2
            if (this.FindControl<ComboBox>("ComboBox").SelectedIndex == 5)
            {
                Shape = "Square2";
                Vertices.Clear();
                Vertices = new List<Point>
                {
                    new Point(0, 0),
                    new Point(0, 400),
                    new Point(400, 400),
                    new Point(400, 0),
                };
                DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
                Clear();
            }
            //square3
            if (this.FindControl<ComboBox>("ComboBox").SelectedIndex == 6)
            {
                Shape = "Square3";
                Vertices.Clear();
                Vertices = new List<Point>
                {
                    new Point(0, 0),
                    new Point(0, 400),
                    new Point(400, 400),
                    new Point(400, 0),
                };
                DrawChaosGame cg = new DrawChaosGame(Vertices, Canvas, Window);
                Clear();
            }
        }
        /// <summary>
        /// switches between the modes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            if(_mode == 0)
            {
                _mode = 1;
                this.FindControl<Button>("Button").Content = "Mode: 1";
            }
            else
            {
                _mode = 0;
                this.FindControl<Button>("Button").Content = "Mode: 0";
            }
        }
        #endregion

    }
}
