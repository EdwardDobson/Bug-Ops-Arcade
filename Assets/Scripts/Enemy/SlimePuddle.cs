using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WowieJam2.Movement;

public class SlimePuddle : MonoBehaviour
{
    private bool m_PlayerOver = false;
    private float m_TimeDelay = 0f;

    private GameObject m_Player;
    private void Start()
    {
        GetComponent<AudioSource>().Play();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 7.5f);
    }
    private void Update()
    {
        if (m_PlayerOver && m_TimeDelay <= 0)
        {
            m_TimeDelay = 3f;
            // TODO player slow
            var moveScript = m_Player.GetComponent<PlayerMovement>();
            moveScript.SlowDown(m_TimeDelay + 0.1f);
            // TODO player dots
            var playerScript = m_Player.GetComponent<PlayerManager>();
            playerScript.TakeDamage(2);
        }

        if (m_TimeDelay > 0) m_TimeDelay -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_PlayerOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_PlayerOver = false;
        }
    }
}
