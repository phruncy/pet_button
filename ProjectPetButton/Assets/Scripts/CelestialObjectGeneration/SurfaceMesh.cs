using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.PetButton
{
    public class SurfaceMesh : MonoBehaviour
    {
        [SerializeField]
        private MeshFilter _meshFilter;
        [SerializeField]
        private Transform _base;
        public Transform Base { get { return _base; } }

        public Mesh Mesh { get { return _meshFilter.sharedMesh; } }

        protected virtual void Awake()
		{
            _meshFilter.sharedMesh = new Mesh();
        }
    }
}