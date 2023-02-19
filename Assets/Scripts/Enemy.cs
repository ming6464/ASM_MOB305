using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator m_anim;
    protected bool m_isDeath,m_isActive;
    private int count;
    

    public virtual void Start()
    {
        m_anim = GetComponent<Animator>();
        
    }

    public virtual void Update()
    {
        
    }
    
    public void End()
    {
        m_isDeath = true;
        m_anim.SetTrigger("Death");
        foreach (BoxCollider2D t in GetComponents<BoxCollider2D>())
        {
            t.isTrigger = true;
        }

        Destroy(this.gameObject,0.8f);
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(TagConst.DEATHZONE))
            Death();
        if (count == 0 && col.gameObject.CompareTag(TagConst.MAINCAM))
        {
            count++;
            SetActiveAnim(true);
        }
            
    }

    public void SetActiveAnim(bool isActive)
    {
        m_isActive = isActive;
        m_anim.enabled = isActive;
    }
    
}