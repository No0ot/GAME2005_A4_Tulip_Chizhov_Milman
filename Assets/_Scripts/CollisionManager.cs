using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    private static CollisionManager instance;
    public static CollisionManager Instance { get { return instance; } }

    public CollisionObject[] actors;
    public CubeBehaviour[] cubes;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        actors = FindObjectsOfType<CollisionObject>();
        cubes = FindObjectsOfType<CubeBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < actors.Length; i++)
        //{
        //    for (int j = 0; j < actors.Length; j++)
        //    {
        //        if (i != j)
        //        {
        //            CollisionType lhs = actors[i].collisionType;
        //            CollisionType rhs = actors[j].collisionType;
        //            if (lhs == rhs && lhs == CollisionType.CUBE)
        //            {
        //                CheckAABBs((CubeBehaviour)actors[i], (CubeBehaviour)actors[j]);
        //            }
        //            if (lhs == CollisionType.CUBE && rhs == CollisionType.SPHERE)
        //            {
        //                CheckCubeSphere(actors[j], (CubeBehaviour)actors[i]);
        //            }
        //            if (rhs == CollisionType.CUBE && lhs == CollisionType.SPHERE)
        //            {
        //                CheckCubeSphere(actors[i], (CubeBehaviour)actors[j]);
        //            }
        //            if (lhs == rhs && lhs == CollisionType.SPHERE)
        //            {
        //                CheckSpheres(actors[i], actors[j]);
        //            }
        //        }
        //    }
        //}
    }

    public static bool CheckAABBs(CubeBehaviour a, CubeBehaviour b)
    {
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            return true;

            //COLLISION RESPONSE
        }
        else 
            return false;
    }

    public bool CheckCubeSphere(CollisionObject a, CubeBehaviour b)
    {
        var x = Mathf.Max(b.min.x, Mathf.Min(a.transform.position.x, b.max.x));
        var y = Mathf.Max(b.min.y, Mathf.Min(a.transform.position.y, b.max.y));
        var z = Mathf.Max(b.min.z, Mathf.Min(a.transform.position.z, b.max.z));

        var distance = Mathf.Sqrt((x - a.transform.position.x) * (x - a.transform.position.x) +
            (y - a.transform.position.y) * (y - a.transform.position.y) +
            (z - a.transform.position.z) * (z - a.transform.position.z));

        if (distance < a.transform.localScale.x)
        {
            return true;
            //COLLISION RESPONSE
        }
        else
            return false;
    }

    public static void CheckSpheres(CollisionObject a, CollisionObject b)
    {
        float distance = Vector3.Distance(a.transform.position, a.transform.position);
        if (distance <= Vector3.Magnitude(a.transform.localScale) + Vector3.Magnitude(b.transform.localScale))
        {
            //COLLISION RESPONSE
        }
    }
}
