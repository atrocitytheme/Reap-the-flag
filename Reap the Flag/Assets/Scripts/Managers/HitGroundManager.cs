using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitGroundManager : MonoBehaviour, Damagable
{
    public GameObject particleEffects;
    List<GameObject> objects = new List<GameObject>();
    // Start is called before the first frame update
    public void TakeDamage(int amount, Vector3 hitPoint) {
        GameObject particle = Instantiate(particleEffects, hitPoint, Quaternion.identity) as GameObject;
        
        particle.GetComponent<ParticleSystem>().Play();
        objects.Add(particle);
    }
    // TODO: revise this to promise
    private void Update()
    {
        objects.ForEach((d) => {
            if (!d.GetComponent<ParticleSystem>().isPlaying) {
                Destroy(d);
            }
        });
        objects.RemoveAll((d) => {
            return !d.GetComponent<ParticleSystem>().isPlaying;
        });
    }

    public bool IsDead() {
        return false;
    }

    public void TakeDamage(int amount, Vector3 hitPoint, TestModel model) {
        TakeDamage(amount, hitPoint);
    }
}
