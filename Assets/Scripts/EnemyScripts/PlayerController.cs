using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private	MovementRigidbody2D	movement2D;

	private void Awake()
	{
		movement2D = GetComponent<MovementRigidbody2D>();
	}

	private void Update()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		// �÷��̾� �̵�
		movement2D.MoveTo(new Vector3(x, y, 0));
	}
}

