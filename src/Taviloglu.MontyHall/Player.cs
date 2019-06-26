using System;

namespace Taviloglu.MontyHall
{
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
}
