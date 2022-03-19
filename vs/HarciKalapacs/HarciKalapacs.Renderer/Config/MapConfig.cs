using System.Windows.Media;

namespace HarciKalapacs.Renderer.Config
{
    public static class MapConfig
    {
        public const string BackgroundImageName = "HarciKalapacs.Renderer.Assets.mapMenu.jpg";

        public const string TileImage = "HarciKalapacs.Renderer.Assets.button.png";
        public const string TileSelectImage = "HarciKalapacs.Renderer.Assets.selectedButton.png";
        public const double TileWidth = 125;
        public const double TileHeight = 125;

        public const string TileEnemy = "HarciKalapacs.Renderer.Assets.tileEnemy.png";
        public const string TilePlayer = "HarciKalapacs.Renderer.Assets.tilePlayer.png";
        public const string TileInSight = "HarciKalapacs.Renderer.Assets.tileInSight.png";
        public const string TileOutOfSight = "HarciKalapacs.Renderer.Assets.tileOutOfSight.png";
        public const string TileSelected = "HarciKalapacs.Renderer.Assets.tileSelected.png";
        public const string TileNature = "HarciKalapacs.Renderer.Assets.tileNature.png";
        public const string TileCanMoveHere = "HarciKalapacs.Renderer.Assets.tileCanMoveHere.png";

        public const double BtBackWidth = 150;
        public const double BtBackHeight = 50;
        public static FontFamily BtFontFamily = new FontFamily("Arial");
        public static Brush FontColor = Brushes.White;
        public const int BtFontSize = 15;

        public const string GridHeaderBackground = "HarciKalapacs.Renderer.Assets.headerBackground.png";
        public static double GridHeaderWidth = MainMenuConfig.WindowWidth;
        public static double GridHeaderHeight = 50;
    }
}
