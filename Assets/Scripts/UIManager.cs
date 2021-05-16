using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance
    {
        get
        {
            if (m_instance == null) m_instance = new GameObject().AddComponent<UIManager>();
            return m_instance;
        }
    }
    private static UIManager m_instance;
    #endregion
    // Start is called before the first frame update
    TextMeshProUGUI m_ammoCount;
    TextMeshProUGUI m_scoreCount;
    TextMeshProUGUI m_reloading;
    TextMeshProUGUI m_pickup;
    TextMeshProUGUI m_reloadKey;
    TextMeshProUGUI m_roundText;
    TextMeshProUGUI m_enemiesLeftText;
    Slider m_healthBar;
    Slider m_shieldSlider;
    void Awake()
    {
        m_instance = this;

    }
    void Start()
    {
        m_healthBar = transform.GetChild(2).GetComponent<Slider>();
            m_shieldSlider = transform.GetChild(3).GetComponent<Slider>();
            m_ammoCount = transform.GetChild(4).GetComponent<TextMeshProUGUI>();

            m_scoreCount = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
     
            m_reloading = transform.GetChild(6).GetComponent<TextMeshProUGUI>();
     
            m_pickup = transform.GetChild(8).GetComponent<TextMeshProUGUI>();
   
            m_reloadKey = transform.GetChild(9).GetComponent<TextMeshProUGUI>();
      
            m_roundText = transform.GetChild(10).GetComponent<TextMeshProUGUI>();
     
            m_enemiesLeftText = transform.GetChild(11).GetComponent<TextMeshProUGUI>();

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void SetAmmoCount(int _ammo)
    {
        if (m_ammoCount != null)
            m_ammoCount.text = "Ammo " + _ammo;
    }
    public void SetHealthBar(float _health)
    {
        if (m_healthBar != null)
            m_healthBar.value = _health;

           
    }
    public void SetShieldBar(float _shield)
    {
        if (m_shieldSlider != null)
            m_shieldSlider.value = _shield;
    }
    public void SetShieldBarRespawn(float _shield)
    {
        if (m_shieldSlider != null)
        {
            m_shieldSlider.maxValue = _shield;
        }
       
    }
    public void SetHealthRespawn(float _health)
    {
        if (m_healthBar != null)
        {
            m_healthBar.maxValue = _health;
            m_healthBar.value = m_healthBar.maxValue;

        }
    }
    public void SetScore(int _score)
    {
        if (m_scoreCount != null)
            m_scoreCount.text = "Score " + _score;
    }
    public void SetEnemiesLeft(int _enemiesLeft)
    {
        if (m_enemiesLeftText != null)
            m_enemiesLeftText.text = "Enemies Left: " + _enemiesLeft;
    }
    public void SetRound(int _round, int _roundMax)
    {
        if (m_roundText != null)
            m_roundText.text = "Round: " + _round + " of " + _roundMax;
    }
    public void ShowReloadingText(bool _show)
    {
        if (m_reloading != null)
        {
            if (_show)

                m_reloading.gameObject.SetActive(true);
            else
            {
                m_reloading.gameObject.SetActive(false);
            }
        }
           
    }

    public void ShowPickupText(bool _show)
    {
        if (m_pickup != null)
        {
            if (_show)
                m_pickup.gameObject.SetActive(true);
            else
            {
                m_pickup.gameObject.SetActive(false);
            }
        }
      
    }

    public void ShowPressReloadText(bool _show)
    {
        if (m_reloadKey != null)
        {
            if (_show)
                m_reloadKey.gameObject.SetActive(true);
            else
            {
                m_reloadKey.gameObject.SetActive(false);
            }
        }
       
    }

}
