// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using HickeryDickery.UIUX;
using UnityEngine;

namespace HickeryDickery.Player
{
    public class PlayerPositionTracking : MonoBehaviour
    {
        [SerializeField] private RewindMeterController _rewindUI;
        [SerializeField] private float _totalRewindTime = 10;
        [SerializeField] private Rigidbody _rigidbody;
        private PlayerPosition _start;
        private float _rewindTime = 0;
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rewindUI = FindAnyObjectByType<RewindMeterController>();
        }
        private void Awake()
        {
            _start = new ()
            {
                Position = transform.position,
                Rotation = transform.rotation,
                Previous = null
            };
        }
        public void AddPosition()
        {
            PlayerPosition newPosition = new ()
            {
                Position = transform.position,
                LinearVelocity = _rigidbody.linearVelocity,
                AngularVelocity = _rigidbody.angularVelocity,
                Rotation = transform.rotation,
                Previous = _start
            };
            _start = newPosition;
        }
        public (Vector3, Vector3, Vector3, Quaternion) GetPreviousPosition()
        {
            if (_rewindTime >= _totalRewindTime)
                return (transform.position, _rigidbody.linearVelocity, _rigidbody.angularVelocity, transform.rotation);
            if (_start.Previous == null)
                return _start.GetProperties();
            PlayerPosition previous = _start;
            _start = _start.Previous;
            ++_rewindTime;
            _rewindUI?.SetRewindMeter((1 - (_rewindTime / _totalRewindTime)) * 100);
            return previous.GetProperties();;
        }
    }
}