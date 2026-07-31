// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using UnityEngine;

namespace HickeryDickery.Obstacles
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenSpawn = 2;
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private GameObject _prefabToSpawn;
        [SerializeField] private Vector3 _bindPosition;
        [SerializeField] private Quaternion _bindRotation;
        private GameObject[] _pool;
        private int _currentPoolIndex = 0;
        private void Start()
        {
            _pool = new GameObject[_poolSize];
            for (int i = 0; i < _pool.Length; ++i)
            {
                _pool[i] = Instantiate(_prefabToSpawn, _bindPosition, _bindRotation);
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