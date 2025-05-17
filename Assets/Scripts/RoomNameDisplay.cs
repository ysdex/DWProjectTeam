using UnityEngine;
using TMPro; // TextMeshPro 사용 시
using UnityEngine.UI; // 일반 Text 사용 시

public class RoomNameDisplay : MonoBehaviour
{
    public static RoomNameDisplay Instance;

    public TextMeshProUGUI roomNameText; // 또는 public Text roomNameText;

    private void Awake()
    {
        Instance = this;
        HideRoomName();
    }

    public void ShowRoomName(string name)
    {
        roomNameText.text = name;
        roomNameText.gameObject.SetActive(true);
    }

    public void HideRoomName()
    {
        roomNameText.gameObject.SetActive(false);
    }
}
