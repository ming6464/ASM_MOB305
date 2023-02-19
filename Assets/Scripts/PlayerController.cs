using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip au_jump;
    public Transform shottingPoint;
    public Bullet bullet;
    public float speed;
    public float minX;
    public bool isGround,isJump;
    private float m_x;
    private Rigidbody2D m_rg;
    private Animator m_anim;
    private const string  IDLE = "Idle",JUMPUP = "JumpUp",FALL = "Fall",RUN = "Run",HURT = "Hurt";
    private float m_curDirect,m_jumpStartTime,m_jumpTime,m_velocX_jumpHurt;
    private string m_curAnim;
    private Vector2 m_vt_jump, m_vt_jumpKillEnemy;

    private AudioSource m_aus;
    // Start is called before the first frame update
    void Start()
    {
        m_curDirect = 1;
        m_rg = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_vt_jump = new Vector2(0, Mathf.Sqrt(70 * 2));
        m_vt_jumpKillEnemy = new Vector2(0, Mathf.Sqrt(70));
        m_velocX_jumpHurt = 1.5f / (m_vt_jumpKillEnemy.y / 35);
        m_jumpTime = 3 / m_vt_jump.y;
        m_aus = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (GameManager.Ins.isGameOverOrWin) return;
        m_x = Input.GetAxisRaw("Horizontal");
        if (m_x != 0)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * m_x);
            if (transform.position.x < minX)
                transform.position = new Vector3(minX, transform.position.y, 0);

            ChangeDirect(m_x);
        }

        if (isGround && !isJump)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeAnim(JUMPUP);
                isJump = true;
                m_jumpStartTime = m_jumpTime;
                PlayEffectSound(au_jump);
            }
            else if (m_x != 0)
                ChangeAnim(RUN);
            else
                ChangeAnim(IDLE);
        }


        if (isJump)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_jumpStartTime -= Time.deltaTime;
                if (m_jumpStartTime > 0) m_rg.velocity = m_vt_jump;
                else
                {
                    isJump = false;
                    StartCoroutine(ChangeAnimJump(m_vt_jump.y));
                }
            }
            else
            {
                isJump = false;
                if (m_jumpStartTime > 0)
                    StartCoroutine(ChangeAnimJump(m_vt_jump.y));
            }
        }
        else if (!isGround && m_rg.velocity.y <= 0) ChangeAnim(FALL);

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Ins.bullet > 0)
        {
            Instantiate(bullet, shottingPoint.position, quaternion.identity).Run(m_curDirect);
            GameManager.Ins.IncreaseItem(false);
        }
        CamController.Ins.Run(transform.position.x,transform.position.y + 1);
    }

    

    void ChangeAnim(string newAnim)
    {
        if (string.Compare(m_curAnim,newAnim,true) == 0) return;
        m_curAnim = newAnim;
        m_anim.Play(newAnim);
    }

    void ChangeDirect(float direct)
    {
        if (m_curDirect != direct)
        {
            transform.localScale = new Vector3(direct, 1, 1);
            m_curDirect = direct;
        }
    }

    IEnumerator ChangeAnimJump(float vt)
    {
        yield return new WaitForSeconds(vt / 35);
        if (!isGround) ChangeAnim(FALL);
    }

    public void JumpKillEnemy()
    {
        ChangeAnim(JUMPUP);
        m_rg.velocity = m_vt_jumpKillEnemy;
        isJump = false;
        StartCoroutine(ChangeAnimJump(m_vt_jumpKillEnemy.y));
    }

    public void AnimHurt(int direct)
    {
        m_rg.velocity = new Vector2(direct * m_velocX_jumpHurt, m_vt_jumpKillEnemy.y);
    }

    public void PlayEffectSound(AudioClip au)
    {
        if(m_aus)
            m_aus.PlayOneShot(au);
    }
}
