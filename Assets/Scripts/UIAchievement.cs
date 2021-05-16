using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAchievement : MonoBehaviour
{
    private List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    private RawImage image;
    private bool fadeIn = true;
    private float aliveTime = 3f;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<RawImage>();
        texts.Add(transform.GetChild(1).GetComponent<TextMeshProUGUI>());
        texts.Add(transform.GetChild(2).GetComponent<TextMeshProUGUI>());
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        foreach(var text in texts)
        {
            text.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }
    private void Update()
    {
        if(aliveTime <= 0)
        {
            var alpha = image.color.a;
            alpha -= Time.deltaTime * 3f;

            if (alpha <= 0)
                Destroy(gameObject);

            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            foreach (var text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            }
        }
        else if (fadeIn)
        {
            var alpha = image.color.a;
            alpha += Time.deltaTime * 1f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            foreach(var text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            }
        }
        aliveTime -= Time.deltaTime;
    }
}
