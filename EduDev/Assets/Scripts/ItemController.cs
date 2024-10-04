using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float itemSpeed;
    public float destroyXPos = -6f;
    void Update()
    {
        if(transform.position.x < destroyXPos){
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * itemSpeed * Time.deltaTime);
    }
}
