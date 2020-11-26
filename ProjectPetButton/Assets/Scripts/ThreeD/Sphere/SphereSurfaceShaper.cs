using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gebaeckmeeting.ThreeD
{
    /// <summary>
    /// Provides a method for subdividing the surfaces of a Shpere
    /// </summary>
    public abstract class SphereSurfaceShaper
    {
        /// <summary>
        /// Subdivides the sphere's surfaces by a gven level of detail and updates the resulting vertices to the sphere's surface
        /// </summary>
        /// <param name="body">The sphere to be shaped</param>
        /// <param name="resolution">the level of detail of the subdivision</param>
        public abstract void Shape(Sphere body, int resolution);
    }
}