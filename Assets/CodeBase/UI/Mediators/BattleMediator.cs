using System;
using CodeBase._Tools.Helpers;
using UnityEngine;

namespace CodeBase.UI.Mediators
{
    public class BattleMediator : MonoBehaviour, IMediator
    {
        public event Action<IMediator> OnCleanUp;
        
        [SerializeField] private CanvasGroup _canvasGroup;

        public GameObject GameObject => gameObject;
        public void Show() => _canvasGroup.Show();
        public void Hide() => _canvasGroup.Hide();
        private void OnDestroy() => OnCleanUp?.Invoke(this);
    }
}