using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentMove : MonoBehaviour
{
    public GameObject[] points; ///// FOR RANK!!!
    public PositionManager master; //// FOR RANK!!!
    CharecterMove Charecter;
    GameControl control;
    private Animator opponentAnimator;
    NavMeshAgent navMeshAgent;
    Rigidbody rigidb;
    Vector3 startPos;
    public bool turningLeft = false, turningRight = false, turningRightPlatform = false, turningLeftPlatform = false, touchRotatingStick = false, collideObstacle = false;
    bool stop = false;
    public float turningSpeed = 0, /*FOR RANK!!!*/opponentDistance/*FOR RANK!!!*/;
    float touchRotatingStickTime = 0;
    void Start()
    {
        Charecter = GameObject.FindGameObjectWithTag("Player").GetComponent<CharecterMove>();
        control = GameObject.FindGameObjectWithTag("lava").GetComponent<GameControl>();
        opponentAnimator = GetComponent<Animator>();
        rigidb = GetComponent<Rigidbody>();
        startPos = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if(stop)
        {
            return;
        }

        if (transform.position.y <= -6)
        {
            StartCoroutine(StartPosition());

        }

        if (Charecter.endGame)
        {
            opponentAnimator.SetBool("Run", false);      //Stop Running animation
            navMeshAgent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            return;
        }
        else if (control.gameStart)
        {
            opponentAnimator.SetBool("Run", true);      //Start Running animation
            navMeshAgent.destination = new Vector3(transform.position.x, transform.position.y, 350);
        } 
        if(collideObstacle)
        {
            opponentAnimator.SetBool("Run", false);
            navMeshAgent.enabled = true;
            navMeshAgent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            StartCoroutine(StartPosition());
        }

        FindPosition();

    }

    private void FixedUpdate()
    {
        if (stop)
        {
            return;
        }


        if (turningRightPlatform)     //If the opponent stays at turning right platform.
        {
            rigidb.AddForce(new Vector3(1f * turningSpeed /** Time.fixedDeltaTime*/, 0, 0));
        }
        else if (turningLeftPlatform)     //If the opponent stays at turning left platform.
        {
            rigidb.AddForce(new Vector3(-1f * turningSpeed /** Time.fixedDeltaTime*/, 0, 0));
        }


        if (touchRotatingStick)    //When collide rotating stick
        {
            touchRotatingStickTime += Time.deltaTime;
            if (touchRotatingStickTime >= 0.5f)
            {
                touchRotatingStickTime = 0;
                opponentAnimator.SetBool("Run", true);
                navMeshAgent.destination = new Vector3(transform.position.x, transform.position.y, 350);
                touchRotatingStick = false;
            }
        }
    }


    public void FindPosition()   ///////////For RANK!!!!!
    {
        opponentDistance = Vector3.Distance(points[master.currentPoint].transform.position, this.transform.position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("turningleftplatform"))    //When collide with turning left platform
        {
            turningRightPlatform = false;
            turningLeftPlatform = true;
            turningLeft = true;
        }

        if (collision.gameObject.tag.Equals("turningrightplatform"))    //When collide with turning left platform
        {
            turningLeftPlatform = false;
            turningRightPlatform = true;
            turningRight = true;
        }

        if (collision.gameObject.tag.Equals("lava") || collision.gameObject.tag.Equals("obstacle"))  //when collide with lava or any obstacles.
        {
            collideObstacle = true;
        }

        if (collision.gameObject.tag.Equals("rotatingstick"))    //when collide with rotating any rotating sticks.
        {
            opponentAnimator.SetBool("Run", false);
            navMeshAgent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            touchRotatingStick = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("turningleftplatform"))
        {
            turningRightPlatform = false;
            turningLeftPlatform = true;
        }

        if (collision.gameObject.tag.Equals("turningrightplatform"))
        {
            turningLeftPlatform = false;
            turningRightPlatform = true;
            Debug.Log("Kız ayak bastı");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("turningleftplatform"))
        {
            turningLeftPlatform = false;
            turningLeft = false;
            turningRight = false;
        }

        if (collision.gameObject.tag.Equals("turningrightplatform"))
        {
            turningRightPlatform = false;
            turningRight = false;
            turningLeft = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("finish"))
        {
            stop = true;
            opponentAnimator.SetBool("Run", false);
            navMeshAgent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }



    IEnumerator StartPosition()
    {
        yield return new WaitForSeconds(1);
        transform.position = startPos;
        navMeshAgent.destination = new Vector3(transform.position.x, transform.position.y, 350);
        opponentAnimator.SetBool("Run", true);
        collideObstacle = false;
    }
}
