// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Characters
{
    public class TimeTraveler : MonoBehaviour
    {
        private History _start;
        private Rigidbody _rigidbody;
        public bool IsAtStart() => _start.Previous == null;
        public History Start { get => _start; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _start = new ()
            {
                Position = transform.position,
                Rotation = transform.rotation,
                Previous = null
            };
        }
        public void RecordHistory()
        {
            Vector3 linearVelocity = _rigidbody != null ? _rigidbody.linearVelocity : Vector3.zero;
            Vector3 angularVelocity = _rigidbody != null ? _rigidbody.angularVelocity : Vector3.zero;
            History newPosition = new ()
            {
                Position = transform.position,
                LinearVelocity = linearVelocity,
                AngularVelocity = angularVelocity,
                Rotation = transform.rotation,
                Previous = _start
            };
            _start = newPosition;
        }
        public void TravelThroughHistory()
        {
            History previous = _start;
            previous.TravelBackwards(transform, _rigidbody);
            if (_start.Previous != null)
                _start = _start.Previous;
        }
    }
}