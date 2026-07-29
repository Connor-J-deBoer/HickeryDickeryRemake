// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Player
{
    public class PlayerLife : MonoBehaviour, IKillable
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerPositionTracking _positionTracking;
        [SerializeField] private TimeController _timeController;
        void OnValidate()
        {
            _movement = GetComponent<PlayerMovement>();
            _positionTracking = GetComponent<PlayerPositionTracking>();
            _timeController = GetComponent<TimeController>();
        }
        public void Die()
        {
            Debug.Log("Player Died!");
            _movement.enabled = false;
            _positionTracking.enabled = false;
            _timeController.enabled = false;
            //TODO: Call some nice UI
        }
    }
}