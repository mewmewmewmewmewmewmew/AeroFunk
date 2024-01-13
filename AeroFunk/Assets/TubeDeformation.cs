using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TubeDeformation : MonoBehaviour
{
    public float strength = 1f;
    public float frequency = 1f;

    private MeshFilter meshFilter;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] deformedVertices;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        // Store the original vertices for later reference
        originalVertices = mesh.vertices;
        deformedVertices = new Vector3[originalVertices.Length];
    }


    void Update()
    {
        // Get the texture from the material
        Texture2D noiseTexture = (Texture2D)GetComponent<Renderer>().material.mainTexture;

        // Calculate deformation based on Perlin noise and vertex normal
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            Vector3 normal = mesh.normals[i];

            // Calculate UV coordinates for the current vertex
            Vector2 uv = new Vector2(vertex.x * frequency, vertex.y * frequency);

            // Sample the Perlin noise texture at the UV coordinates
            float noiseValue = noiseTexture.GetPixelBilinear(uv.x, uv.y).grayscale;

            // Deform the vertex based on the normal and noise value
            deformedVertices[i] = vertex + normal * strength * noiseValue;

            Debug.Log(normal.ToString() + ", " + uv.ToString()+ ", " + noiseValue);
        }

        // Update the mesh with the deformed vertices
        mesh.vertices = deformedVertices;
        mesh.RecalculateNormals();
        //mesh.RecalculateBounds();
    }
}