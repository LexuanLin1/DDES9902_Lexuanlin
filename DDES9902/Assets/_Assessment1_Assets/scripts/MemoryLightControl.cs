using UnityEngine;
using System.Collections;

public class MemoryLightControl : MonoBehaviour
{
    [Header("Main Light Reference")]
    public Light mainLight;   // 拖入 Directional Light 或场景主灯

    [Header("Light Transition Settings")]
    public float transitionSpeed = 1.0f; // 灯光渐变速度（越大越快）

    private Coroutine transitionRoutine;

    // ===== 颜色预设 =====
    private Color calmColor = new Color(0.4f, 0.6f, 1f);   // 蓝色（平静）
    private Color happyColor = new Color(1f, 0.95f, 0.6f); // 黄色（快乐）
    private Color sadColor = new Color(1f, 0.6f, 0.7f);    // 粉红（忧伤）

    // ===== 外部调用方法 =====
    public void SetCalm()
    {
        StartTransition(calmColor);
        Debug.Log("Light changed to CALM mode 💙");
    }

    public void SetHappy()
    {
        StartTransition(happyColor);
        Debug.Log("Light changed to HAPPY mode 💛");
    }

    public void SetSad()
    {
        StartTransition(sadColor);
        Debug.Log("Light changed to SAD mode 💗");
    }

    // ===== 渐变逻辑 =====
    private void StartTransition(Color targetColor)
    {
        if (mainLight == null)
        {
            Debug.LogWarning("Main Light is not assigned!");
            return;
        }

        if (transitionRoutine != null)
            StopCoroutine(transitionRoutine);

        transitionRoutine = StartCoroutine(FadeToColor(targetColor));
    }

    private IEnumerator FadeToColor(Color target)
    {
        Color start = mainLight.color;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;
            mainLight.color = Color.Lerp(start, target, t);
            yield return null;
        }
    }
}

