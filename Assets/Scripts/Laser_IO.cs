using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_IO : MonoBehaviour
{
    SpaceShip forScreenMirroring;
    // Start is called before the first frame update
    void Start()
    {
        forScreenMirroring=FindObjectOfType<SpaceShip>();
    }

    // Update is called once per frame
    void Update()
    {
        forScreenMirroring.ScreenMirroring(gameObject);   
    }
}
