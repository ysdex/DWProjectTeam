using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // GameOver 씬으로 전환
        SceneManager.LoadScene("GameOver");
        // 또는 GameOver UI 활성화 등 원하는 방식으로 처리
    }

    // (선택) 체력바 UI 업데이트 등 추가 가능
    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}
