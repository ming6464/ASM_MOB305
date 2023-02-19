using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip au_collect;
    private PlayerController m_playerController;
    private int m_curItemId;

    private void Start()
    {
        m_playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        

        if (col.gameObject.CompareTag("Enemy"))
        {
            GameManager.Ins.DecreaseHeath();
            int direct = 1;
            if (transform.position.x < col.gameObject.transform.position.x)
                direct = -1;
            m_playerController.AnimHurt(direct);
        }
            
    }
    

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagConst.GROUND))
        {
            m_playerController.isGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(TagConst.DEATHZONE))
            GameManager.Ins.ShowDialog(1);
        else
        {
            if (col.gameObject.CompareTag(TagConst.ITEM))
            {
                m_playerController.PlayEffectSound(au_collect);
                GameObject item = col.gameObject;
                if (item.GetInstanceID() != m_curItemId)
                {
                    m_curItemId = item.GetInstanceID();
                    GameManager.Ins.IncreaseItem(item.GetComponent<Item>().isCoin,true);
                }
            
            }
            if (col.gameObject.CompareTag(TagConst.GROUND))
            {
                m_playerController.isGround = true;
                m_playerController.isJump = false;
            }

            if (col.gameObject.CompareTag(TagConst.ENEMY))
            {
                col.gameObject.GetComponent<Enemy>().End();
                m_playerController.JumpKillEnemy();
            }
        }
        
    }
}