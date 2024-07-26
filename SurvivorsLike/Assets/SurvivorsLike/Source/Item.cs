using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public Weapon   Weapon;
    public Gear     Gear;
    public int      Level;

    Image Icon;
    Text TextLevel;

    private void Awake()
    {
        Icon = GetComponentsInChildren<Image>()[1];
        Icon.sprite = Data.ItemIcon;

        Text[] Texts = GetComponentsInChildren<Text>();
        TextLevel = Texts[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        TextLevel.text = "Lv." + Mathf.Clamp(Level + 1, 1, Data.Damages.Length);
    }

    #region Custom
    public void Onclick()
    {
        switch(Data.Type)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
            {
                // ���� ù ����
                if(0 == Level)
                {
                    GameObject NewWeapon = new GameObject();
                    Weapon = NewWeapon.AddComponent<Weapon>();
                    Weapon.Init(Data);
                }
                // ���� ������ ��
                else
                {
                    float NextDamage = Data.BaseDamage;
                    int NextCount = 0;

                    // ����� ���� ������ ���� ������ �߰�
                    NextDamage += Data.BaseDamage * Data.Damages[Level];
                    // ������ ���� ī��Ʈ ��ġ ����
                    NextCount += Data.Counts[Level];

                    Weapon.LevelUp(NextDamage, NextCount);
                }

                ++Level;

                break;
            }
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
            {
                if (0 == Level)
                {
                    GameObject NewGear = new GameObject();
                    Gear = NewGear.AddComponent<Gear>();
                    Gear.Init(Data);
                }
                else
                {
                    float NextRate = Data.Damages[Level];

                    Gear.LevelUp(NextRate);
                }

                ++Level;

                break;
            }
            case ItemData.ItemType.Heal:
                GameManager.Instance.HP = GameManager.Instance.MaxHP;
                break;
        }

        if(Level == Data.Damages.Length) GetComponent<Button>().interactable = false;
    }
    #endregion
}
