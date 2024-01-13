using UnityEngine;

public class SineWaveDeform : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] currentVertices;

    public float amplitude = 0.5f;
    public float frequency = 1.0f;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        currentVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            vertex.y += amplitude * Mathf.Sin(Time.time * frequency + vertex.x);
            currentVertices[i] = vertex;
        }

        mesh.vertices = currentVertices;
        mesh.RecalculateNormals(); // Update normals for proper lighting
    }
}