using LiteNetLib;
using UnityEngine;
public class NetworkGreetings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start!");
        EventBasedNetListener listener = new EventBasedNetListener();
        NetManager client = new NetManager(listener);
        client.Start();
        client.Connect("10.7.8.185", 9956, "cut!");
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            Debug.Log("we got: {0} " + dataReader.GetString(100));
        };
    }
}
