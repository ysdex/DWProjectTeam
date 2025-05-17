using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public string roomName = "기본 방 이름";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter 호출됨: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player 감지됨, 방 이름 표시: " + roomName);
            RoomNameDisplay.Instance.ShowRoomName(roomName);
        }
    }
}
