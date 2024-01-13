using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PerlinNoiseGen : MonoBehaviour
{
    [SerializeField] public GameObject pillar;
    [SerializeField] int width;
    [SerializeField] int length;
    [SerializeField] public List<GameObject> pillars = new List<GameObject>();

    Dictionary<GameObject, Vector2Int> pillarDictionary = new Dictionary<GameObject, Vector2Int>();


    public float noiseScale; 
    public float spawnDistance;
    public float rateOfAnimation;
    public float heightMultiplier;

    // Try varying the xOrg, yOrg and scale values in the inspector
    // while in Play mode to see the effect they have on the noise.
    // Width and height of the texture in pixels.
    public int pixWidth;
    public int pixHeight;

    // The origin of the sampled area in the plane.
    public float xOrg;
    public float yOrg;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float pillarScale = 1.0F;

    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;
    bool pillarsCreated = false;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Set up the texture and a Color array to hold pixels during processing.
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;
        
        CalcNoise();
    }

    void CalcNoise()
    {
        // For each pixel in the texture...
        float y = 0.0F;

        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * noiseScale;
                float yCoord = yOrg + y / noiseTex.height * noiseScale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
        /*if(pillars.Count!=0)
        {
            DestroyPillars();
        }*/
        if(!pillarsCreated)
        {
            pillarsCreated=true;
            CreatePillars();
        }
        else
        {
            ModifyPillars();
        }
    }
    void CreatePillars()
    {
        float width = noiseTex.width;
        float height = noiseTex.height;

        for (int x = 0; x < width/10; x++)
        {
            for (int y = 0; y < height/10; y++)
            {
                Debug.Log(noiseTex.GetPixel(x, y).grayscale);
                GameObject go = Instantiate(pillar);
                pillarDictionary.Add(go, (new Vector2Int(x,y)));
                go.transform.SetPositionAndRotation(new Vector3(x*spawnDistance, 0, y*spawnDistance), Quaternion.Euler(new Vector3(0, 45, 0)));
                go.transform.localScale =
                    (new Vector3(go.transform.localScale.x * pillarScale, (go.transform.localScale.y * noiseTex.GetPixel(x, y).grayscale * pillarScale*heightMultiplier) , go.transform.localScale.z * pillarScale));

                /*if(texture.GetPixel(x,y).grayscale>0.99f)
                {
                    Light light = Instantiate(Light);
                    light.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
                    light.transform.localScale =
                        (new Vector3(go.transform.localScale.x * 5, go.transform.localScale.y * 5, 30+(go.transform.localScale.z * texture.GetPixel(x, y).grayscale * 100)));
                }*/
            }
        }
    }

    void ModifyPillars()
    {
        float width = noiseTex.width;
        float height = noiseTex.height;

        foreach (var pillarID in pillarDictionary)
        {

            pillarID.Key.transform.localScale =
                    (new Vector3(pillar.transform.localScale.x * pillarScale, (pillar.transform.localScale.z * noiseTex.GetPixel(pillarID.Value.x, pillarID.Value.y).grayscale)*pillarScale*heightMultiplier, pillar.transform.localScale.z * pillarScale));
        }
        /*for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                Debug.Log(noiseTex.GetPixel(x, y).grayscale);
                foreach (var pillar in pillars)
                {
                    pillar.transform.localScale =
                            (new Vector3(pillar.transform.localScale.x * 5, pillar.transform.localScale.y * 5, (pillar.transform.localScale.z * noiseTex.GetPixel(x, y).grayscale)));
                }*/

        /*if(texture.GetPixel(x,y).grayscale>0.99f)
        {
            Light light = Instantiate(Light);
            light.transform.SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
            light.transform.localScale =
                (new Vector3(go.transform.localScale.x * 5, go.transform.localScale.y * 5, 30+(go.transform.localScale.z * texture.GetPixel(x, y).grayscale * 100)));
        }
    }*/

    }
    private void DestroyPillars()
    {
        foreach(var pillar in pillars)
        {
            Destroy(pillar.gameObject);
        }
    }

    void Update()
    {
        yOrg += rateOfAnimation*Time.deltaTime;
        CalcNoise();
    }

    //for each vertex in the list
    //create a pillar
    //rest of script functions normally
   
}


