using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTap : MonoBehaviour
{

    private float buffTime = 5f;
    private SpriteRenderer Renderer;
    private CircleCollider2D Collider;
    private PlayerFiring Firing;
    private PlayerPickup Pickup;
    private int m_AmmoMax, m_AmmoCurrent;
    private float m_FireRate;
    private float m_FireRateBuff = 0.2f;
    BaseWeapon currentWeapon;

    private int BuffMultipliyer = 5;

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
                Debug.Log(currentWeapon.AmmoCapCurrent);
                m_AmmoCurrent = Firing.AmmoCapCurrent;
                //get current weapon max ammo count
                Debug.Log(currentWeapon.AmmoCapMax);                
                m_AmmoMax = Firing.AmmoCapMax;
                //get current fire rate
                Debug.Log(currentWeapon.GunFireRate + " fire rate");
                m_FireRate = currentWeapon.GunFireRate;
                Firing.GettingBuffed = true;
                //buff the current and max ammo counts for a time period
                Firing.AmmoCapMax = m_AmmoMax * BuffMultipliyer;
                Firing.AmmoCapCurrent = Firing.AmmoCapMax;
                Pickup.GetCurrentWeapon().AmmoCapCurrent = Firing.AmmoCapMax;
                //buff fire rate
                Firing.FireRate = m_FireRate * m_FireRateBuff;
                UIManager.Instance.SetAmmoCount(Firing.AmmoCapMax);

            }
        }
        
        yield return new WaitForSeconds(buffTime);
        //debug stuff
        Debug.Log(currentWeapon.AmmoCapCurrent + " current ammo");
        Debug.Log(currentWeapon.AmmoCapMax + " max ammo");
        Debug.Log(currentWeapon.GunFireRate + " fire rate");
        Firing.GettingBuffed = false;
        Firing.AmmoCapMax = m_AmmoMax;
        currentWeapon.GunFireRate = m_FireRate;
        if (Firing.AmmoCapCurrent < m_AmmoCurrent)
        {

        }
        else
        {
            Firing.AmmoCapCurrent = m_AmmoMax;
            Pickup.GetCurrentWeapon().AmmoCapCurrent = Firing.AmmoCapMax;
        }
        UIManager.Instance.SetAmmoCount(Firing.AmmoCapCurrent);
     
        Destroy(gameObject);
    }
}

