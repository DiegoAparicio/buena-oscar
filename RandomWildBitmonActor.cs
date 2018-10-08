using BitmonGeneration1.Source.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmonGeneration1.Source.Battles
{
    public class RandomWildBitmonActor : BattleActor
    {
        public Selection MakeBeginningOfTurnSelection(Battle battle, Side actorSide)
        {
            if (actorSide.CurrentBattleBitmon.IsMultiTurnMoveActive())
            {
                return Selection.MakeContinueMultiTurnMove(actorSide.CurrentBattleBitmon,
                                                           battle.PlayerSide.CurrentBattleBitmon);
            }
            else if (actorSide.CurrentBattleBitmon.PartiallyTrapped)
            {
                return Selection.MakeEmptyFight();
            }
            else
            {
                return MakeRandomFightSelection(battle, actorSide);
            }
        }
        private Selection MakeRandomFightSelection(Battle battle, Side actorSide)
        {
            return Selection.MakeFight(actorSide.CurrentBattleBitmon,
                                       battle.PlayerSide.CurrentBattleBitmon,
                                       PickRandomMove(actorSide));
        }
        private Move PickRandomMove(Side actorSide)
        {
            var nut = actorSide.CurrentBattleBitmon;
            var rng = new Random();
            Move move = null;
            while (move == null)
            {
                int rando = rng.Next(1, 5);
                if (rando == 1 &&
                    nut.Move1 != null)
                {
                    move = nut.Move1;
                }
                else if (rando == 2 &&
                         nut.Move2 != null)
                {
                    move = nut.Move2;
                }
                else if (rando == 3 &&
                         nut.Move3 != null)
                {
                    move = nut.Move3;
                }
                else if (rando == 4 &&
                         nut.Move4 != null)
                {
                    move = nut.Move4;
                }
            }
            return move;
        }

        public Selection MakeForcedSwitchSelection(Battle battle, Side actorSide)
        {
            throw new NotImplementedException();
        }

        public Move PickMoveToMimic(Side opponentSide)
        {
            throw new NotImplementedException();
        }
    }
}
