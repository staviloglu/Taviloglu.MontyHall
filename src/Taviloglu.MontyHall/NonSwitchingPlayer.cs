namespace Taviloglu.MontyHall
{
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
}
