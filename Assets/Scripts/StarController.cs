using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField] int starScore;
    [SerializeField] GameObject goldenStar;
    SpaceShip starCreater;
    // Start is called before the first frame update
    void Start()
    {
        starCreater=FindObjectOfType<SpaceShip>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SpaceShip" )
        {
            starCreater.PowerUpMaker(goldenStar);
            starCreater.PowerUpMaker(goldenStar);
            starCreater.ScoreRiase(starScore);
            Destroy(gameObject);
        }
    }
}
