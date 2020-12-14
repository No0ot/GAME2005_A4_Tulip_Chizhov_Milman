using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : CollisionObject
{
    public float speed = 8f;
    public float lifeDuration = 2f;
    public float radius;

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

                // Vector3 colVec = transform.forward * -1.0f;

                //velocity = Vector3.Cross(velocity, colVec);

            }
        }
          
    }
}
