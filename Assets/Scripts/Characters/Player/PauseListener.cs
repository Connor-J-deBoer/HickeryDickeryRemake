// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using HickeryDickery.UserInterface;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace HickeryDickery.Characters.Player
{
    public class PauseListener : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private PlayerLife _playerLife;
        private Coroutine _opacity;
        private string _previousUIName = "";
        private UXMLLoader _uxmlLoader;
        private bool _paused = false;

        private void OnValidate()
        {
            _playerLife = GetComponent<PlayerLife>();
        }
        private void Awake()
        {
            _uxmlLoader = UXMLLoader.Instance;
        }
        public void OnPause(InputValue _)
        {
            if (_playerLife.IsDead() || _playerLife.HasWon())
                return;
            _paused = !_paused;
            if (_paused)
            {
                _playerLife.SetPlayerActive(false);
                _previousUIName = _uxmlLoader.GetCurrentUXMLName();
                var root = _uxmlLoader.Load("PauseMenuUXML");
                if (_opacity != null)
                    StopCoroutine(_opacity);
                _opacity = StartCoroutine(SetOpacity(root.Q<Label>()));
            }
            else
            {
                _playerLife.SetPlayerActive(true);
                if (_uxmlLoader.GetCurrentUXMLName() != "PauseMenuUXML")
                    return;
                if (_opacity != null)
                    StopCoroutine(_opacity);
                
                if (!string.IsNullOrEmpty(_previousUIName))
                    _uxmlLoader.Load(_previousUIName);
                else
                    _uxmlLoader.UnLoad();
                _opacity = null;
            }
        }
        private IEnumerator SetOpacity(Label pause)
        {
            while (true)
            {
                pause.style.opacity = (pause.style.opacity == 0.9f) ? 0.2f : 0.9f;
                yield return new WaitForSecondsRealtime (0.5f);
            }
        }
    }
}