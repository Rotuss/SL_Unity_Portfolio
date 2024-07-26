using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public Weapon   Weapon;
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
                break;
            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
        }

        ++Level;

        if(Level == Data.Damages.Length) GetComponent<Button>().interactable = false;
    }
    #endregion
}
