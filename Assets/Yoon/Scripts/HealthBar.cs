using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public GameObject Enemy;

    public float max_HP;
    public float current_HP;

    public float speed;
    public float speedEffect;

    public Image HP;
    public Image HP_Effect;

    public TMP_Text HP_Amount_Text;

    public void Initialize()
    {
        current_HP = max_HP;
        GameManager.Instance.PlayerStatus.Hp = max_HP;
    }


        private void Update()
    {
        float target = Mathf.Lerp(HP.fillAmount, current_HP / max_HP, speed * Time.deltaTime);
        float targetEffect = Mathf.Lerp(HP_Effect.fillAmount, current_HP / max_HP, speedEffect * Time.deltaTime);

        HP.fillAmount = target;
        HP_Effect.fillAmount = targetEffect;
        HP_Amount_Text.text = $"{(target * max_HP).ToString("N0")} / {max_HP.ToString("N0")}";
    }

}
