using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if (m_instance == null) m_instance = new GameObject().AddComponent<GameManager>();
            return m_instance;
        }
    }
    private static GameManager m_instance;
    #endregion
    // Start is called before the first frame update
    [SerializeField]
    int m_enemyTotal;
    [SerializeField]
    int m_enemyTotalMax;
    [SerializeField]
    int m_roundTotal = 0;
    [SerializeField]
    int m_roundTotalMax;
    int m_healthBoostTimesBought;
    int m_damageTimesBought;
    int m_healthCost = 50;
    int m_damageCost = 150;
    GameObject m_playerManager;
    int m_healthBoostAmount;
    int m_damageBoostAmount;
    TextMeshProUGUI m_healthBoostCost;
    TextMeshProUGUI m_damageBoostCost;
    TextMeshProUGUI m_healthText;
    TextMeshProUGUI m_damageText;
    TextMeshProUGUI m_scoreShop;
    [SerializeField]
    int m_score;
    GameObject m_enemySpawn;
    bool m_resetRound;
    float m_startHealth;
    float m_startShield;
    int m_startScore;
    /*
    [SerializeField]
    BaseWeapon[] m_savedWeapons;
    */
    void Awake()
    {
        m_instance = this;
        m_enemyTotalMax = 2 * 1;
        m_enemyTotal = m_enemyTotalMax;
    }
    void Start()
    {
        var crosshairInt = PlayerPrefs.GetInt("CrosshairInt");
        var crosshairs = Resources.LoadAll("Crosshair", typeof(Texture2D)).Select(x => x as Texture2D).ToList();
        Cursor.SetCursor(crosshairs[crosshairInt], new Vector2(crosshairs[crosshairInt].width /2, crosshairs[crosshairInt].height / 2), CursorMode.Auto);
    }
    void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_resetRound)
        {
            if (m_roundTotal <= m_roundTotalMax && m_enemyTotal <= 0)
            {
                m_enemyTotal = m_enemyTotalMax;
                m_enemySpawn.GetComponent<EnemySpawn>().TotalReset();
                m_resetRound = true;
                StartCoroutine(ResetRound());
            }
        }

        if (m_roundTotal > m_roundTotalMax)
        {
            m_roundTotal--;
            EndLevel();
        }
        UIManager.Instance.SetEnemiesLeft(m_enemyTotal);
    }
    IEnumerator ResetRound()
    {
        UIManager.Instance.SetRound(m_roundTotal + 1, m_roundTotalMax);
        yield return new WaitForSeconds(0.1f);
        m_roundTotal++;
        m_resetRound = false;
    }
    void EndLevel()
    {
        Time.timeScale = 0f;
        m_damageBoostCost = transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();
        m_healthBoostCost = transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>();
        m_scoreShop = transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>();
        m_healthText = transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>();
        m_damageText = transform.GetChild(0).GetChild(8).GetComponent<TextMeshProUGUI>();
        transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("PlayerUI").SetActive(false);
        m_healthBoostCost.text = "Cost\n" + m_healthCost + " Score";
        m_damageBoostCost.text = "Cost\n" + m_damageCost + " Score";
        if(m_playerManager != null)
        m_damageText.text ="Base Damage: " + m_playerManager.GetComponent<PlayerManager>().GetDamage();
        if (m_playerManager != null)
            m_healthText.text = "Max Health: " + m_playerManager.GetComponent<PlayerManager>().GetMaxHealth();
        m_scoreShop.text = "Score " + m_score;

    }
    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
                m_playerManager = GameObject.Find("Player");
            if(m_playerManager != null)
            {
                Debug.Log("Player is present");
            }
            else
            {
                Debug.Log("Creating Player");
                GameObject player = Resources.Load<GameObject>("Player");
                Instantiate(player);
                m_playerManager = player;
            }
            m_playerManager.transform.position = new Vector3(0, 0, 0);
            /*
            m_savedWeapons = m_playerManager.GetComponent<PlayerPickup>().weapons;
            if (m_savedWeapons[0] != null)
            {
                BaseWeapon clone = m_savedWeapons[0];
                clone.AmmoCapCurrent = clone.AmmoCapMax;
                m_playerManager.GetComponent<PlayerPickup>().SetWeaponRestart(m_savedWeapons[0].WeaponIcon, null);
                m_playerManager.GetComponent<PlayerPickup>().weapons[0] = clone;
            }
            if (m_savedWeapons[1] != null)
            {
                BaseWeapon clone = m_savedWeapons[1];
                clone.AmmoCapCurrent = clone.AmmoCapMax;
                m_playerManager.GetComponent<PlayerPickup>().SetWeaponRestart(null, m_savedWeapons[1].WeaponIcon);
                m_playerManager.GetComponent<PlayerPickup>().weapons[1] = clone;
            }
            */
            
            //Used to reset variables on reset run
            m_startHealth = m_playerManager.GetComponent<PlayerManager>().GetHealth();
            m_startShield = m_playerManager.GetComponent<PlayerManager>().GetShield();
            m_startScore = m_score;
            m_enemySpawn = GameObject.Find("EnemySpawner");
            if (m_enemySpawn != null)
            {

                Debug.Log("EnemySpawn is present");
            }
            else
            {
                Debug.Log("Creating EnemySpawner");
                GameObject enemySpawn = Resources.Load<GameObject>("EnemySpawner");
                Instantiate(enemySpawn);
                m_enemySpawn = enemySpawn;
            }
            m_roundTotalMax = SceneManager.GetActiveScene().buildIndex;
            m_roundTotal = 1;
            m_enemyTotalMax = 25 * m_roundTotalMax *2;
            m_enemyTotal = m_enemyTotalMax;
            UIManager.Instance.SetRound(m_roundTotal, m_roundTotalMax);
            UIManager.Instance.SetEnemiesLeft(m_enemyTotalMax);
        }
    }
    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
  
        /*
        if(m_playerManager != null)
        m_savedWeapons = m_playerManager.GetComponent<PlayerPickup>().weapons;
        */
    }
    public void LoadMainMenu()
    {
        m_playerManager = GameObject.Find("Player");


        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {

        AchievementManager.Instance.CanDisplay = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        m_playerManager = GameObject.Find("Player");
        /*
       if(m_savedWeapons[0] != null)
       {
           BaseWeapon clone = m_savedWeapons[0];
           clone.AmmoCapCurrent = clone.AmmoCapMax;
           m_playerManager.GetComponent<PlayerPickup>().SetWeaponRestart(m_savedWeapons[0].WeaponIcon,null);
           m_playerManager.GetComponent<PlayerPickup>().weapons[0] = clone;
       }
       if (m_savedWeapons[1] != null)
       {
           BaseWeapon clone = m_savedWeapons[1];
           clone.AmmoCapCurrent = clone.AmmoCapMax;
           m_playerManager.GetComponent<PlayerPickup>().SetWeaponRestart(null, m_savedWeapons[1].WeaponIcon);
           m_playerManager.GetComponent<PlayerPickup>().weapons[1] = clone;
       }

       m_savedWeapons = m_playerManager.GetComponent<PlayerPickup>().weapons;
       */
        m_playerManager.GetComponent<PlayerManager>().SetHealth(m_startHealth);
        m_playerManager.GetComponent<PlayerManager>().SetShield(m_startShield);
        Time.timeScale = 1;
        m_score = m_startScore;
        UIManager.Instance.SetScore(m_score);
        UIManager.Instance.ShowReloadingText(false);
        UIManager.Instance.ShowPressReloadText(false);
        UIManager.Instance.SetShieldBar(m_startShield);
        UIManager.Instance.SetHealthBar(m_startHealth);
        UIManager.Instance.ShowPickupText(false);
    }
    public void IncreaseRoundTotal(int _increase)
    {
        m_roundTotal += _increase;

    }
    public void IncreasePlayerHealth()
    {
        if (m_score >= m_healthCost)
        {
            if (m_score - m_healthCost >= 0)
            {
                m_playerManager = GameObject.Find("Player");
                m_healthBoostTimesBought++;
                m_healthCost = 50 * m_healthBoostTimesBought;
                m_playerManager.GetComponent<PlayerManager>().SetHealthAddMax(1);
                m_score -= m_healthCost;
                if (m_score <= 0)
                {
                    m_score = 0;
                }
                m_scoreShop.text = "Score " + m_score;
                Debug.Log("Times bought health " + m_healthBoostTimesBought);
                m_healthBoostCost.text = "Cost\n" + m_healthCost + " Score";
                m_healthText.text = "Max Health: " + m_playerManager.GetComponent<PlayerManager>().GetMaxHealth();
            }
        }
    }
    public void IncreasePlayerDamage()
    {
        if (m_score >= m_damageCost)
        {
            if (m_score - m_damageCost >= 0)
            {
                m_playerManager = GameObject.Find("Player");
                m_damageTimesBought++;
                m_damageCost = 150 * m_damageTimesBought;
                m_playerManager.GetComponent<PlayerManager>().SetDamageBaseAdd(1);
                m_score -= m_damageCost;
                if (m_score <= 0)
                {
                    m_score = 0;
                }
                m_scoreShop.text = "Score " + m_score;
                m_damageText.text = "Base Damage: " + m_playerManager.GetComponent<PlayerManager>().GetDamage();
                m_damageBoostCost.text = "Cost\n" + m_damageCost + " Score";
            }
        }
    }
    public int GetEnemyTotal()
    {
        return m_enemyTotalMax;
    }
    public int GetRoundTotal()
    {
        return m_roundTotal;
    }
    public int GetMaxRoundTotal()
    {
        return m_roundTotalMax;
    }
    public void LowerEnemyTotal(int _lowerAmount)
    {
        m_enemyTotal -= _lowerAmount;
      //  Debug.Log("Enemy total lowering" + m_enemyTotal);
    }
    public void IncreaseEnemyTotal(int _increaseAmount)
    {
        m_enemyTotal += _increaseAmount;
    }
    public int GetScore()
    {
        return m_score;
    }
    public void SetScore(int _score)
    {
        m_score = _score;
    }
    public void SetScoreAdd(int _score)
    {
        m_score += _score;
        UIManager.Instance.SetScore(m_score);
    }
    public void SetScoreMinus(int _score)
    {
        m_score -= _score;
        UIManager.Instance.SetScore(m_score);
    }

}
