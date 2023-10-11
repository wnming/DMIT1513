using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    [SerializeField] GameObject trasureOpen;
    [SerializeField] GameObject trasureClose;

    [SerializeField] TextMeshProUGUI itemText;

    public bool isPlayerfoundTreasure;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerfoundTreasure = false;
        trasureOpen.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trasureClose.SetActive(false);
            trasureOpen.SetActive(true);
            isPlayerfoundTreasure = true;
            itemText.text += "Treasure|";
        }
    }
}
