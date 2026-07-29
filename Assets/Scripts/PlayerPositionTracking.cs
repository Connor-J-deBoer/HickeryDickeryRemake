// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery
{
    public class PlayerPositionTracking : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        private PlayerPosition _start;
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
            if (_start.Previous == null)
                return _start.GetProperties();
            PlayerPosition previous = _start;
            _start = _start.Previous;
            return previous.GetProperties();;
        }
    }
}