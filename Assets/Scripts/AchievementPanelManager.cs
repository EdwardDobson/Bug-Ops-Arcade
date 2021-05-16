using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanelManager : MonoBehaviour
{
    public GameObject AchievementInfoPrefab;
    private int m_Area = 3;
    private float m_XSpacing = 400f;
    private float m_YSpacing = 80f;

    private void Start()
    {
        var achievementParent = new GameObject("AchievementParent");
        achievementParent.transform.SetParent(transform);
        for(int y = 0; y < m_Area; y++)
        {
            for(int x = 0; x < m_Area; x++)
            {
                int pos = (x * m_Area) + y;
                if(Achievements.AllAchievements.Count > pos)
                {
                    var achievement = Achievements.AllAchievements[pos];
                    var uiAchievement = Instantiate(AchievementInfoPrefab, achievementParent.transform);
                    uiAchievement.transform.position = new Vector3(-(m_XSpacing * (m_Area / 2)) + (x * m_XSpacing), -(m_YSpacing * (m_Area / 2)) + (y * m_YSpacing));
                    var script = uiAchievement.GetComponent<AchievementInfo>();
                    script.Achievement = achievement;
                }
            }
        }

        achievementParent.transform.localScale = new Vector3(1, 1, 1);
        achievementParent.transform.localPosition = new Vector3(m_XSpacing / (m_Area -1), m_YSpacing / (m_Area -1));
    }
}
