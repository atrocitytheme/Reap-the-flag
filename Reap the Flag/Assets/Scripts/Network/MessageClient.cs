using LiteNetLib;
using UnityEngine;
using Newtonsoft.Json;
public class MessageClient : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("start!");
        EventBasedNetListener listener = new EventBasedNetListener();
        NetManager client = new NetManager(listener);
        client.Start();
        TestModel model = new TestModel { IP="1,2,3,4,5", CommandType=0};
        client.Connect("10.7.8.185", 9956, JsonConvert.SerializeObject(model));
        listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            Debug.Log("we got: {0} " + dataReader.GetString(100));
        };

        Debug.Log(100 >> 8);
    }
    private void FixedUpdate()
    {
        
    }
}
