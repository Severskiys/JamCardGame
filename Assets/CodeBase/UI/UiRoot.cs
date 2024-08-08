using System;
using UnityEngine;

namespace CodeBase.UI
{
    public class UiRoot : MonoBehaviour
    {
        private static UiRoot _instance;

        private void Awake()
        {
            _instance = this;
           
        }

        public static float GetScale() => _instance.transform.localScale.x;
    }
}