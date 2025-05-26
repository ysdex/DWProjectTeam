using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseDown_SwitchScene : MonoBehaviour
{
    [Tooltip("전환할 씬 이름을 입력하세요 (예: Library_1st)")]
    public string sceneName;

    void OnMouseDown()
    {
        // PlayerHealth 오브젝트 찾기
        var playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            // maxHealth는 인스턴스 변수이므로 playerHealth.maxHealth로 접근
            PlayerHealth.currentHealth = playerHealth.maxHealth;
            Object.Destroy(playerHealth.gameObject);
        }

        // GameManager 오브젝트 삭제
        var gm = Object.FindFirstObjectByType<GameManager>();
        if (gm != null)
            Object.Destroy(gm.gameObject);

        // HealthBar 오브젝트 삭제 (태그 사용)
        var healthBar = GameObject.FindWithTag("HealthBar");
        if (healthBar != null)
            Object.Destroy(healthBar);

        // (선택) 타임스케일 초기화
        Time.timeScale = 1f;

        // 씬 전환
        SceneManager.LoadScene(sceneName);
    }
}
