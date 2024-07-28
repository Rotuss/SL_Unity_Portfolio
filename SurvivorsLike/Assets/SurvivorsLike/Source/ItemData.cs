using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu] 커스텀 메뉴 생성하는 속성
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("Main Information")]
    public ItemType     Type;
    public Sprite       ItemIcon;
    public string       ItemName;
    [TextArea]
    public string       ItemDescription;
    public int          ItemID;

    [Header("Level Data")]
    public float[]      Damages;
    public float        BaseDamage;
    public int[]        Counts;
    public int          BaseCount;

    [Header("Weapon Data")]
    public GameObject   Projectile;
    public Sprite       Hand;
}
