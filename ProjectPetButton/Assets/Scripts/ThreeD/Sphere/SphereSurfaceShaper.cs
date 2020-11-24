using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gebaeckmeeting.ThreeD
{
    public abstract class SphereSurfaceShaper
    {
        public abstract void Shape(Sphere body, int resolution, float radius);
    }
}