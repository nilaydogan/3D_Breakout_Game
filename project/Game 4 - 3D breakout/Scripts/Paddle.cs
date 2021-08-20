using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    public float moveSpeed = 15;

    [HideInInspector]
    public float speedUp_DeactiveTime = 0;
    [HideInInspector]
    public float speedRate;

    [HideInInspector]
    public float grow_DeactiveTime = 0;
    [HideInInspector]
    public float growRate;

    [HideInInspector]
    public float shrink_DeactiveTime = 0;
    [HideInInspector]
    public float shrinkRate;
    [HideInInspector]
    public float max = 14.0f;

    [HideInInspector]
    public bool canShoot = false;

    public GameObject Laser;
    public Transform AttackPointRight;
    public Transform AttackPointLeft;
    public AudioSource laserSound;
    public AudioSource bounce;
    private Animator anim;

    [HideInInspector]
    public float fire_DeactiveTime = 0;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update () {
        //Debug.Log("move: "+ moveSpeed);

        float moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        transform.position += new Vector3(moveInput, 0, 0);

        //float max = 14.0f;
        if (transform.position.x <= -max || transform.position.x >= max)
        {
            float xPos = Mathf.Clamp(transform.position.x, -max, max); //Clamp between min -5 and max 5
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if(speedUp_DeactiveTime > 0)
        {
            StartCoroutine(WaitForDeactivation(speedUp_DeactiveTime,"speed"));
            speedUp_DeactiveTime = 0;
        }
        if (grow_DeactiveTime > 0)
        {
            StartCoroutine(WaitForDeactivation(grow_DeactiveTime, "grow"));
            grow_DeactiveTime = 0;
        }
        if(shrink_DeactiveTime > 0)
        {
            StartCoroutine(WaitForDeactivation(shrink_DeactiveTime, "shrink"));
            shrink_DeactiveTime = 0;

        }
        if(fire_DeactiveTime > 0)
        {
            StartCoroutine(WaitForDeactivation(fire_DeactiveTime, "fireball"));
            fire_DeactiveTime = 0;
        }
    }

    void OnCollisionExit(Collision collisionInfo ) {
        //Add X velocity..otherwise the ball would only go up&down
        bounce.Play();
        Rigidbody rigid = collisionInfo.rigidbody;
        float xDistance = rigid.position.x - transform.position.x;
        rigid.velocity = new Vector3(rigid.velocity.x + xDistance/2, rigid.velocity.y, rigid.velocity.z);
    }
    void speed_Deactive()
    {
        moveSpeed -= speedRate;
    }

    void grow_Deactive()
    {
        transform.localScale -= new Vector3(growRate, 0, 0);
        max = 14.0f;
    }
    void shrink_Deactive()
    {
        transform.localScale += new Vector3(shrinkRate, 0, 0);
        max = 14.0f;
    }

    void shooting_Deactive()
    {
        canShoot = false;
        anim.SetTrigger("End");
    }
    IEnumerator WaitForDeactivation(float deactiveTime, string collectable)
    {
        yield return new WaitForSeconds(deactiveTime);
        if (collectable.Equals("speed"))
        {
            speed_Deactive();
        }
        else if (collectable.Equals("grow"))
        {
            grow_Deactive();
        }
        else if (collectable.Equals("shrink"))
        {
            shrink_Deactive();
        }else if (collectable.Equals("fireball"))
        {
            shooting_Deactive();
        }
    }

    void Shoot()
    {
        Instantiate(Laser, AttackPointRight.position, Quaternion.Euler(90f, 0f, 0f));
        Instantiate(Laser, AttackPointLeft.position, Quaternion.Euler(90f, 0f, 0f));
        laserSound.Play();
    }
}
