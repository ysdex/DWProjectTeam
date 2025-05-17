using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public string roomName = "�⺻ �� �̸�";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter ȣ���: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ������, �� �̸� ǥ��: " + roomName);
            RoomNameDisplay.Instance.ShowRoomName(roomName);
        }
    }
}
