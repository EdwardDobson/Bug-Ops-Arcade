using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    float m_health;
    [SerializeField]
    float m_maxHealth;
    [SerializeField]
    float m_shield;
    [SerializeField]
    float m_maxshield;
    GameObject gameOver;
    [SerializeField]
    float m_damageBase;
    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = CrosshairManager.PlayerSprite;
        UIManager.Instance.SetHealthRespawn(m_health);
        UIManager.Instance.SetShieldBarRespawn(m_maxshield);

    }
   void Update()
    {
        if (GameObject.Find("GameManager").transform.GetChild(2).gameObject.activeSelf)
        {
            GameObject.Find("GameManager").transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void TakeDamage(float dmg)
    {
        if(m_shield <= 0)
        m_health -= dmg;
        else
        {
            m_shield -= dmg;
            if (m_shield < 0)
                m_shield = 0;
        }
        DamageIndicator.Create(new Vector3(transform.position.x, transform.position.y + .75f), dmg);
        Debug.Log("Player took " + dmg + " damage.");
        if(m_health <= 0.0f)
        {
            m_health = 0;
            AchievementManager.Instance.CanDisplay = false;
            GameObject.Find("GameManager").transform.GetChild(2).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        UIManager.Instance.SetHealthBar(m_health);
        UIManager.Instance.SetShieldBar(m_shield);
    }
    #region Getters
    public float GetHealth()
    {
        return m_health;
    }
    public float GetMaxHealth()
    {
        return m_maxHealth;
    }
    public float GetShield()
    {
        return m_shield;
    }
    public float GetMaxShield()
    {
        return m_maxshield;
    }

    public float GetDamage()
    {
        return m_damageBase;
    }
    #endregion
    #region Setters
    public void SetHealth(float _health)
    {
        m_health = _health;
    }
    public void SetHealthMax(float _health)
    {
        m_maxHealth = _health;
    }
    public void SetHealthAdd(float _health)
    {
        m_health += _health;
    }
    public void SetHealthAddMax(float _health)
    {
        m_maxHealth += _health;
        m_health = m_maxHealth;
    }
    public void SetHealthMinus(float _health)
    {
        m_health -= _health;
    }
    public void SetShield(float _shield)
    {
        m_shield = _shield;
    }
    public void SetShieldAdd(float _shield)
    {
        m_shield += _shield;
    }
    public void SetShieldMinus(float _shield)
    {
        m_shield -= _shield;
    }
    public void SetDamageBaseAdd(float _damage)
    {
        m_damageBase += _damage;
    }
    public void SetDamageBaseMinus(float _damage)
    {
        m_damageBase -= _damage;
    }
    #endregion
}
