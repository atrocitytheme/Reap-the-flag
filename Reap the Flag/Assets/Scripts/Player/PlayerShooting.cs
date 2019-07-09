using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace PlayerComponent
{
    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenBullets = 0.15f;        // The time between each shot.
        public float range = 10f;                      // The distance the gun can fire.
        public Image shotImage;

        float timer;                                    // A timer to determine when to fire.
        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        ParticleSystem gunParticles;                    // Reference to the particle system.
        LineRenderer gunLine;                           // Reference to the line renderer.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
		public Light faceLight;								// Duh
        float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
        Color defaultColor = new Color(0f, 0f, 0f, 0f);
        Color shotColor = new Color(0f, 1f, 0f, 0.5f);
        void Awake ()
        {
            shootableMask = LayerMask.GetMask ("Shootable");
            gunParticles = GetComponent<ParticleSystem> ();
            gunLine = GetComponent <LineRenderer> ();
            gunAudio = GetComponent<AudioSource> ();
            gunLight = GetComponent<Light> ();
        }


        void Update ()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the Fire1 button is being press and it's time to fire...
			if(Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                // ... shoot the gun.
                Shoot ();
            }

            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if(timer >= timeBetweenBullets * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects ();
            }

            // restore the shot color
            shotImage.color = Color.Lerp(shotImage.color, defaultColor, Time.deltaTime * 2);
        }


        public void DisableEffects ()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
			faceLight.enabled = false;
            gunLight.enabled = false;
        }


        void Shoot ()
        {
            timer = 0f;
            gunAudio.Play ();
            // enable visual effects
            gunLight.enabled = true;
			faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop ();
            gunParticles.Play ();

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition (0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
            {
                
                // Spot the component
                var components = shootHit.collider.GetComponents<MonoBehaviour>();
                var componentList = new List<MonoBehaviour>(components);
                var targetMonos = componentList.Where((d) => d is Damagable).ToList();
                
                foreach (MonoBehaviour behavior in targetMonos) {
                    var target = (Damagable) behavior;
                    if (!target.IsDead())
                    {
                        target.TakeDamage(damagePerShot, shootHit.point);
                        shotImage.color = shotColor;
                    }
                }
                gunLine.SetPosition (1, shootHit.point);
            }

            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}