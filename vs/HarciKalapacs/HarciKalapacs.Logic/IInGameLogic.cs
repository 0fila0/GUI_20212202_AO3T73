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
        /// Maximizes playerSteps variable.
        /// </summary>
        public void StartTurn();

        /// <summary>
        /// Modifies leftStep variable in model.
        /// </summary>
        public void StepOccured();

        /// <summary>
        /// Unit moves.
        /// </summary>
        /// <param name="unit">This unit moves.</param>
        /// <param name="x">Horizontal position. Increases to the right.</param>
        /// <param name="y">Vertical position. Increases downwards.</param>
        /// <returns>True, if the move was successful.</returns>
        public bool Move(Units unit, int x, int y);

        /// <summary>
        /// Attacks target unit.
        /// </summary>
        /// <param name="attacker">This unit attacks.</param>
        /// <param name="target">The target.</param>
        /// <returns>True, if the attack was successful</returns>
        public bool Attack(Units attacker, Units target);

        /// <summary>
        /// Heals target unit.
        /// </summary>
        /// <param name="healer">This unit heals another one.</param>
        /// <param name="target">This unit needs healing.</param>
        /// <returns>True, if the healing was successful.</returns>
        public bool Heal(Healer healer, Units target);

        /// <summary>
        /// Air unit takes off or lands.
        /// </summary>
        /// <param name="airUnit">This unit switch its vertical position.</param>
        public void SwitchVerticalPosition(Units airUnit);

        /// <summary>
        /// Upgrade unit's max hp.
        /// </summary>
        /// <param name="unit">This unit.</param>
        /// <returns>True, if upgrade was successful.</returns>
        public bool UpgradeMaxHp(Units unit);

        /// <summary>
        /// Upgrade unit's damage.
        /// </summary>
        /// <param name="unit">This unit.</param>
        /// <returns>True, if upgrade was successful.</returns>
        public bool UpgradeDamage(Units unit);

        /// <summary>
        /// Upgrade heal.
        /// </summary>
        /// <param name="unit">This unit.</param>
        /// <returns>True, if upgrade was successful.</returns>
        public bool UpgradeHealer(Units unit);

        /// <summary>
        /// 1) A simple step such as move, attack or heal.
        /// 2) AI units hunt down one player unit.
        /// 3) The units furthest from the base retreat.
        /// </summary>
        public void AIDecisions();
    }
}
