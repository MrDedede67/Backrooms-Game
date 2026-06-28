using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemPickUp : MonoBehaviour
{
    [SerializeField] float reach = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, reach);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(collision.transform.parent.gameObject);
            }
        }
    }
}
