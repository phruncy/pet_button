using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gebaeckmeeting.PetButton
{
    public class CelestialObjectSurface : MonoBehaviour
    {
        [SerializeField]
        private Transform _meshHook = null;
        [SerializeField]
        private SurfaceMesh _surfaceMeshPrefab = null;
        private SurfaceMesh[] _meshes = new SurfaceMesh[0];

        private void createSurfaceMeshes(int count)
        {
            //Destroy existing meshes
            foreach (SurfaceMesh mesh in _meshes)
                Destroy(mesh);

            _meshes = new SurfaceMesh[count];
            for (int i = 0; i < _meshes.Length; i++)
            {
                SurfaceMesh mesh = GameObject.Instantiate(_surfaceMeshPrefab);
                mesh.Base.SetParent(_meshHook, false);
                _meshes[i] = mesh;
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="data"></param>
        public void GenerateMesh(CelestialObjectSurfaceData data)
		{
            DateTime startTime = DateTime.Now;
            Assert.IsNotNull(data);
            createIcosohedronBase(data);
            shapeSurface(data, 1);
            Debug.Log($"Creating the planet mesh took {(DateTime.Now - startTime).TotalMilliseconds}ms.");
        }

        /// <summary>
        /// Source: https://medium.com/@peter_winslow/creating-procedural-planets-in-unity-part-1-df83ecb12e91
        /// </summary>
        /// <param name="data"></param>
        private void createIcosohedronBase(CelestialObjectSurfaceData data)
		{
            createSurfaceMeshes(20);
            Vector3[] vertices = new Vector3[12];
            float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

            vertices[0] = new Vector3(-1, t, 0).normalized * data.Radius;
            vertices[1] = new Vector3(1, t, 0).normalized * data.Radius;
            vertices[2] = new Vector3(-1, -t, 0).normalized * data.Radius;
            vertices[3] = new Vector3(1, -t, 0).normalized * data.Radius;
            vertices[4] = new Vector3(0, -1, t).normalized * data.Radius;
            vertices[5] = new Vector3(0, 1, t).normalized * data.Radius;
            vertices[6] = new Vector3(0, -1, -t).normalized * data.Radius;
            vertices[7] = new Vector3(0, 1, -t).normalized * data.Radius;
            vertices[8] = new Vector3(t, 0, -1).normalized * data.Radius;
            vertices[9] = new Vector3(t, 0, 1).normalized * data.Radius;
            vertices[10] = new Vector3(-t, 0, -1).normalized * data.Radius;
            vertices[11] = new Vector3(-t, 0, 1).normalized * data.Radius;

            updateMesh(_meshes[0], new Vector3[] { vertices[0], vertices[11], vertices[5] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[1], new Vector3[] { vertices[0], vertices[5], vertices[1] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[2], new Vector3[] { vertices[0], vertices[1], vertices[7] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[3], new Vector3[] { vertices[0], vertices[7], vertices[10] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[4], new Vector3[] { vertices[0], vertices[10], vertices[11] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[5], new Vector3[] { vertices[1], vertices[5], vertices[9] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[6], new Vector3[] { vertices[5], vertices[11], vertices[4] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[7], new Vector3[] { vertices[11], vertices[10], vertices[2] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[8], new Vector3[] { vertices[10], vertices[7], vertices[6] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[9], new Vector3[] { vertices[7], vertices[1], vertices[8] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[10], new Vector3[] { vertices[3], vertices[9], vertices[4] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[11], new Vector3[] { vertices[3], vertices[4], vertices[2] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[12], new Vector3[] { vertices[3], vertices[2], vertices[6] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[13], new Vector3[] { vertices[3], vertices[6], vertices[8] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[14], new Vector3[] { vertices[3], vertices[8], vertices[9] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[15], new Vector3[] { vertices[4], vertices[9], vertices[5] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[16], new Vector3[] { vertices[2], vertices[4], vertices[11] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[17], new Vector3[] { vertices[6], vertices[2], vertices[10] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[18], new Vector3[] { vertices[8], vertices[6], vertices[7] }, new int[] { 0, 1, 2 });
            updateMesh(_meshes[19], new Vector3[] { vertices[9], vertices[8], vertices[1] }, new int[] { 0, 1, 2 });
        }

        private void createTerahedronBase(CelestialObjectSurfaceData data)
		{
            createSurfaceMeshes(4);
            Vector3[] vertices = new Vector3[4];
            vertices[0] = Vector3.up * data.Radius;
            vertices[1] = Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * data.Radius;
            vertices[2] = Quaternion.AngleAxis(120, Vector3.up) * Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * data.Radius;
            vertices[3] = Quaternion.AngleAxis(240, Vector3.up) * Quaternion.AngleAxis(120, Vector3.left) * Vector3.up * data.Radius;
            Vector3[] meshFrontVertices = { vertices[2], vertices[3], vertices[0] };
            Vector3[] meshBackLeftVertices = { vertices[1], vertices[2], vertices[0] };
            Vector3[] meshBackRightVertices = { vertices[3], vertices[1], vertices[0] };
            Vector3[] meshBottomVertices = { vertices[2], vertices[1], vertices[3] };
            updateMesh(_meshes[0], meshFrontVertices, new int[] { 0, 1, 2 });
            updateMesh(_meshes[1], meshBackLeftVertices, new int[] { 0, 1, 2 });
            updateMesh(_meshes[2], meshBackRightVertices, new int[] { 0, 1, 2 });
            updateMesh(_meshes[3], meshBottomVertices, new int[] { 0, 1, 2 });
        }

        private void updateMesh(SurfaceMesh mesh, Vector3[] vertices, int[] triangles)
		{
            mesh.Mesh.Clear();
            mesh.Mesh.vertices = vertices;
            mesh.Mesh.triangles = triangles;
            mesh.Mesh.RecalculateNormals();
        }

        private void shapeSurface(CelestialObjectSurfaceData data, int iteration)
        {
            if (iteration >= data.Resolution)
                return;
            foreach (SurfaceMesh mesh in _meshes)
                subdivideMesh(mesh, data);
            //subdivideMesh(_meshes[0], data);
            shapeSurface(data, iteration + 1);
        }

        private void subdivideMesh(SurfaceMesh mesh, CelestialObjectSurfaceData data)
		{
            Vector3[] vertices = mesh.Mesh.vertices;
            int[] triangles = mesh.Mesh.triangles;
            List<Vector3> verticesNew = new List<Vector3>(vertices);
            List<int> trianglesNew = new List<int>();
            Dictionary<string, int> iDToIndex = new Dictionary<string, int>();

            for (int i = 0; i < triangles.Length/3; i++)
			{
                int index = i * 3;

                int i0 = triangles[index];
                int i1 = triangles[index + 1];
                int i2 = triangles[index + 2];

                int i01 = getInbetweenIndex(i0, i1, ref iDToIndex, ref verticesNew, vertices, data);
                int i12 = getInbetweenIndex(i1, i2, ref iDToIndex, ref verticesNew, vertices, data);
                int i20 = getInbetweenIndex(i2, i0, ref iDToIndex, ref verticesNew, vertices, data);

                trianglesNew.AddRange(new int[] { i0, i01, i20 });
                //Debug.Log($"{i0} | {i01} | {i20}");
                trianglesNew.AddRange(new int[] { i01, i1, i12 });
                //Debug.Log($"{i01} | {i1} | {i12}");
                trianglesNew.AddRange(new int[] { i20, i12, i2});
                //Debug.Log($"{i20} | {i12} | {i2}");
                trianglesNew.AddRange(new int[] { i20, i01, i12 });
                //Debug.Log($"{i20} | {i01} | {i12}");
            }

            updateMesh(mesh, verticesNew.ToArray(), trianglesNew.ToArray());
        }

        private int getInbetweenIndex(int i0, int i1, ref Dictionary<string, int> iDToIndex, ref List<Vector3> verticesNew, Vector3[] vertices, CelestialObjectSurfaceData data)
        {
            int result = -1;
            string i01ID = $"{i0}|{i1}";
            string i10ID = $"{i1}|{i0}";

            if(!iDToIndex.TryGetValue(i01ID, out result))
			{
                Vector3 vertex0 = vertices[i0];
                Vector3 vertex1 = vertices[i1];
                Vector3 vertex01 = (vertex0 + (vertex1 - vertex0) / 2).normalized * data.Radius;
                //Debug.Log($"{vertex0} | {vertex1} | {vertex01}");
                result = verticesNew.Count;
                verticesNew.Add(vertex01);
                iDToIndex[i01ID] = result;
                iDToIndex[i10ID] = result;
            }
            
            return result;
        }
    }
}
