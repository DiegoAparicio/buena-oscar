﻿using BitmonGeneration1.Source.Battles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitmonStadiumConsoleApp
{
    public static class ConsoleBattlePlayer
    {
        public static void Run(Side mySide, Side opponentSide, BattleActor opponentActor)
        {
            Battle battle = new Battle(mySide, opponentSide, new ConsoleBattleActor(), opponentActor);

            BattleEventsHandler.AttachMyBattleBitmonEventHandlers(mySide.CurrentBattleBitmon);
            BattleEventsHandler.AttachOpponentBattleBitmonEventHandlers(opponentSide.CurrentBattleBitmon);
            BattleEventsHandler.AttachBattleEventHandlers(battle);

            battle.Play();
        }
    }
}
