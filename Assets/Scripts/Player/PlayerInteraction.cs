using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject heldItem;
    private bool willUse;

    // Start is called before the first frame update
    void Start()
    {
        willUse = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            willUse = true;
        }
    }

    private void FixedUpdate()
    {
        // handle input
        if(heldItem != null && willUse)
        {
            Instantiate(heldItem, transform.position, transform.rotation);
            heldItem.GetComponent<Deployable>().Deploy();
            heldItem = null;
        }
        willUse = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            heldItem = other.gameObject.GetComponent<Pickup>().deployPrefab;
            Destroy(other.gameObject);
        }
    }
}
