using UnityEngine;
using System.Collections.Generic;

namespace ProjectTime.HexGrid
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        public Mesh hexMesh;
        List<Vector3> vertices;
        List<int> triangles;

        void Awake()
        {
            GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
            hexMesh.name = "Hex Mesh";
            vertices = new List<Vector3>();
            triangles = new List<int>();
        }

        public void Start()
        {
            hexMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            Triangulate();
            hexMesh.vertices = vertices.ToArray();
            hexMesh.triangles = triangles.ToArray();
            hexMesh.RecalculateNormals();
        }

        void Triangulate()
        {
            Vector3 center = Vector3.zero;
            for (int i = 0; i < 6; i++)
            {
                AddTriangle(
                    center,
                    center + Hex.corners[i],
                    center + Hex.corners[i + 1]
                );
            }
        }

        void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
        }
    }
}