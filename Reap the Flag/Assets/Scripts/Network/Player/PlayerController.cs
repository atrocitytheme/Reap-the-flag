using UnityEngine;
namespace PlayerComponent
{
    [RequireComponent(typeof(InitializerTimer))]
    public class PlayerController : MonoBehaviour
    {
        public float speed = 6f;            // The speed that the player will move at.
        public float rotateSmooth = 6.0f;          // the smooth of rotation

        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        InitializerTimer timer;

        float h = 0; // the rotation direction of mouse x
        float v = 0; // the rotation direction of mouse y
        void Awake ()
        {
            anim = GetComponent <Animator> ();
            playerRigidbody = GetComponent <Rigidbody> ();
            timer = GetComponent<InitializerTimer>();
        }


        void FixedUpdate ()
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // Store the input axes.
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            // Move the player around the scene.
            Move (h, v);

            // Turn the player to face the mouse cursor.
            /*            if (timer.IsValid)
            */

            Turning ();

            // Animate the player.
            Animating (h, v);
        }


        void Move (float h, float v)
        {

            Vector3 forward = -h * transform.TransformDirection(Vector3.left);
            Vector3 side = v * transform.TransformDirection(Vector3.forward);

            // Set the movement vector based on the axis input.
            movement = forward + side;
            movement.y = 0;
            
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition(transform.position + movement);
        }


        void Turning ()
        {
           
            h += Input.GetAxis("Mouse X");
            v += Input.GetAxis("Mouse Y");
            transform.eulerAngles = (new Vector3(-v, h, 0) * rotateSmooth);
            
        }

        public void Turning(float h, float v) {
            transform.eulerAngles = (new Vector3(-v, h, 0) * rotateSmooth);
        }

        void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsWalking", walking);
        }
    }
}