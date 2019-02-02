using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
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
    }

    private void AddWall(string textureName, Vector3[] vertices, Vector2[] uvs)
    {
        int[] triangles =
        {
            0, 1, 2,
            2, 3, 0,
        };

        var o = new GameObject();
        Instantiate(o);

        var mesh = new Mesh();
        var meshFilter =
            (UnityEngine.MeshFilter)
            o.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = mesh;

        // mesh renderer
        var meshRenderer =
            (UnityEngine.MeshRenderer)
            o.AddComponent(typeof(MeshRenderer));

        var material = new Material(Shader.Find("Diffuse"));
        meshRenderer.materials = new Material[1];
        meshRenderer.materials[0] = material;

        //Load a Texture (Assets/Resources/Textures/texture01.png)
        var texture = Resources.Load<Texture2D>($"Textures/{textureName}");
        material.mainTexture = texture;

        meshRenderer.material = material;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
