using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.SceneManagement;

public class CreateTunnel : MonoBehaviour
{
    public Transform Player;
    public Tun[] TunPrefabs;
    public Tun FirstTun;
    public Obs FirstObs;
    private Scene scene;


    public Obs[] ObstaclePrefabs;

    private List<Tun> spawnedTuns = new List<Tun>();
    private List<Obs> spawnedObs = new List<Obs>();

    private void Start()
    {
        spawnedTuns.Add(FirstTun);
        spawnedObs.Add(FirstObs);
        scene = SceneManager.GetActiveScene();
    }

    private void FixedUpdate()
    {
        
        if (Player.position.z < spawnedTuns[spawnedTuns.Count - 1].Center.position.z + 40)  
        {
            SpawnTun();
        }
        if (Player.position.z < spawnedObs[spawnedObs.Count - 1].Center.position.z + 20)
        {
            
            SpawnObs();
        }
    }
    private void SpawnTun()
    {
        Tun tun = GetRandomTun();

        Vector3 pos = spawnedTuns[spawnedTuns.Count - 1].Center.position - new Vector3(0, 0, 4);
        Quaternion rot = spawnedTuns[spawnedTuns.Count - 1].Center.rotation;
        
        Tun newTun = Instantiate(tun, pos, rot);

        spawnedTuns.Add(newTun);

        if (spawnedTuns.Count >= 100)
        {
            Destroy(spawnedTuns[0].gameObject);
            spawnedTuns.RemoveAt(0);
        }
    }

    private Tun GetRandomTun()
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < TunPrefabs.Length; i++)
        {
            chances.Add(TunPrefabs[i].ChanceFromDistance.Evaluate(Player.transform.position.z));
        }

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return TunPrefabs[i];
            }
        }

        return TunPrefabs[TunPrefabs.Length - 1];
    }

    private void SpawnObs()
    {
        Obs obs = GetRandomObs();
        float zpos = 0f;
        if (scene.name == "Main")
        {
            zpos = 8f;
        }

        if (scene.name == "Level1")
        {
            zpos = 4f;
        }

        if (scene.name == "Level2")
        {
            zpos = 7f;
        }
        if (scene.name == "Level3")
        {
            zpos = 12f;
        }
       
        Vector3 pos = spawnedObs[spawnedObs.Count - 1].Center.position - new Vector3(0, 0, zpos);
        pos.x = 0f;
        pos.y = 0f;

        Quaternion rot = Quaternion.Euler(0,0, spawnedTuns[spawnedTuns.Count-1].Center.rotation.z + 60f * Mathf.Round(Random.Range(0,3)));

        Obs newObs = Instantiate(obs, pos, rot);

        spawnedObs.Add(newObs);

        if (spawnedObs.Count >= 100)
        {
            Destroy(spawnedObs[0].gameObject);
            spawnedObs.RemoveAt(0);
        }
    }

    private Obs GetRandomObs()
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < ObstaclePrefabs.Length; i++)
        {
            chances.Add(ObstaclePrefabs[i].ChanceFromDistance.Evaluate(Player.transform.position.z));
        }

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return ObstaclePrefabs[i];
            }
        }

        return ObstaclePrefabs[ObstaclePrefabs.Length - 1];
    }



}
