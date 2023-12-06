using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    BoxCollider2D spawnRect;
    [SerializeField] float spawnRate;
    [SerializeField] List<GameObject> minionEnemy = new List<GameObject>();
    Vector2 cubeSize;
    Vector2 cubeCenter;
    // Start is called before the first frame update
    void Start()
    {
        spawnRect = GameObject.FindGameObjectWithTag("Player").gameObject.transform.Find("spawnRect").gameObject.GetComponent<BoxCollider2D>();
        startSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void startSpawning()
    {
        InvokeRepeating("spawnMinion",3,spawnRate);
    }
    void spawnMinion()
    {
        GameObject minion = Instantiate(minionEnemy.ElementAt(Random.Range(0,minionEnemy.Count)),getSpawnPos(),Quaternion.identity);
        minion.transform.localScale = new Vector3(2f,2f,2f);
    }
    Vector3 getSpawnPos()
    {
        Transform cubeTrans = spawnRect.GetComponent<Transform>();
        cubeCenter = cubeTrans.position;

        cubeSize.x = cubeTrans.localScale.x * spawnRect.size.x;
        cubeSize.y = cubeTrans.localScale.y * spawnRect.size.y;

        Vector3 randomPosition = new Vector3(Random.Range(-cubeSize.x / 2, cubeSize.x / 2)+ cubeCenter.x, Random.Range(-cubeSize.y / 2, cubeSize.y / 2)+ cubeCenter.y, cubeTrans.position.z);

        return randomPosition;
    }
    IEnumerator countDown()
    {
        yield return new WaitForSeconds(1);
    }
}
