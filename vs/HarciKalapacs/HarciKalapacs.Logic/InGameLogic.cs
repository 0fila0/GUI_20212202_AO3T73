using HarciKalapacs.Model;
using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Logic
{
    public class InGameLogic : IInGameLogic
    {
        IModel model;

        public InGameLogic(IModel model)
        {
            this.model = model;
        }

        public void AIDecisions()
        {
            throw new NotImplementedException();
        }

        public bool Attack(Units attacker, Units target)
        {
            if (attacker.CanAttack)
            {
                target.Hp = target.Hp - (attacker.AttackValue - target.ArmorValue);
                return true;
            }
            return false;
        }

        public bool Heal(Units healer, Units target)
        {
            if (healer.CanHeal)
            {
                if(target.MaxHp-target.Hp >= healer.HealValue)
                {
                    target.Hp += healer.HealValue;
                }
                else
                {
                    target.Hp = target.MaxHp;
                    
                }
                return true;
            }
            return false;
        }

        public bool Move(Units unit, int x, int y)
        {
            int distance = Math.Abs(unit.XPos - x) + Math.Abs(unit.YPos - y);
            if (unit.MaxMove >= distance)
            {
                unit.Move(x, y);
                return true;
            }
            return false;
            
        }

        public void StartTurn()
        {
            this.model.LeftSteps = this.model.PlayerTurn;
        }

        public void StepOccured()
        {
            this.model.LeftSteps--;
        }

        public void SwitchVerticalPosition(Units airUnit)
        {
            airUnit.SwitchVerticalPosition();
        }

        public bool UpgradeDamage(Units unit)
        {
            throw new NotImplementedException();
        }

        public bool UpgradeHealer(Units unit)
        {
            throw new NotImplementedException();
        }

        public bool UpgradeMaxHp(Units unit)
        {
            throw new NotImplementedException();
        }
    }
}
