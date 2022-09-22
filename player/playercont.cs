using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class playercont : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Attack,
        Die,
    }

    public State pl_state;


    Vector3 direction;
    Rigidbody rb;
    public inputs inputs;

    float movFrontal;
    float movLateral;

    public Animator anim;
    public Animator pause_anim;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float basespeed;
    [SerializeField] float runspeed;
    [SerializeField] float jumpforce;
    [SerializeField] GameObject charmesh;

    [SerializeField] GameObject stepray_up;
    [SerializeField] GameObject stepray_down;
    [SerializeField] float stepheight;
    [SerializeField] float stepsmooth;

    [Header("Events")]
    public GameObject pausemenu;
    public bool gameispaused;


    public bool isonevent;

    public bool isgrounded;
    public bool canjump;

    public bool attackmode;
    public bool canattack;

    public bool hurted;
    public bool dead;

    [Header("Stats")]
    public int baseatk;
    public float damage1;
    public float damage2;
    public float damage3;

    public int combo;

    public int luck;
    public float health;
    public float maxhealth;

    public int pearls;
    public int moonpearls;
    public int suncoins;

    public int firedust;
    public int earthdust;
    public int waterdust;

    public bool firegem;
    public int firelvl;
    public bool watergem;
    public int waterlvl;
    public bool earthgem;
    public int earthlvl;

    public int bagcapacity;

    [Header("Controls")]
    public bool gamepad;

    Volume scenevolume;
    
    DepthOfField depth;
    Scene m_Scene;

    public lifecanva canv;
    AudioSource aud;
    public AudioClip hitsound;

    void Awake()
    {//get saved char variables
        baseatk = eventlibrary.baseatk;
        luck = eventlibrary.luck;
        health = eventlibrary.health;

        pearls = eventlibrary.pearls;
        moonpearls = eventlibrary.moonpearls;
        suncoins = eventlibrary.suncoins;

        firedust = eventlibrary.firedust;
        earthdust = eventlibrary.earthdust;
        waterdust = eventlibrary.waterdust;

        firegem = eventlibrary.firegem;
        firelvl = eventlibrary.firegemlvl;
        watergem = eventlibrary.watergem;
        waterlvl = eventlibrary.watergemlvl;
        earthgem = eventlibrary.earthgem;
        earthlvl = eventlibrary.earthgemlvl;

        m_Scene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        aud = GetComponent<AudioSource>();
        canv = FindObjectOfType<lifecanva>();
        pause_anim = pause_anim.gameObject.GetComponent<Animator>();
        scenevolume = FindObjectOfType<Volume>();
        if (scenevolume.profile.TryGet<DepthOfField>(out depth))
        {
            depth.focalLength.value = 1f;
        }

        inputs = GetComponent<inputs>();
        rb = GetComponent<Rigidbody>();
        anim = charmesh.GetComponent<Animator>();

        Time.timeScale = 1f;

        pausemenu.SetActive(false);
        gameispaused = false;
        pl_state = State.Idle;

        stepray_up.transform.position = new Vector3(stepray_up.transform.position.x, stepheight, stepray_up.transform.position.z);
    }

    IEnumerator States()
    {
            switch (pl_state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Walk:
                    Walk();
                    break;
                case State.Run:
                    Run();
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.Die:
                    Die();
                    break;
            }
            yield return 0;
    }

    void Update()
    {

        if (health <= 0)
        {
            pl_state = State.Die;
            isonevent = true;
        }
//controller axis
        if (gamepad)
        {
            movFrontal = Input.GetAxis(inputs.c_movz);
            movLateral = Input.GetAxis(inputs.c_movx);
        }
        else
        {
            movFrontal = Input.GetAxis(inputs.movz);
            movLateral = Input.GetAxis(inputs.movx);
        }

        if (health < 0)
        {
            health = 0;
        }

        if (health > maxhealth)
        {
            health = maxhealth;
        }
//raycast animation
        RaycastHit hit;
        isgrounded = Physics.Raycast(transform.position, -Vector3.up, out hit,1f);
        //Debug.DrawRay(transform.position, -Vector3.up * 0.9f, Color.red);

        if (isgrounded)
        {
            anim.SetBool("isonfloor", true);
        }
        else 
        {
            anim.SetBool("isonfloor", false);
        }
//atk changes
        if (firegem)
        {
            if (firelvl == 1)
            {
                baseatk = 15;
            }
            if (firelvl == 2)
            {
                baseatk = 20;
            }
            if (firelvl == 3)
            {
                baseatk = 25;
            }
        }
        else
        {
            baseatk = 10;
        }
//hp changes
        if (watergem)
        {
            if (waterlvl == 1)
            {
                maxhealth = 120;
            }
            if (waterlvl == 2)
            {
                maxhealth = 150;
            }
            if (waterlvl == 3)
            {
                maxhealth = 200;
            }
        }
        else
        {
            maxhealth = 100;
        }
//luck changes
        if (earthgem)
        {
            luck = earthlvl;
        }
        else
        {
            luck = 0;
        }


        StartCoroutine(States());


        damage1 = baseatk;
        damage2 = baseatk * 1.2f;
        damage3 = baseatk * 1.5f;
        
        
        if (direction != new Vector3 (0,0,0) && !isonevent)
        {
            charmesh.transform.forward = direction;
        }

        

        if ((Input.GetButtonDown(inputs.pause)|| Input.GetButtonDown(inputs.c_pause)) && !isonevent)
        {
            gameispaused = !gameispaused;
            pl_state = State.Idle;
            PauseGame();
        }

        if ((Input.GetButtonDown(inputs.jump) || Input.GetButtonDown(inputs.c_jump)) && canjump &&!isonevent && !gameispaused &&!attackmode && !hurted)
        {
            rb.AddForce(Vector3.up * jumpforce);
            anim.SetTrigger("jump");
        }

    }
    void FixedUpdate()
    {//movement
        if (!isonevent && !hurted && !dead && !gameispaused && !attackmode)
        {
            direction = new Vector3(movLateral, 0, movFrontal);
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        else
        {
            direction = new Vector3(0, 0, 0);
        }


        //get joystick names
        string[] temp = Input.GetJoystickNames();

        //check array
        if (temp.Length > 0)
        {
            //iterate over elements
            for (int i = 0; i < temp.Length; ++i)
            {
                //check if the string is empty
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //controller temp[i] is connected

                    if (!gamepad)
                    {
                        Debug.Log("Controller " + i + " is connected using: " + temp[i]);

                        
                    }
                    gamepad = true;
                }
                else
                {
                    //controller temp[i] is not connected


                    if (gamepad)
                    {
                        Debug.Log("Controller: " + i + " is disconnected.");

                        
                    }
                    gamepad = false;
                }
            }
        }


    }

    void PauseGame()
    {
        if (gameispaused)
        {
            depth.focalLength.value = 300f;//blur
            pausemenu.SetActive(true);
            pause_anim.Play("cloth_in");//ui enter
            Time.timeScale = 0f;
            
        }
        else
        {
            gameispaused = true;
            Time.timeScale = 1;
            Invoke("resume", 1f);
            pause_anim.Play("cloth_out");
        }
    }

    void resume()
    {
        gameispaused = false;
        depth.focalLength.value = 1f;
        pausemenu.SetActive(false);
    }

    void Idle()
    {
        if (canv.group.alpha < 1)
        {
            canv.group.alpha += Time.deltaTime;//smooth appear of hp canva
        }
        
        anim.SetBool("idle", true);
        anim.SetBool("walk", false);
        anim.SetBool("run", false);
        direction = new Vector3(0, 0, 0);
//change state from idle
        if ((Input.GetButton(inputs.movx) || Input.GetButton(inputs.movz) || Input.GetButton(inputs.c_movx) || Input.GetButton(inputs.c_movz)) && !isonevent && !hurted && !dead && !gameispaused)
        {
            pl_state = State.Walk;
            if (m_Scene.name == "misore")
            {
                canv.group.alpha = 0;
            }
            else
            {
                canv.group.alpha = 1;
            }

        }

        if ((Input.GetButtonDown(inputs.attack) || Input.GetButtonDown(inputs.c_attack)) && !isonevent &&!hurted && !dead && !gameispaused)
        {
            pl_state = State.Attack;
            if (m_Scene.name == "misore")
            {
                canv.group.alpha = 0;
            }
            else
            {
                canv.group.alpha = 1;
            }
        }
        if (health <= 0)
        {
            pl_state = State.Die;
        }
    }

    void Walk()
    {
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        anim.SetBool("run", false);
        speed = basespeed;

        stepclimb();//enables raycasts for stair climbing

//change state from walk
        if ((!Input.GetButton(inputs.movx) && !Input.GetButton(inputs.movz) && !Input.GetButton(inputs.c_movx) && !Input.GetButton(inputs.c_movz)))
        {
            pl_state = State.Idle;
        }

        if (hurted)
        {
            pl_state = State.Idle;
        }

        if (health <= 0)
        {
            pl_state = State.Die;
        }

        if ((Input.GetButtonDown(inputs.attack) || Input.GetButtonDown(inputs.c_attack)) && !isonevent && !hurted && !dead && !gameispaused)
        {
            pl_state = State.Attack;
        }

        if ((Input.GetButtonDown(inputs.run) || Input.GetButtonDown(inputs.c_run)) && !isonevent && !hurted && !dead && !gameispaused)
        {
            pl_state = State.Run;
        }
    }

    void Run()
    {
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        anim.SetBool("run", true);
        speed = runspeed;

        stepclimb();//enables raycasts for stair climbing

//change state from run
        if ((!Input.GetButton(inputs.movx) && !Input.GetButton(inputs.movz))||(!Input.GetButton(inputs.c_movx) && !Input.GetButton(inputs.c_movz)
             && gamepad))
        {
            pl_state = State.Idle;
        }

        if ((Input.GetButtonDown(inputs.attack) || Input.GetButtonDown(inputs.c_attack)) && !isonevent && !hurted && !dead)
        {
            pl_state = State.Attack;
        }

        if ((Input.GetButtonUp(inputs.c_run) || Input.GetButtonUp(inputs.run))&&
            (Input.GetButton(inputs.movx) || Input.GetButton(inputs.movz) || Input.GetButton(inputs.c_movx) || Input.GetButton(inputs.c_movz))&& !isonevent)
        {
            pl_state = State.Walk;
        }
        if (health <= 0)
        {
            pl_state = State.Die;
        }
    }

    void Attack()
    {
        if (hurted)
        {
            pl_state = State.Idle;
        }

        if (combo >= 3)
        {
            endcombo();
        }

        if (!attackmode)
        {
            attackmode = true;
            anim.SetTrigger("" + combo);
            
        }
        
        
//encrease combo after being stated
            if ((Input.GetButtonDown(inputs.attack ) || Input.GetButtonDown(inputs.c_attack)) && canjump &&canattack &&attackmode)
            {
                anim.SetTrigger("" + combo);

            }
//die
        if (health <= 0)
        {
            pl_state = State.Die;
        }
    }

    public void startcombo()
    {
        canattack = false;
        hurted = false;
    }

    public void addcombo()
    {
        if (combo < 3)
        {
            canattack = true;
            combo++;
        }
        else
        {
            endcombo();
        }
    }

    public void endcombo()
    {
        pl_state = State.Idle;
        combo = 0;
        attackmode = false;
        canattack = true;
        hurted = false;
    }

    public void Hurt(float hurtdamage)
    {
        direction = new Vector3(0, 0, 0);
        if (!hurted)
        {
            hurted = true;
            pl_state = State.Idle;
            canv.group.alpha = 1;
            
            combo = 0;
            attackmode = false;
            health -= hurtdamage;
            rb.isKinematic = true;
            aud.PlayOneShot(hitsound);
            Invoke("returnhurt", 0.4f);

            anim.SetTrigger("hurt");

            
            
        }

    }

    public void returnhurt()
    {
        rb.isKinematic = false;
        canattack = true;
        hurted = false;
    }

    void Die()
    {
        direction = new Vector3(0, 0, 0);
        if (!dead)
        {
            dead = true;
            anim.SetTrigger("die");
        }
    }

