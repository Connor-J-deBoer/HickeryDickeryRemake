// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HickeryDickery.Player
{
    [RequireComponent(typeof(PlayerPositionTracking))]
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private bool _controlTime = false;
        [SerializeField] private PlayerPositionTracking _positions;
        [SerializeField] private Rigidbody _rigidbody;
        private float _timeDelta = 1;
        void OnValidate()
        {
            _positions = GetComponent<PlayerPositionTracking>();
            _rigidbody = GetComponent<Rigidbody>();
            Time.timeScale = 1;
        }
        private void Start()
        {
            StartCoroutine(CapturePositionInRealTime());
        }
        private void Update()
        {
            Time.timeScale = Mathf.Clamp(_timeDelta, 0, 100);
            
            if (_timeDelta < 0)
                (transform.position, _rigidbody.linearVelocity, _rigidbody.angularVelocity, transform.rotation) = _positions.GetPreviousPosition();
        }

        private IEnumerator CapturePositionInRealTime()
        {
            while (true)
            {

                if (!_controlTime)
                    yield return new WaitForSecondsRealtime(0.1f);
                else
                    yield return new WaitForEndOfFrame();
                if (_timeDelta > 0)
                    _positions.AddPosition();
            }
        }
        private void OnTimeControl(InputValue value)
        {
            if (!_controlTime)
                return;
            _timeDelta = value.Get<float>();
        }
        private void OnToggleTimeControl(InputValue _)
        {
            _controlTime = !_controlTime;
            _timeDelta = _controlTime ? 0 : 1;
        }
    }
}