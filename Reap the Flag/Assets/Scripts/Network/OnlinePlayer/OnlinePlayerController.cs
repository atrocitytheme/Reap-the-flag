using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    Vector3 prev;
    float h = 0; // the rotation direction of mouse x
    float v = 0; // the rotation direction of mouse y
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Move(Vector3 vec)
    {
        playerRigidbody.MovePosition(vec);
        bool r = prev == null || prev != vec;
        anim.SetBool("IsWalking", r);
        prev = vec;
    }

    void Turning(Vector3 angle)
    {
        transform.rotation = Quaternion.Euler(angle);
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
