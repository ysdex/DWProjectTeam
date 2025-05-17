using UnityEngine;
using TMPro; // TextMeshPro ��� ��
using UnityEngine.UI; // �Ϲ� Text ��� ��

public class RoomNameDisplay : MonoBehaviour
{
    public static RoomNameDisplay Instance;

    public TextMeshProUGUI roomNameText; // �Ǵ� public Text roomNameText;

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
