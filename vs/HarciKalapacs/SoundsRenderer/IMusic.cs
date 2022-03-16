using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SoundsRenderer
{
    public enum MusicType
    {
        mainMenu = 1, inGame = 2, endGame = 3
    }

    public interface IMusic
    {
        public MediaPlayer MediaPlayer { get; set; }

        void PlayMusic(MusicType musicType);
    }
}
