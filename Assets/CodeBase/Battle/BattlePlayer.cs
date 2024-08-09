using System;
using System.Collections.Generic;
using CodeBase.Cards;
using CodeBase.StaticData;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Battle
{
    public class BattlePlayer : IPlayer, IBattleCardsConductor
    {
        public event Action OnChangeCardsState;
        public event Action OnWin;
        public event Action OnLose;
        public event Action OnChangeHealth;
        public event Action OnMoveHandToDiscard;
        public event Action OnMoveCardsFromBattleToDiscard;
        public event Action PrepareToCompareState;

        public Action OnSetBattleCards;

        private string _id;
        private readonly IBattleRoom _battleRoom;
        public bool IsAlive => Health > 0;
        public string Id { get; }
        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public int Armor { get; private set; }
        public int HandSize { get; private set; }
        public List<ICard> Deck { get; }
        public List<ICard> Hand { get; }
        public List<ICard> SetToBattle { get; }
        public List<ICard> Discard { get; }
        public Dictionary<EffectType, IEffect> Effects { get; }

        public BattlePlayer(List<ICard> deck, string id, int health, int handSize, IBattleRoom battleRoom)
        {
            _battleRoom = battleRoom;
            Id = id;
            Health = health;
            MaxHealth = health;
            HandSize = handSize;
            Deck = new List<ICard>();
            Hand = new List<ICard>();
            Discard = new List<ICard>();
            SetToBattle = new List<ICard>();
            Effects = new Dictionary<EffectType, IEffect>();
            foreach (var card in deck)
                Deck.Add(new BattleCard(card, id, this));
        }

        public void FillHandToFull()
        {
            while (Hand.Count < HandSize)
            {
                if (Deck.Count <= 0)
                {
                    foreach (var card in Discard)
                        Deck.Add(card);
                    Discard.Clear();
                }

                var rndCard = Deck[Random.Range(0, Deck.Count)];
                CheckCardsEffects(rndCard);
                Deck.Remove(rndCard);
                Hand.Add(rndCard);
            }

            OnChangeCardsState?.Invoke();
        }

        private void CheckCardsEffects(ICard rndCard)
        {
            EffectType checkEffect = rndCard.Type switch
            {
                CardType.Rock => EffectType.StoneBan,
                CardType.Scissor => EffectType.ScissorsBan,
                CardType.Paper => EffectType.PaperBan,
                _ => EffectType.NONE
            };
            
            if (HaveEffect(checkEffect))
            {
                rndCard.IsBanned = true;
                if (Effects[checkEffect].CheckExpired())
                    Effects.Remove(checkEffect);
            }
            else
            {
                if (HaveEffect(EffectType.HealBan) && rndCard.EffectType == EffectType.Heal)
                {
                    rndCard.IsBanned = true;
                    if (Effects[EffectType.HealBan].CheckExpired())
                        Effects.Remove(EffectType.HealBan);
                }
                else
                {
                    rndCard.IsBanned = false;  
                }
            }
        }

        public void ClearHandsToDiscard()
        {
            foreach (var card in Hand)
                Discard.Add(card);

            OnMoveHandToDiscard?.Invoke();
            Hand.Clear();
        }

        public void DiscardCardsFromBattle()
        {
            foreach (var card in SetToBattle)
                Discard.Add(card);

            OnMoveCardsFromBattleToDiscard?.Invoke();
            SetToBattle.Clear();
        }

        public void SetDamage(int damage)
        {
            int pearceDamage = damage - Armor;
            Armor = Mathf.Max(0, Armor - damage);
            Health -= pearceDamage;
            OnChangeHealth?.Invoke();
        }

        public void SetPrepareToCompareState()
        {
            foreach (var card in Hand)
                CheckCardsEffects(card);
            
            PrepareToCompareState?.Invoke();
        }

        public void AddEffect(IEffect effect)
        {
            if (Effects.TryGetValue(effect.Type, out var existingEffect))
                existingEffect.TryAddDuration(effect);
            else
                Effects.Add(effect.Type, effect);
        }

        public void Heal(float healAmount)
        {
            Health += Mathf.CeilToInt(healAmount);
            Health = Mathf.Min(Health, MaxHealth);
            OnChangeHealth?.Invoke();
        }

        public void AddShield(float amount)
        {
            Armor += Mathf.CeilToInt(amount);
            OnChangeHealth?.Invoke();
        }

        public void ClearAllEffects()
        {
            Effects.Clear();
        }

        public virtual void StartSelectingCardsToBattle()
        {
        }

        public bool HaveEffect(EffectType effectType) => Effects.ContainsKey(effectType);
        public float GetEffectValue(EffectType effectType)
        {
            float value = Effects[effectType].GetStat(StatType.Amount);

            if (Effects[effectType].CheckExpired())
                Effects.Remove(effectType);

            return value;
        }

        public void SetLose() => OnLose?.Invoke();

        public void SetWin() => OnWin?.Invoke();

        public bool TrySetCard(ICard card, int slotIndex)
        {
            if (_battleRoom.TrySetCard(card, slotIndex))
            {
                Hand.Remove(card);
                SetToBattle.Add(card);
                return true;
            }

            return false;
        }
    }
}