using UnityEngine;

public class boxPortal : MonoBehaviour
{
    public bool isEntered = false;
    public Transform player;
    public Transform destination;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 editedPosition = new Vector3(destination.position.x, destination.position.y - 1, float.Parse("-0.1"));
        player.position = editedPosition;
        Debug.Log("Darn it");
    }


}
