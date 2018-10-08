using BitmonGeneration1.Source.Battles;
using BitmonGeneration1.Source.Moves;
using BitmonGeneration1.Source.BitmonData;
using System;
using System.Threading;

namespace BitmonStadiumConsoleApp
{
    public class ConsoleBattleActor : BattleActor
    {
        public Selection MakeBeginningOfTurnSelection(Battle battle, Side actorSide)
        {
            Selection playersSelection = null;
            MainMenuState currentState = MainMenuState.MAIN;

            while(playersSelection == null)
                Tick(
                    ref playersSelection,
                    ref currentState,
                    battle,
                    actorSide);

            return playersSelection;
        }



        private void Tick(
            ref Selection selection,
            ref MainMenuState state,
            Battle battle,
            Side actorSide)
        {
            switch (state)
            {
                case MainMenuState.MAIN:
                    Console.Clear();
                    Display.Bitmon(
                        actorSide.CurrentBattleBitmon,
                        battle.OpponentSide.CurrentBattleBitmon,
                        actorSide.Name,
                        battle.OpponentSide.Name);
                    Display.MainPrompt();
                    string mainChoice = Console.ReadLine();
                    if (mainChoice == "1")
                    {
                        if (actorSide.CurrentBattleBitmon.IsMultiTurnMoveActive())
                        {
                            selection = Selection.MakeContinueMultiTurnMove(
                                actorSide.CurrentBattleBitmon,
                                battle.OpponentSide.CurrentBattleBitmon);
                            return;
                        }
                        else if (actorSide.CurrentBattleBitmon.PartiallyTrapped)
                        {
                            selection = Selection.MakeEmptyFight();
                            return;
                        }
                        state = MainMenuState.MOVE;
                    }
                    else if (mainChoice == "2")
                    {
                        state = MainMenuState.ITEM;
                    }
                    else if (mainChoice == "3")
                    {
                        state = MainMenuState.BITMONFORSWITCH;
                    }
                    else if (mainChoice == "4")
                    {
                        // 
                    }
                    break;

                case MainMenuState.MOVE:
                    Console.Clear();
                    var myBit = actorSide.CurrentBattleBitmon;
                    var opponentBit = battle.OpponentSide.CurrentBattleBitmon;
                    Display.Bitmon(
                        myBit,
                        opponentBit,
                        actorSide.Name,
                        battle.OpponentSide.Name);
                    Display.MovePrompt(actorSide);
                    string moveChoice = Console.ReadLine();

                    if (moveChoice == "0")
                    {
                        state = MainMenuState.MAIN;
                    }
                    else if (moveChoice == "1")
                    {
                        selection = Selection.MakeFight(
                            myBit, opponentBit, myBit.Move1);
                    }
                    else if (moveChoice == "2")
                    {
                        selection = Selection.MakeFight(
                            myBit, opponentBit, myBit.Move2);
                    }
                    else if (moveChoice == "3")
                    {
                        selection = Selection.MakeFight(
                            myBit, opponentBit, myBit.Move3);
                    }
                    else if (moveChoice == "4")
                    {
                        selection = Selection.MakeFight(
                            myBit, opponentBit, myBit.Move4);
                    }
                    break;

                case MainMenuState.ITEM:
                    break;

                case MainMenuState.BITMONFORITEM:
                    break;

                case MainMenuState.BITMONFORSWITCH:
                    selection = SwitchBitmonPrompt(battle, actorSide);
                    break;
            }
        }

        public Selection MakeForcedSwitchSelection(Battle battle, Side actorSide)
        {
            return SwitchBitmonPrompt(battle, actorSide);
        }

        public Move PickMoveToMimic(Side opponentSide)
        {
            throw new NotImplementedException();
        }








        private static Selection SwitchBitmonPrompt(Battle battle, Side actorSide)
        {
            Selection selection;
            Console.Clear();
            Display.BitmonPrompt(actorSide);

            int bitmonPick;
            while (!int.TryParse(Console.ReadLine(), out bitmonPick)
                  || bitmonPick < 1
                  || bitmonPick > actorSide.Party.Count
                  || actorSide.Party[bitmonPick - 1].Status == Status.Fainted
                  || actorSide.Party[bitmonPick - 1] == actorSide.CurrentBattleBitmon.Bitmon)
            {
                Console.Clear();
                Display.BitmonPrompt(actorSide);
            }

            selection = Selection.MakeSwitchOut(
                actorSide.CurrentBattleBitmon,
                battle.OpponentSide.CurrentBattleBitmon,
                actorSide.Party[bitmonPick - 1]);

            Console.Clear();
            Display.Bitmon(
                actorSide.CurrentBattleBitmon,
                battle.OpponentSide.CurrentBattleBitmon,
                actorSide.Name,
                battle.OpponentSide.Name);

            return selection;
        }
    }
}
