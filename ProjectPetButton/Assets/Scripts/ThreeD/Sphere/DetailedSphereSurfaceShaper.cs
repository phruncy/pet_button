using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
	public class DetailedSphereSurfaceShaper : SphereSurfaceShaper
	{
		public override void Shape(Sphere body, int resolution, float radius)
		{
			Assert.IsTrue(resolution >= 0);
			Assert.IsTrue(radius > 0);

			if (resolution == 0)
				return;

			foreach (Surface surface in body.Surfaces)
				shapeSurface(surface, resolution, radius);
		}

		private void shapeSurface(Surface surface, int resolution, float radius)
		{
			Assert.AreEqual(surface.Faces.Length, 1, $"To use the {nameof(DetailedSphereSurfaceShaper)} " +
					$"the surfaces are not allowed to have more or less than one face.");
			Face face = surface.Faces[0];
			Vertex v0 = face.Vertices[0];
			Vertex v1 = face.Vertices[1];
			Vertex v2 = face.Vertices[2];
			Vector3 vector01 = v1.Position - v0.Position;
			Vector3 vector20 = v2.Position - v0.Position;
			resolution = resolution + 1;
			Vector3 deltaW = vector01 / resolution;
			Vector3 deltaH = vector20 / resolution;

			List<Vertex> vertices = new List<Vertex>();
			List<Face> faces = new List<Face>();

			for (int i = 0; i <= resolution; i++)
			{
				Vector3 startPos = v0.Position + deltaH * i; 
				for (int j = 0; j <= resolution - i; j++)
				{
					Vector3 vertexPosition = (startPos + deltaW * j).normalized * radius;
					int index = vertices.Count;
					Vertex newVertex = new Vertex(vertexPosition, index);
					vertices.Add(newVertex);

					//Debug.Log($"i {i}");
					//Debug.Log($"j {j}");
					//Debug.Log($"vertices.Count {vertices.Count}");
					//Debug.Log($"index {index}");

					if (i > 0)
					{
						int i1 = index - (resolution + 2 - i);
						int i2 = i1 + 1;
						
						//Debug.Log($"i1 {i1}");
						//Debug.Log($"i2 {i2}");
						faces.Add(new Face(new Vertex[] { newVertex, vertices[i1], vertices[i2] }));

						if(j > 0)
						{
							int i3 = index - 1;
							//Debug.Log($"i3 {i3}");
							faces.Add(new Face(new Vertex[] { newVertex, vertices[i3], vertices[i1] }));
						}
					}
				}
				//Debug.LogWarning($"original | new {v1.Position} | {vertices[vertices.Count-1].Position}");
			}

			//Debug.LogError($"original | new {v2.Position} | {vertices[vertices.Count - 1].Position}");
			surface.UpdateMesh(vertices.ToArray(), faces.ToArray());
		}
	}
}