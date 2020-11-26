
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
    /// <summary>
    /// A wrapper around vertex data
    /// </summary>
    public struct Vertex 
    {
        public Vector3 Position { get; private set; }


        public Vertex(Vector3 position)
		{
            Position = position;
        }

        public void SetPosition(Vector3 position)
		{
            Position = position;
        }

		public override string ToString()
		{
            return $"Vertex({Position.ToString()})";
		}
	}
}