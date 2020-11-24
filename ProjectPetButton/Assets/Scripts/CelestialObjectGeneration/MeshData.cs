using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gebaeckmeeting.PetButton
{
    public class MeshData
    {
        public Vector3[] Vertices { get; private set; } = null;
        public int[] FaceIndices { get; private set; } = null;

        public MeshData(Vector3[] vertices, int[] faceIndices)
		{
            Vertices = vertices;
            FaceIndices = faceIndices;
        }
    }
}