using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.ThreeD
{
	/// <summary>
	/// Creates new Triangles on all Surfaces of a sphere by iterative subdvision
	/// </summary>
	public class IterativeSphereSurfaceShaper : SphereSurfaceShaper
	{
		public override void Shape(Sphere body, int resolution)
		{
			Assert.IsTrue(resolution >= 0);
			DateTime start = DateTime.Now;

			if (resolution == 0)
				return;

			// Creates and schedules shaping jobs
			JobHandle[] jobHandles = new JobHandle[body.Surfaces.Length];
			NativeArray<Vertex>[] vertices = new NativeArray<Vertex>[body.Surfaces.Length];
			NativeArray<Face>[] faces = new NativeArray<Face>[body.Surfaces.Length];
			for (int i = 0; i < body.Surfaces.Length; i++)
			{
				Surface surface = body.Surfaces[i];
				//Debug.Log("Start");

				Assert.AreEqual(surface.Faces.Length, 1, $"To use the {nameof(IterativeSphereSurfaceShaper)} " +
					$"the surfaces are not allowed to have more or less than one face.");

				vertices[i] = new NativeArray<Vertex>(calculateVerticeCount(resolution + 2), Allocator.TempJob);
				faces[i] = new NativeArray<Face>(calculateFacesCount(resolution + 2), Allocator.TempJob);
				Face face = surface.Faces[0];
				Vertex v0 = surface.Vertices[0];
				Vertex v1 = surface.Vertices[1];
				Vertex v2 = surface.Vertices[2];
				Vector3 vector01 = v1.Position - v0.Position;
				Vector3 vector20 = v2.Position - v0.Position;
				int actualResolution = resolution + 1;
				Vector3 deltaW = vector01 / actualResolution;
				Vector3 deltaH = vector20 / actualResolution;

				ShapingJob job = new ShapingJob(actualResolution, v0, deltaH, deltaW, vertices[i], faces[i]);
				jobHandles[i] = job.Schedule();
			}

			// Waits for all shaping jobs to finish
			// Then updates the surfaces with the calculated vertices and faces.
			for (int i = 0; i < jobHandles.Length; i++)
			{
				JobHandle handle = jobHandles[i];
				handle.Complete();
				body.Surfaces[i].UpdateMesh(vertices[i].ToArray(), faces[i].ToArray());
				vertices[i].Dispose();
				faces[i].Dispose();
			}

			double duration = (DateTime.Now - start).TotalMilliseconds;
			Debug.Log($"The shaping of the sphere surfaces with resolution {resolution} took {duration} ms");
		}

		private int calculateVerticeCount(int verticesPerSide)
		{
			Assert.IsTrue(verticesPerSide > 1, "A side has at least 2 vertices");
			int result = 0;
			for(int i = verticesPerSide; i >= 0; i--)
			{
				result += i;
			}
			return result;
		}

		private int calculateFacesCount(int verticesPerSide)
		{
			Assert.IsTrue(verticesPerSide > 1, "A side has at least 2 vertices");
			int result = 1;
			for (int i = 1; i < verticesPerSide; i++)
			{
				// Every additional vertex per side adds two more faces than the vertex before.
				result += i*2-1;
			}
			return result;
		}

		/// <summary>
		/// Struct to parallelize the sphere shaping.
		/// </summary>
		public struct ShapingJob : IJob
		{
			private int Resolution { get; }
			private Vertex StartVertex { get; }
			private Vector3 DeltaH { get; }
			private Vector3 DeltaW { get; }
			public NativeArray<Vertex> Vertices;
			public NativeArray<Face> Faces;

			public ShapingJob(int resolution,
				Vertex startVertex,
				Vector3 deltaH,
				Vector3 deltaW,
				NativeArray<Vertex> vertices,
				NativeArray<Face> faces)
			{
				Resolution = resolution;
				StartVertex = startVertex;
				DeltaH = deltaH;
				DeltaW = deltaW;
				Vertices = vertices;
				Faces = faces;
			}

			public void Execute()
			{
				int faceIndex = 0;
				int vertexIndex = 0;

				// Interating over the triangle 'rows'
				for (int i = 0; i <= Resolution; i++)
				{
					Vector3 startPos = StartVertex.Position + DeltaH * i;
					// Interating over the triangle row vertices. 
					// The number of row vertices reduces with every row. (One vertex on top of the array)
					for (int j = 0; j <= Resolution - i; j++)
					{
						Vector3 vertexPosition = (startPos + DeltaW * j).normalized;
						Vertices[vertexIndex] = new Vertex(vertexPosition);
						
						// Create Face if this is not the first row
						if (i > 0)
						{
							int i1 = vertexIndex - (Resolution + 2 - i);
							int i2 = i1 + 1;
							Faces[faceIndex] = new Face(new Vector3Int(vertexIndex, i1, i2));
							faceIndex++;

							// Create a second upside down triangle if this is not the first vertex of the current row
							if (j > 0)
							{
								int i3 = vertexIndex - 1;
								Faces[faceIndex] = new Face(new Vector3Int(vertexIndex, i3, i1));
								faceIndex++;
							}
						}
						vertexIndex++;
					}
				}
			}
		}
	}
}