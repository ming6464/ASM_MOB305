using UnityEngine;

public class EnemyRun : Enemy
{
    public float positionEndX,velocityOnSecond;
    int m_direction;
    float m_timeChangeDirection,m_maxX,m_minX,m_time;
    public override void Start()
    {
        base.Start();
        float curX = transform.position.x;
        if (curX < positionEndX)
        {
            m_direction = 1;
            m_minX = transform.position.x;
            m_maxX = positionEndX;
        }
        else
        {
            m_direction = -1;
            m_maxX = transform.position.x;
            m_minX = positionEndX;
        }
        m_timeChangeDirection = Mathf.Abs(m_maxX - m_minX) / velocityOnSecond;
        if(m_direction < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0);
        m_time = m_timeChangeDirection;
    }

    public override void Update()
    {
        if (!this.m_isActive)
        {
            return;
        }
        if (!m_isDeath)
        {
            if (m_time < Time.deltaTime)
            {
                transform.position += Vector3.right * velocityOnSecond * m_direction * m_time;
                UpdateDirectionAndTime(false);
                return;
            }
            m_time -= Time.deltaTime;
            transform.position += Vector3.right * velocityOnSecond * m_direction * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            UpdateDirectionAndTime(true);
        }
    }

    void UpdateDirectionAndTime(bool isColl)
    {
        m_direction *= -1;
        if(!isColl)
            m_time = 0;
        m_time = m_timeChangeDirection - m_time;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0);
    }
}