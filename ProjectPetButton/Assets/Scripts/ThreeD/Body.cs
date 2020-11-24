using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
	public abstract class Body : MonoBehaviour
    {
        [SerializeField]
        private Transform _surfacesHook = null;
        [SerializeField]
        private Surface _surfacePrefab = null;

		public Surface[] Surfaces { get; private set; } = new Surface[0];

        protected void createSurfaceMeshes(int count)
        {
            //Destroy existing meshes
            foreach (Surface mesh in Surfaces)
                Destroy(mesh);

            Surfaces = new Surface[count];
            for (int i = 0; i < Surfaces.Length; i++)
            {
                Surface mesh = GameObject.Instantiate(_surfacePrefab);
                mesh.Base.SetParent(_surfacesHook, false);
                Surfaces[i] = mesh;
            }
        }

        public abstract void GenerateMesh();
    }
}