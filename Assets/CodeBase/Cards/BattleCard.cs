using System;
using System.Collections.Generic;
using CodeBase.StaticData;

namespace CodeBase.Cards
{
    public class BattleCard : ICard
    {
        public event Action OnChangeState;
        public event Action OnShowWin;
        public event Action OnShowLose;
        public event Action OnShowEqual;

        private readonly CardData _data;
        private readonly IBattleCardsConductor _conductor;
        private readonly ICard _card;

        public EffectType EffectType { get; }
        public List<SimpleStat> EffectStats { get; }
        public string PlayerId { get; }
        public string Name { get; }
        public int Damage { get; }
        public CardType Type { get; }
        public bool IsSelected { get; set; }

        public BattleCard(ICard card, string playerId, IBattleCardsConductor conductor)
        {
            _card = card;
            _conductor = conductor;
            IsSelected = true;
            PlayerId = playerId;
            EffectType = card.EffectType;
            EffectStats = card.EffectStats;
            Name = card.Name;
            Damage = card.Damage;
            Type = card.Type;
        }

        public void ChangeSelection()
        {
        }

        public void SetWin() => OnShowWin?.Invoke();

        public void SetLose() => OnShowLose?.Invoke();

        public void SetEqual() => OnShowEqual?.Invoke();

        public bool TrySetInBattleSlot(int battleSlotIndex) => _conductor.TrySetCard(this, battleSlotIndex);
        public float GetEffectStat(StatType amount) => _card.GetEffectStat(amount);
    }
}