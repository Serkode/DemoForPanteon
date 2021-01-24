using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using UnityEngine.UI;

public class CharecterMove : MonoBehaviour
{
    public GameObject[] points; ///// FOR RANK!!!
    public PositionManager master; //// FOR RANK!!!
    public GameObject paintingWall, playerSign;
    private Animator karakterAnimator;
    Rigidbody rigid;
    GameControl control;
    RotatorMove rot;
    public float runSpeed = 0, turnSpeed = 0, stopTime = 0, touchRotatingStickTime = 0, touchSpeed = 0, /*FOR RANK!!!*/playerDistance/*FOR RANK!!!*/;
    public bool turningLeftPlatform = false, turningRightPlatform = false, turningLeft = false, turningRight = false, stopGame = false, endGame = false;
    bool touchFloor = true, touchRotatingStick = false;
    Vector3 xValue , pos, lastPos, xValueMousePos;
    float sec = 0;



    public Text directionText;
    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private string direction;

    void Start()
    {
        playerSign.SetActive(true);
        karakterAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        control = GameObject.FindGameObjectWithTag("lava").GetComponent<GameControl>();
        rot = GameObject.FindGameObjectWithTag("rotatingstick").GetComponent<RotatorMove>();
        pos = transform.position;
    }

    private void Update()
    {
        if (!control.gameStart)    //Wait For countdown
        {
            return;
        }
        if (endGame)
        {
            runSpeed = 0;
            karakterAnimator.SetBool("Run", false);      //Stop Running animation
            return;
        }

        if (stopGame)
        {
            return;
        }

        FindPosition();

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStartPosition = theTouch.position;
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    direction = "Tapped";
                }

                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    direction = x > 0 ? "Right" : "Left";
                    if(x > 0)
                    {
                        transform.eulerAngles = new Vector3(0, 45f, 0);



                        if (turningLeft)    //Is Rotating Platform turning left When charecter running right?
                        {
                            transform.position += transform.up * runSpeed * Time.fixedDeltaTime;
                        }
                    }
                    else if(x < 0)
                    {
                        transform.eulerAngles = new Vector3(0, -45f, 0);


                        if (turningRight)    //Is Rotating Platform turning right When charecter running left?
                        {
                            transform.position += transform.up * runSpeed * Time.fixedDeltaTime;
                        }
                    }
                }

                else
                {
                    direction = y > 0 ? "Up" : "Down";
                }
            }
        }
        //else    //There is no turning right or left. For mobile, open this code block and set the line 187 as a comment line.
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //}

        directionText.text = direction;
    }

    private void FixedUpdate()
    {
        if(!control.gameStart)    //Wait For countdown
        {
            return;
        }
        if(endGame)
        {
            runSpeed = 0;
            karakterAnimator.SetBool("Run", false);      //Stop Running animation
            return;
        }

        if(stopGame)
        {
            stopTime += Time.deltaTime;
            if(stopTime >= 3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        //sec += Time.deltaTime;                   ///////Game Time 36 - 39 seconds
        //Debug.Log((int)sec + " saniye " );       ///////Game Time 36 - 39 seconds

        karakterAnimator.SetBool("Run", true);    //Play Running animation

        transform.position += transform.forward * runSpeed * Time.fixedDeltaTime;    //Run forward

        if (Input.GetKey(KeyCode.A))    //turn left
        {
            transform.eulerAngles = new Vector3(0, -45f, 0);

            if (turningRight)    //Is Rotating Platform turning right When charecter running left?
            {
                transform.position += transform.up * runSpeed * Time.fixedDeltaTime;
            }

            if (Input.GetKey(KeyCode.Space) && touchFloor)     //Jump when running left
            {
                rigid.AddForce(0, 350, 0);
                touchFloor = false;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 45f, 0);

            if (turningLeft)    //Is Rotating Platform turning left When charecter running right?
            {
                transform.position += transform.up * runSpeed * Time.fixedDeltaTime;
            }

            if (Input.GetKey(KeyCode.Space) && touchFloor)    //Jump when running right
            {
                rigid.AddForce(0, 350, 0);
                touchFloor = false;
            }
        }
        else if(Input.GetKey(KeyCode.Space) && touchFloor)     //Jump when running forward
        {
            rigid.AddForce(0, 350, 0);
            touchFloor = false;
        }
        else    //There is no turning right or left. For PC, open this code block and set the line 114 as a comment line.
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (touchRotatingStick)    //When collide rotating stick
        {
            touchRotatingStickTime += Time.deltaTime;
            if(touchRotatingStickTime >=0.5f)
            {
                touchRotatingStickTime = 0;
                runSpeed = 10;
                touchRotatingStick = false;
            }
        }

        if (turningLeftPlatform)    //When the player collide to turning platforms, this codes will force the player to fall
        {
            rigid.AddForce(-50, 0, 0);
            if(transform.position.y<0)
            {
                if(transform.position.x < xValue.x)
                {
                    turningLeft = true;
                }
                else if(transform.position.x > xValue.x)
                {
                    turningRight = true;
                }
            }
        }
        else if (turningRightPlatform)     //When the player collide to turning platforms, this codes will force the player to fall
        {
            rigid.AddForce(50, 0, 0);
            if (transform.position.y < 0)
            {
                if (transform.position.x < xValue.x)
                {
                    turningLeft = true;
                }
                else if (transform.position.x > xValue.x)
                {
                    turningRight = true;
                }
            }
        }
    }
    
    public void FindPosition()   ///////////For RANK!!!!!
    {
        playerDistance = Vector3.Distance(points[master.currentPoint].transform.position, this.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("turningleftplatform"))
        {
            Debug.Log("Çarpıştılar.");
            touchFloor = true;
            turningLeft = true;
            xValue = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if (collision.gameObject.tag.Equals("turningrightplatform"))
        {
            Debug.Log("Çarpıştılar.");
            touchFloor = true;
            turningRight = true;
            xValue = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if(collision.gameObject.tag.Equals("floor"))
        {
            touchFloor = true;
        }

        if(collision.gameObject.tag.Equals("lava") || collision.gameObject.tag.Equals("obstacle") )
        {
            stopGame = true;
            karakterAnimator.SetBool("Run", false);
            runSpeed = 0;
        }

        if (collision.gameObject.tag.Equals("rotatingstick"))
        {
            runSpeed = 0;
            touchRotatingStick = true;
            lastPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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
            Debug.Log("Çarpıştılar.");
            turningLeftPlatform = false;
            turningRightPlatform = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("turningleftplatform"))
        {
            turningLeftPlatform = false;
            turningLeft = false;
            turningRight = false;
            touchFloor = false;
        }

        if (collision.gameObject.tag.Equals("turningrightplatform"))
        {
            turningRightPlatform = false;
            turningRight = false;
            turningLeft = false;
            touchFloor = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Equals("finish"))
        {
            endGame = true;
            control.startButton2.SetActive(true);
            StartCoroutine(CreatePaintingWall());
            playerSign.SetActive(false);
        }
    }

    IEnumerator CreatePaintingWall()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(paintingWall, new Vector3(transform.position.x, 1.22f, 345), Quaternion.identity);
    }
}
