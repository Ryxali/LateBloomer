using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class WorldBuilder : MonoBehaviour {
    [Tooltip("Target to build the world around")]
    public Transform target;

    public Bubble bubblePrefab;
    public Wind windPrefab;
    public Leaf leafPrefab;
    public Mushroom mushroomPrefab;
    
    private Dictionary<string, bool> spawned = new Dictionary<string, bool>();

    public RawImage img;
    public Texture2D tex;
    [Range(0, 16)]
    public int cellSize = 4;
    [Range(0, 32)]
    public int scanWidth = 4;

    private int seed = 0;

    // Use this for initialization
    int c = 0;
    void Start () {
        /*tex = new Texture2D(1000, 1000);
        for (int x = 0; x + 4 < tex.width; x += 4)
        {
            for (int y = 0; y + 4 < tex.height; y += 4)
            {
                float f = Noise(x, y, c);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        tex.SetPixel(x + i, y + j, new Color(f, f, f));
                    }

                }


            }
        }
        tex.Apply();
        img.texture = tex;*/

    }
    
    // Called from GameManager
    void Reset()
    {
        Reseed();
        spawned.Clear();
        //foreach()
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
	// Update is called once per frame
	void Update () {
	    //TODO build along x and y
        //divide into cells (3x3 units), populate each cell with (bubble, leaf, wind, nothing) with some percent chance for each.

        if(Input.GetKeyDown(KeyCode.K))
        {
            c++;
            for (int x = 0; x + 4 < tex.width; x += 4)
            {
                for (int y = 0; y + 4 < tex.height; y += 4)
                {
                    float f = Noise(x, y, c);
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            tex.SetPixel(x + i, y + j, new Color(f, f, f));
                        }

                    }


                }
            }
            tex.Apply();
            img.texture = tex;
        }
        if(target != null)
            Scan();
	}

    public void Reseed()
    {
        seed = Random.Range(0, 1000000);
    }
    

    private void Scan()
    {
        for(int x = -scanWidth; x < scanWidth; x++)
        {
            for(int y = -scanWidth; y < scanWidth; y++)
            {
                int posX = (int)target.position.x / cellSize + x;
                int posY = (int)target.position.y / cellSize + y;
                if(!spawned.ContainsKey(posX + "x" + posY))
                {
                    Spawn(posX, posY);
                }
            }
        }
    }

    private void Spawn(int x, int y)
    {
        float val = Noise(x, y, 0);

        System.Type t = null;
        if(y > 1)
        {
            if (val > 0.9f) t = typeof(Wind);
            else if (val > 0.5f && val < 0.6f && y < 50) t = typeof(Bubble);
            else if (val > 0.3f && val < 0.4f) t = typeof(Leaf);
            if (t != null)
            {
                Transform o = null;
                if (t == typeof(Bubble))
                {
                    o = Instantiate<Bubble>(bubblePrefab).transform;

                }
                else if (t == typeof(Wind))
                {
                    o = Instantiate<Wind>(windPrefab).transform;
                }
                else if (t == typeof(Leaf))
                {
                    o = Instantiate<Leaf>(leafPrefab).transform;
                }
                else
                {
                    Debug.LogError("Unknown type!");
                }

                o.parent = transform;
                o.position = new Vector3(x * cellSize + Random.Range(1, cellSize), y * cellSize + Random.Range(1, cellSize));
            }
        }
        else
        {
            if (val > 0.6f) t = typeof(Mushroom);

            if(t!= null)
            {
                Transform o = null;
                if (t == typeof(Mushroom))
                {
                    o = Instantiate<Mushroom>(mushroomPrefab).transform;

                }

                o.parent = transform;
                o.position = new Vector3(x * cellSize, y * cellSize - cellSize + 1);
            }
        }
        spawned.Add(x + "x" + y, true);
    }



    #region noise


    /*
         // Function to linearly interpolate between a0 and a1
         // Weight w should be in the range [0.0, 1.0]
         function lerp(float a0, float a1, float w) {
             return (1.0 - w)*a0 + w*a1;
         }
 
         // Computes the dot product of the distance and gradient vectors.
         function dotGridGradient(int ix, int iy, float x, float y) {
 
             // Precomputed (or otherwise) gradient vectors at each grid node
             extern float Gradient[IYMAX][IXMAX][2];
 
             // Compute the distance vector
             float dx = x - (float)ix;
             float dy = y - (float)iy;
 
             // Compute the dot-product
             return (dx*Gradient[iy][ix][0] + dy*Gradient[iy][ix][1]);
         }
 
         // Compute Perlin noise at coordinates x, y
         function perlin(float x, float y) {
 
             // Determine grid cell coordinates
             int x0 = (x > 0.0 ? (int)x : (int)x - 1);
             int x1 = x0 + 1;
             int y0 = (y > 0.0 ? (int)y : (int)y - 1);
             int y1 = y0 + 1;
 
             // Determine interpolation weights
             // Could also use higher order polynomial/s-curve here
             float sx = x - (float)x0;
             float sy = y - (float)y0;
 
             // Interpolate between grid point gradients
             float n0, n1, ix0, ix1, value;
             n0 = dotGridGradient(x0, y0, x, y);
             n1 = dotGridGradient(x1, y0, x, y);
             ix0 = lerp(n0, n1, sx);
             n0 = dotGridGradient(x0, y1, x, y);
             n1 = dotGridGradient(x1, y1, x, y);
             ix1 = lerp(n0, n1, sx);
             value = lerp(ix0, ix1, sy);
 
             return value;
         }

    */

    float Noise(int x, int y, int z)
    {
        float vec = 1.0f;
        vec = Mathf.Sin(x + y + z);
        vec *= Mathf.Cos(vec * y * x + vec + z);
        vec *= Mathf.Sin(-vec * y + 4 * y);

        return Mathf.Abs(vec) * Mathf.Clamp01(y) * Mathf.Clamp01(1 - (y / 100));
    }
    #endregion
}
