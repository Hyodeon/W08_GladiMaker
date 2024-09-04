using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class ComboTxt : MonoBehaviour
{
    TMPro.TMP_Text text;

    int combo = 0;

    public string textString => $"Combo {combo.ToString()}!";
    public float moveDistance = 100f;
    public float duration = 2f;


    private void Awake()
    {
        text = this.GetComponent<TMPro.TMP_Text>(); 
    }

    virtual public void Set(int bonusScore)
    {

        this.combo = bonusScore;
        text.text = textString;

        Color initialColor = text.color;
        initialColor.a = 1f;
        text.color = initialColor;

        text.rectTransform.DOMoveY(text.rectTransform.position.y + moveDistance, duration).SetEase(Ease.OutQuad);
        text.DOFade(0, duration).SetEase(Ease.OutQuad).OnComplete(() => Destroy(gameObject));
    }
}
