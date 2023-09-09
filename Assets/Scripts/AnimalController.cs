using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [SerializeField] float jumpForce = 1.0f;
    [SerializeField] string animalName;

    Rigidbody animalRb;
    // Start is called before the first frame update
    void Start()
    {
        animalRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitToJump());
    }

    IEnumerator WaitToJump()
    {
        float floorY = transform.position.y;
        if(floorY < 0)
        {
            animalRb.velocity = new Vector2(animalRb.velocity.x, jumpForce);
        }
        yield return new WaitForSeconds(5);
    }

}
