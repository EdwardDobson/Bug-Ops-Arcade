using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets m_Instance;

    public static GameAssets Instance
    {
        get {
            if (m_Instance == null) m_Instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return m_Instance;
        }
    }

    public GameObject DamagePopupPrefab;
}
