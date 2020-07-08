using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle = 0,
    walking,
    attack_sword,
    attack_wand,
    attack_other,
    cheer,
    death
}

public enum Direction
{
    up = 0,
    right,
    down,
    left
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    public Direction direction;
    public float speed;
    public float maxSpeedTilesSec;

    public float xAxis;
    public float yAxis;
    public bool fire1;
    public bool fire2;
    public bool fire3;
    public bool attack;
    private Animator animator;

    private Rigidbody2D rig;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //toetsenboard input
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        fire1 = Input.GetButton("Fire1");
        fire2 = Input.GetButton("Fire2");
        fire3 = Input.GetButton("Fire3");
        attack = Input.GetButton("attack");

        if (xAxis == 0 && yAxis == 0)
            playerState = PlayerState.idle;
        else
            playerState = PlayerState.walking;

        if (attack)
            playerState = PlayerState.attack_sword;
        else if (fire1)
            playerState = PlayerState.attack_wand;
        else if (fire2)
            playerState = PlayerState.attack_other;
        else if (fire3)
            playerState = PlayerState.death;

        if (xAxis < 0)
        {
            direction = Direction.left;
            yAxis = 0;
        }
        else if (xAxis > 0)
        {
            direction = Direction.right;
            yAxis = 0;
        }
        if (yAxis < 0)
        {
            direction = Direction.down;
            xAxis = 0;
        }
        else if (yAxis > 0)
        {
            direction = Direction.up;
            xAxis = 0;
        }

        //stuur de playerstate en direction door naar de animator
        animator.SetFloat("Direction", (float)direction);
        animator.SetInteger("State", (int)playerState);
    }

    private void FixedUpdate()
    {
        Vector3 deraction = new Vector3(xAxis * maxSpeedTilesSec, yAxis * maxSpeedTilesSec, 0);
        rig.MovePosition(transform.position + deraction * speed * Time.fixedDeltaTime);
    }
}