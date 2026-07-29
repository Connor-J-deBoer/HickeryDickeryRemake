// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;
using UnityEngine.InputSystem;

namespace HickeryDickery
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
        [SerializeField] private Rigidbody _rigidbody;
        private Vector3 _deltaVelocity = new ();
        private float _input = 0;
        private bool _grounded = false;
        private bool _jumped = false;
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxLinearVelocity = _maxVelocity;
        }
        private void Update()
        {
            // This is a pretty primitive solution for movement, but I like how it feels with the time manipulation
            _rigidbody.AddForce(_deltaVelocity, ForceMode.Acceleration);
            // We use the analog input to add force left or right
            _deltaVelocity.x = _input * _acceleration * Time.deltaTime;
            // If we jumped, stop jumping
            if (_jumped)
                _deltaVelocity.y = 0;
            // Check ground
            _grounded = Physics.Raycast(transform.position, Vector3.down, _height);
            // Reset jump count on ground
            if (_grounded)
                _jumped = false;
        }
        private void OnMove(InputValue value)
        {
            _input = value.Get<float>();
        }
        private void OnJump(InputValue _)
        {
            if (_jumped || !_grounded)
                return;
            _deltaVelocity.y = _jumpVelocityDelta;
            _jumped = true;
        }
    }
}