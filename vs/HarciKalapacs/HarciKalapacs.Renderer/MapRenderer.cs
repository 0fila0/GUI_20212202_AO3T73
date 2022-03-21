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
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HarciKalapacs.Renderer
{
    public static class MapRenderer
    {
        public static StackPanel UnitPanelRightColumn;
        public static StackPanel UnitPanelLeftColumn;
        public static Label roundCounter;
        public static Label leftSteps;
        public static Label playerGolds;

        private static Grid ActualSelectedUnit;
        private static List<Grid> UnitGrids = new List<Grid>();
        private static List<Grid> InvisibleTiles = new List<Grid>();
        private static Grid UnitPanel;
        private static StackPanel UnitPanelCenterColumn;
        private static Label UnitName;
        // private static Label UnitLevel;
        private static double MapTileOpacity = 0.4;

        public static Canvas Map(int width, int height, List<Units> units)
        {
            UnitGrids.Clear();
            InvisibleTiles.Clear();

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
                    // mapTile.IsHitTestVisible = false;
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


            // ************** Unit panel
            UnitPanel = GetGrid("unitPanel", MapConfig.GridUnitPanelWidth, MapConfig.GridUnitPanelHeight, string.Empty, MapConfig.GridUnitPanelBackground);
            UnitPanel.Margin = new Thickness(MainMenuConfig.WindowWidth - MapConfig.GridUnitPanelWidth, MainMenuConfig.WindowHeight - MapConfig.GridUnitPanelHeight, 0, 0);
            UnitPanel.Visibility = Visibility.Hidden;

            DockPanel columns = new DockPanel();
            columns.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel infos = new StackPanel();

            // Unit name
            UnitName = new Label();
            UnitName.FontSize = MapConfig.GridUnitPanelFontSize;
            UnitName.FontFamily = MapConfig.FontFamily;
            UnitName.HorizontalAlignment = HorizontalAlignment.Center;

            // Values.
            StackPanel leftColumn = new StackPanel();
            leftColumn.Width = MapConfig.GridUnitPanelWidth / 3;
            leftColumn.Margin = new Thickness(40, 20, 0, 0);
            leftColumn.VerticalAlignment = VerticalAlignment.Top;
            leftColumn.HorizontalAlignment = HorizontalAlignment.Left;
            UnitPanelLeftColumn = leftColumn;

            // Symbols.
            StackPanel centerColumn = new StackPanel();
            centerColumn.Width = MapConfig.GridUnitPanelWidth / 3 - 50;
            centerColumn.Margin = new Thickness(20, 20, 0, 0);
            centerColumn.VerticalAlignment = VerticalAlignment.Top;
            centerColumn.HorizontalAlignment = HorizontalAlignment.Left;
            UnitPanelCenterColumn = centerColumn;

            // Upgrade buttons.
            StackPanel rightColumn = new StackPanel();
            rightColumn.Width = MapConfig.GridUnitPanelWidth / 3;
            rightColumn.Margin = new Thickness(20, 20, 0, 0);
            rightColumn.VerticalAlignment = VerticalAlignment.Top;
            rightColumn.HorizontalAlignment = HorizontalAlignment.Left;
            UnitPanelRightColumn = rightColumn;

            Label hpValue = new Label();
            hpValue.Name = "hpLabel";
            hpValue.FontSize = MapConfig.GridUnitPanelFontSize;
            hpValue.FontFamily = MapConfig.FontFamily;
            Label hpSymbol = new Label();
            hpSymbol.Content = "❤";
            hpSymbol.FontSize = MapConfig.GridUnitPanelFontSize;
            hpSymbol.FontFamily = MapConfig.FontFamily;
            Grid upgradeHp = GetGrid("upgradeHp", MapConfig.GridUnitPanelButtonWidth, MapConfig.GridUnitPanelButtonHeight, string.Empty, MapConfig.BtUpgradeBackground);
            upgradeHp.Margin = new Thickness(5);

            Label heal = new Label();
            heal.Name = "healLabel";
            heal.FontSize = MapConfig.GridUnitPanelFontSize;
            heal.FontFamily = MapConfig.FontFamily;
            Grid upgradeHeal = GetGrid("upgradeHeal", MapConfig.GridUnitPanelButtonWidth, MapConfig.GridUnitPanelButtonHeight, string.Empty, MapConfig.BtUpgradeBackground);
            upgradeHeal.Margin = new Thickness(5);

            Label damage = new Label();
            damage.Name = "damageLabel";
            damage.FontSize = MapConfig.GridUnitPanelFontSize;
            damage.FontFamily = MapConfig.FontFamily;
            Grid upgradeDamage = GetGrid("upgradeDamage", MapConfig.GridUnitPanelButtonWidth, MapConfig.GridUnitPanelButtonHeight, string.Empty, MapConfig.BtUpgradeBackground);
            upgradeDamage.Margin = new Thickness(5);
            Label healDamageSymbol = new Label();
            healDamageSymbol.FontSize = MapConfig.GridUnitPanelFontSize;
            healDamageSymbol.FontFamily = MapConfig.FontFamily;

            Grid airState = GetGrid("airStateButton", 50, 50, string.Empty, MapConfig.TileEnemy);
            airState.Margin = new Thickness(0, 30, 0, 0);
            airState.MouseLeftButtonDown += BtAirUnitSpecial_MouseLeftButtonDown;

            leftColumn.Children.Add(hpValue);
            leftColumn.Children.Add(heal);
            leftColumn.Children.Add(damage);
            leftColumn.Children.Add(airState);

            centerColumn.Children.Add(hpSymbol);
            centerColumn.Children.Add(healDamageSymbol);

            rightColumn.Children.Add(upgradeHp);
            rightColumn.Children.Add(upgradeHeal);
            rightColumn.Children.Add(upgradeDamage);
            foreach (Grid btUpgrade in rightColumn.Children)
            {
                btUpgrade.MouseEnter += Grid_MouseEnter;
                btUpgrade.MouseLeave += Grid_MouseLeave;
            }

            columns.Children.Add(leftColumn);
            columns.Children.Add(centerColumn);
            columns.Children.Add(rightColumn);

            infos.Children.Add(UnitName);
            infos.Children.Add(columns);

            UnitPanel.Children.Add(infos);
            // ************** Unit panel

            // Back button's properties.
            Grid backButton = GetGrid("btBack", MapConfig.BtGeneralWidth, MapConfig.BtGeneralHeight, "Főmenü", MapConfig.TileImage);
            backButton.Margin = new Thickness(MainMenuConfig.WindowWidth - 150, 0, 0, 0);
            backButton.MouseEnter += Grid_MouseEnter;
            backButton.MouseLeave += Grid_MouseLeave;

            // Header's properties.
            Grid header = GetGrid("header", MapConfig.GridHeaderWidth, MapConfig.GridHeaderHeight, string.Empty, string.Empty);
            header.Margin = new Thickness(0);
            header.Background = Brushes.Gray;
            leftSteps = new Label();
            leftSteps.Name = "leftSteps";
            leftSteps.FontFamily = MapConfig.FontFamily;
            leftSteps.FontSize = MapConfig.BtFontSize;
            leftSteps.Margin = new Thickness(25, 0, 0, 0);
            leftSteps.HorizontalAlignment = HorizontalAlignment.Left;
            leftSteps.VerticalAlignment = VerticalAlignment.Center;
            roundCounter = new Label();
            roundCounter.Name = "round";
            roundCounter.FontFamily = MapConfig.FontFamily;
            roundCounter.FontSize = MapConfig.BtFontSize;
            roundCounter.Margin = new Thickness(MainMenuConfig.WindowWidth / 2, 0, 0, 0);
            roundCounter.HorizontalAlignment = HorizontalAlignment.Left;
            roundCounter.VerticalAlignment = VerticalAlignment.Center;
            playerGolds = new Label();
            playerGolds.Name = "round";
            playerGolds.FontFamily = MapConfig.FontFamily;
            playerGolds.FontSize = MapConfig.BtFontSize;
            playerGolds.Margin = new Thickness(MainMenuConfig.WindowWidth / 4, 0, 0, 0);
            playerGolds.HorizontalAlignment = HorizontalAlignment.Left;
            playerGolds.VerticalAlignment = VerticalAlignment.Center;
            header.Children.Add(leftSteps);
            header.Children.Add(roundCounter);
            header.Children.Add(playerGolds);

            VisibleMapTiles();
            mainCanvas.Children.Add(mainGrid);
            mainCanvas.Children.Add(header);
            mainCanvas.Children.Add(UnitPanel);
            mainCanvas.Children.Add(backButton);
            return mainCanvas;
        }

        public static void VisibleMapTiles()
        {
            foreach (Grid invisible in InvisibleTiles)
            {
                invisible.Background = GetImage(MapConfig.TileOutOfSight);
                invisible.Opacity = 1;
                invisible.Visibility = Visibility.Visible;
            }

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

        public static void CanActivityTiles(Controllable unit)
        {
            HorizontalWhereCanMove(unit);
            HorizontalAttack(unit);
            VerticalWhereCanMove(unit);
            VerticalAttack(unit);

            // DiagonalActivities(unit);
        }

        private static void HorizontalWhereCanMove(Controllable selectedUnit)
        {
            // if it is an airunit and it is on the ground it can't move.
            if (!(selectedUnit is AirUnit) || (selectedUnit as AirUnit).IsInTheAir)
            {
                for (int leftRight = -1; leftRight <= 1; leftRight += 2)
                {
                    bool foundObstacle = false;
                    int move = 0;
                    if (leftRight < 0)
                    {
                        move = -1;
                    }
                    else
                    {
                        move = 1;
                    }

                    // Right then left move.
                    while (!foundObstacle && Math.Abs(move) <= selectedUnit.MaxMove)
                    {
                        Grid inSight = InvisibleTiles.FirstOrDefault(x => x.Name.Contains((selectedUnit.XPos + move) + "_" + selectedUnit.YPos));

                        // inSight == null means it is the end of the map.
                        if (inSight != null)
                        {
                            Grid inSightUnitGrid = UnitGrids.FirstOrDefault(x => x.Margin == inSight.Margin);

                            // if there is a unit
                            if (inSightUnitGrid != null)
                            {
                                Units inSightUnit = inSightUnitGrid.DataContext as Units;
                                if (!(selectedUnit is AirUnit))
                                {
                                    foundObstacle = true;
                                }
                            }
                            else
                            {
                                inSight.Visibility = Visibility.Visible;
                                inSight.Opacity = MapTileOpacity;
                                inSight.Background = GetImage(MapConfig.TileCanMoveHere);
                            }
                        }

                        move += leftRight;
                    }
                }
            }
        }

        private static void HorizontalAttack(Controllable selectedUnit)
        {
            // if it is an airunit and it is on the ground it can't move.
            if (!(selectedUnit is AirUnit) || (selectedUnit as AirUnit).IsInTheAir)
            {
                for (int leftRight = -1; leftRight <= 1; leftRight += 2)
                {
                    int range = 0;
                    if (leftRight < 0)
                    {
                        range = -1;
                    }
                    else
                    {
                        range = 1;
                    }

                    // Right then left move.
                    while (Math.Abs(range) <= selectedUnit.Vision)
                    {
                        Grid inRange = InvisibleTiles.FirstOrDefault(x => x.Name.Contains((selectedUnit.XPos + range) + "_" + selectedUnit.YPos));

                        // inSight == null means it is the end of the map.
                        if (inRange != null)
                        {
                            Grid inRangeUnitGrid = UnitGrids.FirstOrDefault(x => x.Margin == inRange.Margin);

                            // if there is a unit
                            if (inRangeUnitGrid != null)
                            {
                                Units inRangeUnit = inRangeUnitGrid.DataContext as Units;
                                if (inRangeUnit.Team == Team.enemy)
                                {
                                    inRange.Visibility = Visibility.Visible;
                                    inRange.Opacity = MapTileOpacity;
                                    inRange.Background = GetImage(MapConfig.TileEnemy);
                                }
                                else if (inRangeUnit.Team == Team.natural)
                                {
                                    inRange.Visibility = Visibility.Visible;
                                    inRange.Opacity = MapTileOpacity;
                                    inRange.Background = GetImage(MapConfig.TileNature);
                                }
                                else if (selectedUnit is Healer)
                                {
                                    inRange.Visibility = Visibility.Visible;
                                    inRange.Opacity = MapTileOpacity;
                                    inRange.Background = GetImage(MapConfig.TilePlayer);
                                }
                            }
                        }

                        range += leftRight;
                    }
                }
            }
        }

        private static void VerticalWhereCanMove(Controllable selectedUnit)
        {
            // if it is an airunit and it is on the ground it can't move.
            if (!(selectedUnit is AirUnit) || (selectedUnit as AirUnit).IsInTheAir)
            {
                for (int downUp = -1; downUp <= 1; downUp += 2)
                {
                    bool foundObstacle = false;
                    int move = 0;
                    if (downUp < 0)
                    {
                        move = -1;
                    }
                    else
                    {
                        move = 1;
                    }

                    // Down then up move.
                    while (!foundObstacle && Math.Abs(move) <= selectedUnit.MaxMove)
                    {
                        Grid inSight = InvisibleTiles.FirstOrDefault(x => x.Name.Contains(selectedUnit.XPos + "_" + (selectedUnit.YPos + move)));

                        // inSight == null means it is the end of the map.
                        if (inSight != null)
                        {
                            Grid inSightUnitGrid = UnitGrids.FirstOrDefault(x => x.Margin == inSight.Margin);

                            // if there is a unit
                            if (inSightUnitGrid != null)
                            {
                                Units inSightUnit = inSightUnitGrid.DataContext as Units;
                                if (!(selectedUnit is AirUnit))
                                {
                                    foundObstacle = true;
                                }
                            }
                            else
                            {
                                inSight.Visibility = Visibility.Visible;
                                inSight.Opacity = MapTileOpacity;
                                inSight.Background = GetImage(MapConfig.TileCanMoveHere);
                            }
                        }

                        move += downUp;
                    }
                }
            }
        }

        private static void VerticalAttack(Controllable selectedUnit)
        {
            // if it is an airunit and it is on the ground it can't move.
            if (!(selectedUnit is AirUnit) || (selectedUnit as AirUnit).IsInTheAir)
            {
                for (int downUp = -1; downUp <= 1; downUp += 2)
                {
                    int range = 0;
                    if (downUp < 0)
                    {
                        range = -1;
                    }
                    else
                    {
                        range = 1;
                    }

                    // Down then up attack.
                    while (Math.Abs(range) <= selectedUnit.Vision)
                    {
                        Grid inRange = InvisibleTiles.FirstOrDefault(x => x.Name.Contains(selectedUnit.XPos + "_" + selectedUnit.YPos + range));

                        // inSight == null means it is the end of the map.
                        if (inRange != null)
                        {
                            Grid inRangeUnitGrid = UnitGrids.FirstOrDefault(x => x.Margin == inRange.Margin);

                            // if there is a unit
                            if (inRangeUnitGrid != null)
                            {
                                Units inRangeUnit = inRangeUnitGrid.DataContext as Units;
                                if (inRangeUnit.Team == Team.enemy)
                                {
                                    inRange.Visibility = Visibility.Visible;
                                    inRange.Opacity = MapTileOpacity;
                                    inRange.Background = GetImage(MapConfig.TileEnemy);
                                }
                                else if (inRangeUnit.Team == Team.natural)
                                {
                                    inRange.Visibility = Visibility.Visible;
                                    inRange.Opacity = MapTileOpacity;
                                    inRange.Background = GetImage(MapConfig.TileNature);
                                }
                                else if (selectedUnit is Healer)
                                {
                                    inRange.Visibility = Visibility.Visible;
                                    inRange.Opacity = MapTileOpacity;
                                    inRange.Background = GetImage(MapConfig.TilePlayer);
                                }
                            }
                        }

                        range += downUp;
                    }
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

            if (!(grid.Name.Contains("Back")) && !(grid.Name.Contains("upgrade")) && !(grid.Name.Contains("airState")))
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

        private static void BtAirUnitSpecial_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid temp = new Grid();
            temp.Background = GetImage(MapConfig.BtLandingBackground);
            if ((bool)(sender as Grid).DataContext)
            {
                (sender as Grid).Background = GetImage(MapConfig.BtTakeOffBackground);
                (sender as Grid).DataContext = false;
            }
            else
            {
                (sender as Grid).Background = GetImage(MapConfig.BtLandingBackground);
                (sender as Grid).DataContext = true;
            }
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
                        if (ActualSelectedUnit.DataContext == null)
                        {
                            ActualSelectedUnit.DataContext = gridSender.DataContext;
                            ActualSelectedUnit.Margin = gridSender.Margin;
                            ActualSelectedUnit.Visibility = Visibility.Visible;
                            FillOrRefreshUnitPanel();
                            gridSender.Background = GetImage((gridSender.DataContext as Units).IdleImage1);
                            CanActivityTiles(unit as Controllable);
                        }
                        else if (ActualSelectedUnit.DataContext != null && ActualSelectedUnit.DataContext == gridSender.DataContext)
                        {
                            HorizontalVision(ActualSelectedUnit.DataContext as Controllable);
                            VerticalVision(ActualSelectedUnit.DataContext as Controllable);
                            DiagonalVision(ActualSelectedUnit.DataContext as Controllable);
                            ActualSelectedUnit.DataContext = null;
                            ActualSelectedUnit.Visibility = Visibility.Hidden;
                            UnitPanel.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
        }

        private static void FillOrRefreshUnitPanel()
        {
            UnitPanel.Visibility = Visibility.Visible;
            switch ((ActualSelectedUnit.DataContext as Units).GetType().Name)
            {
                case "Helicopter":
                    UnitName.Content = "Helikopter";
                    break;
                case "Tank":
                    UnitName.Content = "Harckocsi";
                    break;
                case "Infantryman":
                    UnitName.Content = "Gyalogos";
                    break;
                case "Truck":
                    UnitName.Content = "Teherautó";
                    break;
                default:
                    UnitName.Content = "Ismeretlen";
                    break;
            }

            (UnitPanelLeftColumn.Children[0] as Label).Content = (ActualSelectedUnit.DataContext as Units).Hp + "/" + (ActualSelectedUnit.DataContext as Units).MaxHp;

            if (ActualSelectedUnit.DataContext is Attacker)
            {
                (UnitPanelCenterColumn.Children[1] as Label).Content = "🗡";
                (UnitPanelLeftColumn.Children[1] as Label).Content = (ActualSelectedUnit.DataContext as Attacker).Damage;
                (UnitPanelRightColumn.Children[2] as Grid).Visibility = Visibility.Visible;
                (UnitPanelRightColumn.Children[1] as Grid).Visibility = Visibility.Collapsed;
            }
            else if (ActualSelectedUnit.DataContext is Healer)
            {
                (UnitPanelCenterColumn.Children[1] as Label).Content = "🛠";
                (UnitPanelLeftColumn.Children[1] as Label).Content = (ActualSelectedUnit.DataContext as Healer).Heal;
                (UnitPanelRightColumn.Children[1] as Grid).Visibility = Visibility.Visible;
                (UnitPanelRightColumn.Children[2] as Grid).Visibility = Visibility.Collapsed;
            }

            if (ActualSelectedUnit.DataContext is AirUnit)
            {
                (UnitPanelLeftColumn.Children[3] as Grid).Visibility = Visibility.Visible;
                if ((ActualSelectedUnit.DataContext as AirUnit).IsInTheAir)
                {
                    (UnitPanelLeftColumn.Children[3] as Grid).Background = GetImage(MapConfig.BtLandingBackground);
                    (UnitPanelLeftColumn.Children[3] as Grid).DataContext = true;
                }
                else
                {
                    (UnitPanelLeftColumn.Children[3] as Grid).Background = GetImage(MapConfig.BtTakeOffBackground);
                    (UnitPanelLeftColumn.Children[3] as Grid).DataContext = false;
                }
            }
            else
            {
                (UnitPanelLeftColumn.Children[3] as Grid).Visibility = Visibility.Hidden;
            }
        }

        private static void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Grid thisGrid = sender as Grid;
            if (thisGrid.Name.Contains("Back"))
            {
                thisGrid.Background = GetImage(MainMenuConfig.BtImage);
            }
            else if (thisGrid.Name.Contains("upgrade"))
            {
                thisGrid.Background = GetImage(MapConfig.BtUpgradeBackground);
            }
        }

        private static void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Grid thisGrid = sender as Grid;
            if (thisGrid.Name.Contains("Back"))
            {
                thisGrid.Background = GetImage(MainMenuConfig.BtSelectImage);
            }
            else if (thisGrid.Name.Contains("upgrade"))
            {
                thisGrid.Background = GetImage(MapConfig.BtUpgradeSelectBackground);
            }
        }

        /// <summary>
        /// Not a perfect solution.
        /// </summary>
        /// <param name="selectedUnit"></param>
        private static void DiagonalActivities(Controllable selectedUnit)
        {
            for (int x = -1; x <= 1; x += 2)
            {
                for (int y = -1; y <= 1; y += 2)
                {
                    Grid inSight = InvisibleTiles.FirstOrDefault(grid => grid.Name.Contains((selectedUnit.XPos + x) + "_" + (selectedUnit.YPos + y)));

                    if (inSight != null)
                    {
                        inSight.Visibility = Visibility.Visible;
                        inSight.Opacity = MapTileOpacity;
                        Grid inSightUnit = UnitGrids.FirstOrDefault(x => x.Margin == inSight.Margin);
                        if (!(inSightUnit != null && inSightUnit.DataContext is Obstacle && (!(selectedUnit is AirUnit) || (selectedUnit is AirUnit) && !(selectedUnit as AirUnit).IsInTheAir)))
                        {
                            if (inSightUnit != null)
                            {
                                if (inSightUnit.DataContext is Obstacle && (!(selectedUnit is AirUnit) || (selectedUnit is AirUnit) && !(selectedUnit as AirUnit).IsInTheAir))
                                {
                                    inSight.Background = GetImage(MapConfig.TileNature);
                                }
                                else if ((inSightUnit.DataContext as Units).Team == Team.natural)
                                {
                                    inSight.Background = GetImage(MapConfig.TileNature);
                                }
                                else if ((inSightUnit.DataContext as Units).Team == Team.enemy)
                                {
                                    inSight.Background = GetImage(MapConfig.TileEnemy);
                                }
                                else if ((inSightUnit.DataContext as Units).Team == Team.player)
                                {
                                    inSight.Background = GetImage(MapConfig.TilePlayer);
                                }
                            }
                            else
                            {
                                inSight.Background = GetImage(MapConfig.TileCanMoveHere);
                            }

                            Grid diagonal_1 = InvisibleTiles.FirstOrDefault(grid => grid.Name.Contains((selectedUnit.XPos + x + x) + "_" + (selectedUnit.YPos + y)));
                            if (diagonal_1 != null)
                            {
                                diagonal_1.Visibility = Visibility.Visible;
                                diagonal_1.Opacity = MapTileOpacity;
                                inSightUnit = UnitGrids.FirstOrDefault(x => x.Margin == diagonal_1.Margin);
                                if (!(inSightUnit != null && inSightUnit.DataContext is Obstacle && (!(selectedUnit is AirUnit) || (selectedUnit is AirUnit) && !(selectedUnit as AirUnit).IsInTheAir)))
                                {
                                    if (inSightUnit != null)
                                    {
                                        if (inSightUnit.DataContext is Obstacle && (!(selectedUnit is AirUnit) || (selectedUnit is AirUnit) && !(selectedUnit as AirUnit).IsInTheAir))
                                        {
                                            diagonal_1.Background = GetImage(MapConfig.TileNature);
                                        }
                                        else if ((inSightUnit.DataContext as Units).Team == Team.natural)
                                        {
                                            diagonal_1.Background = GetImage(MapConfig.TileNature);
                                        }
                                        else if ((inSightUnit.DataContext as Units).Team == Team.enemy)
                                        {
                                            diagonal_1.Background = GetImage(MapConfig.TileEnemy);
                                        }
                                        else if ((inSightUnit.DataContext as Units).Team == Team.player)
                                        {
                                            diagonal_1.Background = GetImage(MapConfig.TilePlayer);
                                        }
                                    }
                                    else
                                    {
                                        diagonal_1.Background = GetImage(MapConfig.TileCanMoveHere);
                                    }
                                }
                            }

                            Grid diagonal_2 = InvisibleTiles.FirstOrDefault(grid => grid.Name.Contains((selectedUnit.XPos + x) + "_" + (selectedUnit.YPos + y + y)));
                            if (diagonal_2 != null)
                            {
                                diagonal_2.Visibility = Visibility.Visible;
                                diagonal_2.Opacity = MapTileOpacity;
                                inSightUnit = UnitGrids.FirstOrDefault(x => x.Margin == diagonal_2.Margin);
                                if (!(inSightUnit != null && inSightUnit.DataContext is Obstacle && (!(selectedUnit is AirUnit) || (selectedUnit is AirUnit) && !(selectedUnit as AirUnit).IsInTheAir)))
                                {
                                    if (inSightUnit != null)
                                    {
                                        if (inSightUnit.DataContext is Obstacle && (!(selectedUnit is AirUnit) || (selectedUnit is AirUnit) && !(selectedUnit as AirUnit).IsInTheAir))
                                        {
                                            diagonal_2.Background = GetImage(MapConfig.TileNature);
                                        }
                                        else if ((inSightUnit.DataContext as Units).Team == Team.natural)
                                        {
                                            diagonal_2.Background = GetImage(MapConfig.TileNature);
                                        }
                                        else if ((inSightUnit.DataContext as Units).Team == Team.enemy)
                                        {
                                            diagonal_2.Background = GetImage(MapConfig.TileEnemy);
                                        }
                                        else if ((inSightUnit.DataContext as Units).Team == Team.player)
                                        {
                                            diagonal_2.Background = GetImage(MapConfig.TilePlayer);
                                        }
                                    }
                                    else
                                    {
                                        diagonal_2.Background = GetImage(MapConfig.TileCanMoveHere);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
