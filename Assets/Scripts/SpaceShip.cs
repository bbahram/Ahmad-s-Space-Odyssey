using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] float movePlane = 0f;
    [SerializeField] float moveLaser = 0f;
    [SerializeField] float rotation = 0f;
    [SerializeField] GameObject laserTag;
    [SerializeField] GameObject redPowerUpLaser;
    [SerializeField] GameObject greenPowerUpLaser;
    [SerializeField] float forSecendsLaser = 0f;
    //  [SerializeField] float forSecendsRock = 0f;
    [SerializeField] GameObject[] spaceRock;
    [SerializeField] GameObject goldenStar;
    [SerializeField] GameObject redPill;
    [SerializeField] float moveRock = 0f;
    [SerializeField] TextMeshProUGUI textForFuel;
    [SerializeField] TextMeshProUGUI textForScore;
    [SerializeField] TextMeshProUGUI textForLaser;
    [SerializeField] TextMeshProUGUI textForTime;
    [SerializeField] GameObject sheild;
    Rigidbody2D toMoveThePlane;
    AudioSource getSound;
    [SerializeField] AudioClip[] sounds;
    Camera cam;
    int fuelToMove;
    int powerUpController = 0;
    int redLaser = 0, greenLaser = 0;
    static public int starScore;
    float height, width;
    float minX = 0f, maxX = 0f;
    float minY = 0f, maxY = 0f;
    bool checkToDestroy = false;
    bool checkToCreate = true;
    //long temp;
    // Start is called before the first frame update
    void Start()
    {
        toMoveThePlane = GetComponent<Rigidbody2D>();
        getSound = GetComponent<AudioSource>();
        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        float width = height * cam.aspect;
        minY = -height;
        maxY = height;
        minX = -width;
        maxX = width;
        fuelToMove = 1000;
        starScore = 0;
        ShowLaser();
        ShowScoreText();
        ShowFuelText();
        RockMaker();
        PowerUpMaker(redPill);
        PowerUpMaker(goldenStar);
        StartCoroutine(ShieldMaker());
        //temp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        //DestroyAll();
    }

    // Update is called once per frame
    void Update()
    {
        PlaneMovement();
        PlaneRotation();
        if (redLaser != 0)
        {
            StartCoroutine(LaserShooter(redPowerUpLaser));
        }
        else
            StartCoroutine(LaserShooter(laserTag));
        if (checkToDestroy)
        {
            ScreenCleaner();
        }
        //Debug.Log(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - temp);
        //if(powerUpController%10==0 && powerUpController!=0)
        //   StartCoroutine(ShieldMaker());
        //addForceForScreen();
        //RockMaker();
        //Debug.Log(Screen.width);
    }

    private void ScreenCleaner()
    {
        DestroyAll();
        checkToDestroy = false;
        RockMaker();
        PowerUpMaker(goldenStar);
        PowerUpMaker(redPill);
    }

    private IEnumerator LaserShooter(GameObject laser) // to shoot laser and destroy it after some seconds
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (redLaser != 0)
                redLaser--;
            ShowLaser();
            GameObject laserTemp = Instantiate(laser, new Vector2(transform.position.x + transform.up.x, transform.position.y + transform.up.y), transform.rotation) as GameObject;
            laserTemp.GetComponent<Rigidbody2D>().AddForce(transform.up * moveLaser);
            getSound.PlayOneShot(sounds[0]);
            yield return new WaitForSeconds(forSecendsLaser);
            Destroy(laserTemp);
        }
    }
    public void ScreenMirroring(GameObject objectTemp) // to apear the object from the oppsite side of the screen where it falls off
    {
        Vector2 oldPos = objectTemp.transform.position;
        if (objectTemp.transform.position.x > maxX)
        {
            objectTemp.transform.position = new Vector2(minX, oldPos.y);
        }
        else if (objectTemp.transform.position.x < minX)
        {
            objectTemp.transform.position = new Vector2(maxX, oldPos.y);
        }
        else if (objectTemp.transform.position.y > maxY)
        {
            objectTemp.transform.position = new Vector2(oldPos.x, minY);
        }
        else if (objectTemp.transform.position.y < minY)
        {
            objectTemp.transform.position = new Vector2(oldPos.x, maxY);
        }
    }
    private void PlaneMovement() // to move the spaceship forward and backward (in it's facing direction)
    {
        if (fuelToMove > 0)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                toMoveThePlane.AddForce(transform.up * movePlane); // to move in the direction that the spaceship is facing
                ScreenMirroring(gameObject);
                fuelToMove--;
                ShowFuelText();
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                toMoveThePlane.AddForce(transform.up * -movePlane);
                ScreenMirroring(gameObject);
                fuelToMove--;
                ShowFuelText();
            }
        }
    }
    private void PlaneRotation() // to rotate the spaceship to right or left (z axis)
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.forward * rotation); // to rotate the object
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.forward * -rotation);
    }
    public Rigidbody2D ReturnsPlaneRigidBody() // to get the rigidbody of the spaceship
    {
        return toMoveThePlane;
    }
    public float GetSpeed() // get the speed of the spaceship
    {
        var vel = toMoveThePlane.velocity;
        float speed = vel.magnitude; // magnitude Returns the length of this vector , The length of the vector is square root of (x*x+y*y+z*z).
        return speed;
    }
    public void RockMaker() // create rocks
    {
        int temp = UnityEngine.Random.Range(0, spaceRock.Length);
        Vector2 tempForVector;
        if (UnityEngine.Random.Range(0, 2) == 1)
            tempForVector = new Vector2(maxX + 5, UnityEngine.Random.Range(minY, maxY));
        else
            tempForVector = new Vector2(UnityEngine.Random.Range(minX, maxX), maxY + 5);
        GameObject rockTemp = Instantiate(spaceRock[temp],
        tempForVector,
        Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(-180, 181)))) as GameObject;
        rockTemp.GetComponent<Rigidbody2D>().AddForce(rockTemp.transform.up * UnityEngine.Random.Range(moveRock, moveRock + 100));
    }
    public void PowerUpMaker(GameObject powerUpTemp) // to create stars
    {
        float tempX = UnityEngine.Random.Range(minX + 1f, maxX - 1f);
        float tempY = UnityEngine.Random.Range(minY + 1f, maxY - 1f);
        GameObject starTemp = Instantiate(powerUpTemp,
        new Vector2(tempX, tempY),
        goldenStar.transform.rotation) as GameObject;
    }
    public void FuelRiase(int fuelTemp)// to raise the fuel when a rock get destroyed
    {
        for (int i = 0; i < fuelTemp; i++)
        {
            fuelToMove++;
            ShowFuelText();
        }
    }

    public void ShowFuelText()
    {
        textForFuel.text = fuelToMove.ToString();
    }
    public void ScoreRiase(int scoreTemp)// to raise the fuel when a rock get destroyed
    {
        for (int i = 0; i < scoreTemp; i++)
        {
            starScore++;
            ShowScoreText();
        }
    }
    public void ShowScoreText()
    {
        textForScore.text = starScore.ToString();
    }
    public int PowerUpControllerRaise()
    {
        powerUpController++;
        return powerUpController;
    }
    public void RaiseRedLaser()
    {
        redLaser += 5;
        ShowLaser();
    }
    public void RaiseGreenLaser()
    {
        greenLaser++;
    }
    public IEnumerator ShieldMaker()
    {
        int temp = 5;
        getSound.PlayOneShot(sounds[2]);
        GameObject shieldTemp = Instantiate(sheild, transform.position, sheild.transform.rotation);
        textForTime.text = Convert.ToString(temp);
        for (int i = temp-1; i >=0; i--)
        {
            yield return new WaitForSeconds(1f);
            textForTime.text = i.ToString();
        }
        getSound.PlayOneShot(sounds[3]);
        Destroy(shieldTemp);
    }
    public void DestroyAll()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Rock");
        foreach (GameObject target in gameObjects)
        {
            GameObject.Destroy(target);
        }
        gameObjects = GameObject.FindGameObjectsWithTag("Star");
        foreach (GameObject target in gameObjects)
        {
            GameObject.Destroy(target);
        }
        gameObjects = GameObject.FindGameObjectsWithTag("ClearAll");
        foreach (GameObject target in gameObjects)
        {
            GameObject.Destroy(target);
        }
        gameObjects = GameObject.FindGameObjectsWithTag("RedPill");
        foreach (GameObject target in gameObjects)
        {
            GameObject.Destroy(target);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ClearAll")
        {
            checkToDestroy = true;
            Destroy(collision.gameObject);
            checkToCreate = true;
            powerUpController = 0;
        }
        else if (collision.tag == "RedPill")
        {
            RaiseRedLaser();
            Destroy(collision.gameObject);
        }
    }
    private void ShowLaser()
    {
        textForLaser.text = redLaser.ToString();
    }
    public bool Check()
    {
        return checkToCreate;
    }
    public void SetCheck()
    {
        checkToCreate = false;
    }
    public void GameOver()
    {
        getSound.PlayOneShot(sounds[2]);
    }
}
