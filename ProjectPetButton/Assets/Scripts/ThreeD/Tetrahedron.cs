using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
	public class Tetrahedron : Body
	{
        private float _radius = 1;
        public float Radius
        {
            get { return _radius; }
            set
            {
                Assert.IsTrue(value > 0.0f, $"The radius of a {nameof(Icosahedron)} has to be larger than 0");
                _radius = value;
            }
        }

        public override void GenerateMesh()
		{
            createSurfaceMeshes(4);
            Vertex[] vertices = new Vertex[4];
            vertices[0] = new Vertex(Vector3.up * Radius, 0);
            vertices[1] = new Vertex(Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * Radius, 1);
            vertices[2] = new Vertex(Quaternion.AngleAxis(120, Vector3.up) * Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * Radius, 2);
            vertices[3] = new Vertex(Quaternion.AngleAxis(240, Vector3.up) * Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * Radius, 3);
            updateSurface(Surfaces[0], vertices, new int[] { 2, 3, 0 });
            updateSurface(Surfaces[1], vertices, new int[] { 1, 2, 0 });
            updateSurface(Surfaces[2], vertices, new int[] { 3, 1, 0 });
            updateSurface(Surfaces[3], vertices, new int[] { 2, 1, 3 });
        }

        protected void updateSurface(Surface surface, Vertex[] vertices, int[] indizes)
        {
            Vertex[] surfaceVertices = new Vertex[]
            {
                new Vertex(vertices[indizes[0]].Position, 0),
                new Vertex(vertices[indizes[1]].Position, 1),
                new Vertex(vertices[indizes[2]].Position, 2) 
            };
            surface.UpdateMesh(surfaceVertices, new Face[] { new Face(surfaceVertices) });
        }
    }
}