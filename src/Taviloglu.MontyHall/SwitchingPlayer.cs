namespace Taviloglu.MontyHall
{
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
}
