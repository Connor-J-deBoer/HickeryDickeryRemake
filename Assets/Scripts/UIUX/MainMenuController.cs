// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace HickeryDickery.Obstacles
{
    public class MainMenuController : MonoBehaviour
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
            _root.Q<Button>("Play").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(11); 
            });
            _root.Q<Button>("LevelSelect").RegisterCallback<ClickEvent>(evt =>
            {
                _root.Q<VisualElement>("Options").style.display = DisplayStyle.None;
                _root.Q<VisualElement>("Levels").style.display = DisplayStyle.Flex;
            });
            _root.Q<Button>("Back").RegisterCallback<ClickEvent>(evt =>
            {
                _root.Q<VisualElement>("Options").style.display = DisplayStyle.Flex;
                _root.Q<VisualElement>("Levels").style.display = DisplayStyle.None;
            });
            _root.Q<Button>("LevelOne").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(1); 
            });
            _root.Q<Button>("LevelTwo").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(2); 
            });
            _root.Q<Button>("LevelThree").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(3); 
            });
            _root.Q<Button>("LevelFour").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(4); 
            });
            _root.Q<Button>("LevelFive").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(5); 
            });
            _root.Q<Button>("LevelSix").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(6); 
            });
            _root.Q<Button>("LevelSeven").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(7); 
            });
            _root.Q<Button>("LevelEight").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(8); 
            });
            _root.Q<Button>("LevelNine").RegisterCallback<ClickEvent>(evt =>
            {
                SceneManager.LoadScene(9); 
            });
        }
    }
}