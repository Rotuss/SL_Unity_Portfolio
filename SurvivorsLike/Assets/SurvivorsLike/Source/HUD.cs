using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, HP }
    public InfoType Type;

    Text MyText;
    Slider MySlider;

    private void Awake()
    {
        MyText = GetComponent<Text>();
        MySlider = GetComponent<Slider>();
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
        switch (Type)
        {
            case InfoType.Exp:
            {
                float CurExp = GameManager.Instance.Exp;
                float MaxExp = GameManager.Instance.NextExp[GameManager.Instance.Level];
                MySlider.value = CurExp / MaxExp;
                break;
            }
            case InfoType.Level:
                MyText.text = string.Format("Lv.{0:F0}", GameManager.Instance.Level);
                break;
            case InfoType.Kill:
                MyText.text = string.Format("{0:F0}", GameManager.Instance.KillCount);
                break;
            case InfoType.Time:
            {
                float RemainTime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
                int Minute = Mathf.FloorToInt(RemainTime / 60.0f);
                int Second = Mathf.FloorToInt(RemainTime % 60.0f);
                MyText.text = string.Format("{0:D2}:{1:D2}", Minute, Second);
                break;
            }
            case InfoType.HP:
                break;
            default:
                break;
        }
    }
}
