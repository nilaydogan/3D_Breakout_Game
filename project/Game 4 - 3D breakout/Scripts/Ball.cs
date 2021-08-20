using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float maxVelocity = 20;
    public float minVelocity = 15;
    private BreakoutGame bgScript;
    private bool flag = false;
    void Awake () {
        bgScript = FindObjectOfType<BreakoutGame>();

        if (bgScript.ballSpawned || !bgScript.isRevived)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -18);
            bgScript.ballSpawned = false;
        }

        else if (bgScript.isRevived)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            flag = true;
            bgScript.isRevived = false;
        }

    }

    void Update () {
        if (bgScript.revive == 0 && !bgScript.ballSpawned)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -18);
                
                flag = false;
            }
        }
        //Make sure we stay between the MAX and MIN speed.
        if (!flag) 
        {
            Move();
        }
	}

    void Move()
    {
        float totalVelocity = Vector3.Magnitude(GetComponent<Rigidbody>().velocity);
        if (totalVelocity > maxVelocity)
        {
            float tooHard = totalVelocity / maxVelocity;
            GetComponent<Rigidbody>().velocity /= tooHard;
        }
        else if (totalVelocity < minVelocity)
        {
            float tooSlowRate = totalVelocity / minVelocity;
            GetComponent<Rigidbody>().velocity /= tooSlowRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            BreakoutGame.SP.LostBall();
            Destroy(gameObject);
        }
    }
}
