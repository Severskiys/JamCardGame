using System.Collections.Generic;

namespace CodeBase.Battle
{
    public interface IPlayersHolder
    {
        List<IPlayer> BattlePlayers { get; }
    }
}