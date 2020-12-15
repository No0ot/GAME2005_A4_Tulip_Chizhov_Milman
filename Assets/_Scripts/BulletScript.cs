using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : CollisionObject
{
    public float speed = 50f;
    public float lifeDuration = 2f;
    public float radius;
    public Vector3 closestwall;

    private float lifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.forward * speed;
    }

    void OnEnable()
    {
        velocity = transform.forward * speed;
        lifeTimer = lifeDuration;
        collisionType = CollisionType.SPHERE;
        radius = transform.localScale.magnitude / 2.0f;
        restitution = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        //base.Update();
        //Bullet Movement
        acceleration = new Vector3(0,-0.1f, 0);
        velocity += acceleration;


        transform.position += velocity * Time.deltaTime;

        // Bullet Liftime
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }

        //Check Collisons
        foreach (CubeBehaviour actor in CollisionManager.Instance.cubes)
        {
            if (CollisionManager.Instance.CheckCubeSphere(this, actor))
            {
                if (!actor.isWall)
                {
                    actor.transform.forward = transform.forward;
                    actor.colliding = true;
                }

                Vector3[] cube_walls = actor.surfaces;
                float closestdistance = 1000.0f;
                foreach (Vector3 wall in cube_walls)
                {
                    float tempdistance = Mathf.Sqrt((transform.position.x - wall.x) * (transform.position.x - wall.x) +
                                                    (transform.position.y - wall.y) * (transform.position.y - wall.y) +
                                                    (transform.position.z - wall.z) * (transform.position.z - wall.z));
                    if (tempdistance < closestdistance)
                    {
                        closestdistance = tempdistance;
                        closestwall = wall;
                    }
                }


                if (closestwall == actor.surfaces[0] || closestwall == actor.surfaces[1])
                {
                    velocity.y *= -0.8f;
                }
                else if (closestwall == actor.surfaces[2] || closestwall == actor.surfaces[3])
                {
                    velocity.x *= -0.8f;
                }
                else if (closestwall == actor.surfaces[4] || closestwall == actor.surfaces[5])
                {
                    velocity.z *= -0.8f;
                }

            }
        }
        closestwall *= 0;
    }
}
