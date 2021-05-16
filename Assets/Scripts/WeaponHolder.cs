using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public BaseWeapon weapon;
  
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.WeaponIcon;
    }
}
