using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    List<GameObject> slots;
    List<GameObject> fullSlots;
    public float chancePerFrame = 0.05f;
    public GameObject estate;
    public static float Money = 500;
    public static GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<GameObject>(GameObject.FindGameObjectsWithTag("Slot"));
        fullSlots = new List<GameObject>();
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        CreateEstates();
        GameObject.FindGameObjectWithTag("TabelaMoney").GetComponent<TMPro.TextMeshProUGUI>().text = "Money" + Money.ToString("0.0");
    }

    void CreateEstates()
    {
        float rollForCreate = Random.Range(0f, 1f);
        if (rollForCreate > 1 - chancePerFrame && slots.Count > 0)
        {
            int estateNumber = Random.Range(0, slots.Count);
            GameObject instantiated = GameObject.Instantiate(estate, slots[estateNumber].transform);
            instantiated.transform.localPosition = Vector3.zero;

            Estate _estate = instantiated.GetComponent<Estate>();
            _estate.Price = Random.Range(50, 150);

            fullSlots.Add(slots[estateNumber]);
            slots.RemoveAt(estateNumber);
        }
    }

    public void ReturnEmpty(GameObject Slot)
    {
        slots.Add(Slot);
        fullSlots.Remove(Slot);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
