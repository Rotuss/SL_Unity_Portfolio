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

    private void LateUpdate()
    {
        foreach (Achive Achive in Achives)
        {
            CheckAchive(Achive);
        }
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

    void CheckAchive(Achive InAchive)
    {
        bool IsAchive = false;

        switch(InAchive)
        {
            case Achive.UnlockBarley:
                IsAchive = 10 <= GameManager.Instance.KillCount;
                break;
            case Achive.UnlockPotato:
                IsAchive = GameManager.Instance.MaxGameTime <= GameManager.Instance.GameTime;
                break;
        }

        // 업적 달성을 했고 해당 업적이 잠겨 있다는 경우
        if (true == IsAchive && 0 == PlayerPrefs.GetInt(InAchive.ToString()))
        {
            // 해금
            PlayerPrefs.SetInt(InAchive.ToString(), 1);
        }
    }
    #endregion
}
