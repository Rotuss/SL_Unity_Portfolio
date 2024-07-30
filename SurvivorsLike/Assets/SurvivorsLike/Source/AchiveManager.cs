using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] LockCharacters;
    public GameObject[] UnlockCharacters;
    public GameObject   UINotice;

    enum Achive { UnlockBarley, UnlockPotato }
    Achive[] Achives;
    WaitForSecondsRealtime Wait;

    private void Awake()
    {
        Achives = (Achive[])Enum.GetValues(typeof(Achive));
        Wait = new WaitForSecondsRealtime(5.0f);

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
            LockCharacters[i].SetActive(true != IsUnlock);      // ������� ������ Ȱ��ȭ
            UnlockCharacters[i].SetActive(true == IsUnlock);    // ������� ������ ��Ȱ��ȭ
        }
    }

    void CheckAchive(Achive InAchive)
    {
        bool IsAchive = false;

        switch(InAchive)
        {
            case Achive.UnlockBarley:
                if (false == GameManager.Instance.IsStop)
                {
                    IsAchive = 10 <= GameManager.Instance.KillCount;
                }
                break;
            case Achive.UnlockPotato:
                IsAchive = GameManager.Instance.MaxGameTime <= GameManager.Instance.GameTime;
                break;
        }

        // ���� �޼��� �߰� �ش� ������ ��� �ִٴ� ���
        if (true == IsAchive && 0 == PlayerPrefs.GetInt(InAchive.ToString()))
        {
            // �ر�
            PlayerPrefs.SetInt(InAchive.ToString(), 1);

            // �ر� �˸�
            for(int i = 0; i < UINotice.transform.childCount; ++i)
            {
                bool IsActive = i == (int)InAchive;
                UINotice.transform.GetChild(i).gameObject.SetActive(IsActive);
            }
            //UINotice.transform.GetChild((int)InAchive).gameObject.SetActive(IsAchive);
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        UINotice.SetActive(true);

        yield return Wait;

        UINotice.SetActive(false);
    }
    #endregion
}
