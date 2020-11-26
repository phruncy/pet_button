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
            vertices[0] = new Vertex(Vector3.up * Radius);
            vertices[1] = new Vertex(Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * Radius);
            vertices[2] = new Vertex(Quaternion.AngleAxis(120, Vector3.up) * Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * Radius);
            vertices[3] = new Vertex(Quaternion.AngleAxis(240, Vector3.up) * Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * Radius);
            Face face = new Face(new Vector3Int(0, 1, 2));
            Surfaces[0].UpdateMesh(new Vertex[] { vertices[2], vertices[3], vertices[0] }, new Face[] { face });
            Surfaces[0].UpdateMesh(new Vertex[] { vertices[1], vertices[2], vertices[0] }, new Face[] { face });
            Surfaces[0].UpdateMesh(new Vertex[] { vertices[3], vertices[1], vertices[0] }, new Face[] { face });
            Surfaces[0].UpdateMesh(new Vertex[] { vertices[2], vertices[1], vertices[3] }, new Face[] { face });
        }

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