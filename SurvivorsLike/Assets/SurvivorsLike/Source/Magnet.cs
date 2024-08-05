using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    Collider2D Col;

    private void Awake()
    {
        Col = GetComponent<Collider2D>();
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
        List<GameObject> Coins = GameManager.Instance.Pool.PoolsGet(4);
        foreach (GameObject Coin in Coins)
        {
            Coin.GetComponent<Coin>().OnMagnet();
        }
        gameObject.SetActive(false);
    }
}
