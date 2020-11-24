using Gebaeckmeeting.ThreeD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gebaeckmeeting.PetButton
{
    public class PlanetCreator : MonoBehaviour
    {
        [Range(1, 200)]
        [SerializeField]
        private int _resolution = 300;
        [Range(20.0f, 50.0f)]
        [SerializeField]
        private float _radius = 20.0f;
        [SerializeField]
        private CelestialObjectGenerator _generator = null;
        [SerializeField]
        private Icosahedron _baseBodyPrefab;

        // Start is called before the first frame update
        void Start()
        {
            Body result = _generator.Create(new SphereData(_radius, _resolution, new DetailedSphereSurfaceShaper(), new IcosahedronSphereBaseBodyCreator(_baseBodyPrefab)));
            result.transform.localPosition = Vector3.zero;
        }

    }
}