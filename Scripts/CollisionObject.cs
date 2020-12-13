using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionType
{
    CUBE,
    SPHERE
}

public class CollisionObject : MonoBehaviour
{
    public CollisionType collisionType;
    public float restitution; //0 - no bounce, 1 - full bounce
    public float mass;
    bool physicsEnabled, gravityEnabled;
    public Vector3 velocity;
    public Vector3 acceleration;

    //keeping just in case
    public bool isColliding;
    public List<CollisionObject> contacts;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (physicsEnabled)
        {
            if (gravityEnabled) acceleration.y -= 9.8f;
            velocity += acceleration;
            transform.position = velocity + transform.position;
            acceleration *= 0;
        }
    }

    public void Push(Vector3 force)
    {
        acceleration += force;
        //will need some more stuff
    }
}
