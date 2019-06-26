using System;

namespace Taviloglu.MontyHall
{
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
}
