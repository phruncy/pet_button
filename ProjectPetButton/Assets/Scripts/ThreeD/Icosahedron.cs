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

            vertices[0] = new Vertex(new Vector3(-1, t, 0).normalized * Radius);
            vertices[1] = new Vertex(new Vector3(1, t, 0).normalized * Radius);
            vertices[2] = new Vertex(new Vector3(-1, -t, 0).normalized * Radius);
            vertices[3] = new Vertex(new Vector3(1, -t, 0).normalized * Radius);
            vertices[4] = new Vertex(new Vector3(0, -1, t).normalized * Radius);
            vertices[5] = new Vertex(new Vector3(0, 1, t).normalized * Radius);
            vertices[6] = new Vertex(new Vector3(0, -1, -t).normalized * Radius);
            vertices[7] = new Vertex(new Vector3(0, 1, -t).normalized * Radius);
            vertices[8] = new Vertex(new Vector3(t, 0, -1).normalized * Radius);
            vertices[9] = new Vertex(new Vector3(t, 0, 1).normalized * Radius);
            vertices[10] = new Vertex(new Vector3(-t, 0, -1).normalized * Radius);
            vertices[11] = new Vertex(new Vector3(-t, 0, 1).normalized * Radius);

            Face face = new Face(new Vector3Int( 0, 1, 2 ));
            Surfaces[0].UpdateMesh(new Vertex[] { vertices[0], vertices[11], vertices[5] }, new Face[] { face });
            Surfaces[1].UpdateMesh(new Vertex[] { vertices[0], vertices[5], vertices[1] }, new Face[] { face });
            Surfaces[2].UpdateMesh(new Vertex[] { vertices[0], vertices[1], vertices[7] }, new Face[] { face });
            Surfaces[3].UpdateMesh(new Vertex[] { vertices[0], vertices[7], vertices[10] }, new Face[] { face });
            Surfaces[4].UpdateMesh(new Vertex[] { vertices[0], vertices[10], vertices[11] }, new Face[] { face });
            Surfaces[5].UpdateMesh(new Vertex[] { vertices[1], vertices[5], vertices[9] }, new Face[] { face });
            Surfaces[6].UpdateMesh(new Vertex[] { vertices[5], vertices[11], vertices[4] }, new Face[] { face });
            Surfaces[7].UpdateMesh(new Vertex[] { vertices[11], vertices[10], vertices[2] }, new Face[] { face });
            Surfaces[8].UpdateMesh(new Vertex[] { vertices[10], vertices[7], vertices[6] }, new Face[] { face });
            Surfaces[9].UpdateMesh(new Vertex[] { vertices[7], vertices[1], vertices[8] }, new Face[] { face });
            Surfaces[10].UpdateMesh(new Vertex[] { vertices[3], vertices[9], vertices[4] }, new Face[] { face });
            Surfaces[11].UpdateMesh(new Vertex[] { vertices[3], vertices[4], vertices[2] }, new Face[] { face });
            Surfaces[12].UpdateMesh(new Vertex[] { vertices[3], vertices[2], vertices[6] }, new Face[] { face });
            Surfaces[13].UpdateMesh(new Vertex[] { vertices[3], vertices[6], vertices[8] }, new Face[] { face });
            Surfaces[14].UpdateMesh(new Vertex[] { vertices[3], vertices[8], vertices[9] }, new Face[] { face });
            Surfaces[15].UpdateMesh(new Vertex[] { vertices[4], vertices[9], vertices[5] }, new Face[] { face });
            Surfaces[16].UpdateMesh(new Vertex[] { vertices[2], vertices[4], vertices[11] }, new Face[] { face });
            Surfaces[17].UpdateMesh(new Vertex[] { vertices[6], vertices[2], vertices[10] }, new Face[] { face });
            Surfaces[18].UpdateMesh(new Vertex[] { vertices[8], vertices[6], vertices[7] }, new Face[] { face });
            Surfaces[19].UpdateMesh(new Vertex[] { vertices[9], vertices[8], vertices[1] }, new Face[] { face });
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
                surface.UpdateMeshVertices();
            }
        }
	}
}