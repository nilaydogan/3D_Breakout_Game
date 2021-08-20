using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 25.0f;

    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);

        if (transform.position.z >= 25)
        {
            Destroy(gameObject);
        }
    }
}
