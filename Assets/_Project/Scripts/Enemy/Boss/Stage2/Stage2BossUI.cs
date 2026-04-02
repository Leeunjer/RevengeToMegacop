using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stage2 보스 전용 UI. 상단에 HP바 + 퍼센트 텍스트를 표시한다.
/// BossEnemy의 OnHpChanged, OnDeath 이벤트를 구독하여 자동 갱신.
/// BossUI와 별개로 동작하며, Stage2Boss에만 사용한다.
/// </summary>
public class Stage2BossUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Image hpFill;
    [SerializeField] private Text hpPercentText;
    [SerializeField] private Text bossNameText;

    private BossEnemy boss;

    /// <summary>
    /// 보스 전투 시작 시 호출. 이벤트를 구독하고 UI를 표시한다.
    /// </summary>
    public void Initialize(BossEnemy boss)
    {
        this.boss = boss;
        boss.OnHpChanged += UpdateUI;
        boss.OnDeath += OnBossDied;

        if (bossNameText != null)
        {
            bossNameText.text = boss.gameObject.name;
        }

        UpdateUI(boss.HpRatio);
        Show();
    }

    public void Show()
    {
        if (container != null) container.SetActive(true);
    }

    public void Hide()
    {
        if (container != null) container.SetActive(false);
    }

    private void UpdateUI(float ratio)
    {
        // HP바 채우기
        if (hpFill != null)
        {
            hpFill.fillAmount = ratio;
        }

        // 퍼센트 텍스트
        if (hpPercentText != null)
        {
            int percent = Mathf.RoundToInt(ratio * 100f);
            hpPercentText.text = percent + "%";
        }
    }

    private void OnBossDied(GameObject obj)
    {
        Hide();
    }

    void OnDestroy()
    {
        if (boss != null)
        {
            boss.OnHpChanged -= UpdateUI;
            boss.OnDeath -= OnBossDied;
        }
    }
}
