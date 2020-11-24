
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
    public class Vertex 
    {
        public Vector3 Position { get; private set; }
        public int Index { get; private set; }


        public Vertex(Vector3 position, int index)
		{
            Assert.IsTrue(index >= 0, "The vertex index has to be larger than or equal to 0.");
            Position = position;
            Index = index;
        }

        public void SetPosition(Vector3 position)
		{
            Position = position;
        }
    }
}