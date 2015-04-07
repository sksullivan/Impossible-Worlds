using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Keeps track of each room, displaying or hiding the contents of each room appropriately based on the location of the player.
public class RoomDisplayManager : MonoBehaviour {

    public GameObject player;

    private Dictionary<string,bool> roomTransitioningStatus;
    private List<string> rooms;
    public string playerCurrentRoom;

	// Use this for initialization
	void Start () {
        rooms = new List<string>();
        roomTransitioningStatus = new Dictionary<string, bool>();
        GameObject atrium = GameObject.Find("DynamicRooms");
        for (int i = 0; i < atrium.transform.childCount; i++)
        {
            if (atrium.transform.GetChild(i).gameObject.activeSelf)
            {
                rooms.Add(atrium.transform.GetChild(i).name);
                roomTransitioningStatus.Add(atrium.transform.GetChild(i).name, false);
            }
        }
        playerCurrentRoom = "";
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < rooms.Count; i++)
        {
            updateRoomStatus(GameObject.Find(rooms[i]));
        }
	}

    // Iterates over all rooms, and sets all of the room's objects to hidden or not based on the player's location.
    void updateRoomStatus(GameObject room)
    {
        if (!playerCurrentRoom.Equals("")) // If the player is in a room, display that room's objets and nothing else.
        {
            foreach (Transform child in room.transform)
            {
                if (child.name.Equals("Objects"))
                {
                    foreach (Transform roomObject in child)
                    {
                        roomObject.renderer.enabled = playerCurrentRoom.Equals(room.name);
                    }
                }
                if (child.name.Equals("DoorHider"))
                {
                    child.gameObject.SetActive(playerCurrentRoom.Equals(room.name));
                }
            }
        }
        else // Otherwise, do the fancy stuff and calulate what should be seen and what should not be seen.
        {
            Transform leftDoorEdge = room.transform.Find("LeftDoorEdge");
            Transform rightDoorEdge = room.transform.Find("RightDoorEdge");
            float anglePlayerToLeftEdge = angleBetween(player, leftDoorEdge.gameObject, 0);
            float anglePlayerToRightEdge = angleBetween(player, rightDoorEdge.gameObject, 0);
            foreach (Transform child in room.transform)
            {
                if (child.name.Equals("Objects"))
                {
                    foreach (Transform roomObject in child)
                    {
                        roomObject.renderer.enabled = shouldDisplayObject(roomObject, anglePlayerToLeftEdge, anglePlayerToRightEdge);
                    }
                }
            }
        }
    }

    // Based on the angle to the door's left & right edge, determine whether an object should be shown.
    bool shouldDisplayObject(Transform childObject, float anglePlayerToLeftEdge, float anglePlayerToRightEdge)
    {
        float angleToMe = angleBetween(player, childObject.gameObject, 0);
        float minAngleToMe = angleBetween(player, childObject.gameObject, 1);
        float maxAngleToMe = angleBetween(player, childObject.gameObject, 2);
        return minAngleToMe < anglePlayerToLeftEdge && maxAngleToMe > anglePlayerToRightEdge;
    }

    // Based on two game objects, get the angle from the first game object to the center, left edge or right edge of the second game object, in 2D space.
    // The first line of each case does an Algebra II-esque arctan calculation, then makes all the angles positive so we can easily make comparisons between them.
    float angleBetween(GameObject a, GameObject b, int type)
    {
        float angleRaw = 0;
        if (type == 0)
        {
            // General (center) angle
            angleRaw = Mathf.Atan((b.transform.position.z - a.transform.position.z) / (b.transform.position.x - a.transform.position.x));
            if (angleRaw < 0)
            {
                angleRaw += Mathf.PI;

            }
            if (b.transform.position.z < a.transform.position.z)
            {
                angleRaw += Mathf.PI;
            }
        }
        else if (type == 1)
        {
            // Min angle
            angleRaw = Mathf.Atan((b.renderer.bounds.max.z - a.transform.position.z) / (b.renderer.bounds.max.x - a.transform.position.x));
            if (angleRaw < 0)
            {
                angleRaw += Mathf.PI;

            }
            if (b.renderer.bounds.max.z < a.transform.position.z)
            {
                angleRaw += Mathf.PI;
            }
        }
        else if (type == 2)
        {
            // Max angle
            angleRaw = Mathf.Atan((b.renderer.bounds.min.z - a.transform.position.z) / (b.renderer.bounds.max.x - a.transform.position.x));
            if (angleRaw < 0)
            {
                angleRaw += Mathf.PI;

            }
            if (b.renderer.bounds.min.z < a.transform.position.z)
            {
                angleRaw += Mathf.PI;
            }
        }
        return angleRaw;
    }

    // These following methods just update the status of the player relative to each room. They're called by each room's front and back triggers.
    public void playerEnteredExteriorTrigger(string room)
    {
        if (playerCurrentRoom.Equals(room))
        {
            roomTransitioningStatus[room] = true;
        }
        else
        {

        }
    }

    public void playerExitedExteriorTrigger(string room)
    {
        if (!playerCurrentRoom.Equals(""))
        {
            roomTransitioningStatus[room] = false;
            playerCurrentRoom = "";
        }
        else
        {

        }
    }

    public void playerEnteredInteriorTrigger(string room)
    {
        if (playerCurrentRoom.Equals(room))
        {
            roomTransitioningStatus[room] = true;
        }
        else
        {

        }
    }

    public void playerExitedInteriorTrigger(string room)
    {
        if (playerCurrentRoom.Equals(room))
        {
            
        }
        else
        {
            roomTransitioningStatus[room] = false;
            playerCurrentRoom = room;
        }
    }
}