void stepclimb()
    {
        RaycastHit lowerhit;
        if (Physics.Raycast(stepray_down.transform.position, charmesh.transform.TransformDirection(Vector3.forward), out lowerhit, 0.1f) && lowerhit.collider.CompareTag("stair"))
        {
            RaycastHit upperhit;
            if (!Physics.Raycast(stepray_up.transform.position, charmesh.transform.TransformDirection(Vector3.forward), out upperhit, 0.2f))
            {
                rb.position -= new Vector3(0f, -stepsmooth, 0f);
            }
        }

        RaycastHit lowerhit45;
        if (Physics.Raycast(stepray_down.transform.position, charmesh.transform.TransformDirection(1.5f,0,1), out lowerhit45, 0.1f) && lowerhit45.collider.CompareTag("stair"))
        {
            RaycastHit upperhit45;
            if (!Physics.Raycast(stepray_up.transform.position, charmesh.transform.TransformDirection(1.5f, 0, 1), out upperhit45, 0.2f))
            {
                rb.position -= new Vector3(0f, -stepsmooth, 0f);
            }
        }

        RaycastHit lowerhitminus45;
        if (Physics.Raycast(stepray_down.transform.position, charmesh.transform.TransformDirection(-1.5f, 0, 1), out lowerhitminus45, 0.1f) && lowerhitminus45.collider.CompareTag("stair"))
        {
            RaycastHit upperhitminus45;
            if (!Physics.Raycast(stepray_up.transform.position, charmesh.transform.TransformDirection(-1.5f, 0, 1), out upperhitminus45, 0.2f))
            {
                rb.position -= new Vector3(0f, -stepsmooth, 0f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "enemy")
        {
            if (health > 0)
            {
                pl_state = State.Idle;
            }
            
            
        }
    }



}
