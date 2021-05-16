using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    public static Sprite PlayerSprite;

    public RawImage CrosshairImage;
    public RawImage PlayerImage;
    private List<Texture2D> m_Crosshairs = new List<Texture2D>();
    private List<Texture2D> m_Players = new List<Texture2D>();
    private int m_SelectedCrosshair;
    private int m_SelectedPlayer;

    private void Start()
    {
        m_SelectedCrosshair = PlayerPrefs.GetInt("CrosshairInt", 0);
        m_Crosshairs = Resources.LoadAll("Crosshair", typeof(Texture2D)).Select(x => x as Texture2D).ToList();
        SetCrosshair();

        m_SelectedPlayer = PlayerPrefs.GetInt("PlayerInt", 0);
        m_Players = Resources.LoadAll("Player/Round", typeof(Texture2D)).Select(x => x as Texture2D).ToList();
        m_Players.AddRange(Resources.LoadAll("Player/Square", typeof(Texture2D)).Select(x => x as Texture2D));
        SetPlayer();
    }

    public void NextCrosshair()
    {
        m_SelectedCrosshair++;
        if(m_SelectedCrosshair >= m_Crosshairs.Count)
        {
            m_SelectedCrosshair = 0;
        }
        SetCrosshair();
    }

    public void PreviousCrosshair()
    {
        m_SelectedCrosshair--;
        if(m_SelectedCrosshair < 0)
        {
            m_SelectedCrosshair = m_Crosshairs.Count - 1;
        }
        SetCrosshair();
    }

    public void SetCrosshair()
    {
        PlayerPrefs.SetInt("CrosshairInt", m_SelectedCrosshair);
        CrosshairImage.texture = m_Crosshairs[m_SelectedCrosshair];
        Cursor.SetCursor(m_Crosshairs[m_SelectedCrosshair], new Vector2(m_Crosshairs[m_SelectedCrosshair].width / 2, m_Crosshairs[m_SelectedCrosshair].height / 2), CursorMode.Auto);
    }

    public void NextPlayer()
    {
        m_SelectedPlayer++;
        if(m_SelectedPlayer < 0)
        {
            m_SelectedPlayer = m_Players.Count - 1;
        }
        SetPlayer();
    }

    public void PreviousPlayer()
    {
        m_SelectedPlayer--;
        if(m_SelectedPlayer >= m_Players.Count)
        {
            m_SelectedPlayer = 0;
        }
        SetPlayer();
    }

    public void SetPlayer()
    {
        PlayerPrefs.SetInt("PlayerInt", m_SelectedPlayer);
        PlayerSprite = Sprite.Create(m_Players[m_SelectedPlayer], new Rect(0, 0, m_Players[m_SelectedPlayer].width, m_Players[m_SelectedPlayer].height), new Vector2(.5f, .5f), 100);
        PlayerImage.texture = m_Players[m_SelectedPlayer];
        PlayerImage.GetComponent<RectTransform>().sizeDelta = new Vector2(m_Players[m_SelectedPlayer].width, m_Players[m_SelectedPlayer].height);
    }
}
