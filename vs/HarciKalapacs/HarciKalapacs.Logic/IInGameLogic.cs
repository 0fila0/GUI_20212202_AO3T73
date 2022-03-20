using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Logic
{
    public interface IInGameLogic
    {
        /// <summary>
        /// Unit moves.
        /// </summary>
        /// <param name="x">Horizontal position. Increases to the right.</param>
        /// <param name="y">Vertical position. Increases downwards.</param>
        public void Move(int x, int y);

        /// <summary>
        /// Attacks target unit.
        /// </summary>
        /// <param name="target">Unit attacks this unit.</param>
        public void Attack(Units target);

        /// <summary>
        /// Heals target unit.
        /// </summary>
        /// <param name="target">Unit heals this unit.</param>
        public void Heal(Units target);

        /// <summary>
        /// Air unit takes off or lands.
        /// </summary>
        public void SwitchVerticalPosition();

        /// <summary>
        /// Upgrade unit's max hp.
        /// </summary>
        public void UpgradeMaxHp();

        /// <summary>
        /// Upgrade unit's damage.
        /// </summary>
        public void UpgradeDamage();

        /// <summary>
        /// Upgrade heal.
        /// </summary>
        public void UpgradeHealer();

        /// <summary>
        /// 1) A simple step such as move, attack or heal.
        /// 2) AI units hunt down one player unit.
        /// 3) The units furthest from the base retreat.
        /// </summary>
        public void AIDecisions();


    }
}
