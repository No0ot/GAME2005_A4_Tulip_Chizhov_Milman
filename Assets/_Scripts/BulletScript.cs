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
    void OnEnable()
    {
        lifeTimer = lifeDuration;
        collisionType = CollisionType.SPHERE;
        radius = transform.localScale.magnitude / 2.0f;
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
            gameObject.SetActive(false);
        }

        //Check Collisons
        foreach (CubeBehaviour actor in CollisionManager.Instance.cubes)
        {
            if (CollisionManager.Instance.CheckCubeSphere(this, actor))
            {
                Debug.Log("Bullet Hit: " + actor.name);
                actor.transform.forward = transform.forward;
                actor.colliding = true;
                transform.forward = transform.forward * -1.0f;

            }
        }
          
    }
}
