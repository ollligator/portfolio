using UnityEngine;

public class Follower : MonoBehaviour
{
    public Player mm;
    public Camera camera;
    public Light light;
    public float down;
    public float up;
    public float x;
    public float y;
    private Vector3 offset;
    private Vector3 offset1;
    private Vector3 pos;
    private void Start()
    {
        x = camera.transform.position.x;
        y = camera.transform.position.y;
    }

    void Awake()
    {
        
        offset = camera.transform.position - mm.transform.position;
        offset1 = light.transform.position - mm.transform.position;
    }

    private void OnValidate()
    {
       
        camera.orthographicSize = down;
    }

    
    void Update()
    {
        if (!mm) return;
        float speed = mm.rigidbody.velocity.magnitude;
        float maxSpeed = mm.maxSpeed;

        camera.orthographicSize = Mathf.Lerp(down, up, speed / maxSpeed); ;
        camera.transform.position = mm.transform.position + offset;
        pos.x = x;
        pos.y = y;
        pos.z = camera.transform.position.z;
        camera.transform.position = pos;

        light.transform.position = mm.transform.position + offset1;
    }
}