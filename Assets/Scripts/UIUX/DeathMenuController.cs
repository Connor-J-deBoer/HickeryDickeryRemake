// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace HickeryDickery.UIUX
{
    public class DeathMenuController : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        private VisualElement _root;
        private void OnValidate()
        {
            _document = GetComponent<UIDocument>();
        }
        private void Awake()
        {
            _root = _document.rootVisualElement;
            _root.Q<Button>("Retry").RegisterCallback<ClickEvent>(_ =>
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            });
            _root.Q<Button>("Continue").RegisterCallback<ClickEvent>(_ =>
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
            });
        }

        public void OpenDeathMenu()
        {
            _root.Q<VisualElement>("Death").style.display = DisplayStyle.Flex;
        }
        public void OpenNotDeathMenu()
        {
            _root.Q<VisualElement>("NoDeath").style.display = DisplayStyle.Flex;
        }
    }
}