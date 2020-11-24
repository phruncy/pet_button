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
        private int _resolution = 100;
        [Range(1.0f, 100.0f)]
        [SerializeField]
        private float _radius = 20.0f;
        [SerializeField]
        private CelestialObjectGenerator _generator = null;
        [SerializeField]
        private Icosahedron _baseBodyPrefab;

        // Start is called before the first frame update
        void Start()
        {
            _generator.Create(new SphereData(_radius, _resolution, new DetailedSphereSurfaceShaper(), new IcosahedronSphereBaseBodyCreator(_baseBodyPrefab)));
        }

    }
}