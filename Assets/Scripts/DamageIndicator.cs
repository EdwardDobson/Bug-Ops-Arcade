using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public static DamageIndicator Create(Vector3 pos, float dmg)
    {
        var popup = Instantiate(GameAssets.Instance.DamagePopupPrefab, pos, Quaternion.identity);
        var script = popup.GetComponent<DamageIndicator>();
        script.Setup(dmg);
        return script;
    }
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    public void Setup(float dmgAmount)
    {
        textMesh.SetText(dmgAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = 1f;
    }

    private void Update()
    {
        float moveSpeed = 1.5f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer <= 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
