using System;
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
        private const int gridSize = 10; 
        private const int cellSize = 40; 
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
            
            obstacles.Add(Mouse.GetPosition(CanvasGrid));
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
            
            int[,] waveGrid = new int[gridSize, gridSize];
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    waveGrid[x, y] = int.MaxValue; 
                }
            }

            Queue<Point> wavefront = new Queue<Point>();
            wavefront.Enqueue(startPoint);
            waveGrid[(int)startPoint.X / cellSize, (int)startPoint.Y / cellSize] = 0; // Set start point wave value to 0

           
            while (wavefront.Count > 0)
            {
                Point currentPoint = wavefront.Dequeue();

                // Check neighbors
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int newX = (int)currentPoint.X / cellSize + dx;
                        int newY = (int)currentPoint.Y / cellSize + dy;

                       
                        if (newX >= 0 && newX < gridSize && newY >= 0 && newY < gridSize)
                        {
                            
                            if (!obstacles.Contains(new Point(newX * cellSize, newY * cellSize)) && waveGrid[newX, newY] == int.MaxValue)
                            {
                           
                                waveGrid[newX, newY] = waveGrid[(int)currentPoint.X / cellSize, (int)currentPoint.Y / cellSize] + 1;
                                wavefront.Enqueue(new Point(newX * cellSize, newY * cellSize));
                            }
                        }
                    }
                }
            }

          
            List<Point> shortestPath = new List<Point>();
            Point current = endPoint;
            shortestPath.Add(current);
            while (current != startPoint)
            {
                int minWaveValue = int.MaxValue;
                Point next = current;
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int newX = (int)current.X / cellSize + dx;
                        int newY = (int)current.Y / cellSize + dy;

                        if (newX >= 0 && newX < gridSize && newY >= 0 && newY < gridSize)
                        {
                            if (waveGrid[newX, newY] < minWaveValue)
                            {
                                minWaveValue = waveGrid[newX, newY];
                                next = new Point(newX * cellSize, newY * cellSize);
                            }
                        }
                    }
                }
                current = next;
                shortestPath.Add(current);
            }

            
            DrawShortestPath(shortestPath);
        }

        private void DrawShortestPath(List<Point> path)
        {
            foreach (Point point in path)
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = cellSize,
                    Height = cellSize,
                    Fill = Brushes.Yellow
                };

                Canvas.SetLeft(ellipse, (int)point.X / cellSize * cellSize);
                Canvas.SetTop(ellipse, (int)point.Y / cellSize * cellSize);

                CanvasGrid.Children.Add(ellipse);
            }
        }

        private void CanvasGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
