// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using UnityEngine;

namespace HickeryDickery.Characters.NPC
{
    public class Despawner : MonoBehaviour, IKillable
    {
        public float LifeTime;
        private Coroutine _despawnCoroutine;
        private void OnEnable()
        {
            if (LifeTime == 0)
                return;
            _despawnCoroutine = StartCoroutine(WaitAndDespawn());
        }
        private void OnDisable()
        {
            if (_despawnCoroutine != null)
                StopCoroutine(_despawnCoroutine);
        }
        private void Despawn()
        {
            gameObject.SetActive(false);
        }
        private IEnumerator WaitAndDespawn()
        {
            yield return new WaitForSeconds(LifeTime);
            Despawn();
        }
        public void Die()
        {
            Despawn();
        }
    }
}