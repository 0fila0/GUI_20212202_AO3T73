namespace HarciKalapacs.Renderer
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using HarciKalapacs.Renderer.Config;

    public static class MenuRenderer
    {
        public static Canvas MainMenu()
        {
            Canvas mainCanvas = new Canvas();
            mainCanvas.Background = GetImage(MainMenuConfig.BackgroundImageName);

            List<Grid> grids = new List<Grid>();
            grids.Add(GetGrid("btContinue", MainMenuConfig.BtWidth, MainMenuConfig.BtHeight, "Folytatás", MainMenuConfig.BtImage));
            grids.Add(GetGrid("btNewGame", MainMenuConfig.BtWidth, MainMenuConfig.BtHeight, "Új játék", MainMenuConfig.BtImage));
            grids.Add(GetGrid("btExit", MainMenuConfig.BtWidth, MainMenuConfig.BtHeight, "Kilépés", MainMenuConfig.BtImage));

            Grid mainGrid = GetGrid("mainGrid", MainMenuConfig.WindowWidth, MainMenuConfig.WindowHeight, string.Empty, string.Empty);
            mainGrid.Margin = new Thickness(0, MainMenuConfig.WindowHeight / (grids.Count + 2), 0, 0);
            grids.ForEach(x => mainGrid.Children.Add(x));

            double space = 0;
            foreach (Grid grid in grids)
            {
                grid.Margin = new Thickness(0, space, 0, 0);
                space += MainMenuConfig.SpaceBetweenButtons;
            }

            mainCanvas.Children.Add(mainGrid);
            return mainCanvas;
        }

        private static Canvas SelectMap()
        {
            Canvas canvas = new Canvas();
            return canvas;
        }

        private static Canvas HelpMenu()
        {
            Canvas canvas = new Canvas();
            return canvas;
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

                if (!image.Contains("mainMenu"))
                {
                    imageBrush.Stretch = Stretch.UniformToFill;
                    imageBrush.AlignmentX = AlignmentX.Center;
                    imageBrush.AlignmentY = AlignmentY.Center;
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
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            if (controllerName != "mainGrid")
            {
                grid.MouseLeftButtonDown += Grid_MouseLeftButtonDown;
                grid.MouseEnter += Grid_MouseEnter;
                grid.MouseLeave += Grid_MouseLeave;
            }

            Label label = new Label
            {
                Content = text,
                FontFamily = MainMenuConfig.BtFontFamily,
                FontSize = MainMenuConfig.BtFontSize,
                Foreground = MainMenuConfig.FontColor,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(label);
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

        private static void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
