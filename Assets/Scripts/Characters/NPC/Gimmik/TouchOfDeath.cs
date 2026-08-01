// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using HickeryDickery.Characters.Player;
using UnityEngine;
using UnityEngine.Rendering;

namespace HickeryDickery.Characters.NPC.Gimmik
{
    public class TouchOfDeath : MonoBehaviour
    {
        public float Delay = 10;
        public IKillable ThingToKill;
        private Coroutine _dieLaterCoroutine;
        private GameObject _feedbackPrefab;
        private Volume _feedbackVolume;
        private float _timeSinceEnabled = 0f;
        private void OnEnable()
        {
            if (gameObject.TryGetComponent(out PlayerLife _))
            {
                _feedbackPrefab = Resources.Load<GameObject>("Prefabs/Environment/Gimmik/TouchOfDeathFeedback");
                _feedbackPrefab = Instantiate(_feedbackPrefab);
                _feedbackVolume = _feedbackPrefab.GetComponent<Volume>();
            }
            _timeSinceEnabled = 0f;
            _dieLaterCoroutine = StartCoroutine(DieLater());
        }
        private void OnDisable()
        {
            if (_dieLaterCoroutine != null)
                StopCoroutine(_dieLaterCoroutine);
            Destroy(this);
        }
        private IEnumerator DieLater()
        {
            while (_timeSinceEnabled < Delay)
            {
                _timeSinceEnabled += Time.deltaTime;
                if (_feedbackVolume != null)
                    _feedbackVolume.weight = Mathf.Clamp01(_timeSinceEnabled / Delay);
                yield return null;
            }
            if (ThingToKill != null)
                ThingToKill.Die();
        }
    }
}