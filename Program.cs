using BitmonGeneration1.Source.Battles;
using BitmonGeneration1.Source.BitmonData;
using BitmonGeneration1.Source.Trainers;
using System;
using System.Threading;

namespace BitmonStadiumConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Clear();



            Console.WriteLine("Select your Bitmon: ");
            Thread.Sleep(2000);
            Bitmon playersBitmon = SelectBitmon.Choose();
            Console.Clear();

            Console.WriteLine("Select your 2nd Bitmon: ");
            Thread.Sleep(2000);
            Bitmon players2ndBitmon = SelectBitmon.Choose();
            Console.Clear();

            Console.WriteLine("Select opponent's Bitmon: ");
            Thread.Sleep(2000);
            Bitmon enemyBitmon = SelectBitmon.Choose();
            Console.Clear();



            Trainer Player = new Trainer(name);
            Player.AddToParty(playersBitmon);
            Player.AddToParty(players2ndBitmon);


            Side playerSide = new TrainerSide(Player);
            Side enemySide = new WildBitmonSide(enemyBitmon);

            ConsoleBattlePlayer.Run(playerSide, enemySide, new RandomWildBitmonActor());

            Console.Write("Press enter to exit");
            Console.ReadLine();
        }
    }
}
