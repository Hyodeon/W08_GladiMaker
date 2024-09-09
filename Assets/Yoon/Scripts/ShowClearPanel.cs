using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class ShowClearPanel : MonoBehaviour
{
    public GameObject ClearPanel;
    public InGameStatPanel StatManage;

    public float gold, attackRate, overKillRate, LowHPRate, CriticalRate;

    private bool _isUpdateGold = false;

    private float _targetGold;
    private float _currentGold;

    [Header("�� �̸�")]
    [SerializeField] TMP_Text EnemyNameText;


    [SerializeField] TMP_Text Gold;
    [SerializeField] TMP_Text Bonus1;
    [SerializeField] TMP_Text Bonus2;
    [SerializeField] TMP_Text Bonus3;
    [SerializeField] TMP_Text Bonus4;
    [SerializeField] WeaponObj weapon;


    [Header("����ǰ")]
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text WeaponName;
    [SerializeField] TMP_Text WeaponGradeType;
    [SerializeField] TMP_Text WeaponTypeText;
    [SerializeField] TMP_Text WeaponStat;
    [SerializeField] TMP_Text WeaponInfo;
    [SerializeField] TMP_Text WeaponSkillInfo;

    [Header("ť��ĭ")]
    [SerializeField] CubePanel[] CubePanels;

    [Header("Ŭ����� �г�")]
    public GameObject ClearEffectPanel;


    GameObject Enemy;

    [Header("�������������")]
    public Utils.Weapon drop_Weapon;
    public WeaponStruct drop_weaponStruct;

    [Header("����ȭ��")]
    public SpriteRenderer Flame, Flame2, RedPanel;


    private void Start()
    {
        StartCoroutine(FindEnemyName());
    }

    private void Update()
    {
        if(BattleManager.Instance.IsFighting) SetFlameColor();
    }

    void SetFlameColor()
    {
        if (Time.timeScale <= 2) return;
        Flame.color = new Color(Flame.color.r, Flame.color.g, Flame.color.b, Time.timeScale / 10);
        Flame2.color = new Color(Flame.color.r, Flame.color.g, Flame.color.b, Time.timeScale / 10);
        RedPanel.color = new Color(Flame.color.r, Flame.color.g, Flame.color.b, Time.timeScale / 10);
    }

    public void HideAllFlames()
    {
        Flame.gameObject.SetActive(false); Flame2.gameObject.SetActive(false);
        RedPanel.gameObject.SetActive(false);

    }


    IEnumerator FindEnemyName()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Enemy") != null);
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        EnemyNameText.text = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ActorBase>().Name;

        drop_Weapon = Enemy.GetComponent<ActorBase>().DropWeapon.GetComponent<WeaponObj>().weapon;
        drop_weaponStruct = Enemy.GetComponent<ActorBase>().DropWeapon.GetComponent<WeaponObj>().weaponStruct;
    }

    public void ConnectUI(float gol, float atk, float ok, float lo, float crit, WeaponObj weap)
    {
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
        WeaponGradeType.text = $"[{ConvertWeaponGrade(weap.weapon.Tier)}]";
        WeaponTypeText.color = Color.white;
        WeaponTypeText.text = $"{ConvertWeaponType(weap.weapon.Type)}";


        WeaponStat.text = "<color=white>���� " + weap.weapon.WeaponDamage + "%";
        WeaponInfo.text = "<color=white>" + weap.weaponStruct._itemInfo;
        WeaponSkillInfo.text = "<color=white><��ų ����>\n" + weap.weaponStruct._skillInfo;

        ClearPanel.SetActive(true);
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
        GameObject.Find("Canvas").GetComponent<ShowClearPanel>().drop_weaponStruct._cubeOption.Clear();
        foreach (var x in CubePanels)
        {
            x.SetMyResult(weapon);
            yield return new WaitForSeconds(.075f);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("TrainingScene");
    }

    public void SwapWeapon()
    {
        weapon.weaponStruct = drop_weaponStruct;
        weapon.weapon = drop_Weapon;
        GameManager.Instance.SwapWeapon(weapon);
    }

    public void ShowDieImage(bool isPlayerDie)
    {
        if (isPlayerDie) ClearEffectPanel.GetComponent<Image>().color = Color.red;
        ClearEffectPanel.SetActive(true);

        if (isPlayerDie) StatManage.PlayerDie.SetActive(true);
        else StatManage.EnemyDie.SetActive(true);
    }

    public string ConvertWeaponGrade(WeaponTier tier)
    {
        return tier switch
        {
            WeaponTier.Boss => "����",
            WeaponTier.Legendary => "����",
            WeaponTier.Epic => "����",
            WeaponTier.Rare => "����",
            WeaponTier.Common => "�Ϲ�",
            _ => "���� �̰�"
        };
    }

    public string ConvertWeaponType(WeaponAttackType type)
    {
        return type switch
        {
            WeaponAttackType.Strike => "Ÿ��",
            WeaponAttackType.Slash => "����",
            WeaponAttackType.Penetration => "����",
            WeaponAttackType.Ranged => "��ô",
            _ => "���� �̰�"
        };
    }
}
