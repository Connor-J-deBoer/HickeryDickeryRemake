// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using System.Linq;
using HickeryDickery.UserInterface;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace HickeryDickery.Characters.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private float _totalRewindTime = 1000f;
        [HideInInspector] [SerializeField] private bool _controlTime = false;
        [HideInInspector] [SerializeField] private VisualElement _root;
        [SerializeField] private TimeTraveler[] _timeTravelers;
        private UXMLLoader _uxmlLoader;
        private static WaitForSecondsRealtime _wait16msRealtime = new WaitForSecondsRealtime(0.016f);
        private float _timeSpentRewinding = 0f;
        private float _timeDelta = 1;
        private bool _atStartOfTime => _timeTravelers.All(traveler => traveler.IsAtStart());
        public bool ControllingTime() => _controlTime;
        private void OnValidate()
        {
            Time.timeScale = 1;
        }
        private void Start()
        {
            _uxmlLoader = UXMLLoader.Instance;
            _root = _uxmlLoader.Load("RewindMeterUXML");
            _timeTravelers = FindObjectsByType<TimeTraveler>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            StartCoroutine(CapturePositionInRealTime());
        }
        private void Update()
        {
            if (_root == null || _uxmlLoader.GetCurrentUXMLName() != "RewindMeterUXML")
                _root = _uxmlLoader.Load("RewindMeterUXML");
            Time.timeScale = Mathf.Clamp(_timeDelta, 0, 100);
            if (_timeDelta < 0 && _timeSpentRewinding < _totalRewindTime && !_atStartOfTime)
            {
                foreach (var traveler in _timeTravelers)
                {
                    traveler.TravelThroughHistory();
                    if (traveler.IsAtStart())
                        continue;
                    ++_timeSpentRewinding;
                    float percentLeft = (1 - (_timeSpentRewinding / _totalRewindTime)) * 100;
                    _root.Q<VisualElement>("Fill").style.width = Length.Percent(percentLeft);
                }
            }
        }
        private IEnumerator CapturePositionInRealTime()
        {
            while (true)
            {
                yield return _wait16msRealtime;
                if (_timeDelta > 0)
                {
                    foreach (var traveler in _timeTravelers)
                    {
                        traveler.RecordHistory();
                    }
                }
            }
        }
        private void OnTimeControl(InputValue value)
        {
            if (!_controlTime)
                return;
            _timeDelta = value.Get<float>();
        }
        private void OnToggleTimeControl(InputValue _)
        {
            _controlTime = !_controlTime;
            _timeDelta = _controlTime ? 0 : 1;
        }
        public void AddTimeTraveler(TimeTraveler traveler)
        {
            if (_timeTravelers.Contains(traveler))
                return;
            var tempList = _timeTravelers.ToList();
            tempList.Add(traveler);
            _timeTravelers = tempList.ToArray();
        }
    }
}