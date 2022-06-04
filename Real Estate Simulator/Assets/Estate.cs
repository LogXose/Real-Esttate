using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Estate : MonoBehaviour
{
    public int Price;
    public bool Purchased = false;
    [SerializeField] Material materialPurchased;
    TextMeshPro priceLabel;

    private void Start()
    {
        priceLabel = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        priceLabel.text = Price.ToString();
    }
    public float upper = 0.0005f;
    public float downer = 0.0005f;

    private void Update()
    {
        MarketVolatility();
        Demolish();
    }
    void MarketVolatility()
    {
        float chance = Random.Range(0f, 1f);
        if (chance > 1 - upper)
        {
            int size = Random.Range(10, 50);
            priceLabel.color = Color.green;
            Price += size;
            priceLabel.text = Price.ToString();
        } else if(chance < downer )
        {
            int size = Random.Range(10, 50);
            priceLabel.color = Color.red;
            Price -= size;
            if (Price < 0) Price = 0;
            priceLabel.text = Price.ToString();
        }
    }

    void OnMouseDown()
    {
        if (!Purchased && GameManager.Money >= Price)
        {
            GameManager.Money -= Price;
            gameObject.GetComponent<MeshRenderer>().material = materialPurchased;
            Purchased = true;
        }
        else if(Purchased)
        {
            GameManager.Money += Price;
            GameManager.gameManager.ReturnEmpty(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    void Demolish()
    {
        float chance = Random.Range(0f, 1f);
        if(chance < downer / 20f && !Purchased)
        {
            GameManager.gameManager.ReturnEmpty(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
    
}
