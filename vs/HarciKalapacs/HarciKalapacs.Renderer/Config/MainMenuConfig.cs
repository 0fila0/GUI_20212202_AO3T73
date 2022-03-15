using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HarciKalapacs.Renderer.Config
{
    public static class MainMenuConfig
    {
        public const string BackgroundImageName = "HarciKalapacs.Renderer.Assets.mainMenuBackground.png";
        public static double WindowHeight;
        public static double WindowWidth;

        public const string BtImage = "HarciKalapacs.Renderer.Assets.button.png";
        public const string BtSelectImage = "HarciKalapacs.Renderer.Assets.selectedButton.png";
        public const double BtWidth = 300;
        public const double BtHeight = 100;
        public const double SpaceBetweenButtons = 150;
        public const int BtFontSize = 15;

        public static double TopBtXPos;
        public static double TopBtYPos;

        public static FontFamily BtFontFamily = new FontFamily("Arial");
        public static Brush FontColor = Brushes.White;
    }
}
