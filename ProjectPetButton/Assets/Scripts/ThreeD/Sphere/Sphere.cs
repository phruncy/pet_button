using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
    public class Sphere : Body
    {
        public SphereData Data { get; set; } = null;

		public override void GenerateMesh()
        {
            Assert.IsNotNull(Data, "Please provide Data before trying to generate the mesh");
            Body baseBody = Data.BaseBodyCreator.Create(Data.Radius);
            copySurfaces(baseBody);
            Data.Shaper.Shape(this, Data.Resolution, Data.Radius);
            GameObject.Destroy(baseBody.gameObject);
        }

        private void copySurfaces(Body body)
        {
            foreach (Surface surface in Surfaces)
                Destroy(surface.Base);

            createSurfaceMeshes(body.Surfaces.Length);
            for (int i = 0; i < body.Surfaces.Length; i++)
            {
                Surface other = body.Surfaces[i];
                Surfaces[i].UpdateMesh(other.Vertices, other.Faces);
            }
        }
    }
}