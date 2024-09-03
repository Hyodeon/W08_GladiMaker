using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float max_HP;
    public float current_HP;

    public float speed;
    public float speedEffect;

    public Image HP;
    public Image HP_Effect;

    [SerializeField] float damage = 10;

    private void Start()
    {
        current_HP = max_HP;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage(damage);
        if (Input.GetKeyDown(KeyCode.LeftShift)) current_HP = max_HP;

        float target = Mathf.Lerp(HP.fillAmount, current_HP / max_HP, speed * Time.deltaTime);
        float targetEffect = Mathf.Lerp(HP_Effect.fillAmount, current_HP / max_HP, speedEffect * Time.deltaTime);

        HP.fillAmount = target;
        HP_Effect.fillAmount = targetEffect;
    }

    public void TakeDamage(float damage)
    {
        current_HP -= damage;
    }

}
