using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGameStatPanel : MonoBehaviour
{
    [SerializeField] ActorBase Target;

    [SerializeField] bool isEnemy;

    [SerializeField] Image Avatar;
    [SerializeField] TMP_Text NameText;
    [SerializeField] TMP_Text AttackStatText;
    [SerializeField] TMP_Text DefenceText;
    [SerializeField] TMP_Text AccuracyText;
    [SerializeField] TMP_Text EvadeText;

    [SerializeField] Image Enemy_Avatar;
    [SerializeField] TMP_Text Enemy_NameText;
    [SerializeField] TMP_Text Enemy_AttackStatText;
    [SerializeField] TMP_Text Enemy_DefenceText;
    [SerializeField] TMP_Text Enemy_AccuracyText;
    [SerializeField] TMP_Text Enemy_EvadeText;

    public GameObject PlayerDie;
    public GameObject EnemyDie;

    private void Start()
    {
        StartCoroutine(GetStats());
    }

    IEnumerator GetStats()
    {
        yield return new WaitUntil(() =>GameObject.FindGameObjectWithTag("Player") != null);
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorBase>();
        SetValuesToPanel();
    }

    IEnumerator GetEnemyStats()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Enemy") != null );
        Target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ActorBase>();
        SetValuesToPanel();
    }

    void SetValuesToPanel()
    {
        if (isEnemy)
        {
            Enemy_Avatar.sprite = Target.EnemyAvatar;
            Enemy_NameText.text = Target.Name;
            Enemy_AttackStatText.text = $"공격력\n{Target.Status.Attack}";
            Enemy_DefenceText.text = $"방어력\n{Target.Status.Guard}";
            Enemy_AccuracyText.text = $"명중\n{Target.Status.Accuracy}";
            Enemy_EvadeText.text = $"회피\n{Target.Status.Evade}";
            return;
        }

        Avatar.sprite = Target.EnemyAvatar;
        NameText.text = Target.Name;
        AttackStatText.text = $"공격력\n{Target.Status.Attack}";
        DefenceText.text = $"방어력\n{Target.Status.Guard}";
        AccuracyText.text = $"명중\n{Target.Status.Accuracy}";
        EvadeText.text = $"회피\n{Target.Status.Evade}";

        isEnemy = true;
        StartCoroutine(GetEnemyStats());
    }
}
