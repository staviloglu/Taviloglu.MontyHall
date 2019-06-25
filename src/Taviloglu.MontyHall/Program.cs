using System;
using System.Collections.Generic;

namespace Taviloglu.MontyHall
{
    class Program
    {
        private const int PlayCount = 10000;
        private const int DoorCount = 10000;

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

    public abstract class Player
    {
        protected Random _random = new Random();

        public int PlayCount { get; protected set; }

        public int WinCount { get; protected set; }

        public int SelectedDoorIndexForLastPlay { get; protected set; }

        public virtual void Play(Game game)
        {
            PlayCount++;
            SelectedDoorIndexForLastPlay = _random.Next(0, game.DoorCount);
            game.SelectDoor(SelectedDoorIndexForLastPlay);
        }

        public virtual void Play(Game game, int preSelectedDoorIndex)
        {
            PlayCount++;
            game.SelectDoor(preSelectedDoorIndex);
        }

        protected void UpdateWinCount(Game game)
        {
            if (game.DidPlayerWin())
            {
                WinCount++;
            }
        }
    }

    public class NonSwitchingPlayer : Player
    {
        public override void Play(Game game)
        {
            base.Play(game);
            UpdateWinCount(game);
        }

        public override void Play(Game game, int preSelectedDoorIndex)
        {
            base.Play(game, preSelectedDoorIndex);
            UpdateWinCount(game);
        }
    }

    public class SwitchingPlayer : Player
    {
        public override void Play(Game game)
        {
            base.Play(game);
            game.SwitchDoors();
            UpdateWinCount(game);
        }

        public override void Play(Game game, int decidedIndex)
        {
            base.Play(game, decidedIndex);
            game.SwitchDoors();
            UpdateWinCount(game);
        }
    }

    public class Game
    {
        private static readonly Random _random = new Random();

        private bool _isWin;
        private int _luckyDoorIndex;

        private bool _isDoorChosen = false;

        public int DoorCount { get; private set; }

        public Game(int doorCount)
        {
            if (doorCount < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(doorCount), "should be greater than or equal to 2");
            }

            DoorCount = doorCount;

            _luckyDoorIndex = _random.Next(0, doorCount);
        }

        public void SelectDoor(int doorIndex)
        {
            _isWin = doorIndex == _luckyDoorIndex;
            _isDoorChosen = true;
        }

        public void SwitchDoors()
        {
            if (!_isDoorChosen)
            {
                throw new DoorNotSelectedException();
            }
            _isWin = !_isWin;
        }

        public bool DidPlayerWin()
        {
            if (!_isDoorChosen)
            {
                throw new DoorNotSelectedException();
            }

            return _isWin;
        }

        public Game() : this(3) { }
    }

    public class DoorNotSelectedException : Exception
    {
        public DoorNotSelectedException() : base("You should have selected a door") { }
    }
}
