// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using HickeryDickery.UserInterface;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace HickeryDickery.Characters.Player
{
    public class PlayerLife : MonoBehaviour, IPlayer, IKillable
    {
        [HideInInspector] [SerializeField] private PlayerMovement _movement;
        [HideInInspector] [SerializeField] private TimeController _timeController;
        private UXMLLoader _uxmlLoader;
        private bool _dead = false;
        public bool IsDead() => _dead;
        private bool _won = false;
        public bool HasWon() => _won;
        void OnValidate()
        {
            _movement = GetComponent<PlayerMovement>();
            _timeController = GetComponent<TimeController>();
        }
        private void Awake()
        {
            SetPlayerActive(true);
            _uxmlLoader = UXMLLoader.Instance;
        }
        public void SetPlayerActive(bool active)
        {
            _movement.enabled = active;
            _timeController.enabled = active;
            Time.timeScale = active ? 1 : 0;
        }
        public void Die()
        {
            if (_won)
                return;
            Debug.Log("Player Died!");
            SetPlayerActive(false);
            _dead = true;
            var root = _uxmlLoader.Load("DeathMenuUXML");
            root.Q<Button>("Retry").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            });
        }
        public void Win()
        {
            if (_dead)
                return;
            Debug.Log("Player Won!");
            SetPlayerActive(false);
            _won = true;
            var root = _uxmlLoader.Load("WinMenuUXML");
            root.Q<Button>("Continue").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
            });
        }
    }
}