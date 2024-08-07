using CodeBase._Tools.StateMachine;

namespace CodeBase.Battle
{
    public class PrepareHandsState : ISelfCompleteState
    {
        private IPlayersHolder _playersHolder;
        public bool Complete { get; }
        
        public PrepareHandsState(IPlayersHolder playersHolder)
        {
            _playersHolder = playersHolder;
        }

        public void OnEnter()
        {
            foreach (IPlayer player in _playersHolder.BattlePlayers)
            {
                if (player.Hand.Count < player.HandSize)
                    player.FillHandToFull();
            }
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
        }


    }
}