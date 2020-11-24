using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gebaeckmeeting.PetButton
{
    public class PlanetCreator : MonoBehaviour
    {
        [Range(1, 9)]
        [SerializeField]
        private int _resolution = 5;
        [Range(1.0f, 100.0f)]
        [SerializeField]
        private float _radius = 20.0f;
        [SerializeField]
        private CelestialObjectGenerator _generator = null;

        // Start is called before the first frame update
        void Start()
        {
            _generator.Create(new CelestialObjectSurfaceData(_radius, _resolution));
        }

    }
}