// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System.Collections;
using UnityEngine;

namespace HickeryDickery.Characters.NPC.Gimmik
{
    public class Teleporter : AGimmik
    {
        [SerializeField] private Transform _teleportDestination;
        private GameObject _feedbackPrefab;
        // Can't use awake because AGimmik does
        private void Start()
        {
            _feedbackPrefab = Resources.Load<GameObject>("Prefabs/Environment/Gimmik/TeleportEffect");
        }
        protected override void Gimmik(Rigidbody rigidbody, Collider collider, RaycastHit hit)
        {
            if (_teleportDestination == null || _feedbackPrefab == null)
                return;
            StartCoroutine(TeleportFeedback(collider.transform));
        }
        private IEnumerator TeleportFeedback(Transform transform)
        {
            transform.gameObject.SetActive(false);
            GameObject startingFeedback = Instantiate(_feedbackPrefab, transform.position + new Vector3(0, 0, -1), transform.rotation);
            GameObject endingFeedback = Instantiate(_feedbackPrefab, _teleportDestination.position + new Vector3(0, 0, -1), _teleportDestination.rotation);
            yield return new WaitForSeconds(1f);
            transform.position = _teleportDestination.position;
            transform.gameObject.SetActive(true);
            Destroy(startingFeedback);
            Destroy(endingFeedback);
        }
    }
}