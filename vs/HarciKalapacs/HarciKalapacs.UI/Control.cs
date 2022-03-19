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
    using System.Collections.Generic;
    using HarciKalapacs.Repository.GameElements;
    using System.Windows.Input;
    using HarciKalapacs.Renderer.Config;

    class Control : ContentControl
    {
        enum Contents
        {
            MainMenu = 1, SelectMap = 2, InGame = 3
        }

        static Grid ActualSelectedUnit = null;
        static Grid Destination = null;

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
            MainMenuConfig.WindowWidth = this.ActualWidth;
            MainMenuConfig.WindowHeight = this.ActualHeight;
            MainMenuConfig.TopBtXPos = this.ActualWidth / 2;
            MainMenuConfig.TopBtYPos = this.ActualHeight;
            if (this.ActualContent != Contents.InGame)
            {
                this.ValidateFrame(sender);
            }
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
                    case "bt2":
                        this.model.LoadMap(1);
                        this.ActualContent = Contents.InGame;
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
                    int width = this.model.MapWidth;
                    int height = this.model.MapHeight;
                    this.Content = MapRenderer.Map(width, height, this.model.AllUnits as List<Units>);
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
            mainCanvas.MouseMove += MainCanvas_MouseMove;
            Grid mainGrid = mainCanvas.Children[0] as Grid;

            // Grids in mainGrid.
            foreach (object grid in mainGrid.Children)
            {
                if (grid is Grid)
                {
                    if ((grid as Grid).Name.Contains("unit"))
                    {
                        (grid as Grid).MouseLeftButtonDown += Unit_MouseLeftButtonDown;
                    }
                    else
                    {
                        (grid as Grid).MouseLeftButtonDown += Grid_MouseLeftButtonDown;
                    }
                }
            }

            // Grids out of mainGrid.
            foreach (object grid in mainCanvas.Children)
            {
                if (grid is Grid)
                {
                    (grid as Grid).MouseLeftButtonDown += Grid_MouseLeftButtonDown;
                }
            }
        }

        /// <summary>
        /// Navigation on the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.ActualContent == Contents.InGame)
            {
                Canvas m = this.Content as Canvas;
                Grid g = m.Children[0] as Grid;
                Point mouseCursor = Mouse.GetPosition(this);
                double xMovementSpeed = mouseCursor.X * (this.model.MapWidth * MapConfig.TileWidth / MainMenuConfig.WindowWidth);
                double yMovementSpeed = mouseCursor.Y * (this.model.MapHeight * MapConfig.TileHeight / MainMenuConfig.WindowHeight);
                double leftLimit = this.model.MapWidth * MapConfig.TileWidth / 3;
                double upLimit = this.model.MapHeight * MapConfig.TileHeight / 5;
                g.Margin = new Thickness(leftLimit - xMovementSpeed, upLimit - yMovementSpeed, 0, 0);
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
                    (this.Content as Canvas).Children.Clear();
                    this.ValidateFrame(sender);
                    break;
                case "bt2":
                    this.ValidateFrame(sender);
                    break;
            }
        }

        private void Unit_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}