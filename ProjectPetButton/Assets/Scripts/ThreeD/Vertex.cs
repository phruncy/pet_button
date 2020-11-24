
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
    public struct Vertex 
    {
        public Vector3 Position { get; }
        public int Index { get; }

        public Vertex(Vector3 position, int index)
		{
            Assert.IsTrue(index >= 0, "The vertex index has to be larger than or equal to 0.");
            Position = position;
            Index = index;
        }
    }
}