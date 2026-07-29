// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;
using UnityEngine.InputSystem;

namespace HickeryDickery
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _height = 1;
        [SerializeField] private float _maxMoveVelocity = 10;
        [SerializeField] private float _moveVelocityMultiplier = 1000;
        [SerializeField] private float _jumpVelocityMultiplier = 250;
        [SerializeField] private Rigidbody _rigidbody;
        private Vector3 _deltaVelocity = new ();
        private Vector2 _input = new ();
        private bool _grounded;
        private bool _jumped;
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxLinearVelocity = _maxMoveVelocity;
        }
        private void Update()
        {
            _rigidbody.AddForce(_deltaVelocity, ForceMode.Acceleration);
            _deltaVelocity.x = _input.x * _moveVelocityMultiplier * Time.deltaTime;
            if (_jumped)
                _deltaVelocity.y = 0;
            _grounded = Physics.Raycast(transform.position, Vector3.down, _height);
            if (_grounded)
                _jumped = false;
        }
        private void OnMove(InputValue value)
        {
            _input = value.Get<Vector2>().normalized;
        }
        private void OnJump(InputValue _)
        {
            if (_jumped || !_grounded)
                return;
            _deltaVelocity.y = _jumpVelocityMultiplier;
            _jumped = true;
        }
    }
}
