using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [System.Serializable]
    public struct CoinData
    {
        public Sprite CoinSprite;
        public int CoinValue;
    }

    public CoinData[] Coins;

    Collider2D Col;
    SpriteRenderer Spriter;
    CoinData Type;

    private void Awake()
    {
        Col = GetComponent<Collider2D>();
        Spriter = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Col.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (false == collision.CompareTag("Player")) return;

        Col.enabled = false;
        GameManager.Instance.GetExp(Type.CoinValue);
        gameObject.SetActive(false);
    }

    #region Custom
    public void Init(int InType)
    {
        Spriter.sprite = Coins[InType].CoinSprite;
        Type = Coins[InType];
    }
    #endregion
}
