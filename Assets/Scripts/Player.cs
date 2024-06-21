using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    
    [SerializeField] float moveSpeed= 10f;
    Vector2 rawInput;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    //this is bot left top right corners
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    private void Awake()
    {//shooter is get component shooter
        shooter = GetComponent<Shooter>();
    }
    private void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    //initialise boundaries
    void InitBounds()
    { //camera.main is camera in hierarchy
        //tagged with "main" tag
        Camera mainCamera = Camera.main;
        //minimum bounds equals our main camera dot viewport to world point.
        //it suggested Vector 3, this means third parameter is 0
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    void Move()
    {
        //And this is going to store the delta position for our movement,
        //which we can then apply to our transform posiiton
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();

        //this will allow ush us to then edit the X and the Y component
        //of this new vector and then apply the changes
        //back to the transform dot position.
        //So let's start by clamping the X position of our player so we can say that the new dot x and to clamp
        //this variable, we're going to use a method from the math f class called math f clamp.
        //And what this will do is just restrict any values that float outside of the minimum and maximum values we set

        //So the current position of our player plus the Delta X, so the amount we're going to be moving and
        //this needs to be kept between our minimum balance dot X and our maximum bounds dot x.
        //Next we need to do a similar thing but clamping our y position.

        newPos.x = Mathf.Clamp(transform.position.x+delta.x,minBounds.x+paddingLeft, maxBounds.x-paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y+paddingBottom, maxBounds.y-paddingTop);

        transform.position = newPos;
    }
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null) 
        {//pretty muchif shooter is not null??
            // we are firing = when button is pressed
            shooter.isFiring = value.isPressed;
            //Debug.Log(rawInput);
        }
    }
}
