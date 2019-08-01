using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OnlineIdentity : MonoBehaviour
{
    TestModel identity;

    public void RegisterIdentity(TestModel id) {
        this.identity = id;
    }

    public TestModel GetIdentity() {
        return identity;
    }
}
