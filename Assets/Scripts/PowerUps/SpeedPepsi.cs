using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WowieJam2.Movement;
public class SpeedPepsi : MonoBehaviour
{
    private SpriteRenderer Renderer;
    private CircleCollider2D Collider;
    PlayerMovement Movement;
    private float buffTime = 10f;
    private float m_Speed;
    private float buffPercent = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        Collider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Power Up");
            Movement = collision.gameObject.GetComponent<PlayerMovement>();

            StartCoroutine("Buff");
        }

    }

    IEnumerator Buff()
    {
        //temporerolly remove the renderer and collider while the coroutine is going off
        if (Renderer && Collider)
        {
            Renderer.enabled = false;
            Collider.enabled = false;
        }

        m_Speed = Movement.GetSpeed();
        Debug.Log(Movement.GetSpeed() + " pre buff");
        Movement.SetSpeed(m_Speed * buffPercent);
        Debug.Log(Movement.GetSpeed() + " post buff");

        yield return new WaitForSeconds(buffTime);

        Movement.SetSpeed(m_Speed);
        Debug.Log(Movement.GetSpeed() + " end buff");
        Destroy(gameObject);

    }
}
