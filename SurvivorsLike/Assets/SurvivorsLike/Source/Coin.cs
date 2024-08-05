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
    Rigidbody2D Rigid;
    Rigidbody2D Target;
    CoinData Type;
    bool IsOnMagnet;

    private void Awake()
    {
        Col = GetComponent<Collider2D>();
        Spriter = GetComponent<SpriteRenderer>();
        Rigid = GetComponent<Rigidbody2D>();
        Target = GameManager.Instance.MainPlayer.GetComponent<Rigidbody2D>();
        IsOnMagnet = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        IsOnMagnet = false;         // OnTriggerEnter2D에도 해뒀지만 혹시 모를 미연의 방지 위함
        Col.enabled = true;
        Rigid.simulated = true;
    }

    private void FixedUpdate()
    {
        if (true == GameManager.Instance.IsStop) return;

        if (true == IsOnMagnet)
        {
            Vector2 DirVec = Target.position - Rigid.position;
            Vector2 NextVec = DirVec.normalized * 50.0f * Time.fixedDeltaTime;
            Rigid.MovePosition(Rigid.position + NextVec);
            Rigid.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (false == collision.CompareTag("Player")) return;

        IsOnMagnet = false;
        Col.enabled = false;
        Rigid.simulated = false; 
        GameManager.Instance.GetExp(Type.CoinValue);
        gameObject.SetActive(false);
    }

    #region Custom
    public void Init(int InType)
    {
        Spriter.sprite = Coins[InType].CoinSprite;
        Type = Coins[InType];
    }

    public void OnMagnet()
    {
        IsOnMagnet = true;
    }
    #endregion
}
