using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Record data of the player, damaging taking, enemy tracing and so on.
/// </summary>
public class Player : MonoBehaviour
{
    bool damageLocked = false;
    List<TestModel> damageDealers = new List<TestModel>();
    /// <summary>
    /// Damaged by whom
    /// </summary>
    public void DamagedBy(TestModel model) {
        if (!damageLocked)
        damageDealers.Add(model);
    }

    public void SetLockState(bool lockState) {
        damageLocked = lockState;
    }

    public TestModel GetLast() {
        if (damageDealers.Count == 0) return null;

        return damageDealers[damageDealers.Count - 1];
    }
}
