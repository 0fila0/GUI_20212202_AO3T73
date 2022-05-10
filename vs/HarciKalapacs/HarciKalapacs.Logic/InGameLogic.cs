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

        // not tested
        public bool UpgradeDamage(Units unit)
        {
            if (unit.CanAttack)
            {
                if (unit.AttackValue<=15)
                {

                    unit.AttackValue = unit.AttackValue + 15;

                    return true;
                }
                else if (unit.AttackValue >= 15 && unit.AttackValue <=25)                 
                {

                    unit.AttackValue = unit.AttackValue + 10;

                    return true;
                }
                else
                {
                    unit.AttackValue = unit.AttackValue + 5;

                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        //not tested
        public bool UpgradeHealer(Units unit)
        {
            if (unit.CanHeal)
            {
                unit.HealValue = unit.HealValue + 5;
                return true;
            }
            else
            {
                return false;
            }
        }

        //not tested
        public bool UpgradeMaxHp(Units unit)
        {   
            
            if (unit.Hp>0)             
            {  
                unit.MaxHp = unit.MaxHp + 20;
                int HPdifference = unit.MaxHp - unit.Hp;
                if (HPdifference>20)
                {
                    unit.Hp = unit.Hp + 20;
                    return true;
                }
                else
                {
                    unit.Hp = unit.MaxHp;
                    return true;
                }  
            }
            else
            {
                return false;
            }

        }
    }
}
