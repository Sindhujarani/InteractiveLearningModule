using UnityEngine;
using TMPro;

public class ObjectClick : MonoBehaviour
{
    public Color clickColor = Color.red;
    public string objectName = "Apple";
    public TextMeshProUGUI nameText;

    private Vector3 originalScale;
    private Material objectMaterial;
    private AudioSource audioSource;

    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        objectMaterial.EnableKeyword("_EMISSION");

        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        objectMaterial.color = clickColor;
        audioSource.Play();
        nameText.text = objectName;

        StopAllCoroutines();
        StartCoroutine(GlowPulse());
    }

    System.Collections.IEnumerator GlowPulse()
    {
        Vector3 bigScale = originalScale * 1.2f;
        Color brightGlow = clickColor * 1.8f;
        Color steadyGlow = clickColor * 0.6f;

        // Scale up + glow up
        for (float t = 0; t < 1f; t += Time.deltaTime * 6f)
        {
            transform.localScale = Vector3.Lerp(originalScale, bigScale, t);
            objectMaterial.SetColor("_EmissionColor", Color.Lerp(Color.black, brightGlow, t));
            yield return null;
        }

        // Scale down + glow settles
        for (float t = 0; t < 1f; t += Time.deltaTime * 6f)
        {
            transform.localScale = Vector3.Lerp(bigScale, originalScale, t);
            objectMaterial.SetColor("_EmissionColor", Color.Lerp(brightGlow, steadyGlow, t));
            yield return null;
        }

        transform.localScale = originalScale;
        objectMaterial.SetColor("_EmissionColor", steadyGlow);
    }

}