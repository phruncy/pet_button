using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gebaeckmeeting.ThreeD
{
	/// <summary>
    /// Generates new Triangles on all Sphere surfaces by recursive subdivision
    /// </summary>
    public class RecursiveSphereSurfaceShaper : SphereSurfaceShaper
	{

        public override void Shape(Sphere body, int resolution)
        {
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
            Dictionary<string, int> iDToIndex = new Dictionary<string, int>();

            for (int i = 0; i < faces.Length; i++)
            {
                int index = i * 3;

                Face face = faces[i];
                int i0 = face.VertexIndices[0];
                int i1 = face.VertexIndices[1];
                int i2 = face.VertexIndices[2];
                Vertex v0 = vertices[i0];
                Vertex v1 = vertices[i1];
                Vertex v2 = vertices[i2];

                int i01 = getInbetweenVertexIndex(v0, v1, ref iDToIndex, ref verticesNew);
                int i12 = getInbetweenVertexIndex(v1, v2, ref iDToIndex, ref verticesNew);
                int i20 = getInbetweenVertexIndex(v2, v0, ref iDToIndex, ref verticesNew);

                facesNew.Add(new Face(new Vector3Int( i0, i01, i20 )));
                facesNew.Add(new Face(new Vector3Int(i01, i1, i12 )));
                facesNew.Add(new Face(new Vector3Int(i20, i12, i2 )));
                facesNew.Add(new Face(new Vector3Int(i20, i01, i12 )));
            }

            surface.UpdateMesh(verticesNew.ToArray(), facesNew.ToArray());
        }


        private int getInbetweenVertexIndex(Vertex v0, Vertex v1, ref Dictionary<string, int> iDToIndex, ref List<Vertex> verticesNew)
        {
            int result;
            string iD1 = $"{v0.Position.ToString()}|{v1.Position.ToString()}";
            string iD2 = $"{v1.Position.ToString()}|{v0.Position.ToString()}";

            if (!iDToIndex.TryGetValue(iD1, out result))
            {
                Vector3 vertex01Position = (v0.Position + (v1.Position - v0.Position) / 2).normalized;
                Vertex v = new Vertex(vertex01Position);
                //Debug.Log($"{vertex0} | {vertex1} | {vertex01}");
                verticesNew.Add(v);
                iDToIndex[iD1] = result;
                iDToIndex[iD2] = result;
            }

            return result;
        }
    }
}