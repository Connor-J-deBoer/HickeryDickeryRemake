// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved


using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace HickeryDickery.UIUX
{
    public class RewindMeterController : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        private VisualElement _root;
        private VisualElement _fill;
        private void OnValidate()
        {
            _document = GetComponent<UIDocument>();
        }
        private void Awake()
        {
            _root = _document.rootVisualElement;
            _fill = _root.Q<VisualElement>("Fill");
        }
        public void SetRewindMeter(float percent)
        {
            _fill.style.width = Length.Percent(percent);
        }
    }
}