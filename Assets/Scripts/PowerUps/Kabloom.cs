using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kabloom : MonoBehaviour
{
    private SpriteRenderer Renderer;
    private CircleCollider2D Collider;
    private float buffTime = 10f;
    private float buffPercent = 2f;
    private PlayerFiring Firing;
    private PlayerPickup Pickup;
    BaseWeapon currentWeapon;
    private float m_damage;

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
            Firing = collision.gameObject.GetComponent<PlayerFiring>();
            Debug.Log("Hit quick tap power Up");
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
        if (Firing)
        {
            Pickup = Firing.GetPlayerPickup();
            if (Pickup)
            {
                currentWeapon = Pickup.GetCurrentWeapon();
                //get current weapon current ammo count
                Debug.Log(currentWeapon.Damage);
                m_damage = (currentWeapon.Damage);

                currentWeapon.Damage = m_damage * buffPercent;
                Debug.Log(currentWeapon.Damage + " new damage");
            }
        }

        yield return new WaitForSeconds(buffTime);
        //debug stuff      
        
        currentWeapon.Damage = m_damage;
        Debug.Log(currentWeapon.Damage + " buff end damage");
        Destroy(gameObject);
    }
}
