using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType Type;
    public float Rate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Custom
    public void Init(ItemData InData)
    {
        // 기본 세팅
        name = "Gear " + InData.ItemID;
        transform.parent = GameManager.Instance.MainPlayer.transform;
        transform.localPosition = Vector3.zero;

        // 프로퍼티 세팅
        Type = InData.Type;
        Rate = InData.Damages[0];

        ApplyGear();
    }

    public void LevelUp(float InRate)
    {
        Rate = InRate;

        ApplyGear();
    }

    void ApplyGear()
    {
        switch(Type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    // 장갑
    void RateUp()
    {
        Weapon[] Weapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach(Weapon Weapon in Weapons)
        {
            switch(Weapon.ID)
            {
                case 0:
                    Weapon.Speed = -150.0f + (-150.0f * Rate);
                    break;
                case 1:
                    Weapon.Speed = 0.5f * (1.0f - Rate);
                    break;
            }
        }
    }

    // 신발
    void SpeedUp()
    {
        float Speed = GameManager.Instance.MainPlayer.DefaultSpeed;
        GameManager.Instance.MainPlayer.Speed = Speed + (Speed * Rate);
    }
    #endregion
}
