﻿using System;
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
        public EffectType EffectType { get; }
        public List<StatType> EffectStats { get; }
        public string PlayerId { get;}
        public string Name { get;}
        public int Damage { get;}
        public CardType Type { get;}
        public bool IsSelected { get; set; }
        
        private readonly CardData _data;
        private IBattleCardsConductor _conductor;

        public BattleCard(ICard card, string playerId, IBattleCardsConductor conductor)
        {
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
        
    }
}