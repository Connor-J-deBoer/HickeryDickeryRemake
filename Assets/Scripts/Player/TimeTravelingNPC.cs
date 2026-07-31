// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using HickeryDickery.Player;
using UnityEngine;

public class TimeTravelingNPC : MonoBehaviour, ITimeTraveler
{
    private History _start;
    private Rigidbody _rigidbody;
    public bool IsAtStart() => _start.Previous == null;
    public History Start { get => _start; }
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
    public void RecordHistory()
    {
        History newPosition = new ()
        {
            Position = transform.position,
            LinearVelocity = _rigidbody.linearVelocity,
            AngularVelocity = _rigidbody.angularVelocity,
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