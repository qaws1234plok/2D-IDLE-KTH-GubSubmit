using UnityEngine;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    public void Initialize(float damage)
    {
        damageText.text = damage.ToString();
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float duration = 1.0f;
        float elapsed = 0.0f;
        Color originalColor = damageText.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            damageText.color = Color.Lerp(originalColor, Color.clear, elapsed / duration);
            transform.Translate(Vector3.up * Time.deltaTime); // �ؽ�Ʈ�� ���� �̵�
            yield return null;
        }

        Destroy(gameObject);
    }
}