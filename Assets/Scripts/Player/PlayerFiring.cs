using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WowieJam2.Enemy;
using UnityEngine.SceneManagement;
public class PlayerFiring : MonoBehaviour
{
    PlayerPickup m_playerPickup;
    public GameObject BaseBullet;
    public GameObject BaseGrenade;
    public GameObject BaseMag;
    Transform m_firePoint;
    Transform m_firePointLaser;
    bool m_isFiring;
    bool m_isReloading;
    float m_shotTimer;
    float m_reloadTimer;
    GunTypes m_gunTypes;
    GameObject m_pause;
    public int AmmoCapMax;
    public int AmmoCapCurrent;
    public float FireRate;
    private float m_BUGFastFireCheck;
    public bool GettingBuffed;
    public AudioClip Pistol1;
    public AudioClip ShotgunSound;
    // Start is called before the first frame update
    void Start()
    {
        m_playerPickup = GetComponent<PlayerPickup>();
        m_firePoint = transform.GetChild(1);
        m_firePointLaser = transform.GetChild(2);
        m_pause = GameObject.Find("GameManager");
    }
    void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    void OnSceneLoaded(Scene _scene,LoadSceneMode _mode)
    {
        m_pause = GameObject.Find("GameManager");
        Debug.Log("Loading");
    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            if(!GettingBuffed)
            {
                AmmoCapMax = m_playerPickup.GetCurrentWeapon().AmmoCapMax;
                FireRate = m_playerPickup.GetCurrentWeapon().GunFireRate;
            }

            
        if (!m_pause.GetComponent<Pausing>().GetPausedState())
        {
            if (m_playerPickup.GetCurrentWeapon() != null)
            {
                    UIManager.Instance.SetAmmoCount(m_playerPickup.GetCurrentWeapon().AmmoCapCurrent);
                    if (Input.GetMouseButton(0) && !m_isReloading)
                {
                    m_isFiring = true;
                    UIManager.Instance.ShowPressReloadText(false);
                        if (Time.timeScale == 0 && m_playerPickup.GetCurrentWeapon().GunType == GunTypes.eLaser)
                        {
                            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                        }
                        if (Time.timeScale == 1 && m_playerPickup.GetCurrentWeapon().GunType == GunTypes.eLaser && m_playerPickup.GetCurrentWeapon().AmmoCapCurrent >0)
                        {
                            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
                        }
                        else
                        {
                            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                        }
                    }
                if (Input.GetMouseButtonUp(0) && !m_isReloading)
                {
                    m_isFiring = false;
                    transform.GetChild(2).gameObject.SetActive(false);

                }
              
                if (m_playerPickup.GetCurrentWeapon().AmmoCapCurrent <= 0 && m_isFiring)
                {
                    /*
                    m_isReloading = true;
                    m_isFiring = false;
                    Debug.Log("OUT OF AMMO");
                    m_reloadTimer = m_playerPickup.GetCurrentWeapon().ReloadTime;
                    */
                    UIManager.Instance.ShowPressReloadText(true);
                }
              
                if (m_isFiring && m_playerPickup.GetCurrentWeapon().AmmoCapCurrent > 0 && !m_isReloading)
                {
                    m_shotTimer -= Time.deltaTime;
                    m_BUGFastFireCheck -= Time.deltaTime;
                    if (m_shotTimer <= 0)
                    {
                        if (m_BUGFastFireCheck > 0)
                        {
                            Debug.Log(m_BUGFastFireCheck);
                            Achievements.Progress(Achievements.AchievementType.FastShoot, 1);
                        }

                        m_shotTimer = FireRate;
                        m_BUGFastFireCheck = m_shotTimer;
                            m_playerPickup.GetCurrentWeapon().AmmoCapCurrent--;
                            UIManager.Instance.SetAmmoCount(m_playerPickup.GetCurrentWeapon().AmmoCapCurrent);
                            Fire();
                    }
                }
                else
                {
                    m_shotTimer = 0;
                }
                Reload();
            }
        }
        }
    }
    void Reload()
    {
        if (m_playerPickup.GetCurrentWeapon().AmmoCapCurrent < AmmoCapMax)
        {
            if (Input.GetKeyDown(KeyCode.R) && !m_isReloading)
            {
                m_isFiring = false;
                m_isReloading = true;
                m_reloadTimer = m_playerPickup.GetCurrentWeapon().ReloadTime;
            }
        
        }
        if (m_isReloading)
        {
            UIManager.Instance.ShowReloadingText(true);
            UIManager.Instance.ShowPressReloadText(false);
            m_reloadTimer -= Time.deltaTime;
            if (m_reloadTimer <= 0)
            {
                m_isReloading = false;
                m_playerPickup.GetCurrentWeapon().AmmoCapCurrent = AmmoCapMax;
                UIManager.Instance.SetAmmoCount(AmmoCapMax);
                UIManager.Instance.ShowReloadingText(false);
                DropMag();
            }
            transform.GetChild(2).gameObject.SetActive(false);
            Debug.Log("Reloading " + m_isReloading);
        }

    }
    void DropMag()
    {
        if (m_playerPickup.GetCurrentWeapon().Mag != null)
        {
            GameObject mag = Instantiate(BaseMag, transform.position, Quaternion.identity);
            mag.GetComponent<SpriteRenderer>().sprite = m_playerPickup.GetCurrentWeapon().Mag;
        }
    }
    public void StopReload()
    {
        m_isReloading = false;
        m_isFiring = false;
        UIManager.Instance.ShowReloadingText(false);
        UIManager.Instance.ShowPressReloadText(false);
    }
    public void BulletSetup(GameObject _baseObject)
    {
        Vector2 direction = transform.position - (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        direction.Normalize();
        GameObject Clone = Instantiate(_baseObject, m_firePoint.position, Quaternion.identity);
        if (Clone.GetComponent<Bullet>() != null)
        {
            Clone.GetComponent<Bullet>().Damage = m_playerPickup.GetCurrentWeapon().Damage + GetComponent<PlayerManager>().GetDamage() ;
            Clone.GetComponent<Bullet>().SetAliveTime(m_playerPickup.GetCurrentWeapon().BulletAliveTime);
        }
        else if (Clone.GetComponent<Grenade>() != null)
        {
            Clone.GetComponent<Grenade>().Damage = m_playerPickup.GetCurrentWeapon().Damage + GetComponent<PlayerManager>().GetDamage();
            Clone.GetComponent<Grenade>().SetAliveTime(m_playerPickup.GetCurrentWeapon().BulletAliveTime);
            Clone.GetComponent<Grenade>().SetGrenadeRaidus(m_playerPickup.GetCurrentWeapon().AOE);
        }

        Clone.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        Clone.GetComponent<SpriteRenderer>().sprite = m_playerPickup.GetCurrentWeapon().BulletSprite;
        Clone.GetComponent<Rigidbody2D>().velocity = -direction * m_playerPickup.GetCurrentWeapon().BulletMoveSpeed;
        transform.GetChild(2).gameObject.SetActive(false);
    }
    void Fire()
    {
        Vector2 direction = transform.position - (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        direction.Normalize();
        switch (m_playerPickup.GetCurrentWeapon().GunType)
        {
            case GunTypes.eRifle:
            case GunTypes.ePistol:
                BulletSetup(BaseBullet);
                Achievements.Progress(Achievements.AchievementType.Shoot, 1);
                GetComponent<AudioSource>().PlayOneShot(Pistol1);
                break;
            case GunTypes.eLaser:
                RaycastHit2D hit = Physics2D.Raycast(m_firePoint.position, -direction);
                if (hit.collider != null)
                {
                    if (hit.transform.gameObject.activeSelf)
                    {
                        if (hit.collider.tag == "Enemy")
                        {
                            hit.collider.GetComponent<BaseEnemy>().TakeDamage(m_playerPickup.GetCurrentWeapon().Damage + GetComponent<PlayerManager>().GetDamage());

                        }
                    }
                }
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.transform.localScale = new Vector2(m_playerPickup.GetCurrentWeapon().LaserRange * 5, 1);
                transform.GetChild(2).gameObject.transform.position = m_firePointLaser.transform.position;
                Achievements.Progress(Achievements.AchievementType.UseLaser, 1);
                break;
            case GunTypes.eGrenadeLauncher:
                BulletSetup(BaseGrenade);
                Achievements.Progress(Achievements.AchievementType.Shoot, 1);
                break;
            case GunTypes.eShotgun:
                transform.GetChild(2).gameObject.SetActive(false);
                for (int i = 0; i < m_playerPickup.GetCurrentWeapon().BulletAmount; ++i)
                {
                    GameObject CloneS = Instantiate(BaseBullet, m_firePoint.position, Quaternion.identity);
                    float spreadAngle = Random.Range(-20, 20);
                    float angle = spreadAngle + (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                    Vector2 directionS = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
                    CloneS.GetComponent<Bullet>().Damage = m_playerPickup.GetCurrentWeapon().Damage;
                    CloneS.GetComponent<Bullet>().SetAliveTime(m_playerPickup.GetCurrentWeapon().BulletAliveTime);
                    CloneS.transform.rotation = Quaternion.LookRotation(Vector3.forward, directionS);
                    CloneS.GetComponent<SpriteRenderer>().sprite = m_playerPickup.GetCurrentWeapon().BulletSprite;
                    CloneS.GetComponent<Rigidbody2D>().velocity = -directionS * m_playerPickup.GetCurrentWeapon().BulletMoveSpeed;
                    Achievements.Progress(Achievements.AchievementType.Shoot, 1);
                    GetComponent<AudioSource>().PlayOneShot(ShotgunSound, 0.5f);
                }
                break;
        }

        // Spawn bullet shell
        ShellParticleSystemHandler.Instance.SpawnShell(m_firePoint.position, UtilsClass.ApplyRotationToVector(direction, Random.Range(-95f, -85f)));
        CameraShake.Instance.ShootShake();
    }

    public bool GetReloading()
    {
        return m_isReloading;
    }
    public void SetReloading(bool _state)
    {
        m_isReloading = _state;
    }

    public PlayerPickup GetPlayerPickup()
    {
        return m_playerPickup;
    }
}
