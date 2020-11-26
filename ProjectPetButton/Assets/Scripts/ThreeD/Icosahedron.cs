using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
	public class Icosahedron : Body
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

        /// <summary>
        /// Generates an individual mesh for each of the 20 faces
        /// Source: https://medium.com/@peter_winslow/creating-procedural-planets-in-unity-part-1-df83ecb12e91
        /// </summary>
		public override void GenerateMesh()
		{
            createSurfaceMeshes(20);
            Vertex[] vertices = new Vertex[12];
            float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

            vertices[0] = new Vertex(new Vector3(-1, t, 0).normalized * Radius, 0);
            vertices[1] = new Vertex(new Vector3(1, t, 0).normalized * Radius, 1);
            vertices[2] = new Vertex(new Vector3(-1, -t, 0).normalized * Radius, 2);
            vertices[3] = new Vertex(new Vector3(1, -t, 0).normalized * Radius, 3);
            vertices[4] = new Vertex(new Vector3(0, -1, t).normalized * Radius, 4);
            vertices[5] = new Vertex(new Vector3(0, 1, t).normalized * Radius, 5);
            vertices[6] = new Vertex(new Vector3(0, -1, -t).normalized * Radius, 6);
            vertices[7] = new Vertex(new Vector3(0, 1, -t).normalized * Radius, 7);
            vertices[8] = new Vertex(new Vector3(t, 0, -1).normalized * Radius, 8);
            vertices[9] = new Vertex(new Vector3(t, 0, 1).normalized * Radius, 9);
            vertices[10] = new Vertex(new Vector3(-t, 0, -1).normalized * Radius, 10);
            vertices[11] = new Vertex(new Vector3(-t, 0, 1).normalized * Radius, 11);

            updateSurface(Surfaces[0], vertices, new int[] { 0, 11, 5 });
            updateSurface(Surfaces[1], vertices, new int[] { 0, 5, 1 });
            updateSurface(Surfaces[2], vertices, new int[] { 0, 1, 7 });
            updateSurface(Surfaces[3], vertices, new int[] { 0, 7, 10 });
            updateSurface(Surfaces[4], vertices, new int[] { 0, 10, 11 });
            updateSurface(Surfaces[5], vertices, new int[] { 1, 5, 9 });
            updateSurface(Surfaces[6], vertices, new int[] { 5, 11, 4 });
            updateSurface(Surfaces[7], vertices, new int[] { 11, 10, 2 });
            updateSurface(Surfaces[8], vertices, new int[] { 10, 7, 6 });
            updateSurface(Surfaces[9], vertices, new int[] { 7, 1, 8 });
            updateSurface(Surfaces[10], vertices, new int[] { 3, 9, 4 });
            updateSurface(Surfaces[11], vertices, new int[] { 3, 4, 2 });
            updateSurface(Surfaces[12], vertices, new int[] { 3, 2, 6 });
            updateSurface(Surfaces[13], vertices, new int[] { 3, 6, 8 });
            updateSurface(Surfaces[14], vertices, new int[] { 3, 8, 9 });
            updateSurface(Surfaces[15], vertices, new int[] { 4, 9, 5 });
            updateSurface(Surfaces[16], vertices, new int[] { 2, 4, 11 });
            updateSurface(Surfaces[17], vertices, new int[] { 6, 2, 10 });
            updateSurface(Surfaces[18], vertices, new int[] { 8, 6, 7 });
            updateSurface(Surfaces[19], vertices, new int[] { 9, 8, 1 });
        }

		/// <summary>
        /// Updates the vertices of all surfaces
        /// </summary>
        public override void UpdateVertexPositions()
		{
            foreach (Surface surface in Surfaces)
            {
                foreach (Vertex vertex in surface.Vertices)
                {
                    vertex.SetPosition(vertex.Position.normalized * Radius);
                }
                surface.UpdateVertexPositions();
            }
        }

        /// <summary>
        /// Creates a Triangle from the vertices with the given indeces and updates the surface's mesh with it
        /// </summary>
        /// <param name="surface">the surface to be updated</param>
        /// <param name="vertices">array of all vertices</param>
        /// <param name="indizes">indices of the triangle's vertices</param>
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