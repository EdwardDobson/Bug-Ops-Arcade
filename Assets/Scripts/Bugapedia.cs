using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bugapedia : MonoBehaviour
{
    [System.Serializable]
    public struct Bug
    {
       public string Name;
       public string Description;
       public Image BugImage;
    }

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public Image BugImage;
    int m_index;
    public List<Bug> bugs = new List<Bug>();

    void OnEnable()
    {
        ChangeBug();
    }
    public void ChangeBug()
    {
        Name.text = bugs[m_index].Name;
        Description.text = bugs[m_index].Description;
        BugImage.sprite = bugs[m_index].BugImage.sprite;
    }
    public void AddIndex()
    {
        m_index++;
        if (m_index >= bugs.Count)
        {
            m_index = 0;
        }  
        ChangeBug();
    }
    public void MinusIndex()
    {
        m_index--;
        if(m_index < 0)
        {
            m_index = bugs.Count-1;
        }
        ChangeBug();
    }
}
