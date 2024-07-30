using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] LockCharacters;
    public GameObject[] UnlockCharacters;

    enum Achive { UnlockBarley, UnlockPotato }
    Achive[] Achives;

    private void Awake()
    {
        Achives = (Achive[])Enum.GetValues(typeof(Achive));

        if (false == PlayerPrefs.HasKey("AchiveData")) Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        UnlockCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Custom
    void Init()
    {
        PlayerPrefs.SetInt("AchiveData", 1);
        foreach (Achive Achive in Achives)
        {
            PlayerPrefs.SetInt(Achive.ToString(), 0);
        }
    }

    void UnlockCharacter()
    {
        for(int i = 0; i < LockCharacters.Length; ++i)
        {
            string AchiveName = Achives[i].ToString();
            bool IsUnlock = (1 == PlayerPrefs.GetInt(AchiveName));
            LockCharacters[i].SetActive(true != IsUnlock);      // 잠겨있지 않으면 활성화
            UnlockCharacters[i].SetActive(true == IsUnlock);    // 잠겨있지 않으면 비활성화
        }
    }
    #endregion
}
