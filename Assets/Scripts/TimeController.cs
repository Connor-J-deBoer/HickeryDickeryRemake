// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;
using UnityEngine.InputSystem;

namespace HickeryDickery
{
    public class TimeController : MonoBehaviour
    {
        private float _timeDelta;
        private void Update()
        {
            Time.timeScale = Mathf.Max(_timeDelta * Time.unscaledDeltaTime, 0);
        }
        private void OnTimeControl(InputValue value)
        {
            _timeDelta = value.Get<float>();
        }
    }
}