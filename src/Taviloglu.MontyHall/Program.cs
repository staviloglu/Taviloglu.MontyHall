using System;
using System.Collections.Generic;

namespace Taviloglu.MontyHall
{
    class Program
    {
        private const int PlayCount = 10000;
        private const int DoorCount = 3;

        static void Main(string[] args)
        {
            var randomPlayers = new List<Player> { new NonSwitchingPlayer(), new SwitchingPlayer() };

            var sameFirstDoorPlayers = new List<Player> { new NonSwitchingPlayer(), new SwitchingPlayer() };

            for (int i = 0; i < PlayCount; i++)
            {
                var game = new Game(DoorCount);

                for (int pi = 0; pi < randomPlayers.Count; pi++)
                {
                    //sum of the wins may not be equal to PlayCount
                    //but their ratio should still be around 1/(DoorCount-1)
                    randomPlayers[pi].Play(game);
                }

                for (int pi = 0; pi < sameFirstDoorPlayers.Count; pi++)
                {
                    //Sum of the wins should be equal to PlayCount
                    if (pi == 0)
                    {
                        sameFirstDoorPlayers[pi].Play(game);
                    }
                    else
                    {
                        sameFirstDoorPlayers[pi].Play(game, sameFirstDoorPlayers[pi - 1].SelectedDoorIndexForLastPlay);
                    }
                }
            }

            Console.WriteLine("Random plays:");
            WriteResultsToConsole(randomPlayers);

            Console.WriteLine();

            Console.WriteLine("Same first door plays:");
            WriteResultsToConsole(sameFirstDoorPlayers);

            Console.ReadLine();
        }

        private static void WriteResultsToConsole(List<Player> players)
        {
            foreach (var player in players)
            {
                Console.WriteLine($"{player.GetType()} wins {player.WinCount} times");
            }
        }
    }
}
