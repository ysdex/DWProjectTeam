using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 1000f; // Inspector에서 설정
    public static float currentHealth; // 씬 전환에도 값 유지

    private Slider healthBar;

    private void Awake()
    {
        // 중복 생성 방지 (싱글톤)
        if (FindObjectsOfType<PlayerHealth>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // maxHealth 변경 시 currentHealth 동기화
        if (currentHealth <= 0 || currentHealth > maxHealth)
            currentHealth = maxHealth;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 3개 게임맵에서만 HealthBar 연결
        if (scene.name == "Library_1st" || scene.name == "Wonhyo_1st" || scene.name == "Jayeon_1st")
        {
            healthBar = FindFirstObjectByType<Slider>();
            if (healthBar != null)
            {
                healthBar.maxValue = maxHealth;
                healthBar.value = currentHealth;
            }
        }
        else
        {
            healthBar = null;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;
    }
}
