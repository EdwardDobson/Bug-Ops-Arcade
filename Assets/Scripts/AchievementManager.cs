using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }
    public GameObject UIAchievementPrefab;
    public bool CanDisplay = true;

    private List<Achievements.Achievement> Queue = new List<Achievements.Achievement>();
    private GameObject displayedAchievement;

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayAchievement(Achievements.Achievement achievement)
    {
        Queue.Add(achievement);
    }

    private void Update()
    {
        if(Queue.Count > 0)
        {
            if(displayedAchievement == null && CanDisplay)
            {
                Debug.Log("Displaying achievement");
                var ui = Instantiate(UIAchievementPrefab, transform);
                displayedAchievement = ui;
                ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Queue[0].Title;
                ui.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Queue[0].Description;
                Queue.RemoveAt(0);
            }
        }

        if (!CanDisplay && displayedAchievement != null)
        {
            Destroy(displayedAchievement);
        }
    }
}
