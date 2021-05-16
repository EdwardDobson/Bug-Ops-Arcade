using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupTypes
{
    eHealthPickup,
    eShieldPickup,
}

public class BuffPickup : MonoBehaviour
{
    public PickupTypes pickupType;
    public float giveValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.tag == "Player")
        {
            switch (pickupType)
            {
                case PickupTypes.eHealthPickup:
                    if(_other.GetComponent<PlayerManager>().GetHealth() < _other.GetComponent<PlayerManager>().GetMaxHealth())
                    {
                        Debug.Log("Health");
                        _other.GetComponent<PlayerManager>().SetHealthAdd(giveValue);
                        UIManager.Instance.SetHealthBar(_other.GetComponent<PlayerManager>().GetHealth());
                        Destroy(gameObject);
                    }             
                    break;
                case PickupTypes.eShieldPickup:
                    if (_other.GetComponent<PlayerManager>().GetShield() < _other.GetComponent<PlayerManager>().GetMaxShield())
                    {
                        Debug.Log("Shield");
                        _other.GetComponent<PlayerManager>().SetShieldAdd(giveValue);
                        UIManager.Instance.SetShieldBar(_other.GetComponent<PlayerManager>().GetShield());
                        Destroy(gameObject);
                    }
                    break;
            }

        
        }
    }
}
