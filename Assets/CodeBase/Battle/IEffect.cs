using CodeBase.Cards;

namespace CodeBase.Battle
{
    public interface IEffect
    {
        public EffectType Type { get; }
        public float GetStat(StatType type);
        public void TryAddDuration(IEffect effect);
    }
}