// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using System.Linq;
using HickeryDickery.UIUX;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HickeryDickery.Player
{
    [RequireComponent(typeof(PlayerPositionTracking))]
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private float _timeRewinded = 0f;
        [SerializeField] private float _totalRewindTime = 1000f;
        [SerializeField] private bool _controlTime = false;
        [SerializeField] private ITimeTraveler[] _positions;
        [SerializeField] private RewindMeterController _rewindUI;
        private float _timeDelta = 1;
        private static WaitForSecondsRealtime _wait16msRealtime = new WaitForSecondsRealtime(0.016f);

        void OnValidate()
        {
            _rewindUI = FindAnyObjectByType<RewindMeterController>();
            _positions = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ITimeTraveler>().ToArray();
            Time.timeScale = 1;
        }
        private void Start()
        {
            StartCoroutine(CapturePositionInRealTime());
        }
        private void Update()
        {
            Time.timeScale = Mathf.Clamp(_timeDelta, 0, 100);
            
            if (_timeDelta < 0 && _timeRewinded < _totalRewindTime)
            {
                foreach (var traveler in _positions)
                {
                    traveler.TravelThroughHistory();
                    if (traveler.IsAtStart())
                        continue;
                    ++_timeRewinded;
                    _rewindUI?.SetRewindMeter((1 - (_timeRewinded / _totalRewindTime)) * 100);
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
                    foreach (var traveler in _positions)
                    {
                        traveler.RecordHistory();
                    }
                }
            }
        }
        public bool ControllingTime() => _controlTime;
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
    }
}