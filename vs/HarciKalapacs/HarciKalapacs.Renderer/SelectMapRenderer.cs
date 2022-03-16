using HarciKalapacs.Renderer.Config;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HarciKalapacs.Renderer
{
    public static class SelectMapRenderer
    {
        public static Canvas SelectMap()
        {
            Canvas mainCanvas = new Canvas();
            mainCanvas.Background = GetImage(SelectMapConfig.BackgroundImageName);

            List<Grid> grids = new List<Grid>();

            // Map buttons.
            //grids.Add(GetGrid("bt1", SelectMapConfig.BtWidth, SelectMapConfig.BtHeight, "Easy", SelectMapConfig.BtImage));
            grids.Add(GetGrid("bt2", SelectMapConfig.BtWidth, SelectMapConfig.BtHeight, "Medium", SelectMapConfig.BtImage));
            //grids.Add(GetGrid("bt3", SelectMapConfig.BtWidth, SelectMapConfig.BtHeight, "Hard", SelectMapConfig.BtImage));

            // MainGrid contains only map buttons.
            Grid mainGrid = GetGrid("mainGrid", MainMenuConfig.WindowWidth - 50, MainMenuConfig.WindowHeight, string.Empty, string.Empty);
            mainGrid.Margin = new Thickness(0, MainMenuConfig.WindowHeight / (grids.Count + 2), 0, 0);
            grids.ForEach(x => mainGrid.Children.Add(x));

            // Buttons' positions.
            double space = 0;   // -MainMenuConfig.WindowWidth / grids.Count;
            foreach (Grid grid in grids)
            {
                grid.Margin = new Thickness(space, 0, 0, 0);
                space += MainMenuConfig.WindowWidth / grids.Count;
            }

            // Back button's properties.
            Grid backButton = GetGrid("btBack", SelectMapConfig.BtWidth, SelectMapConfig.BtHeight, "Vissza", SelectMapConfig.BtImage);
            backButton.Margin = new Thickness(50, MainMenuConfig.WindowHeight - 100, 0, 0);



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
                bitmap.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(image);
                bitmap.EndInit();
                ImageBrush imageBrush = new ImageBrush(bitmap);

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
                HorizontalAlignment = HorizontalAlignment.Center,
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
            (sender as Grid).Background = GetImage(MainMenuConfig.BtImage);
        }

        private static void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as Grid).Background = GetImage(MainMenuConfig.BtSelectImage);
        }
    }
}
