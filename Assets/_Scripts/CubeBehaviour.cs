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
    public Vector3[] normals;
    public Vector3 closestwall;
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
        normals = new Vector3[6];
    }

    // Update is called once per frame
    void Update()
    {
            max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
            min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
            BuildSurfaceFlags();
           BuildNormals();

        if (!isWall)
        {
            base.Update();

            if (colliding)
            {
                velocity = transform.forward * 10 * Time.deltaTime;
                
                //colliding = false;
            }
            foreach (CubeBehaviour actor in CollisionManager.Instance.cubes)
            {
                if (CollisionManager.Instance.CheckAABBs(this, actor))
                {
                    Vector3[] cube_walls = actor.surfaces;
                    float closestdistance = 1000.0f;
                    foreach (Vector3 wall in cube_walls)
                    {
                        float tempdistance = Mathf.Sqrt((transform.position.x - wall.x) * (transform.position.x - wall.x) +
                                                        (transform.position.y - wall.y) * (transform.position.y - wall.y) +
                                                        (transform.position.z - wall.z) * (transform.position.z - wall.z));
                        if(tempdistance < closestdistance)
                        {
                            closestdistance = tempdistance;
                            closestwall = wall;
                        }
                    }
                
                    
                    if(closestwall == actor.surfaces[0] || closestwall == actor.surfaces[1])
                    {
                        velocity.y *= 0;
                    }
                    else if (closestwall == actor.surfaces[2] || closestwall == actor.surfaces[3])
                    {
                        velocity.x *= 0;
                    }
                    else if (closestwall == actor.surfaces[4] || closestwall == actor.surfaces[5])
                    {
                        velocity.z *= 0;
                    }
                    
                    
                   
                }
                else
                    colliding = false;
            }
            transform.position += velocity;
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
        surfaces[0] = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y), transform.position.z);
        //bottom
        surfaces[1] = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y), transform.position.z);
        //left
        surfaces[2] = new Vector3(transform.position.x - (transform.localScale.x), transform.position.y, transform.position.z);
        //right
        surfaces[3] = new Vector3(transform.position.x + (transform.localScale.x), transform.position.y, transform.position.z);
        //front
        surfaces[4] = new Vector3(transform.position.x, transform.position.y, transform.position.z - (transform.localScale.z));
        //back
        surfaces[4] = new Vector3(transform.position.x, transform.position.y, transform.position.z + (transform.localScale.z));
    }

    void BuildNormals()
    {
        //top
        normals[0] = transform.position - surfaces[0];
        //bottom
        normals[1] = transform.position - surfaces[1];
        //left
        normals[2] = transform.position - surfaces[2];
        //right
        normals[3] = transform.position - surfaces[3];
        //front
        normals[4] = transform.position - surfaces[4];
        //back
        normals[5] = transform.position - surfaces[5];
    }
}
