using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
    /// <summary>
    /// A container for information about a sphere body 
    /// </summary>
    public class SphereData 
    {
        public float Radius { get; set; }
        public int Resolution { get; set; }
        public SphereSurfaceShaper Shaper { get; }
        public SphereBaseBodyCreator BaseBodyCreator { get; }

        public SphereData(float radius,
            int resolution,
            SphereSurfaceShaper shaper,
            SphereBaseBodyCreator baseBodyCreator)
		{
            Assert.IsTrue(radius > 0.0);
            Assert.IsTrue(resolution > 0);
            Assert.IsNotNull(shaper);
            Assert.IsNotNull(baseBodyCreator);
            Radius = radius;
            Resolution = resolution;
            Shaper = shaper;
            BaseBodyCreator = baseBodyCreator;
        }
    }
}