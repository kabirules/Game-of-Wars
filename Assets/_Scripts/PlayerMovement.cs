using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class PlayerMovement : MonoBehaviour {

    public float speed = 2f;            // The speed that the player will move at.

    public GameObject bullet;
    public Transform bulletSpawn;
    public float fireRate;
    private float nextFire;

    public GameController gameController;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    Transform playerTransform;
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    private float rotation = 0f;
    private float h;
    private float v;
    private bool attack = false;

    public bool killed;

    // Use this for initialization
    void Start () {
        // Unkill the player
        killed = false;
        // Get the gameController
        GameObject[] gameControllerArray = GameObject.FindGameObjectsWithTag("GameController");
        GameObject gameControllerObject = gameControllerArray[0];
        gameController = gameControllerObject.GetComponent<GameController>();
        // Make sure the player has proper scale
		Vector3 newScale = new Vector3(1f, 1f, 1f);
        transform.localScale = newScale;
	}

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (!this.killed) 
        {
            //Attack
            if (TCKInput.GetButtonDown("Button0") && Time.time > nextFire)
            {
                attack = true;
            }

            // Store the input axes.
    #if UNITY_EDITOR
            // h = Input.GetAxisRaw("Horizontal");
            // v = Input.GetAxisRaw("Vertical");
    #endif
    #if UNITY_ANDROID
            h = TCKInput.GetAxis("Joystick0", AxisType.X);
            v = TCKInput.GetAxis("Joystick0", AxisType.Y);
    #endif

            // Move the player around the scene.
            Move(h, v);

            // Turn the player to face the mouse cursor.
            Turning(h, v);

            if (attack)
            {
                Shoot();
                attack = false;
            }
        } else {
            // Player is dead, make it smaller until is almost invisble and destroy it.
            GetComponent<Animator>().enabled = false;
            Vector3 scale = transform.localScale;
            Vector3 newScale = new Vector3(scale.x*0.95f, scale.y*0.95f, scale.z*0.95f);
            transform.localScale = newScale;
            if (newScale.x < 0.1f)
            {
                gameController.GameOver();
            }
        }
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Avoid the player to 'fly'
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (newPosition.y > 0.5f)
            newPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(newPosition + movement);
    }

    void Turning(float h, float v)
    {
        Vector3 facingrotation = Vector3.Normalize(new Vector3(h, 0f, v));
        if (facingrotation != Vector3.zero)         //This condition prevents from spamming "Look rotation viewing vector is zero" when not moving.
            transform.forward = facingrotation;
  
    }

    void Shoot() 
    {
        nextFire = Time.time + fireRate;
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }
}
