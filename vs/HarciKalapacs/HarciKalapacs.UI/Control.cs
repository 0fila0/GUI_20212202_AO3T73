namespace HarciKalapacs.UI
{
    using HarciKalapacs.Model;
    using HarciKalapacs.Repository;
    using HarciKalapacs.Renderer;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using System.Windows.Media;
    using System;
    using System.Windows.Threading;
    using SoundsRenderer;

    class Control : ContentControl
    {
        enum Contents
        {
            MainMenu = 1, SelectMap = 2, InGame = 3
        }

        readonly IRepository repository;
        readonly IModel model;
        readonly IMusic musicPlayer;

        public Control()
        {
            this.repository = App.Current.Services.GetService<IRepository>();
            this.model = App.Current.Services.GetService<IModel>();
            this.musicPlayer = App.Current.Services.GetService<IMusic>();

            this.musicPlayer.PlayMusic(MusicType.mainMenu);

            this.Loaded += this.WindowLoaded;
            this.SizeChanged += this.WindowSizeChanged;
        }

        private Window MainWindow { get; set; }

        private Contents ActualContent { get; set; }

        private void WindowSizeChanged(object sender, RoutedEventArgs e)
        {
            this.MainWindow = this.Parent as Window;
            Renderer.Config.MainMenuConfig.WindowWidth = this.ActualWidth;
            Renderer.Config.MainMenuConfig.WindowHeight = this.ActualHeight;
            Renderer.Config.MainMenuConfig.TopBtXPos = this.ActualWidth / 2;
            Renderer.Config.MainMenuConfig.TopBtYPos = this.ActualHeight;
            this.ValidateFrame(sender);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            BitmapImage iconImage = new BitmapImage();
            iconImage.BeginInit();
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HarciKalapacs.UI.Assets.Images.hammer.ico");
            iconImage.StreamSource = stream;
            iconImage.EndInit();
            (this.Parent as Window).Icon = iconImage;

            Window window = Window.GetWindow(this);
        }


        /// <summary>
        /// Screen refresh.
        /// </summary>
        /// <param name="sender">The grid button.</param>
        private void ValidateFrame(object sender)
        {
            // When button clicked.
            if (sender is Grid)
            {
                Grid gridButton = sender as Grid;
                switch (gridButton.Name)
                {
                    case "btNewGame":
                        this.ActualContent = Contents.SelectMap;
                        break;
                    case "btContinueGame":
                        break;
                    case "btBack":
                        this.ActualContent = Contents.MainMenu;
                        break;
                }
            }

            // When window's size changed or button clicked.
            switch (this.ActualContent)
            {
                case Contents.MainMenu:
                    this.Content = MenuRenderer.MainMenu();
                    break;
                case Contents.SelectMap:
                    this.Content = SelectMapRenderer.SelectMap();
                    break;
                case Contents.InGame:
                    break;
                default:
                    this.Content = MenuRenderer.MainMenu();
                    break;
            }

            EventsSubscribe();
        }

        private void EventsSubscribe()
        {
            Canvas mainCanvas = (Canvas)this.Content;
            Grid mainGrid = mainCanvas.Children[0] as Grid;

            // Grids in mainGrid.
            foreach (object grid in mainGrid.Children)
            {
                if (grid is Grid)
                {
                    (grid as Grid).MouseLeftButtonDown += Grid_MouseLeftButtonDown;
                }
            }

            // Grids out of mainGrid.
            foreach (Grid grid in mainCanvas.Children)
            {
                if (grid is Grid)
                {
                    string gridname = (grid as Grid).Name;
                    (grid as Grid).MouseLeftButtonDown += Grid_MouseLeftButtonDown;
                }
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            switch (grid.Name)
            {
                case "btExit":
                    this.MainWindow.Close();
                    break;
                case "btNewGame":
                case "btContinue":
                    this.ValidateFrame(sender);
                    break;
                case "btBack":
                    this.ValidateFrame(sender);
                    break;
                case "bt2":

                    break;
            }
        }
    }
}