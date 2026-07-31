// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace HickeryDickery.UIUX
{
    public class PauseListener : MonoBehaviour
    {
        public UnityEvent OnGamePaused = new ();
        public UnityEvent OnGameUnPaused = new ();
        private bool _paused = false;
        private void OnPause(InputValue value)
        {
            _paused = !_paused;
            if (_paused)
                OnGamePaused.Invoke();
            else
                OnGameUnPaused.Invoke();
        }
    }
}