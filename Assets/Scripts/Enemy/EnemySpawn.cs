using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WowieJam2.Enemy;
public class EnemySpawn : MonoBehaviour
{
    public GameObject[] SpawnLocations;
    public List<GameObject> EnemyTypes = new List<GameObject>();
    [SerializeField]
    int m_totalSpawned;
    public int AmountToSpawn;
    bool m_stopSpawning;
    // Start is called before the first frame update
    void Start()
    {
        SpawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoints");
     
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_stopSpawning)
        {
            if (m_totalSpawned > GameManager.Instance.GetEnemyTotal())
            {

                CancelInvoke("SpawnEnemyRandom");
                m_stopSpawning = true;
            }
        }

        if (m_totalSpawned < GameManager.Instance.GetEnemyTotal())
        {
            Invoke("SpawnEnemyRandom", 0);
        }
        if (m_stopSpawning)
        {
            if (m_totalSpawned >= GameManager.Instance.GetEnemyTotal())
            {
                m_totalSpawned = 0;
                m_stopSpawning = false;
            }
        }
           
    }
    void SpawnEnemyRandom()
    {
        if(SpawnLocations.Length > 0)
        {
            int randomPos = Random.Range(0, SpawnLocations.Length);
            int randomEnemyType = Random.Range(0, EnemyTypes.Count);
            GameObject copy = Instantiate(EnemyTypes[randomEnemyType], SpawnLocations[randomPos].transform);
            m_totalSpawned++;
        }
      
    
    }
    void SpawnEnemy(int _objectID, int _spawnPoint)
    {
        for (int i = 0; i < AmountToSpawn; ++i)
        {
            m_totalSpawned++;
            GameObject copy = Instantiate(EnemyTypes[_objectID], SpawnLocations[_spawnPoint].transform);
        }
    }
    public void TotalReset()
    {
        m_totalSpawned = 0;
    }
}
