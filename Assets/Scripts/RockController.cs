using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    [SerializeField] int fuelAmount;
    [SerializeField] GameObject[] powerUpPill;
    [SerializeField] GameObject clear;
    SpaceShip forScreenMirroring;
    int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        forScreenMirroring = FindObjectOfType<SpaceShip>(); 
    }

    // Update is called once per frame
    void Update()
    {
        forScreenMirroring.ScreenMirroring(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser" )
        {
            //GameObject powerUpTemp;
            forScreenMirroring.RockMaker();
            forScreenMirroring.RockMaker();
            forScreenMirroring.FuelRiase(fuelAmount);
            /* if (forScreenMirroring.PowerUpControllerRaise() % 11 == 0)
             {
                 powerUpTemp = Instantiate(powerUpPill[1], transform.position, powerUpPill[1].transform.rotation);
             }*/
            int temp = forScreenMirroring.PowerUpControllerRaise();
            if (temp % 30 == 0)
                forScreenMirroring.StartCoroutine("ShieldMaker");
            if (temp % 15 == 0)
            {
                forScreenMirroring.PowerUpMaker(powerUpPill[1]);
            }
            if(temp%40==0 && forScreenMirroring.Check())
            {
                forScreenMirroring.PowerUpMaker(clear);
                forScreenMirroring.SetCheck();
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.tag == "SpaceShip")
        {
            //Destroy(collision.gameObject);
            SceneChanger toChangeTheScene;
            toChangeTheScene = FindObjectOfType<SceneChanger>();
            toChangeTheScene.NextScene();
            //Destroy(gameObject);
        }
    }
}
