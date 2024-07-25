using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, HP}
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
                break;
            case InfoType.Level:
                break;
            case InfoType.Kill:
                break;
            case InfoType.Time:
                break;
            case InfoType.HP:
                break;
            default:
                break;
        }
    }
}
