// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace HickeryDickery.UIUX
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        private VisualElement _root;
        private Label _pause;
        private Coroutine _opacity;
        private void OnValidate()
        {
            _document = GetComponent<UIDocument>();
        }
        private void Start()
        {
            _root = _document.rootVisualElement;
            _pause = _root.Q<Label>();
            _opacity = null;
        }
        public void OpenPauseMenu()
        {
            Time.timeScale = 0;
            _pause.style.opacity = 0.9f;
            if (_opacity != null)
                StopCoroutine(_opacity);
            _opacity = StartCoroutine(SetOpacity());
        }
        public void ClosePauseMenu()
        {
            Time.timeScale = 1;
            _pause.style.opacity = 0f;
            if (_opacity == null)
                return;
            StopCoroutine(_opacity);
            _opacity = null;
        }
        private IEnumerator SetOpacity()
        {
            while (true)
            {
                _pause.style.opacity = (_pause.style.opacity == 0.9f) ? 0.2f : 0.9f;
                yield return new WaitForSecondsRealtime (0.5f);
            }
        }
    }
}