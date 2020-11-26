using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace Gebaeckmeeting.ThreeD
{
    /// <summary>
    /// A Container for the data of a single mesh
    /// </summary>
    public class Surface : MonoBehaviour
    {
        [SerializeField]
        private MeshFilter _meshFilter;
        [SerializeField]
        private Transform _base;
        public Transform Base { get { return _base; } }

        public Face[] Faces { get; private set; } = new Face[0];
        public Vertex[] Vertices { get; private set; } = new Vertex[0];

        private Mesh Mesh { get { return _meshFilter.sharedMesh; } }

        protected virtual void Awake()
		{
            _meshFilter.sharedMesh = new Mesh();
        }

        /// <summary>
        /// Updates the vertices of the surface mesh.
        /// (Very quick)
        /// </summary>
        public void UpdateMeshVertices()
		{
            Vector3[] vectors = new Vector3[Vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
                vectors[i] = Vertices[i].Position;
            Mesh.vertices = vectors;
        }

        /// <summary>
        /// Updates the surface data with the data of the given vertices and triangles
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="faces"></param>
        public void UpdateMesh(Vertex[] vertices, Face[] faces)
		{
            Vertices = vertices;
            Faces = faces;
            Mesh.Clear();
            UpdateMeshVertices();
            updateTriangles();
            Mesh.RecalculateNormals();
        }

        /// <summary>
        /// Updates the triangles of the surface mesh.
        /// (not so quick)
        /// </summary>
        private void updateTriangles()
		{
            int[] indices = new int[Faces.Length * 3];
            for(int i = 0; i<Faces.Length; i++)
			{
                Face face = Faces[i];
                indices[i * 3] = face.VertexIndices.x;
                indices[i * 3 + 1] = face.VertexIndices.y;
                indices[i * 3 + 2] = face.VertexIndices.z;
            }
            Mesh.triangles = indices;
        }
	}
}