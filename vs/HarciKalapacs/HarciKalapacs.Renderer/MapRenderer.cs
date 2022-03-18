using HarciKalapacs.Renderer.Config;
using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HarciKalapacs.Renderer
{
    public static class MapRenderer
    {
        private static Grid PrevousSelectedTile = null;

        public static Canvas Map(int width, int height, List<Units> units)
        {
            Canvas mainCanvas = new Canvas();
            mainCanvas.Background = Brushes.Black;

            List<Grid> grids = new List<Grid>();

            // Map buttons.
            // Buttons' positions.
            double horizontalSpace = 0;
            double verticalSpace = 0;
            int x = 0;
            int y = 0;
            for (x = 0; x < height; x++)
            {
                horizontalSpace = 0;
                for (y = 0; y < width; y++)
                {
                    // Tile name:  tile_xPos_yPos
                    Grid oneTile = GetGrid("tile_" + x + "_" + y, MapConfig.TileWidth, MapConfig.TileHeight, "", MapConfig.TileInSight);
                    oneTile.Margin = new Thickness(horizontalSpace, verticalSpace, 0, 0);
                    grids.Add(oneTile);
                    horizontalSpace += MapConfig.TileWidth;
                }

                verticalSpace += MapConfig.TileHeight;
            }

            foreach (Units unit in units)
            {
                Grid tileOfUnit = grids.Where(x => x.Name == "tile_" + unit.XPos + "_" + unit.YPos).Select(x => x).FirstOrDefault();
                tileOfUnit.DataContext = unit;
                Thickness unitMargin = tileOfUnit.Margin;
                tileOfUnit.Background = GetImage(GetTileImageName(tileOfUnit));

                Grid soldier = GetGrid("unit_" + unit.XPos + "_" + unit.YPos, MapConfig.TileWidth, MapConfig.TileHeight - 20, "", unit.IdleImage1);
                unitMargin.Top += 10;
                soldier.Margin = unitMargin;
                soldier.IsHitTestVisible = false;
                grids.Add(soldier);
            }

            // MainGrid contains only map buttons.
            Grid mainGrid = GetGrid("mainGrid", MapConfig.TileWidth * width, MapConfig.TileHeight * height, string.Empty, string.Empty);
            double leftMargin = (MainMenuConfig.WindowWidth - width * MapConfig.TileWidth) / 2;
            double topMargin = (MainMenuConfig.WindowHeight - height * MapConfig.TileHeight) / 2;
            mainGrid.Margin = new Thickness(leftMargin, topMargin, 0, 0);
            grids.ForEach(x => mainGrid.Children.Add(x));

            // Back button's properties.
            Grid backButton = GetGrid("btBack", MapConfig.BtWidth, MapConfig.BtHeight, "Vissza a főmenübe", MapConfig.TileImage);
            backButton.Margin = new Thickness(25, MainMenuConfig.WindowHeight - 100, 0, 0);
            backButton.MouseEnter += Grid_MouseEnter;
            backButton.MouseLeave += Grid_MouseLeave;

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

            if (controllerName != "mainGrid")
            {
                grid.MouseLeftButtonDown += Grid_MouseLeftButtonDown;
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
            }

            return grid;
        }

        private static void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;

            // Previous selected tile is now unselected.
            if (PrevousSelectedTile != null)
            {
                PrevousSelectedTile.Background = GetImage(MapConfig.TilePlayer);
            }

            if (grid.DataContext != null)
            {
                Units unit = grid.DataContext as Units;
                if (unit.Team == Team.player)
                {
                    // This tile is the new selected tile.
                    PrevousSelectedTile = sender as Grid;
                    (sender as Grid).Background = GetImage(MapConfig.TileSelected);
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
