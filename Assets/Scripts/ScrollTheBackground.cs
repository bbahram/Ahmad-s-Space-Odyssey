using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTheBackground : MonoBehaviour
{
    [SerializeField] float speedController = 0f;
    int tempForScroll;
    Material myMaterial;
    SpaceShip spaceShipPosition;
    // Start is called before the first frame update
    void Start()
    {
        tempForScroll = 0;
        myMaterial = GetComponent<Renderer>().material;
        spaceShipPosition = FindObjectOfType<SpaceShip>();
    }

    // Update is called once per frame
    void Update()
    { // scrolls the bakcground in the way of the moiving object's direction
        if (spaceShipPosition)
            myMaterial.mainTextureOffset += (new Vector2(spaceShipPosition.ReturnsPlaneRigidBody().velocity.x, spaceShipPosition.ReturnsPlaneRigidBody().velocity.y)) * Time.deltaTime * spaceShipPosition.GetSpeed() / speedController;
        else
        {
            myMaterial.mainTextureOffset += (new Vector2(5, 5)) * Time.deltaTime/speedController;
        }
    }
}
