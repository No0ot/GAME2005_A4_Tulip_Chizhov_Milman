using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Camera m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
  
            GameObject bulletObject = BulletManager.Instance.getBullet();
            bulletObject.transform.position = m_Camera.transform.position + m_Camera.transform.forward;
            bulletObject.transform.forward = m_Camera.transform.forward;
        }
    }
}
