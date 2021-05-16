using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPickup : MonoBehaviour
{
    public static GameObject WeaponUnder;
    PlayerFiring m_playerFiring;
    public BaseWeapon[] weapons;
    [SerializeField]
    BaseWeapon m_selectedWeapon;
    int m_weaponSelected = 0;
    GameObject m_playerUI;
    PlayerManager m_playerManager;
    
    // Start is called before the first frame update
    void Start()
    {
        m_playerUI = GameObject.Find("PlayerUI");
    
        if (weapons[0] != null)
        {
            m_selectedWeapon = weapons[0];
            UIManager.Instance.SetAmmoCount(m_selectedWeapon.AmmoCapCurrent);
        }
        m_playerFiring = GetComponent<PlayerFiring>();
        m_playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Drop();
        PickUp();
        if(m_playerUI != null)
        {

       
        SwitchWeapon();
        if (weapons[0] != null)
        {
            m_playerUI.transform.GetChild(0).GetComponent<Image>().sprite = weapons[0].WeaponIcon;

        }
        if (weapons[1] != null)
        {
            m_playerUI.transform.GetChild(1).GetComponent<Image>().sprite = weapons[1].WeaponIcon;
        }
        }
    }
    public void SetWeaponRestart(Sprite _weaponOne,Sprite _weaponTwo)
    {
        if(m_playerUI != null)
        {
            if (weapons[0] != null && _weaponOne != null)
            {
                m_playerUI.transform.GetChild(0).GetComponent<Image>().sprite = _weaponOne;
            }
            if (weapons[1] != null && _weaponTwo != null)
            {
                m_playerUI.transform.GetChild(1).GetComponent<Image>().sprite = _weaponTwo;
            }
        }
   
    }
    void SwitchWeapon()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            m_weaponSelected += 1;
            m_playerFiring.StopReload();
            m_playerFiring.SetReloading(false);
            m_playerFiring.AmmoCapCurrent = m_selectedWeapon.AmmoCapCurrent;
       
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            m_weaponSelected -= 1;
            m_playerFiring.StopReload();
            m_playerFiring.SetReloading(false);
            m_playerFiring.AmmoCapCurrent = m_selectedWeapon.AmmoCapCurrent;
          
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_weaponSelected = 0;
            m_playerFiring.StopReload();
            m_playerFiring.SetReloading(false);
            m_playerFiring.AmmoCapCurrent = m_selectedWeapon.AmmoCapCurrent;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_weaponSelected = 1;
            m_playerFiring.StopReload();
            m_playerFiring.SetReloading(false);
            m_playerFiring.AmmoCapCurrent = m_selectedWeapon.AmmoCapCurrent;
        }
        if (m_weaponSelected == 1)
        {
            if (weapons[1] != null)
            {
              
                m_selectedWeapon = weapons[1];
                m_playerUI.transform.GetChild(0).GetComponent<Image>().color = new Vector4(1, 1, 1, 0.5f);
                if (weapons[0] != null)
                    m_playerUI.transform.GetChild(1).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            }
        }
        if (m_weaponSelected == 0)
        {
            if (weapons[0] != null)
            {
                m_playerFiring.AmmoCapCurrent = m_selectedWeapon.AmmoCapCurrent;
                m_selectedWeapon = weapons[0];
                m_playerUI.transform.GetChild(0).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);

                if (weapons[1] != null)
                    m_playerUI.transform.GetChild(1).GetComponent<Image>().color = new Vector4(1, 1, 1, 0.5f);
            }
        }

       
        if (weapons[0] == null)
        {
            m_weaponSelected = 1;
     
            m_playerUI.transform.GetChild(0).GetComponent<Image>().color = new Vector4(1, 1, 1, 0);

            m_playerUI.transform.GetChild(1).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
        if (weapons[1] == null)
        {
            m_weaponSelected = 0;
            m_playerUI.transform.GetChild(1).GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
            m_playerUI.transform.GetChild(0).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
        
        if (m_weaponSelected < 0)
        {
            m_weaponSelected = 1;
        }
        if (m_weaponSelected > 1)
        {
            m_weaponSelected = 0;
        }
        if (weapons[0] == null && weapons[1] == null)
        {
            m_selectedWeapon = null;
            UIManager.Instance.SetAmmoCount(0);

            m_playerUI.transform.GetChild(1).GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
            m_playerUI.transform.GetChild(0).GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
        }
    }
    void Drop()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_playerFiring.StopReload();

            for (int i = 0; i < 2; i++)
            {
                if (weapons[i] != null && m_weaponSelected == i)
                {
                    m_playerUI.transform.GetChild(i).GetComponent<Image>().sprite = null;
                    m_playerUI.transform.GetChild(i).GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
                    

                    weapons[i] = null;
                }
            }
        }
    }
    void PickUp()
    {
        if (WeaponUnder != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < weapons.Length; ++i)
                {
                    if (weapons[i] == null)
                    {
                        weapons[i] = Instantiate(WeaponUnder.GetComponent<WeaponHolder>().weapon);
                        weapons[i].Damage = weapons[i].Damage + m_playerManager.GetDamage();
                        m_playerUI.transform.GetChild(i).GetComponent<Image>().sprite = weapons[i].WeaponIcon;
                        if (weapons[i].WeaponIcon.rect.width > 120)
                        {
                            m_playerUI.transform.GetChild(i).GetComponent<Image>().rectTransform.localScale = new Vector3(2, 1, 1);
                        }
                        m_playerUI.transform.GetChild(i).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                        Destroy(WeaponUnder);
                        break;
                    }
                }
                if (weapons[0] != null && weapons[1] != null)
                {
                    if (WeaponUnder != null)
                    {
                        weapons[m_weaponSelected] = Instantiate(WeaponUnder.GetComponent<WeaponHolder>().weapon);
                        weapons[m_weaponSelected].Damage = weapons[m_weaponSelected].Damage + m_playerManager.GetDamage();
                        m_playerUI.transform.GetChild(m_weaponSelected).GetComponent<Image>().sprite = weapons[m_weaponSelected].WeaponIcon;
                        if (weapons[m_weaponSelected].WeaponIcon.rect.width > 120)
                        {
                            m_playerUI.transform.GetChild(m_weaponSelected).GetComponent<Image>().rectTransform.localScale = new Vector3(2, 1, 1);
                        }
                    

                        m_playerUI.transform.GetChild(m_weaponSelected).GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                        Destroy(WeaponUnder);
                    }
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WeaponPickup")
        {
            UIManager.Instance.ShowPickupText(true);
            WeaponUnder = collision.GetComponent<WeaponHolder>().gameObject;
            Debug.Log("OVER WEAPON");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WeaponPickup")
        {
            UIManager.Instance.ShowPickupText(false);
            var weapon = collision.GetComponent<WeaponHolder>().gameObject;

            if (weapon == WeaponUnder)
                WeaponUnder = null;

            Debug.Log("LEFT WEAPON");
        }
    }
    public BaseWeapon GetCurrentWeapon()
    {
        return m_selectedWeapon;
    }
}
