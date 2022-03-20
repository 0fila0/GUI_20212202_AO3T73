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
        mainMenu = 1, desert = 2, endGame = 3
    }

    public enum SoundEffectType
    {
        selectHelicopter = 1, selectTank = 2, selectInfantryman = 3, selectTruck = 4, helicopterFire = 5, tankFire = 6, infantrymanFire = 7, truckFire = 8, destroyedUnit = 9
    }

    public interface IMusic
    {
        void PlayMusic(MusicType musicType);

        void PlaySoundEffect(SoundEffectType effectType);
    }
}
