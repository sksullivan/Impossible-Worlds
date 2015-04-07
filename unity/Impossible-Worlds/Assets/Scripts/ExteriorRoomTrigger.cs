using UnityEngine;
using System.Collections;

public class ExteriorRoomTrigger : MonoBehaviour
{
    string room;
    RoomDisplayManager roomDisplayManager;

    // Use this for initialization
    void Start()
    {
        room = gameObject.transform.parent.name;
        GameObject roomDisplayManagerObject = GameObject.Find("RoomDisplayManager");
        roomDisplayManager = roomDisplayManagerObject.GetComponent<RoomDisplayManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        roomDisplayManager.playerEnteredExteriorTrigger(room);
    }

    void OnTriggerExit(Collider collider)
    {
        roomDisplayManager.playerExitedExteriorTrigger(room);
    }
}
