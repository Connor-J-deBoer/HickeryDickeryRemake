// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace HickeryDickery.UserInterface
{
    public class UXMLLoader : MonoBehaviour
    {
        [SerializeField] private List<VisualTreeAsset> _uxmls;
        [SerializeField] private UIDocument _document;
        private static UXMLLoader _instance;
        public static UXMLLoader Instance 
        { 
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<UXMLLoader>();
                    if (_instance == null)
                    {
                        var prefab = Resources.Load<GameObject>("Prefabs/UIDocuments/UXMLLoader");
                        GameObject loaderObject = Instantiate(prefab);
                        _instance = loaderObject.GetComponent<UXMLLoader>();
                    }
                }
                return _instance;
            }
        }
        private void OnValidate()
        {
            _document = GetComponent<UIDocument>();
        }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }
        protected virtual void OnDestroy() { if (_instance == this) _instance = null; }
        public VisualElement Load(string name)
        {
            if (_uxmls == null || _uxmls.Count == 0)
            {
                Debug.LogError("No UXML files assigned to UXMLLoader.");
                return null;
            }
            _document ??= GetComponent<UIDocument>();
            foreach (var uxml in _uxmls)
            {
                if (uxml.name == name)
                {
                    _document.visualTreeAsset = uxml;
                    return _document.rootVisualElement;
                }
            }
            Debug.LogError($"UXML with name {name} not found in UXMLLoader.");
            return null;
        }
        public void UnLoad()
        {
            _document ??= GetComponent<UIDocument>();
            _document.visualTreeAsset = null;
        }
        public string GetCurrentUXMLName()
        {
            _document ??= GetComponent<UIDocument>();
            return (_document.visualTreeAsset != null) ? _document.visualTreeAsset.name : "";
        }
    }
}