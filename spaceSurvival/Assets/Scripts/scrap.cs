using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Item Destroyed");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().incDecScrapCount(1, 1);
        Destroy(this.gameObject);
    }
}
