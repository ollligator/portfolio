using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public static Transform blocks;

    // Update is called once per frame
    void Update()
    {
        blocks = transform;
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion rot = Quaternion.Euler(0, 0, -100*Time.deltaTime);
            transform.localRotation *= rot;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Quaternion rot = Quaternion.Euler(0, 0, 100 * Time.deltaTime);
            transform.localRotation *= rot;
        } 
        
    }
    
}
