using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    
    public Rigidbody rigidbody;
    public static Player Instance;
    public float upSpeed;
    public float maxSpeed;
    public float accelerationLinear;

    public float maxHP;

    [SerializeField] private float HP;
    [SerializeField] private float distance;
    public TMPro.TextMeshProUGUI textHP;
    public TMPro.TextMeshProUGUI textDistance;
    public TMPro.TextMeshProUGUI textState;
    private float start;

    public AudioSource game;
    public AudioSource red;
    public AudioSource fight;

    private Scene scene;

    public static Transform play;
    private float completed;
    private float leveldistance = 200f;

    void Start()
    {
        game.Play();
       scene = SceneManager.GetActiveScene();
    }
    private void Awake()
    {
        
        HP = maxHP;
        distance = 0;
        textHP.text = HP.ToString();
        start = transform.position.z;

        if (scene.name == "Main")
        {
            textDistance.text = distance.ToString();
        }

        if (scene.name == "Level1")
        {
            textDistance.text = distance.ToString() + "%";
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        play = transform;
        if (GamePlayController.Instance.state != GamePlayController.State.Play)
            return;
        LinearMovement();

        if (scene.name == "Main")
        {
            distance = Mathf.Round(Mathf.Abs(start - transform.position.z));
            textDistance.SetText(distance.ToString());
        }
        else
        {
            completed = Mathf.Round(Mathf.Abs(start - transform.position.z)) ;
            distance = Mathf.Round((Mathf.Abs(start - transform.position.z) / (leveldistance / 100)));
            textDistance.SetText(distance.ToString()+"%");
        }

        if(transform.position.y < -1)
        {
            HP = 0;
        }

        if (HP == 0)
        {
            string f = "GAME OVER";
            textState.SetText(f);
            //GameObject.Destroy(gameObject);
            if (scene.name == "Main")
            {
                Records.SaveRecord(distance);
            }
            PlayerDestroyedEvent.Invoke();
        }

        if ((scene.name != "Main") && (completed== leveldistance))
        {
            string f = "LEVEL COMPLETED";
            textState.SetText(f);
            PlayerDestroyedEvent.Invoke();
        }

    }

    private void LinearMovement()
    {
       if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(transform.forward * accelerationLinear);
        }

        

    }

    public event System.Action PlayerDestroyedEvent;

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "black")
        {
        GameObject.Destroy(gameObject);
        }

        
        if (tag == "green")
        {
            rigidbody.AddForce(transform.forward * accelerationLinear);

        }

        if (tag == "red")
        {
            red.Play();
            if (scene.name == "Main")
            {
                HP -= 5;
                if (HP < 0)
                {
                    HP = 0;
                }
                textHP.SetText(HP.ToString());
                rigidbody.AddForce(transform.forward * (-5) * accelerationLinear);
            }
            
            
            if  (scene.name == "Level1")
            {
                for (int i = 0; i < 10; i++)
                {
                    collision.gameObject.transform.position -= new Vector3(0, 0.1f, 0) * Time.deltaTime;
                    
                }
            }
            
            

        }

        if (tag == "obstacle")
        {
            fight.Play();
            rigidbody.AddForce(transform.forward * (-5) * accelerationLinear);
            HP -= 10;
            if (HP < 0)
            {
                HP = 0;
            }
            textHP.SetText(HP.ToString());
        }


        if (tag == "Finish")
        {
            string f = "FINISH";
            textState.SetText(f);
            PlayerDestroyedEvent.Invoke();
        }


        


    }



}
