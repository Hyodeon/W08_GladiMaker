using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowClearPanel : MonoBehaviour
{
    public GameObject ClearPanel;

    public float gold, attackRate, overKillRate, LowHPRate, CriticalRate;

    private bool _isUpdateGold = false;

    private int _targetGold;
    private int _currentGold;

    [Header("적 이름")]
    [SerializeField] TMP_Text EnemyNameText;


    [SerializeField] TMP_Text Gold;
    [SerializeField] TMP_Text Bonus1;
    [SerializeField] TMP_Text Bonus2;
    [SerializeField] TMP_Text Bonus3;
    [SerializeField] TMP_Text Bonus4;
    [SerializeField] WeaponObj weapon;


    [Header("전리품")]
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text WeaponName;
    [SerializeField] TMP_Text WeaponGradeType;
    [SerializeField] TMP_Text WeaponStat;
    [SerializeField] TMP_Text WeaponInfo;

    [Header("큐브칸")]
    [SerializeField] CubePanel[] CubePanels;

    private void Start()
    {
        StartCoroutine(FindEnemyName());
    }


    IEnumerator FindEnemyName()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Enemy") != null);
        EnemyNameText.text = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ActorBase>().Name;
    }

    public void ConnectUI(float gol, float atk, float ok, float lo, float crit, WeaponObj weap)
    {
        ClearPanel.SetActive(true);
        gold = gol;
        attackRate = atk;
        overKillRate = ok;
        LowHPRate = lo;
        CriticalRate = crit;
        weapon = weap;

        _currentGold = GameManager.Instance.CurrentStageInfo.gold;
        _targetGold = Mathf.CeilToInt(_currentGold * gold);
        GameManager.Instance.EditGold(_targetGold);
        Gold.text = $"{_currentGold}";
        Bonus1.text = $"X{attackRate.ToString("N2")}";
        Bonus2.text = $"X{overKillRate.ToString("N2")}";
        Bonus3.text = $"X{LowHPRate.ToString("N2")}";
        Bonus4.text = $"X{CriticalRate.ToString("N2")}";

        var weaponItemUI = WeaponName.transform.parent.GetComponent<WeaponUI>();
        Icon.sprite = weap.GetComponent<SpriteRenderer>().sprite;
        WeaponName.text = "<color=black>" + weap.weaponStruct._name;

        WeaponGradeType.color = weaponItemUI.tierColor(weap.weapon.Tier);
        WeaponGradeType.text = $"{weap.weapon.Tier} | {weap.weapon.Type}";

        WeaponStat.text = "<color=black>Att +" + weap.weapon.WeaponDamage;
        WeaponInfo.text = "<color=orange>" + weap.weaponStruct._skillInfo;
    }

    private IEnumerator UpdateGold()
    {
        yield return null;

        while (_currentGold < _targetGold)
        {
            yield return null;
            _currentGold += (int)Mathf.Max(1, Mathf.CeilToInt((_targetGold - _currentGold) / 50));
            Gold.text = _currentGold.ToString();
        }

        _currentGold = _targetGold;
        Gold.text = _currentGold.ToString();
    }

    public void StartUpdateGold()
    {
        StartCoroutine(UpdateGold());
    }

    public void SetCubeResult() => StartCoroutine(Set_Cube_Result_Sequencely());

    IEnumerator Set_Cube_Result_Sequencely()
    {
        foreach (var x in CubePanels)
        {
            x.SetMyResult(weapon);
            yield return new WaitForSeconds(.5f);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("TrainingScene");
    }

    public void SwapWeapon()
    {
        GameManager.Instance.SwapWeapon(weapon);
    }
}
