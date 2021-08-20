using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {

    public int default_hit;
    //int i = 0;
    public Material[] material;
    
    private Renderer blockRenderer;

    private AudioSource crash;
    private void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        crash = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        crash.Play();
        if (collision.gameObject.tag == "Player")
        {
            default_hit--;
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            default_hit--;
            if (default_hit <= 0)
            {
                spawnRandom();
                BreakoutGame.SP.HitBlock();
                Destroy(gameObject);
            }
            else
            {
                blockRenderer.material = material[default_hit - 1];
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (default_hit <= 0)
        {
            spawnRandom();
            BreakoutGame.SP.HitBlock();
            Destroy(gameObject);
        }else
        {
            blockRenderer.material = material[default_hit-1];
        }
    }

    void spawnRandom()
    {
        float spawnChance = UnityEngine.Random.Range(0, 100f);
        float reviveChance = UnityEngine.Random.Range(0,100f);
        bool alreadySpawned = false;

        if (spawnChance <= Game_Manager.Instance.chance)
        {
            alreadySpawned = true;
            Collectable newCollectable = this.SpawnCollectable(false);
        }

        if(reviveChance <= Game_Manager.Instance.rareChance && !alreadySpawned)
        {
            Collectable revCollectable = this.SpawnCollectable(true);
        }
    }

    private Collectable SpawnCollectable(bool isRevive)
    {
        List<Collectable> collection;

        if (!isRevive)
        {
            collection = Game_Manager.Instance.collectables;
        }
        else
        {
            collection = Game_Manager.Instance.rare;
        }

        int collectableIndex = UnityEngine.Random.Range(0, collection.Count);
        Collectable prefab = collection[collectableIndex];
        Collectable newCollectable = Instantiate(prefab, this.transform.position, Quaternion.Euler(180f, 180f, -90f)) as Collectable;

        return newCollectable;
    }
}
