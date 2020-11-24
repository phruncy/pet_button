using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.PetButton
{
    public class CelestialObjectSurfaceData 
    {
        public float Radius { get; }
        public int Resolution { get; }

        public CelestialObjectSurfaceData(float radius,
            int resolution)
		{
            Assert.IsTrue(radius > 0.0);
            Assert.IsTrue(resolution > 0);
            Radius = radius;
            Resolution = resolution;
        }
    }
}