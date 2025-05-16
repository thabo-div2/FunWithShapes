using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FunWithShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum SelectedShape 
        {
            Circle,
            Rectangle,
            Line
        }

        private SelectedShape _currentShape;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CircleOption_Click(object sender, RoutedEventArgs e)
        {
            _currentShape = SelectedShape.Circle;
        }

        private void RectOption_Click(object sender, RoutedEventArgs e)
        {
            _currentShape = SelectedShape.Rectangle;
        }

        private void LineOption_Click(object sender, RoutedEventArgs e)
        {
            _currentShape= SelectedShape.Line;
        }

        private void flipCanvas_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int randomNum = random.Next(-100, 100);
            if (flipCanvas.IsChecked == true)
            {
                RotateTransform rotate = new RotateTransform(randomNum);
                canvasDrawingArea.LayoutTransform = rotate;
            }

            else 
            {
                canvasDrawingArea.LayoutTransform = null;
            }
        }

        private void CanvasDrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape shapeToRender = null;

            switch (_currentShape) 
            {
                case SelectedShape.Circle:
                    shapeToRender = new Ellipse() { Height = 30, Width = 30 };
                    RadialGradientBrush brush = new RadialGradientBrush();
                    brush.GradientStops.Add(new GradientStop(
                        (Color)ColorConverter.ConvertFromString("#FF00B952"), 0));
                    brush.GradientStops.Add(new GradientStop(
                        (Color)ColorConverter.ConvertFromString("#FF9A1010"), 1));
                    brush.GradientStops.Add(new GradientStop(
                        (Color)ColorConverter.ConvertFromString("#FFE3C815"), 0.5));
                    shapeToRender.Fill = brush;
                    break;
                case SelectedShape.Rectangle:
                    shapeToRender = new Rectangle() { Fill = Brushes.Green, Height = 35, Width = 50, RadiusX = 10, RadiusY = 10 };
                    break;
                case SelectedShape.Line:
                    shapeToRender = new Line()
                    {
                        Stroke = Brushes.Blue,
                        StrokeThickness = 10,
                        X1 = 0,
                        X2 = 50,
                        Y1 = 0,
                        Y2 = 50,
                        StrokeStartLineCap = PenLineCap.Triangle,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    break;
                default:
                    return;
            }

            Canvas.SetLeft(shapeToRender, e.GetPosition(canvasDrawingArea).X);
            Canvas.SetTop(shapeToRender, e.GetPosition(canvasDrawingArea).Y);

            canvasDrawingArea.Children.Add(shapeToRender);
        }

        private void CanvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((Canvas)sender);
            HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, pt);

            if (result != null) 
            {
                canvasDrawingArea.Children.Remove(result.VisualHit as Shape);
            }
        }
    }
}
