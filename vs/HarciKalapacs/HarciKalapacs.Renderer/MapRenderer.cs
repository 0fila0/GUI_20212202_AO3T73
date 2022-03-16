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
                    Grid oneTile = GetGrid("tile_" + x + "_" + y, MapConfig.TileWidth, MapConfig.TileHeight, "", MapConfig.TileImage);
                    oneTile.Margin = new Thickness(horizontalSpace, verticalSpace, 0, 0);
                    grids.Add(oneTile);
                    horizontalSpace += MapConfig.TileWidth;
                }

                verticalSpace += MapConfig.TileHeight;
            }

            foreach (Units unit in units)
            {
                Thickness unitMargin = grids.Where(x => x.Name == "tile_" + unit.XPos + "_" + unit.YPos).Select(x => x.Margin).FirstOrDefault();
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
                grid.MouseEnter += Grid_MouseEnter;
                grid.MouseLeave += Grid_MouseLeave;
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

        private static void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!(sender as Grid).Name.Contains("unit"))
            {
                (sender as Grid).Background = GetImage(MainMenuConfig.BtImage);
            }
        }

        private static void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!(sender as Grid).Name.Contains("unit"))
            {
                (sender as Grid).Background = GetImage(MainMenuConfig.BtSelectImage);
            }
        }
    }
}
