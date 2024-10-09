using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject item;
    public float spawnTime;
    public float yPosMin, yPosMax;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPipeCoroutine());
    }


   IEnumerator SpawnPipeCoroutine(){
    yield return new WaitForSeconds(Random.Range(spawnTime - 1f, spawnTime + 1f));
    Instantiate(item, transform.position + Vector3.up * Random.Range(yPosMin, yPosMax), Quaternion.identity);
    StartCoroutine(SpawnPipeCoroutine());
   }

   void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(transform.position, 0.5f);
}
}
