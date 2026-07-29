// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Player
{
    public class PlayerLife : MonoBehaviour, IPlayer, IKillable
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
            DeactivatePlayer();
            //TODO: Call some nice UI
        }

        public void Win()
        {
            Debug.Log("Player Won!");
            DeactivatePlayer();
            //TODO: Call some nice UI
        }
        private void DeactivatePlayer()
        {
            _movement.enabled = false;
            _positionTracking.enabled = false;
            _timeController.enabled = false;
            Time.timeScale = 0;
        }
    }
}