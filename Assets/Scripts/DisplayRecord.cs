using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayRecord : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        score.text = SpaceShip.starScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
