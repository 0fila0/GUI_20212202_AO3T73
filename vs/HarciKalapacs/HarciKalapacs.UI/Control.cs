namespace HarciKalapacs.UI
{
    using HarciKalapacs.Model;
    using HarciKalapacs.Renderer;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using SoundsRenderer;
    using System.Collections.Generic;
    using HarciKalapacs.Repository.GameElements;
    using System.Windows.Input;
    using HarciKalapacs.Renderer.Config;
    using HarciKalapacs.Logic;
    using System.Linq;

    class Control : ContentControl
    {
        enum Contents
        {
            MainMenu = 1, SelectMap = 2, InGame = 3
        }

        private static bool NavigationViaMouse = true;
        private static Units actualSelectedUnit = null;

        readonly IModel model;
        readonly IMusic musicPlayer;
        readonly IInGameLogic inGameLogic;
        readonly IGeneralLogic generalLogic;

        public Control()
        {
            this.model = App.Current.Services.GetService<IModel>();
            this.musicPlayer = App.Current.Services.GetService<IMusic>();
            this.inGameLogic = App.Current.Services.GetService<IInGameLogic>();
            this.generalLogic = App.Current.Services.GetService<IGeneralLogic>();

            this.Loaded += this.WindowLoaded;
            this.SizeChanged += this.WindowSizeChanged;

            this.musicPlayer.PlayMusic(MusicType.mainMenu);
        }

        private Window MainWindow { get; set; }

        private Contents ActualContent { get; set; }

        private void GameControl()
        {
            MapRenderer.leftSteps.Content = this.model.LeftSteps + "/" + this.model.MaxSteps + " lépés maradt hátra";
            MapRenderer.playerGolds.Content = this.model.PlayerGold + " arany";
            MapRenderer.roundCounter.Content = this.model.Round + ". kör";

           

            if (this.model.LeftSteps <= 0)
            {
                for (int i = 0; i < this.model.MaxSteps; i++)
                {
                    MapRenderer.VisibleMapTiles();
                    //MapRenderer.MoveUnit();
                    //this.inGameLogic.AIDecisions();
                    
                }
            }
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
                    case "btContinue":
                        this.generalLogic.LoadGame();
                        this.ActualContent = Contents.InGame;
                        break;
                    case "btBack":
                        this.ActualContent = Contents.MainMenu;
                        break;
                    case "map1":
                        this.generalLogic.StartNewGame(1);
                        this.ActualContent = Contents.InGame;
                        break;
                    case "map2":
                        this.generalLogic.StartNewGame(2);
                        this.ActualContent = Contents.InGame;
                        break;
                    case "map3":
                        this.generalLogic.StartNewGame(3);
                        this.ActualContent = Contents.InGame;
                        break;
                }
            }

            // When window's size changed or button clicked.
            switch (this.ActualContent)
            {
                case Contents.MainMenu:
                    this.model.MapNumber = 0;
                    this.Content = MenuRenderer.MainMenu();
                    this.musicPlayer.PlayMusic(MusicType.mainMenu);
                    break;
                case Contents.SelectMap:
                    this.Content = SelectMapRenderer.SelectMap();
                    break;
                case Contents.InGame:
                    int width = this.model.MapWidth;
                    int height = this.model.MapHeight;
                    this.Content = MapRenderer.Map(width, height, this.model.AllUnits.ToList());
                    break;
                default:
                    this.Content = MenuRenderer.MainMenu();
                    this.musicPlayer.PlayMusic(MusicType.mainMenu);
                    break;
            }

            switch (this.model.MapNumber)
            {
                case 1:
                    this.musicPlayer.PlayMusic(MusicType.desert);
                    break;
                case 2:
                    this.musicPlayer.PlayMusic(MusicType.desert);
                    break;
                case 3:
                    this.musicPlayer.PlayMusic(MusicType.desert);
                    break;
            }

            EventsSubscribe();

            if (this.ActualContent == Contents.InGame)
            {
                GameControl();
            }
        }

        private void EventsSubscribe()
        {
            Canvas mainCanvas = (Canvas)this.Content;
            mainCanvas.MouseMove += MainCanvas_MouseMove;
            mainCanvas.MouseRightButtonDown += MainCanvas_MouseRightButtonDown;
            Grid mainGrid = mainCanvas.Children[0] as Grid;

            // Grids in mainGrid.
            foreach (object grid in mainGrid.Children)
            {
                if (grid is Grid)
                {
                    if ((grid as Grid).Name.Contains("unit") || (grid as Grid).Name.Contains("invisible"))
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
                if ((grid is Grid))
                {
                    if (!(grid as Grid).Name.Contains("Panel"))
                    {
                        (grid as Grid).MouseLeftButtonDown += Grid_MouseLeftButtonDown;
                    }
                }
            }

            if (MapRenderer.UnitPanelRightColumn != null)
            {
                foreach (Grid btUpgrade in MapRenderer.UnitPanelRightColumn.Children)
                {
                    btUpgrade.MouseLeftButtonDown += BtUnitPanel_MouseLeftButtonDown;
                }
            }

            if (MapRenderer.UnitPanelLeftColumn != null)
            {
                foreach (object btUpgrade in MapRenderer.UnitPanelLeftColumn.Children)
                {
                    if (btUpgrade is Grid)
                    {
                        (btUpgrade as Grid).MouseLeftButtonDown += BtUnitPanel_MouseLeftButtonDown;
                    }
                }
            }
        }

        private void BtUnitPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool successful = false;
            switch ((sender as Grid).Name)
            {
                case "upgradeDamage":
                    successful = this.inGameLogic.UpgradeDamage(actualSelectedUnit);
                    break;
                case "upgradeHp":
                    successful = this.inGameLogic.UpgradeMaxHp(actualSelectedUnit);
                    break;
                case "upgradeHeal":
                    successful = this.inGameLogic.UpgradeHealer(actualSelectedUnit);
                    break;
                case "airStateButton":
                    successful = true;
                    this.inGameLogic.SwitchVerticalPosition(actualSelectedUnit);
                    MapRenderer.VisibleMapTiles();
                    MapRenderer.CanActivityTiles(actualSelectedUnit);
                    break;
            }

            if (successful)
            {
                this.inGameLogic.StepOccured();
                GameControl();
            }
        }

        private void MainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationViaMouse = !NavigationViaMouse;
        }

        /// <summary>
        /// Navigation on the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (NavigationViaMouse)
            {
                if (this.ActualContent == Contents.InGame)
                {
                    Canvas m = this.Content as Canvas;
                    Grid g = m.Children[0] as Grid;
                    Point mouseCursor = Mouse.GetPosition(this);
                    double xMovementSpeed = mouseCursor.X * (this.model.MapWidth * MapConfig.TileWidth / MainMenuConfig.WindowWidth);
                    double yMovementSpeed = mouseCursor.Y * (this.model.MapHeight * MapConfig.TileHeight / MainMenuConfig.WindowHeight);
                    double leftLimit = this.model.MapWidth * MapConfig.TileWidth / 2;
                    double upLimit = this.model.MapHeight * MapConfig.TileHeight / 4;
                    g.Margin = new Thickness(leftLimit - xMovementSpeed, upLimit - yMovementSpeed, 0, 0);
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
                    (this.Content as Canvas).Children.Clear();
                    this.ValidateFrame(sender);
                    break;
                case "map1":
                    this.ValidateFrame(sender);
                    break;
                case "map2":
                    this.ValidateFrame(sender);
                    break;
                case "map3":
                    this.ValidateFrame(sender);
                    break;
            }
        }

        private void Unit_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Units unit = (sender as Grid).DataContext as Units;
            if (unit == null)
            {
                int x = (int)((sender as Grid).Margin.Left / MapConfig.TileWidth);
                int y = (int)((sender as Grid).Margin.Top / MapConfig.TileHeight);
                unit = (this.model.AllUnits as List<Units>)?.Find(u => u.XPos == x && u.YPos == y);
            }

            // Unselect unit.
            if (actualSelectedUnit == unit)
            {
                actualSelectedUnit = null;
            }

            // Select player unit.
            else if (actualSelectedUnit == null && unit.Team == Team.player)
            {
                actualSelectedUnit = unit;
                if (unit.CanFly)
                {
                    this.musicPlayer.PlaySoundEffect(SoundEffectType.selectHelicopter);
                }
                else if (unit.CanHeal)
                {
                    this.musicPlayer.PlaySoundEffect(SoundEffectType.selectTruck);
                }
                else if (unit.CanAttack)
                {
                    if (unit.UnitType == UnitType.InfantryMan)
                    {
                        this.musicPlayer.PlaySoundEffect(SoundEffectType.selectInfantryman);
                    }
                    else if (unit.UnitType == UnitType.Tank)
                    {
                        this.musicPlayer.PlaySoundEffect(SoundEffectType.selectTank);
                    }
                }
            }

            // Move
            else if (actualSelectedUnit != null && unit == null)
            {
                Grid destination = sender as Grid;
                int x = (int)(destination.Margin.Left / MapConfig.TileWidth);
                int y = (int)(destination.Margin.Top / MapConfig.TileHeight);
                if (this.inGameLogic.Move(actualSelectedUnit, x, y))
                {
                    MapRenderer.MoveUnit();
                    this.inGameLogic.StepOccured();
                    GameControl();
                }
            }

            // Heal any unit.
            else if (actualSelectedUnit != null && actualSelectedUnit is Healer && unit != null)
            {
                if (this.inGameLogic.Heal(actualSelectedUnit, unit))
                {
                    this.inGameLogic.StepOccured();
                    this.musicPlayer.PlaySoundEffect(SoundEffectType.truckFire);
                    GameControl();
                }
            }

            // Attack unit.
            else if (actualSelectedUnit != null && unit.Team != Team.player)
            {
                if (this.inGameLogic.Attack(actualSelectedUnit, unit))
                {
                    if(unit.Hp<= 0)
                    {
                        this.model.AllUnits.Remove(unit);
                    }
                    this.inGameLogic.StepOccured();

                    if (actualSelectedUnit.CanFly)
                    {
                        this.musicPlayer.PlaySoundEffect(SoundEffectType.helicopterFire);
                    }
                    else if (actualSelectedUnit.UnitType == UnitType.Tank)
                    {
                        this.musicPlayer.PlaySoundEffect(SoundEffectType.tankFire);
                    }
                    else
                    {
                        this.musicPlayer.PlaySoundEffect(SoundEffectType.infantrymanFire);
                    }

                    GameControl();
                }
            }
        }

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
    }
}