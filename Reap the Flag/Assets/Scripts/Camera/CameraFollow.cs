using UnityEngine;
using System.Collections;

namespace PlayerComponent
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Transform rotationTarget;
        public float tangent = 5f;        


        Vector3 offset;
        Quaternion rotationOffset;

        void Start ()
        {
            // Calculate the initial offset.
            offset = transform.position - target.position;
            rotationOffset = transform.rotation * Quaternion.Inverse(rotationTarget.rotation);
        }


        void FixedUpdate ()
        {
            Vector3 cameraPos = target.position + offset;

            // follow the player with curve level
            transform.position = Vector3.Lerp(transform.position, cameraPos, tangent * Time.deltaTime);
            transform.rotation = rotationTarget.rotation * rotationOffset;
        }
    }
}