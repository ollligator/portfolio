using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{

    private float speed = 0.8f;
    private float acc = 0.1f;


    private void FixedUpdate()
    {
        
        if (Player.play.transform.position.z < transform.position.z +10)
        {
            FallingDown();
        }
    }

    private void FallingDown()
    {
        //  transform.localPosition += transform.up * speed * Time.deltaTime*Mathf.Cos(transform.localRotation.z) + transform.right* speed * Time.deltaTime * Mathf.Sin(transform.localRotation.z);

        transform.localPosition -= transform.forward * speed * Time.deltaTime;
        //transform.localRotation = BlockMovement.blocks.transform.localRotation;
        speed += acc*speed*Time.deltaTime;
        //Destroy(gameObject);


    }
}
