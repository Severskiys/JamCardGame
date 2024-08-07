using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button CloseButton;

        private void Awake() => OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => Cleanup();

        protected void OnAwake() => CloseButton.onClick.AddListener(() => Destroy(gameObject));

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void Cleanup()
        {
        }
    }
}