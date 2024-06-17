using UnityEngine;
using TMPro;
using System.Collections;
using System.Numerics;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    public void Initialize(BigInteger damage)
    {
        damageText.text = PlayerCharacter.Instance.Character.GetFormattedAttackPower();
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
            transform.Translate(UnityEngine.Vector3.up * Time.deltaTime); // 텍스트가 위로 이동
            yield return null;
        }

        Destroy(gameObject);
    }
}
