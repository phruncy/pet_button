using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
    /// <summary>
    /// A polygonal sphere composed of multiple surface meshes
    /// </summary>
    public class Sphere : Body
    {
        private SphereData Data { get; set; } = null;
        public int Resolution
		{
			get { return Data.Resolution; }
			set
			{
                Data.Resolution = value;
                GenerateMesh();
            }
		}

        public float Radius
        {
            get { return Data.Radius; }
            set
            {
                Data.Radius = value;
                UpdateVertexPositions();
            }
        }


        public void SetData(SphereData value)
		{
            Data = value;
		}

		public override void GenerateMesh()
        {
            Assert.IsNotNull(Data, "Please provide Data before trying to generate the mesh");
            Body baseBody = Data.BaseBodyCreator.Create(Data.Radius);
            copySurfaces(baseBody);
            Data.Shaper.Shape(this, Data.Resolution, Data.Radius);
            GameObject.Destroy(baseBody.gameObject);
        }

		public override void UpdateVertexPositions()
		{
			foreach(Surface surface in Surfaces)
			{
                foreach(Vertex vertex in surface.Vertices)
				{
                    vertex.SetPosition(vertex.Position.normalized * Radius);
                }
                surface.UpdateVertexPositions();
            }

            Vertex v = Surfaces[0].Vertices[0];
        }

		private void copySurfaces(Body body)
        {
            foreach (Surface surface in Surfaces)
                Destroy(surface.Base.gameObject);

            createSurfaceMeshes(body.Surfaces.Length);
            for (int i = 0; i < body.Surfaces.Length; i++)
            {
                Surface other = body.Surfaces[i];
                Surfaces[i].UpdateMesh(other.Vertices, other.Faces);
            }
        }
    }
}