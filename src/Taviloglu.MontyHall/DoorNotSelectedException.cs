using System;

namespace Taviloglu.MontyHall
{
    public class DoorNotSelectedException : Exception
    {
        public DoorNotSelectedException() : base("You should have selected a door") { }
    }
}
