namespace CodeBase.Cards
{
    public interface IBattleCardsConductor
    {
        public bool TrySetCard(ICard card, int slotIndex);
    }
}