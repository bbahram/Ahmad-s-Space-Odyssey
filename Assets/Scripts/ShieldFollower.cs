using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFollower : MonoBehaviour
{
    SpaceShip toFollowTheShip;
    // Start is called before the first frame update
    void Start()
    {
        toFollowTheShip = FindObjectOfType<SpaceShip>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(toFollowTheShip.transform.position.x, toFollowTheShip.transform.position.y);
    }
}
