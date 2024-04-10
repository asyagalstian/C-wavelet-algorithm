using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WaveletAlgorithmPathfinding
{
    public partial class MainWindow : Window
    {
        private const int gridSize = 10; // Change grid size as needed
        private const int cellSize = 40; // Change cell size as needed
        private Point startPoint;
        private Point endPoint;
        private List<Point> obstacles = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
            DrawGrid();
        }

        private void DrawGrid()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = cellSize,
                        Height = cellSize,
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 1,
                        Fill = Brushes.White
                    };

                    Canvas.SetLeft(rect, x * cellSize);
                    Canvas.SetTop(rect, y * cellSize);
                    CanvasGrid.Children.Add(rect);
                }
            }
        }

        private void AddObstacleButton_Click(object sender, RoutedEventArgs e)
        {
            // Add obstacle to the list
            obstacles.Add(Mouse.GetPosition(CanvasGrid));

            // Draw obstacle on the canvas
            DrawObstacle(Mouse.GetPosition(CanvasGrid));
        }

        private void DrawObstacle(Point position)
        {
            Ellipse obstacle = new Ellipse
            {
                Width = cellSize,
                Height = cellSize,
                Fill = Brushes.Red
            };

            Canvas.SetLeft(obstacle, (int)position.X / cellSize * cellSize);
            Canvas.SetTop(obstacle, (int)position.Y / cellSize * cellSize);

            CanvasGrid.Children.Add(obstacle);
        }

        private void SetStartPointButton_Click(object sender, RoutedEventArgs e)
        {
            startPoint = Mouse.GetPosition(CanvasGrid);
            DrawPoint(startPoint, Brushes.Green);
        }

        private void SetEndPointButton_Click(object sender, RoutedEventArgs e)
        {
            endPoint = Mouse.GetPosition(CanvasGrid);
            DrawPoint(endPoint, Brushes.Blue);
        }

        private void DrawPoint(Point point, SolidColorBrush color)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = cellSize,
                Height = cellSize,
                Fill = color
            };

            Canvas.SetLeft(ellipse, (int)point.X / cellSize * cellSize);
            Canvas.SetTop(ellipse, (int)point.Y / cellSize * cellSize);

            CanvasGrid.Children.Add(ellipse);
        }

        private void FindShortestPathButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement wavelet algorithm to find the shortest path
        }

        private void CanvasGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point clickedPoint = e.GetPosition(CanvasGrid);
            int gridX = (int)clickedPoint.X / cellSize;
            int gridY = (int)clickedPoint.Y / cellSize;
            Point closestGridPoint = new Point(gridX * cellSize, gridY * cellSize);
            DrawObstacle(closestGridPoint);
            obstacles.Add(closestGridPoint);
        }
    }
}
