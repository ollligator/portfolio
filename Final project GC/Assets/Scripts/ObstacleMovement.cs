using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleMovement : MonoBehaviour
{
    private float selfSpeedRotation;

    private Scene scene;
    private void Start()
    {

        scene = SceneManager.GetActiveScene();

        if (scene.name == "Main")
        {
            selfSpeedRotation = Random.Range(15, 25);
        }

        if (scene.name == "Level1")
        {
            selfSpeedRotation = Random.Range(30, 50);
        }

        if (scene.name == "Level2")
        {
            selfSpeedRotation = Random.Range(50, 70);
        }
        if (scene.name == "Level3")
        {
            selfSpeedRotation = Random.Range(40, 100);
        }
        

    }

    void FixedUpdate()
    {
        SelfRotation();
    }

    private void SelfRotation()
    {
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, 0, selfSpeedRotation * Time.deltaTime);
    }

}
