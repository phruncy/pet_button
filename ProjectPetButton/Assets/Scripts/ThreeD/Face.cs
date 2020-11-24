
using System.Linq;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
    public class Face
    {
        public Vertex[] Vertices { get; }
        public int[] VertexIndices { get { return Vertices.Select(vertex => vertex.Index).ToArray(); } }

        public Face(Vertex[] vertices)
		{
            Assert.IsTrue(vertices.Length == 3, "A face always consists of three vertices.");
            Vertices = vertices;
        }
    }
}