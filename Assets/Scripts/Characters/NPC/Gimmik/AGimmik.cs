// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Characters.NPC.Gimmik
{
    public abstract class AGimmik : MonoBehaviour
    {
        // We're trying to save some performance as this will be web based, this likely wasn't 
        // going to be an issue with the scope of this project, but better over built than under
        [SerializeField] private int _maxSimultaneousActions = 5;
        [HideInInspector] [SerializeField] BoxCollider _collider;
        // We define a box to look for overlaps
        private Vector3 _center;
        private Vector3 _halfExtends;
        // This array is overwritten in update
        private Collider[] _cashedColliders;
        private void OnValidate()
        {
            _collider = GetComponent<BoxCollider>();
            CalculateBox();
        }
        private void Awake()
        {
            _cashedColliders = new Collider[_maxSimultaneousActions];
        }
        private void FixedUpdate()
        {
            // We check all the objects we found in the tiny box we made right above our collider
            int count = Physics.OverlapBoxNonAlloc(_center, _halfExtends, _cashedColliders, transform.rotation);
            for (int i = 0; i < count; ++i)
            {
                if (_cashedColliders[i] == null)
                    continue;
                if (!_cashedColliders[i].gameObject.TryGetComponent(out Rigidbody rb))
                    continue;
                if (!Physics.Raycast(rb.position, Physics.gravity.normalized, out RaycastHit hit))
                    continue;
                // If we made it here, it means we should do our gimmik on whatever is on top
                Gimmik(rb, _cashedColliders[i], hit);
            }
        }
        /// <summary>
        /// We use the collider attached to us to define a tiny box for anything on top of us
        /// </summary>
        private void CalculateBox()
        {
            Vector3 halfExtend = _collider.size * 0.5f;
            halfExtend.y = 0.1f;
            _halfExtends = Vector3.Scale(halfExtend, transform.lossyScale);
            Vector3 center = _collider.center;
            center.y = _collider.size.y / 2;
            _center = transform.position + (transform.rotation * Vector3.Scale(center, transform.lossyScale));
        }
        // For debugging
        private void OnDrawGizmosSelected()
        {
            CalculateBox();
            Gizmos.color = Color.green;
            // Cache the standard gizmo matrix
            Matrix4x4 oldMatrix = Gizmos.matrix;
            // Rotate the gizmo drawer to match your transform
            Gizmos.matrix = Matrix4x4.TRS(_center, transform.rotation, Vector3.one);
            // Draw the wire cube at local (0,0,0) relative to the shifted matrix
            Gizmos.DrawWireCube(Vector3.zero, _halfExtends * 2f);
            // Restore the standard matrix
            Gizmos.matrix = oldMatrix;
        }
        // Now we just have to overwrite this one method to do anything to something on top of us
        protected virtual void Gimmik(Rigidbody rigidbody, Collider collider, RaycastHit hit) {}
    }
}