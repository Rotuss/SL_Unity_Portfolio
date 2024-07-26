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
                // 무기 첫 생성
                if(0 == Level)
                {
                    GameObject NewWeapon = new GameObject();
                    Weapon = NewWeapon.AddComponent<Weapon>();
                    Weapon.Init(Data);
                }
                // 무기 존재할 떄
                else
                {
                    float NextDamage = Data.BaseDamage;
                    int NextCount = 0;

                    // 대미지 현재 레벨에 따른 비율로 추가
                    NextDamage += Data.BaseDamage * Data.Damages[Level];
                    // 레벨에 따른 카운트 수치 누적
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
