using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WowieJam2.Enemy;

public class Bullet : MonoBehaviour
{
    public float Damage;
    float m_aliveTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !collision.isTrigger)
        {
            collision.gameObject.GetComponent<BaseEnemy>().TakeDamage(Damage);
            BloodParticleSystemHandler.Instance.SpawnBlood(collision.gameObject.transform.position, collision.gameObject.transform.position - transform.position);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == 8)
        {
            // OBSTACLE
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Destroy(gameObject, m_aliveTime);
    }
    public void SetAliveTime(float _aliveTime)
    {
        m_aliveTime = _aliveTime;
    }
}
