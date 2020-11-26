
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
    /// <summary>
    /// A Triangle Polygon
    /// </summary>
    public struct Face
    {
        public Vector3Int VertexIndices { get ; }

        public Face(Vector3Int vertexIndices)
		{
            VertexIndices = vertexIndices;
        }

        public override string ToString()
        {
            return $"Face({VertexIndices.x} | {VertexIndices.y} | {VertexIndices.z})";
        }

        public int[] IndicesToArray()
		{
            return new int[] { VertexIndices.x, VertexIndices.y, VertexIndices.z };
        }
    }
}