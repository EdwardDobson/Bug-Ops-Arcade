using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WowieJam2.Enemy;
public class Grenade : MonoBehaviour
{
    public float Damage;
    float m_grenadeAOE;
    float m_aliveTime;
    bool m_aboutToBlowup;
    float m_detonateTime;
    // Start is called before the first frame update
    void Start()
    {
        m_detonateTime = m_aliveTime - 0.1f;
        StartCoroutine(Kill(m_detonateTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.layer == 8)
        {
            StopAllCoroutines();
            StartCoroutine(Kill(0));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(m_detonateTime <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Kill(0));
        }
    }
    public void SetGrenadeRaidus(float _radius)
    {
        m_grenadeAOE = _radius;
    }
    public void SetAliveTime(float _aliveTime)
    {
        m_aliveTime = _aliveTime;
    }

    private IEnumerator Kill(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider2D>().enabled = false;
        var colls = Physics2D.OverlapCircleAll(transform.position, m_grenadeAOE);
        Debug.Log(colls.Length);
        foreach (var col in colls)
        {
            if (col.tag == "Enemy")
            {
                var hit = Physics2D.Linecast(transform.position, col.transform.position);
                Debug.Log(hit.collider);
                if (hit.collider != null && hit.collider.tag == "Enemy")
                {
                    Debug.Log("DAMAGING ENEMY");
                    hit.collider.gameObject.GetComponent<BaseEnemy>().TakeDamage(Damage, true);
                }
            }
        }
        Explosion.Create(transform.position);
        Destroy(gameObject);
    }
}
