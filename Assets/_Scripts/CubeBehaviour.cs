using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;


[System.Serializable]
public class CubeBehaviour : CollisionObject
{
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public Vector3[] surfaces;
    public bool colliding;
    public bool isWall;

    private MeshFilter meshFilter;
    private Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        bounds = meshFilter.mesh.bounds;
        size = bounds.size;

        collisionType = CollisionType.CUBE;
        restitution = 0.5f;
        surfaces = new Vector3[6];
    }

    // Update is called once per frame
    void Update()
    {
            max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
            min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
            BuildSurfaceFlags();

        if (!isWall)
        {
            base.Update();

            if (colliding)
            {
                transform.position += transform.forward * 10 * Time.deltaTime;
                colliding = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(transform.position, Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), transform.localScale));
    }

    void BuildSurfaceFlags()
    {
        //top
        surfaces[0] = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
        //bottom
        surfaces[1] = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z);
        //left
        surfaces[2] = new Vector3(transform.position.x - transform.localScale.x, transform.position.y, transform.position.z);
        //right
        surfaces[3] = new Vector3(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z);
        //front
        surfaces[4] = new Vector3(transform.position.x, transform.position.y, transform.position.z - transform.localScale.x);
        //back
        surfaces[4] = new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.x);
    }
}
