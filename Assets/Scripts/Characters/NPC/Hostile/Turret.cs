// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using UnityEngine;

namespace HickeryDickery.Characters.NPC.Hostile
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float _fireRate = 1f;
        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private Transform _head;
        [SerializeField] private Spawner _spawner;
        private Transform _player;
        private Vector3 _bindForward;
        private Vector3 _directionToPlayer;
        private void OnValidate()
        {
            _spawner = GetComponent<Spawner>();
            _head = transform.GetChild(0);
        }
        private void Awake()
        {
            _player = FindAnyObjectByType<Player.PlayerLife>().transform;
            // capture the head's rest pose
            _bindForward = _head.forward;
            StartCoroutine(TryToFireAtPlayer());
        }
        private void Update()
        {
            if (_player == null || _head == null)
                return;

            _directionToPlayer = _player.position - _head.position;
            Vector3 flattenedDirection = Vector3.ProjectOnPlane(_directionToPlayer, _bindForward);

            if (flattenedDirection.sqrMagnitude > 0.0001f)
            {
                // forward slot -> local Z -> stays fixed on _twistAxis
                // up slot -> local Y -> swings to point at the player
                Quaternion targetRotation = Quaternion.LookRotation(_bindForward, flattenedDirection);
                _head.rotation = Quaternion.Slerp(_head.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }

        private IEnumerator TryToFireAtPlayer()
        {
            while (true)
            {
                yield return null;
                if (_directionToPlayer.sqrMagnitude < 0.0001f)
                    continue;
                if (Physics.Raycast(_head.position, _directionToPlayer.normalized, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.TryGetComponent(out Player.PlayerLife _))
                    {
                        Quaternion bulletRotation = Quaternion.LookRotation(_bindForward,_directionToPlayer.normalized);
                        _spawner.SpawnManual(_directionToPlayer.normalized * _bulletSpeed, _head.position, bulletRotation);
                        yield return new WaitForSeconds(_fireRate);
                    }
                }
            }
        }
    }
}