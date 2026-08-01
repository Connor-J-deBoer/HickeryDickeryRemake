// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;
using UnityEngine.InputSystem;

namespace HickeryDickery.Characters.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(TimeController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _height = 1;
        [SerializeField] private float _maxVelocity = 10;
        [SerializeField] private float _acceleration = 1000;
        [SerializeField] private float _jumpVelocityDelta = 750;
        [HideInInspector] [SerializeField] private Rigidbody _rigidbody;
        [HideInInspector] [SerializeField] private TimeController _timeController;
        private Vector3 _deltaVelocity = new ();
        private float _input = 0;
        private bool _grounded = false;
        private bool _jumped = false;
        private void OnValidate()
        {
            _timeController = GetComponent<TimeController>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxLinearVelocity = _maxVelocity;
        }
        private void FixedUpdate()
        {
            // This is a pretty primitive solution for movement, but I like how it feels with the time manipulation
            _rigidbody.AddForce(_deltaVelocity, ForceMode.Acceleration);
            // We use the analog input to add force left or right
            _deltaVelocity.x = _input * _acceleration;
            // If we jumped, stop jumping
            if (_jumped)
                _deltaVelocity.y = 0;
            // Check ground
            _grounded = Physics.Raycast(transform.position, Physics.gravity.normalized, _height);
            // Reset jump count on ground
            if (_grounded)
                _jumped = false;
        }
        private void OnMove(InputValue value)
        {
            if (!_grounded && !_timeController.ControllingTime())
            {
                _input = 0;
                return;
            }
            _input = value.Get<float>();
        }
        private void OnJump(InputValue _)
        {
            if (_jumped || !_grounded)
                return;
            _deltaVelocity += transform.up * _jumpVelocityDelta;
            _jumped = true;
        }
    }
}