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
        lifeTimer = lifeDuration;
        collisionType = CollisionType.SPHERE;
        radius = transform.localScale.magnitude/2.0f;
        restitution = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet Movement
        transform.position += transform.forward * speed * Time.deltaTime;

        // Bullet Liftime
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
