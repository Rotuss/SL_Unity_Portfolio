using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] Titles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Custom
    public void Over()
    {
        Titles[0].SetActive(true);
    }

    public void Clear()
    {
        Titles[1].SetActive(true);
    }
    #endregion
}
