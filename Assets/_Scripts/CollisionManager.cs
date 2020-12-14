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

    public bool CheckAABBs(CubeBehaviour a, CubeBehaviour b)
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
        Vector3 bulletmin = new Vector3(a.transform.position.x - a.transform.localScale.x, a.transform.position.y - a.transform.localScale.y, a.transform.position.z - a.transform.localScale.z);
        Vector3 bulletmax = new Vector3(a.transform.position.x + a.transform.localScale.x, a.transform.position.y + a.transform.localScale.y, a.transform.position.z + a.transform.localScale.z);

        var x = Mathf.Max(b.min.x, Mathf.Min(a.transform.position.x, b.max.x));
        var y = Mathf.Max(b.min.y, Mathf.Min(a.transform.position.y, b.max.y));
        var z = Mathf.Max(b.min.z, Mathf.Min(a.transform.position.z, b.max.z));

        var distanceX = Mathf.Sqrt(x - a.transform.position.x) * (x - a.transform.position.x);
        var distanceY = Mathf.Sqrt(y - a.transform.position.y) * (y - a.transform.position.y);
        var distanceZ = Mathf.Sqrt(z - a.transform.position.z) * (z - a.transform.position.z);

        bool checkXmin = bulletmax.x > b.min.x && bulletmax.x < b.max.x;
        bool checkXmax = bulletmin.x < b.max.x && bulletmin.x > b.min.x;
        bool checkYmin = bulletmax.y > b.min.y && bulletmax.y < b.max.y;
        bool checkYmax = bulletmin.y < b.max.y && bulletmin.y > b.min.y;
        bool checkZmin = bulletmax.z > b.min.z && bulletmax.z < b.max.z;
        bool checkZmax = bulletmin.z < b.max.z && bulletmin.z > b.min.z;

        if (distanceX < a.transform.localScale.x && distanceY < a.transform.localScale.x && distanceZ < a.transform.localScale.x)
        {
            // -x side
            if (checkXmin && checkXmax && !checkYmin && !checkYmax && !checkZmin && !checkZmax)
            {
                a.velocity.x *= -1.0f;
            }
            // +x side
            else if (checkXmin && checkXmax && !checkYmin && !checkYmax && !checkZmin && !checkZmax)
            {
                a.velocity.x *= -1.0f;
            }
            // -y side
            else if (!checkXmin && !checkXmax && checkYmin && checkYmax && !checkZmin && !checkZmax)
            {
                a.velocity.y *= -1.0f;
            }
            // +y side
            else if (!checkXmin && !checkXmax && checkYmin && checkYmax && !checkZmin && !checkZmax)
            {
                a.velocity.y *= -1.0f;
            }
            // -z side
            else if (!checkXmin && !checkXmax && !checkYmin && !checkYmax && checkZmin && checkZmax)
            {
                a.velocity.z *= -1.0f;
            }
            // +z side
            else if (!checkXmin && !checkXmax && !checkYmin && !checkYmax && checkZmin && checkZmax)
            {
                a.velocity.z *= -1.0f;
            }
            return true;
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
