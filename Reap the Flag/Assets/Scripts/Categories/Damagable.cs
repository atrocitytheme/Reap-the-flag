using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damagable
{
    // Start is called before the first frame update
    void TakeDamage(int amount, Vector3 hitPoint);
    bool IsDead();
}
