using System;
using CodeBase._Tools.Helpers;
using TMPro;
using UnityEngine;
using VContainer;

namespace CodeBase.UI.Mediators
{
    public class EndBattleMediator : MonoBehaviour, IMediator
    {
        public event Action<IMediator> OnCleanUp;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private SimpleButton _exitToMenu;
        [SerializeField] private TMP_Text _gameOverMessage;
        private MediatorFactory _mediatorFactory;

        [Inject]
        public void Construct(MediatorFactory mediatorFactory)
        {
            _mediatorFactory = mediatorFactory;
        }
        
        public GameObject GameObject => gameObject;
        public void Show() => _canvasGroup.Show();
        public void Hide() => _canvasGroup.Hide();
        
        public void SetGameOverMessage(string message) => _gameOverMessage.text = message;

        private void Awake()
        {
            Hide();
            _exitToMenu.OnClick += ExitToMenu;
        }

        private void OnDestroy() => _exitToMenu.OnClick -= ExitToMenu;

        private void ExitToMenu()
        {
           _= _mediatorFactory.Show<MenuMediator>();
        }
    }
}