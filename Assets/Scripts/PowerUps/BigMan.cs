using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMan : MonoBehaviour
{

    PlayerManager manager;
    private float buffTime = 1f;
    private float ExtraHealth = 5f;
    private float PreBuffHealth;
    private SpriteRenderer Renderer;
    private CircleCollider2D Collider;

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


            
            manager = collision.gameObject.GetComponent<PlayerManager>();
            

            StartCoroutine("Buff");
        }
    }

    private IEnumerator Buff()
    {
        Debug.Log("buffing");
        //check max health before changing it
        PreBuffHealth = manager.GetMaxHealth();
        Debug.Log(PreBuffHealth + " = pre");
        //adding max health 
        manager.SetHealthAddMax(ExtraHealth);
        Debug.Log(manager.GetMaxHealth() + " = post");
        //checking max health again and setting players health to max no matter what it currently is
        manager.SetHealth(manager.GetMaxHealth());
        Debug.Log("working");


        //temporerolly remove the renderer and collider while the coroutine is going off
        if (Renderer)
        {
            Renderer.enabled = false;
        }
        if (Collider)
        {
            Collider.enabled = false;
        }

        //wait the buff time       
        yield return new WaitForSeconds(buffTime);

        Debug.Log("Debuffing");
        //set the max health back to its before changed value
        manager.SetHealthMax(PreBuffHealth);

        //if heath is more than max health set health to max health
        if (manager.GetMaxHealth() < manager.GetHealth())
        {
            manager.SetHealth(manager.GetMaxHealth());
        }
        Destroy(gameObject);
    }
}
