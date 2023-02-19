using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private float m_direct;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * m_direct * speed * Time.deltaTime;
    }

    public void Run(float direct)
    {
        m_direct = direct;
        Destroy(gameObject,2);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().End();
        }
        
        Destroy(gameObject);
    }
}
