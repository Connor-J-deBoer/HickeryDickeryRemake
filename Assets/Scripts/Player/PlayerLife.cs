// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Linq;
using HickeryDickery.UIUX;
using UnityEngine;

namespace HickeryDickery.Player
{
    public class PlayerLife : MonoBehaviour, IPlayer, IKillable
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerPositionTracking _positionTracking;
        [SerializeField] private DeathMenuController _death;
        [SerializeField] private TimeController _timeController;
        void OnValidate()
        {
            _movement = GetComponent<PlayerMovement>();
            _positionTracking = GetComponent<PlayerPositionTracking>();
            _timeController = GetComponent<TimeController>();
            _death = FindAnyObjectByType<DeathMenuController>();
        }
        private void Awake()
        {
            SetPlayerActive(true);
        }
        public void Die()
        {
            Debug.Log("Player Died!");
            SetPlayerActive(false);
            _death?.OpenDeathMenu();
        }

        public void Win()
        {
            Debug.Log("Player Won!");
            SetPlayerActive(false);
            _death?.OpenNotDeathMenu();
        }
        public void SetPlayerActive(bool active)
        {
            _movement.enabled = active;
            _positionTracking.enabled = active;
            _timeController.enabled = active;
            Time.timeScale = active ? 1 : 0;
        }
    }
}