using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gebaeckmeeting.ThreeD
{
	public class RecursiveSphereSurfaceShaper : SphereSurfaceShaper
	{
        private float _radius = 1;


        public override void Shape(Sphere body, int resolution, float radius)
        {
            _radius = radius;
            shapeSurface(body, 1, resolution);
        }

        private void shapeSurface(Sphere body, int iteration, int maxIteration)
        {
            if (iteration >= maxIteration)
                return;
            foreach (Surface mesh in body.Surfaces)
                subdivideMesh(mesh);
            //subdivideMesh(_meshes[0], data);
            shapeSurface(body, iteration + 1, maxIteration);
        }

        private void subdivideMesh(Surface surface)
        {
            Vertex[] vertices = surface.Vertices;
            Face[] faces = surface.Faces;
            List<Vertex> verticesNew = new List<Vertex>(vertices);
            List<Face> facesNew = new List<Face>();
            Dictionary<string, Vertex> iDToIndex = new Dictionary<string, Vertex>();

            for (int i = 0; i < faces.Length; i++)
            {
                int index = i * 3;

                Vertex[] v = faces[i].Vertices;
                Vertex v0 = v[0];
                Vertex v1 = v[1];
                Vertex v2 = v[2];

                Vertex v01 = getInbetweenVertex(v0, v1, ref iDToIndex, ref verticesNew);
                Vertex v12 = getInbetweenVertex(v1, v2, ref iDToIndex, ref verticesNew);
                Vertex v20 = getInbetweenVertex(v2, v0, ref iDToIndex, ref verticesNew);

                facesNew.Add(new Face(new Vertex[] { v0, v01, v20 }));
                facesNew.Add(new Face(new Vertex[] { v01, v1, v12 }));
                facesNew.Add(new Face(new Vertex[] { v20, v12, v2 }));
                facesNew.Add(new Face(new Vertex[] { v20, v01, v12 }));
            }

            surface.UpdateMesh(verticesNew.ToArray(), facesNew.ToArray());
        }


        private Vertex getInbetweenVertex(Vertex v0, Vertex v1, ref Dictionary<string, Vertex> iDToIndex, ref List<Vertex> verticesNew)
        {
            Vertex result;
            string iD1 = $"{v0.Index}|{v1.Index}";
            string iD2 = $"{v1.Index}|{v0.Index}";

            if (!iDToIndex.TryGetValue(iD1, out result))
            {
                Vector3 vertex01Position = (v0.Position + (v1.Position - v0.Position) / 2).normalized * _radius;
                result = new Vertex(vertex01Position, verticesNew.Count);
                //Debug.Log($"{vertex0} | {vertex1} | {vertex01}");
                verticesNew.Add(result);
                iDToIndex[iD1] = result;
                iDToIndex[iD2] = result;
            }

            return result;
        }
    }
}