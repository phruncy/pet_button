using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gebaeckmeeting.ThreeD
{
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

        public void UpdateVertexPositions()
		{
            Mesh.vertices = Vertices.Select(vertex => vertex.Position).ToArray();
        }

        public void UpdateMesh(Vertex[] vertices, Face[] faces)
		{
            Vertices = vertices;
            Faces = faces;
            Mesh.Clear();
            UpdateVertexPositions();
            updateTriangles();
            Mesh.RecalculateNormals();
        }

        private void updateTriangles()
		{
            int[] indices = Faces.Aggregate(new List<int>(), (list, next) =>
            {
                list.AddRange(next.VertexIndices);
                return list;
            }).ToArray();
            Mesh.triangles = indices;
        }
    }
}