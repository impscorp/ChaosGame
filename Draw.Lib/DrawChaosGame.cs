using System.Diagnostics;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Path = System.IO.Path;
using Point = Avalonia.Point;

namespace Draw.Lib;

public class DrawChaosGame
{
    
    #region Properties
    static List<Point> _vertices { get; set; }
    
    static Canvas _canvas { get; set; }
    static Window _window { get; set; }
        
    #endregion

    #region Constructor
    public DrawChaosGame(List<Point> v, Canvas c, Window w)
    {
        _vertices = v;
        _canvas = c;
        _window = w;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Paints an ellipse on the canvas at the given coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="c">the Canvas that should be painted</param>
    public static void  PaintPoint(double x, double y)
    {
        var ellipse = new Ellipse
        {
            Width = 2,
            Height = 2,
            Fill = Brushes.White,
            Stroke = Brushes.White,
            StrokeThickness = 1
        };
        _canvas.Children.Add(ellipse);
        Canvas.SetLeft(ellipse, x - ellipse.Width/2);
        Canvas.SetTop(ellipse, y - ellipse.Height/2);
    }
    /// <summary>
    /// draws the base points
    /// </summary>
    public static void BasePoints()
    {
        var vertices = _vertices;
        int j = 0;
        foreach (var p in vertices)
        {
            PaintPoint(p.X,p.Y);
            j++;
        }
    }
    
    /// <summary>
    /// Saves the canvas as a Bitmap and puts it in the background of the window
    /// </summary>
    public static void SaveCanvas()
    {
        var bitmap = new RenderTargetBitmap(new PixelSize((int)_window.Width, (int)_window.Height), new Vector(96, 96));
        bitmap.Render(_canvas);
        //combine two image brushes
        var brush = new ImageBrush(bitmap);
        _window.Background = brush;
        DeleteCanvas();
    }
        
    /// <summary>
    /// Get all ellipses from canvas and delete them
    /// </summary>
    public static void DeleteCanvas()
    {
        var ellipses = _canvas.Children.OfType<Ellipse>().ToList();
        foreach (var ellipse in ellipses)
        {
            _canvas.Children.Remove(ellipse);
        }
    }

    /// <summary>
    /// Clears the canvas
    /// </summary>
    public static void Clear()
    {
        _canvas.Children.Clear();
        BasePoints();
    }

    //generate a random color
    public static Color RandomColor()
    {
        Random random = new Random();
        return Color.FromArgb(255, (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
    }
    /// <summary>
    /// A point inside a pentagon repeatedly jumps half of the distance towards a randomly chosen vertex, but the currently chosen vertex cannot be the same as the previously chosen vertex.
    /// </summary>
    public void GenerateChaosGamePentagon()
    {
        var rnd = new Random();
        var x = 300d;
        var y = 300d;
        var lastPoint = new Point(0,0);
        for (int i = 0; i < 20; i++)
        {
            var nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            while (nextPoint == lastPoint)
            {
                nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            }
            x = (x + nextPoint.X) / 2;
            y = (y + nextPoint.Y) / 2;
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 1;
            ellipse.Height = 1;
            ellipse.Fill = new SolidColorBrush(RandomColor());
            Canvas.SetLeft(ellipse, x - ellipse.Width/2 );
            Canvas.SetTop(ellipse, y - ellipse.Height/2);
            _canvas.Children.Add(ellipse);
            lastPoint = nextPoint;
        }
    }
    /// <summary>
    /// Chaos game for a pentagon with golden ratio
    /// </summary>
    public void GenerateChaosGamePentagongolden()
    {
        var rnd = new Random();
        var x = 300d;
        var y = 300d;
        var lastPoint = new Point(0,0);
        for (int i = 0; i < 20; i++)
        {
            var nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            while (nextPoint == lastPoint)
            {
                nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            }
            x = (x + nextPoint.X) / 1.618;
            y = (y + nextPoint.Y) / 1.618;
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 1;
            ellipse.Height = 1;
            ellipse.Fill = new SolidColorBrush(RandomColor());
            Canvas.SetLeft(ellipse, x - ellipse.Width/2 );
            Canvas.SetTop(ellipse, y - ellipse.Height/2);
            _canvas.Children.Add(ellipse);
            lastPoint = nextPoint;
        }
    }
    /// <summary>
    /// the current point cannot be one place away from the previous vertex (anti-clockwise) 
    /// </summary>
    public void GenerateChaosGameSquareAntiClockwise()
    {
        var rnd = new Random();
        var x = 300d;
        var y = 300d;
        var lastPoint = new Point(0,0);
        for (int i = 0; i < 20; i++)
        {
            var nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            while (nextPoint == _vertices[(_vertices.IndexOf(lastPoint) + 1) % _vertices.Count])
            {
                nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            }
            x = (x + nextPoint.X) / 2;
            y = (y + nextPoint.Y) / 2;
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 1;
            ellipse.Height = 1;
            ellipse.Fill = new SolidColorBrush(RandomColor());
            Canvas.SetLeft(ellipse, x - ellipse.Width/2 );
            Canvas.SetTop(ellipse, y - ellipse.Height/2);
            _canvas.Children.Add(ellipse);
            lastPoint = nextPoint;
        }
    }
    /// <summary>
    /// The Algorithm for the Chaos Game
    /// </summary>
    public void GenerateChaosGame()
    {
        var rnd = new Random();
        var x = 300d;
        var y = 300d;
        for (int i = 0; i < 20; i++)
        {
            var nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            x = (x + nextPoint.X) / 2;
            y = (y + nextPoint.Y) / 2;
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 1;
            ellipse.Height = 1;
            ellipse.Fill = new SolidColorBrush(RandomColor());
            Canvas.SetLeft(ellipse, x - ellipse.Width/2 );
            Canvas.SetTop(ellipse, y - ellipse.Height/2);
            _canvas.Children.Add(ellipse);
        }
    }
    /// <summary>
    /// Create Square Fractal: <br></br>
    /// A point inside a square repeatedly jumps half of the distance towards a randomly chosen vertex, but the currently chosen vertex cannot be 2 places away from the previously chosen vertex.
    /// </summary>
    public void GenerateChaosGameSquare()
    {
        var rnd = new Random();
        var x = 300d;
        var y = 300d;
        var lastPoint = new Point(0,0);
        for (int i = 0; i < 20; i++)
        {
            var nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            while (nextPoint == _vertices[(_vertices.IndexOf(lastPoint) + 2) % _vertices.Count])
            {
                nextPoint = _vertices[rnd.Next(0, _vertices.Count)];
            }
            x = (x + nextPoint.X) / 2;
            y = (y + nextPoint.Y) / 2;
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 1;
            ellipse.Height = 1;
            ellipse.Fill = new SolidColorBrush(RandomColor());
            Canvas.SetLeft(ellipse, x - ellipse.Width/2 );
            Canvas.SetTop(ellipse, y - ellipse.Height/2);
            _canvas.Children.Add(ellipse);
            lastPoint = nextPoint;
        }
    }
}
#endregion