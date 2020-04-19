using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public GameObject powerupEffect;
    private GameObject player;
    public GameObject item;
    public float powerTime;

    private AudioSource powerSound;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bolt"))
        {

            gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(Pickup() );
        }
        else
        {
            return;
        }
    }

    IEnumerator Pickup()
    {

        Instantiate(powerupEffect, transform.position, transform.rotation);

        player.GetComponent<PlayerController>().speed = 35.0f;
        player.GetComponent<PlayerController>().fireRate = 0.07f;
        player.GetComponent<PlayerController>().tilt = 2.0f;

        item.GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(powerTime);


        player.GetComponent<PlayerController>().speed = 10.0f;
        player.GetComponent<PlayerController>().fireRate = 0.5f;
        player.GetComponent<PlayerController>().tilt = 4.0f;

        Destroy(gameObject);
    }
}
