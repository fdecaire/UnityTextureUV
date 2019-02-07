using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{
    private Texture[] textures;
    private int frameCounter = 0;
    private Material _doorMaterial;
    private bool CloseOpen = false; // false == open

    // animated texture
    // http://www.41post.com/4726/programming/unity-animated-texture-from-image-sequence-part-1
    //
    void Start()
    {
        float size = 1f;
        Vector3[] vertices0 =
        {
            // right
            new Vector3(size, 0, 0),
            new Vector3(size, size, 0),
            new Vector3(size, size, size),
            new Vector3(size, 0, size),
        };

        Vector2[] uvs0 =
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
        };
        AddWall("door13_0", vertices0, uvs0);

        var textureList = Resources.LoadAll("Textures/door13", typeof(Texture));
        textures = new Texture[textureList.Length];

        for (var i = 0; i < textureList.Length; i++)
        {
            textures[i] = (Texture)textureList[i];
        }        
    }

    void Awake()
    {
        //_doorMaterial.mainTexture = textures[frameCounter];
    }

    void Update()
    {
        StartCoroutine("PlayLoop", 0.10f);
        _doorMaterial.mainTexture = textures[frameCounter];
    }

    //The following methods return a IEnumerator so they can be yielded:  
    //A method to play the animation in a loop  
    IEnumerator PlayLoop(float delay)
    {
        //Wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);

        if (CloseOpen && frameCounter == 0)
        {
            CloseOpen = !CloseOpen;
        }

        if (!CloseOpen && frameCounter == textures.Length - 1)
        {
            CloseOpen = !CloseOpen;
        }

        if (CloseOpen)
        {
            frameCounter--;
        }
        else
        {
            frameCounter++;
        }

        //Stop this co-routine  
        StopCoroutine("PlayLoop");
    }

    private void AddWall(string textureName, Vector3[] vertices, Vector2[] uvs)
    {
        int[] triangles =
        {
            0, 1, 2,
            2, 3, 0,
        };

        var door = new GameObject();
        Instantiate(door);

        var mesh = new Mesh();
        var meshFilter =
            (UnityEngine.MeshFilter)
            door.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = mesh;

        // mesh renderer
        var meshRenderer =
            (UnityEngine.MeshRenderer)
            door.AddComponent(typeof(MeshRenderer));

        _doorMaterial = new Material(Shader.Find("Transparent/Diffuse"));
        meshRenderer.materials = new Material[1];
        meshRenderer.materials[0] = _doorMaterial;

        var texture = Resources.Load<Texture2D>($"Textures/{textureName}");

        _doorMaterial.mainTexture = texture;

        meshRenderer.material = _doorMaterial;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
