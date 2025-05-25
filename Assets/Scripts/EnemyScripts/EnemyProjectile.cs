using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	private	MovementRigidbody2D	movement;
	private	Transform			target;
	private	float				damage;

	public void Setup(Transform target, float damage)
	{
		movement = GetComponent<MovementRigidbody2D>();

		this.target = target;
		this.damage = damage;
		
		movement.MoveTo((target.position - transform.position).normalized);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( collision.CompareTag("Player") )
		{
			collision.GetComponent<PlayerHealth>().TakeDamage(damage);

			Destroy(gameObject);
		}
	}
}

