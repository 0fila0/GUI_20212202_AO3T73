using HarciKalapacs.Renderer.Config;
using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HarciKalapacs.Renderer
{
    public static class MapRenderer
    {
        private static Grid ActualSelectedUnit;
        private static List<Grid> UnitGrids = new List<Grid>();
        private static List<Grid> InvisibleTiles = new List<Grid>();

        public static Canvas Map(int width, int height, List<Units> units)
        {
            Canvas mainCanvas = new Canvas();
            mainCanvas.Background = Brushes.Black;

            List<Grid> grids = new List<Grid>();

            foreach (Units unit in units)
            {
                Grid tileOfUnit = GetGrid("unit_" + unit.YPos + "_" + unit.XPos, MapConfig.TileWidth, MapConfig.TileHeight, string.Empty, unit.IdleImage1);
                tileOfUnit.Margin = new Thickness(unit.XPos * MapConfig.TileHeight, unit.YPos * MapConfig.TileWidth, 0, 0);
                tileOfUnit.DataContext = unit;
                grids.Add(tileOfUnit);
                UnitGrids.Add(tileOfUnit);
            }

            // Map buttons.
            // Buttons' positions.
            double horizontalSpace = 0;
            double verticalSpace = 0;
            int x = 0;
            int y = 0;
            for (y = 0; y < height; y++)
            {
                horizontalSpace = 0;
                for (x = 0; x < width; x++)
                {
                    // Tile name:  tile_xPos_yPos
                    Grid mapTile = GetGrid("invisibleTile_" + x + "_" + y, MapConfig.TileWidth, MapConfig.TileHeight, "", MapConfig.TileOutOfSight);
                    mapTile.Margin = new Thickness(horizontalSpace, verticalSpace, 0, 0);
                    grids.Add(mapTile);
                    InvisibleTiles.Add(mapTile);
                    horizontalSpace += MapConfig.TileWidth;
                }

                verticalSpace += MapConfig.TileHeight;
            }


            // MainGrid contains only map buttons.
            Grid mainGrid = GetGrid("mainGrid", MapConfig.TileWidth * width, MapConfig.TileHeight * height, string.Empty, MapConfig.TileInSight);
            double leftMargin = (MainMenuConfig.WindowWidth - width * MapConfig.TileWidth) / 2;
            double topMargin = (MainMenuConfig.WindowHeight - height * MapConfig.TileHeight) / 2;
            mainGrid.Margin = new Thickness(leftMargin, topMargin, 0, 0);

            // Create selectedUnit marker.
            Grid selectedGrid = GetGrid("selectedGrid", MapConfig.TileWidth, MapConfig.TileHeight, string.Empty, MapConfig.TileSelected);
            mainGrid.Children.Add(selectedGrid);
            ActualSelectedUnit = selectedGrid;
            ActualSelectedUnit.Visibility = Visibility.Hidden;

            grids.ForEach(x => mainGrid.Children.Add(x));

            // Back button's properties.
            Grid backButton = GetGrid("btBack", MapConfig.BtWidth, MapConfig.BtHeight, "Vissza a főmenübe", MapConfig.TileImage);
            backButton.Margin = new Thickness(25, MainMenuConfig.WindowHeight - 100, 0, 0);
            backButton.MouseEnter += Grid_MouseEnter;
            backButton.MouseLeave += Grid_MouseLeave;

            VisibleMapTiles();
            mainCanvas.Children.Add(mainGrid);
            mainCanvas.Children.Add(backButton);
            return mainCanvas;
        }

        private static ImageBrush GetImage(string image)
        {
            if (image != string.Empty)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                try
                {
                    bitmap.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(image);
                    bitmap.EndInit();
                }
                catch (Exception)
                {
                }

                ImageBrush imageBrush = new ImageBrush();
                if (bitmap.StreamSource == null)
                {
                    imageBrush.ImageSource = new BitmapImage(new Uri(image));
                }
                else
                {
                    imageBrush = new ImageBrush(bitmap);
                }

                return imageBrush;
            }

            return null;
        }

        /// <summary>
        /// Create a new grid. It functions as a button.
        /// </summary>
        /// <param name="controllerName">Name of grid regarding future references.</param>
        /// <param name="width">Width of grid.</param>
        /// <param name="height">Height of grid.</param>
        /// <param name="text">Text in grid.</param>
        /// <param name="image">Background image of grid.</param>
        /// <returns></returns>
        private static Grid GetGrid(string controllerName, double width, double height, string text, string image)
        {
            Grid grid = new Grid
            {
                Name = controllerName,
                Width = width,
                Height = height,
                Background = GetImage(image),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            if (!(grid.Name.Contains("Back")))
            {
                grid.MouseLeftButtonDown += Grid_MouseLeftButtonDown;
            }

            Label label = new Label
            {
                Content = text,
                FontFamily = MainMenuConfig.BtFontFamily,
                FontSize = MainMenuConfig.BtFontSize,
                Foreground = MainMenuConfig.FontColor,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            grid.Children.Add(label);

            return grid;
        }

        private static void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid gridSender = sender as Grid;
            if (!gridSender.Name.Contains("main"))
            {
                if (gridSender.DataContext != null)
                {
                    Units unit = gridSender.DataContext as Units;
                    if (unit.Team == Team.player)
                    {
                        if (ActualSelectedUnit.DataContext != gridSender.DataContext)
                        {
                            ActualSelectedUnit.DataContext = gridSender.DataContext;
                            ActualSelectedUnit.Margin = gridSender.Margin;
                            ActualSelectedUnit.Visibility = Visibility.Visible;
                            gridSender.Background = GetImage((gridSender.DataContext as Units).IdleImage1);
                        }
                        else
                        {
                            ActualSelectedUnit.DataContext = null;
                            ActualSelectedUnit.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
        }

        private static void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as Grid).Background = GetImage(MainMenuConfig.BtImage);
        }

        private static void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as Grid).Background = GetImage(MainMenuConfig.BtSelectImage);
        }

        private static void MoveUnit(Grid grid)
        {
            Units u = grid.DataContext as Units;
            double x = u.XPos * MapConfig.TileHeight;
            double y = u.YPos * MapConfig.TileWidth;
            u.XPos += 1;
            double xdes = u.XPos * MapConfig.TileHeight;
            double ydes = u.YPos * MapConfig.TileWidth;
            Thickness destination = new Thickness(xdes, ydes, 0, 0);
            grid.Margin = destination;
        }

        private static void VisibleMapTiles()
        {
            foreach (Grid unitGrid in UnitGrids)
            {
                Units unit = unitGrid.DataContext as Units;
                if (unit.Team == Team.player && unit is Controllable)
                {
                    Controllable controllableUnit = unit as Controllable;
                    HorizontalVision(controllableUnit);
                    VerticalVision(controllableUnit);
                    DiagonalVision(controllableUnit);
                }
            }
        }

        private static void HorizontalVision(Controllable controllableUnit)
        {
            for (int leftRight = -1; leftRight <= 1; leftRight += 2)
            {
                bool foundObstacle = false;
                int vision = 0;

                // Right then left vision.
                while (!foundObstacle && Math.Abs(vision) <= controllableUnit.Vision)
                {
                    Grid inSight = InvisibleTiles.FirstOrDefault(x => x.Name.Contains((controllableUnit.XPos + vision) + "_" + controllableUnit.YPos));
                    if (inSight != null)
                    {
                        inSight.Visibility = Visibility.Hidden;
                        Grid inSightUnit = UnitGrids.FirstOrDefault(x => x.Margin == inSight.Margin);
                        if (inSightUnit != null && inSightUnit.DataContext is Obstacle && (!(controllableUnit is AirUnit) || (controllableUnit is AirUnit) && !(controllableUnit as AirUnit).IsInTheAir))
                        {
                            foundObstacle = true;
                        }
                    }

                    // End of map
                    else
                    {
                        foundObstacle = true;
                    }

                    vision += leftRight;
                }
            }
        }

        private static void VerticalVision(Controllable controllableUnit)
        {
            for (int downUp = -1; downUp <= 1; downUp += 2)
            {
                bool foundObstacle = false;
                int vision = 0;

                // Right then left vision.
                while (!foundObstacle && Math.Abs(vision) <= controllableUnit.Vision)
                {
                    Grid inSight = InvisibleTiles.FirstOrDefault(x => x.Name.Contains(controllableUnit.XPos + "_" + (controllableUnit.YPos + vision)));
                    if (inSight != null)
                    {
                        inSight.Visibility = Visibility.Hidden;
                        Grid inSightUnit = UnitGrids.FirstOrDefault(x => x.Margin == inSight.Margin);
                        if (inSightUnit != null && inSightUnit.DataContext is Obstacle && (!(controllableUnit is AirUnit) || (controllableUnit is AirUnit) && !(controllableUnit as AirUnit).IsInTheAir))
                        {
                            foundObstacle = true;
                        }
                    }

                    // End of map
                    else
                    {
                        foundObstacle = true;
                    }

                    vision += downUp;
                }
            }
        }

        private static void DiagonalVision(Controllable controllableUnit)
        {
            for (int x = -1; x <= 1; x += 2)
            {
                for (int y = -1; y <= 1; y += 2)
                {
                    Grid inSight = InvisibleTiles.FirstOrDefault(grid => grid.Name.Contains((controllableUnit.XPos + x) + "_" + (controllableUnit.YPos + y)));

                    if (inSight != null)
                    {
                        inSight.Visibility = Visibility.Hidden;
                        Grid inSightUnit = UnitGrids.FirstOrDefault(x => x.Margin == inSight.Margin);
                        if (!(inSightUnit != null && inSightUnit.DataContext is Obstacle && (!(controllableUnit is AirUnit) || (controllableUnit is AirUnit) && !(controllableUnit as AirUnit).IsInTheAir)))
                        {
                            Grid diagonal_1 = InvisibleTiles.FirstOrDefault(grid => grid.Name.Contains((controllableUnit.XPos + x + x) + "_" + (controllableUnit.YPos + y)));
                            if (diagonal_1 != null)
                            {
                                diagonal_1.Visibility = Visibility.Hidden;
                            }

                            Grid diagonal_2 = InvisibleTiles.FirstOrDefault(grid => grid.Name.Contains((controllableUnit.XPos + x) + "_" + (controllableUnit.YPos + y + y)));
                            if (diagonal_2 != null)
                            {
                                diagonal_2.Visibility = Visibility.Hidden;
                            }
                        }
                    }
                }
            }
        }

        private static string GetTileImageName(Grid grid)
        {
            if (grid.DataContext != null)
            {
                Units unit = grid.DataContext as Units;

                // Set mapTile image depends on unit's team.
                switch (unit.Team)
                {
                    case Team.player:
                        return MapConfig.TilePlayer;
                    case Team.enemy:
                        return MapConfig.TileEnemy;
                    case Team.natural:
                        return MapConfig.TileNature;
                    default:
                        return MapConfig.TileInSight;
                }
            }
            // If tile is in sight or not
            else
            {
                if (IsInSight(grid))
                {
                    return MapConfig.TileInSight;
                }
                else
                {
                    return MapConfig.TileOutOfSight;
                }
            }
        }

        private static bool IsInSight(Grid grid)
        {
            return true;
        }
    }
}
