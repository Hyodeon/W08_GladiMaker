using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class ComboTxt : MonoBehaviour
{
    TMPro.TMP_Text text;

    public float moveDistance = 100f;
    public float duration = 2f;


    private void Awake()
    {
        text = this.GetComponent<TMPro.TMP_Text>(); 
    }
    
    virtual public void Set(string textValue,Color32 color)
    {

        text.text = textValue;

        Color initialColor = color;
        initialColor.a = 1f;
        text.color = initialColor;

        text.rectTransform.DOMoveY(text.rectTransform.position.y + moveDistance, duration).SetEase(Ease.OutQuad);
        text.DOFade(0, duration).SetEase(Ease.InQuad).OnComplete(() => Destroy(gameObject));
    }
}
