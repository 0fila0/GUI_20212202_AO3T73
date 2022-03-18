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

        public const double BtWidth = 150;
        public const double BtHeight = 50;
        public static FontFamily BtFontFamily = new FontFamily("Arial");
        public static Brush FontColor = Brushes.White;
        public const int BtFontSize = 15;
    }
}
