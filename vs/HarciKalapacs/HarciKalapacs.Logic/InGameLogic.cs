using HarciKalapacs.Model;
using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Logic
{
    public enum Decision
    {
        none, move, attack, heal, huntingDown, retreat
    }

    public class InGameLogic : IInGameLogic
    {
        // AI START
        private static Decision decision = Logic.Decision.none;
        private static Random aiDecisionRandomNumber = new Random();

        private const int maxHealChance = 75;
        private const int maxAttackChance = 80;
        private const int maxMoveChance = 100;
        private const int maxHuntingDownChance = 130;
        private const int maxRetreatChance = 85;

        private const int minHealChance = 10;
        private const int minAttackChance = 25;
        private const int minMoveChance = 25;
        private const int minHuntingDownChance = 5;
        private const int minRetreatChance = 10;

        private static int healChance = 0;
        private static int attackChance = 0;
        private static int moveChance = 0;
        private static int huntingDownChance = 0;
        private static int retreatChance = 0;

        private static int enemyUnitsCountNow = 0;
        private static int enemyUnitsCountBefore = 0;
        private static int losses = 0;
        private static int damagedEnemyUnits = 0;
        private static int damagedRate = 0;
        private static int largestDamage = 0;

        // huntingDownIndex = player's unit's index that AI wants to hunting down.
        private static int huntingDownIndex = -1;
        private static int moveHereX = 0;
        private static int moveHereY = 0;
        private static int enemySelectedUnit = -1;
        private static int enemyToHeal = -1;
        private static int enemyAttackThisPlayer = -1;
        private const int healingThreshold = 10;

        // AI END

        private IModel model;

        public static Decision AIDecision { get => decision; set => decision = value; }
        public static int AIEnemySelectedUnit { get => enemySelectedUnit; set => enemySelectedUnit = value; }
        public static int AIHuntingDownIndex { get => huntingDownIndex; set => huntingDownIndex = value; }
        public static int MoveHereX { get => moveHereX; set => moveHereX = value; }
        public static int MoveHereY { get => moveHereY; set => moveHereY = value; }
        public static int EnemyAttackThisPlayerIndex { get => enemyAttackThisPlayer; set => enemyAttackThisPlayer = value; }
        public static int EnemyToHealIndex { get => enemyToHeal; set => enemyToHeal = value; }

        public InGameLogic(IModel model)
        {
            this.model = model;
        }

        public void AIDamageDetails()
        {
            largestDamage = 0;
            damagedEnemyUnits = 0;
            int allDamage = 0;
            foreach (var unit in this.model.AllUnits)
            {
                if (unit is Units && (unit as Units).Team == Team.enemy && unit.Hp < unit.MaxHp)
                {
                    damagedEnemyUnits += 1;
                    allDamage += unit.MaxHp - unit.Hp;

                    if (unit.MaxHp - unit.Hp < largestDamage)
                    {
                        largestDamage = unit.MaxHp - unit.Hp;
                    }
                }
            }

            damagedRate = allDamage / damagedEnemyUnits;
            int add = damagedRate % 5;
            damagedRate -= add;
        }

        private int AILossesCount()
        {
            return enemyUnitsCountBefore - enemyUnitsCountNow;
        }

        public void AISetup()
        {
            enemyUnitsCountBefore = this.model.AllUnits.Count(x => x is Units && (x as Units).Team == Team.enemy);
            attackChance = 70;
            moveChance = 65;
            huntingDownChance = 45;
            healChance = minHealChance;
            retreatChance = minRetreatChance;
        }

        public void AIEnemyUnitsCountNow()
        {
            enemyUnitsCountNow = this.model.AllUnits.Count(x => x is Units && (x as Units).Team == Team.enemy);
        }

        public void AIDecisions()
        {
            // this.DamageDetails();
            this.AIEnemyUnitsCountNow();
            losses = this.AILossesCount();
            this.AIChances();

            int activity = aiDecisionRandomNumber.Next(0, 201);
            if (activity <= moveChance)
            {
                decision = Decision.move;
            }
            else if (activity <= moveChance + attackChance)
            {
                decision = Decision.attack;
            }
            else if (activity <= moveChance + attackChance + healChance)
            {
                decision = Decision.heal;
            }
            else if (activity <= moveChance + attackChance + healChance + huntingDownChance)
            {
                decision = Decision.huntingDown;
            }
            else if (activity <= moveChance + attackChance + healChance + huntingDownChance + retreatChance)
            {
                decision = Decision.retreat;
            }
        }

        private void AIChances()
        {
            if (losses > 0)
            {
                enemyUnitsCountBefore -= losses;

                int newRetreat = retreatChance + losses * 5;
                int newMove = moveChance + losses * 5;
                int newAttack = attackChance - losses * 5;
                int newHunting = huntingDownChance - losses * 5;
                // int newHeal = healChance - losses * 5;
                if (newRetreat <= maxRetreatChance && newMove <= maxMoveChance && newAttack >= minAttackChance && newHunting >= minHuntingDownChance)
                {
                    retreatChance += losses * 5;
                    moveChance += losses * 5;
                    attackChance -= losses * 5;
                    huntingDownChance -= losses * 5;
                }

                losses = 0;
            }
            else
            {
                int newRetreat = retreatChance - 5;
                // int newMove = moveChance + 5;
                int newAttack = attackChance + 10;
                int newHunting = huntingDownChance + 5;
                int newHeal = healChance - 10;
                if (newRetreat >= minRetreatChance && newHunting <= maxHuntingDownChance && newAttack <= maxAttackChance && newHeal >= minHealChance)
                {
                    retreatChance -= 5;
                    huntingDownChance += 5;
                    attackChance += 10;
                    healChance -= 10;
                }
            }

            if (damagedRate >= 25)
            {
                int newRetreat = retreatChance + damagedRate / 5;
                // int newMove = moveChance + damagedRate / 5;
                int newAttack = attackChance - damagedRate / 5;
                int newHunting = huntingDownChance - damagedRate / 5;
                int newHeal = healChance + damagedRate / 5;
                if (newRetreat <= maxRetreatChance && newAttack >= minAttackChance && newHunting >= minHuntingDownChance && newHeal <= maxHealChance)
                {
                    retreatChance += damagedRate / 5;
                    // moveChance += damagedRate / 5;
                    attackChance -= damagedRate / 5;
                    huntingDownChance -= damagedRate / 5;
                    healChance += damagedRate / 5;
                }
            }
            else
            {
                int newRetreat = retreatChance - damagedRate / 5;
                int newMove = moveChance - damagedRate / 5;
                int newAttack = attackChance + damagedRate / 5;
                int newHunting = huntingDownChance + 2 * (damagedRate / 5);
                int newHeal = healChance - damagedRate / 5;
                if (newRetreat >= minRetreatChance && newMove >= minMoveChance && newHunting <= maxHuntingDownChance && newHeal >= minHealChance && newAttack <= maxAttackChance)
                {
                    retreatChance -= damagedRate / 5;
                    moveChance -= damagedRate / 5;
                    attackChance += damagedRate / 5;
                    huntingDownChance += 2 * (damagedRate / 5);
                    healChance -= damagedRate / 5;
                }
            }

            if (damagedEnemyUnits >= 5)
            {
                // int newRetreat = retreatChance + damagedRate / 5;
                int newMove = moveChance + damagedEnemyUnits * 2;
                int newAttack = attackChance - damagedEnemyUnits;
                int newHunting = huntingDownChance - damagedEnemyUnits;
                int newHeal = healChance + damagedEnemyUnits * 2;
                if (newMove <= maxMoveChance && newAttack >= minAttackChance && newHunting >= minHuntingDownChance && newHeal <= maxHealChance)
                {
                    // retreatChance += damagedRate / 5;
                    moveChance += damagedEnemyUnits * 2;
                    attackChance -= damagedEnemyUnits;
                    huntingDownChance -= damagedEnemyUnits;
                    healChance += damagedEnemyUnits;
                }
            }
            else
            {
                // int newRetreat = retreatChance + damagedRate / 5;
                int newMove = moveChance + damagedEnemyUnits * 2;
                int newAttack = attackChance - damagedEnemyUnits;
                int newHunting = huntingDownChance + damagedEnemyUnits;
                int newHeal = healChance - damagedEnemyUnits * 2;
                if (newMove <= maxMoveChance && newAttack >= minAttackChance && newHunting <= maxHuntingDownChance && newHeal >= minHealChance)
                {
                    // retreatChance += damagedRate / 5;
                    moveChance += damagedEnemyUnits * 2;
                    attackChance -= damagedEnemyUnits;
                    huntingDownChance += damagedEnemyUnits;
                    healChance -= damagedEnemyUnits;
                }
            }
        }

        public Units AISelectUnit()
        {
            switch (AIDecision)
            {
                case Decision.move:
                    return this.AISelectWhenMove();
                case Decision.attack:
                    return this.AISelectWhenAttack();
                case Decision.heal:
                    return this.AISelectWhenHeal();
                case Decision.huntingDown:
                    return this.AISelectWhenHunting();
                case Decision.retreat:
                    return this.AISelectWhenRetreat();
                default:
                    return null;
            }
        }

        private Units AISelectWhenMove()
        {
            int index = aiDecisionRandomNumber.Next(1, enemyUnitsCountNow + 1);
            int search = 0;
            int i = 0;
            while (search < this.model.AllUnits.Count && search != index)
            {
                if (this.model.AllUnits[i] is Units && (this.model.AllUnits[i] as Units).Team == Team.enemy)
                {
                    if (search == index)
                    {
                        int newX = aiDecisionRandomNumber.Next(-(this.model.AllUnits[i] as Units).MaxMove, (this.model.AllUnits[i] as Units).MaxMove + 1);
                        int newY = aiDecisionRandomNumber.Next(-(this.model.AllUnits[i] as Units).MaxMove, (this.model.AllUnits[i] as Units).MaxMove + 1);
                        MoveHereX = newX;
                        MoveHereY = newY;

                        return this.model.AllUnits[i] as Units;
                    }

                    search++;
                }
                else
                {
                    i++;
                }
            }

            return this.model.AllUnits.Where(x => x is Units && (x as Units).Team == Team.enemy).FirstOrDefault() as Units;
        }

        private Units AISelectWhenAttack()
        {
            int index = -1;
            foreach (IMapItem unit in this.model.AllUnits)
            {
                if (unit is Units && (unit as Units).Team == Team.enemy && (unit as Units).CanAttack)
                {
                    Units temp = unit as Units;

                    // if it is an airunit and it is on the ground it can't move.
                    if (!(temp.CanFly) || (temp.IsInTheAir))
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
                            while (Math.Abs(range) <= temp.Vision)
                            {
                                int y = range + downUp;
                                Units attackThisTemp = this.model.AllUnits.FirstOrDefault(x => x.XPos == temp.XPos && x.YPos == y) as Units;

                                if (attackThisTemp != null)
                                {
                                    EnemyAttackThisPlayerIndex = this.model.AllUnits.IndexOf(attackThisTemp);
                                    return unit as Units;
                                }

                                range += downUp;
                            }
                        }
                    }

                    // if it is an airunit and it is on the ground it can't move.
                    if (!(temp.CanFly) || (temp.IsInTheAir))
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
                            while (Math.Abs(range) <= temp.Vision)
                            {
                                int xP = range + leftRight;
                                Units attackThisTemp = this.model.AllUnits.FirstOrDefault(x => x.XPos == xP && x.YPos == temp.YPos) as Units;

                                if (attackThisTemp != null)
                                {
                                    EnemyAttackThisPlayerIndex = this.model.AllUnits.IndexOf(attackThisTemp);
                                    return unit as Units;
                                }

                                range += leftRight;
                            }
                        }
                    }
                }
            }

            return this.model.AllUnits[index] as Units;
        }

        private Units AISelectWhenHeal()
        {
            int healerIndex = 0;
            while (healerIndex < this.model.AllUnits.Count)
            {
                int needHealingIndex = 0;

                if (this.model.AllUnits[healerIndex] is Units && (this.model.AllUnits[healerIndex] as Units).Team == Team.enemy && (this.model.AllUnits[healerIndex] as Units).CanHeal)
                {
                    while (needHealingIndex < this.model.AllUnits.Count)
                    {
                        if (healerIndex != needHealingIndex && this.model.AllUnits[needHealingIndex] is Units && (this.model.AllUnits[needHealingIndex] as Units).Team == Team.enemy && this.model.AllUnits[needHealingIndex].Hp < this.model.AllUnits[needHealingIndex].MaxHp)
                        {
                            int distance = Math.Abs(this.model.AllUnits[healerIndex].XPos - this.model.AllUnits[needHealingIndex].XPos) + Math.Abs(this.model.AllUnits[healerIndex].YPos - this.model.AllUnits[needHealingIndex].YPos);
                            if (distance <= (this.model.AllUnits[healerIndex] as Units).MaxMove)
                            {
                                EnemyToHealIndex = needHealingIndex;
                                return this.model.AllUnits[healerIndex] as Units;
                            }
                        }

                        needHealingIndex++;
                    }
                }

                healerIndex++;
            }

            return null;
        }

        private Units AISelectWhenHunting()
        {
            int x = -1;
            int i = 0;
            foreach (IMapItem item in this.model.AllUnits)
            {
                if (item is Units)
                {
                    if ((item as Units).Team == Team.player)
                    {
                        if (item.XPos > x)
                        {
                            x = item.XPos;
                            i = this.model.AllUnits.IndexOf(item);
                        }
                    }
                }
            }

            foreach (IMapItem item in this.model.AllUnits)
            {
                if (item is Units)
                {
                    if ((item as Units).Team == Team.enemy)
                    {
                        int selectThis = aiDecisionRandomNumber.Next(0, 3);
                        if (selectThis == 0)
                        {
                            int xOrYMovement = aiDecisionRandomNumber.Next(0, 2);

                            if (xOrYMovement == 0)
                            {
                                int directionX = this.model.AllUnits[i].XPos - item.XPos;
                                int stepX = aiDecisionRandomNumber.Next(1, (item as Units).MaxMove + 1);
                                if (directionX < 0)
                                {
                                    moveHereX = item.XPos - stepX;
                                    MoveHereY = item.YPos;
                                }
                                else
                                {
                                    moveHereX = item.XPos + stepX;
                                    MoveHereY = item.YPos;
                                }
                            }
                            else
                            {
                                int directionY = this.model.AllUnits[i].YPos - item.YPos;
                                int stepY = aiDecisionRandomNumber.Next(1, (item as Units).MaxMove + 1);
                                if (directionY < 0)
                                {
                                    moveHereY = item.YPos - stepY;
                                    MoveHereX = item.XPos;
                                }
                                else
                                {
                                    moveHereY = item.YPos + stepY;
                                    MoveHereX = item.XPos;
                                }
                            }

                            return item as Units;
                        }
                    }
                }
            }

            return null;
        }

        private Units AISelectWhenRetreat()
        {
            int x = 999;
            int i = 0;
            foreach (IMapItem item in this.model.AllUnits)
            {
                if (item is Units)
                {
                    if ((item as Units).Team == Team.enemy)
                    {
                        if (item.XPos < x)
                        {
                            x = item.XPos;
                            i = this.model.AllUnits.IndexOf(item);
                        }
                    }
                }
            }

            MoveHereX = this.model.AllUnits[i].XPos + aiDecisionRandomNumber.Next(1, (this.model.AllUnits[i] as Units).MaxMove + 1);
            MoveHereY = this.model.AllUnits[i].YPos;

            return this.model.AllUnits[i] as Units;
        }

        public bool Attack(Units attacker, IMapItem target)
        {
            int distance = Math.Abs(attacker.XPos - target.XPos) + Math.Abs(attacker.YPos - target.YPos);
            if (distance <= attacker.MaxMove)
            {
                if (attacker.CanAttack)
                {
                    if (target is Units)
                    {
                        target.Hp = target.Hp - (attacker.AttackValue - (target as Units).ArmorValue);
                    }
                    else
                    {
                        target.Hp = target.Hp - attacker.AttackValue;
                    }

                    if (target.Hp <= 0)
                    {
                        this.model.AllUnits.Remove(target);
                    }

                    return true;
                }
            }

            return false;
        }

        public bool Heal(Units healer, IMapItem target)
        {
            int distance = Math.Abs(healer.XPos - target.XPos) + Math.Abs(healer.YPos - target.YPos);
            if (healer.CanHeal && distance <= healer.MaxMove)
            {
                if (target.Hp < target.MaxHp)
                {
                    if (target.MaxHp - target.Hp >= healer.HealValue)
                    {
                        target.Hp += healer.HealValue;
                    }
                    else
                    {
                        target.Hp = target.MaxHp;
                    }

                    return true;
                }
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
                if (unit.AttackValue <= 15)
                {

                    unit.AttackValue = unit.AttackValue + 15;

                    return true;
                }
                else if (unit.AttackValue >= 15 && unit.AttackValue <= 25)
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

            if (unit.Hp > 0)
            {
                unit.MaxHp = unit.MaxHp + 20;
                int HPdifference = unit.MaxHp - unit.Hp;
                if (HPdifference > 20)
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
