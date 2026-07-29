// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;
using UnityEngine.InputSystem;

namespace HickeryDickery.Player
{
    [RequireComponent(typeof(PlayerPositionTracking))]
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private PlayerPositionTracking _positions;
        [SerializeField] private Rigidbody _rigidbody;
        private float _timeDelta;
        void OnValidate()
        {
            _positions = GetComponent<PlayerPositionTracking>();
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            Time.timeScale = Mathf.Max(_timeDelta * Time.unscaledDeltaTime, 0);
            if (_timeDelta > 0)
                _positions.AddPosition();
            else if (_timeDelta < 0)
                (transform.position, _rigidbody.linearVelocity, _rigidbody.angularVelocity, transform.rotation) = _positions.GetPreviousPosition();
        }
        private void OnTimeControl(InputValue value)
        {
            _timeDelta = value.Get<float>();
        }
    }
}