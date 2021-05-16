using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementInfo : MonoBehaviour
{
    public Achievements.Achievement Achievement;

    public void Start()
    {
        // Image colour set
        var image = transform.GetChild(0).GetComponent<RawImage>();
        if (Achievement.Achieved)
        {
            image.color = new Color(0, 100, 0, 255);
        }

        // Title set
        var text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text.text = Achievement.Title;
    }
}
