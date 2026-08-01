// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace HickeryDickery.Characters.NPC
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenSpawn = 2;
        [SerializeField] private float _automaticDespawnTime = 20f;
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private GameObject _prefabToSpawn;
        [SerializeField] private Vector3 _bindPosition;
        [SerializeField] private Quaternion _bindRotation;
        private GameObject[] _pool;
        private int _currentPoolIndex = 0;
        private void Awake()
        {
            _pool = new GameObject[_poolSize];
            for (int i = 0; i < _pool.Length; ++i)
            {
                _pool[i] = Instantiate(_prefabToSpawn, _bindPosition, _bindRotation);
                var despawner = _pool[i].AddComponent<Despawner>();
                despawner.LifeTime = _automaticDespawnTime;
                _pool[i].SetActive(false);
            }
            StartCoroutine(Respawner());
        }

        private IEnumerator Respawner()
        {
            while (true)
            {
                GameObject gameObject = _pool[_currentPoolIndex];
                gameObject.SetActive(false);
                yield return new WaitForSeconds(_timeBetweenSpawn);
                gameObject.transform.SetPositionAndRotation(_bindPosition, _bindRotation);
                gameObject.TryGetComponent(out Rigidbody rb);
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.SetActive(true);
                _currentPoolIndex = _currentPoolIndex < _poolSize - 1 ? _currentPoolIndex + 1 : 0;
            }
        }
    }
}