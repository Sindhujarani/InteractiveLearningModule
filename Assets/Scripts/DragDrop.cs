using UnityEngine;
using TMPro;

public class DragDrop : MonoBehaviour
{
    public string correctSlotName = "SlotApple";
    public TextMeshProUGUI feedbackText;

    public static int correctlyPlacedCount = 0;
    private const int totalObjects = 5;

    private Vector3 originalPosition;
    private bool isDragging = false;
    private bool placedCorrectly = false;
    private float zDepth;

    void Start()
    {
        originalPosition = transform.position;
        zDepth = Camera.main.WorldToScreenPoint(transform.position).z;
    }

    void OnMouseDown()
    {
        if (placedCorrectly) return;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDepth);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (!placedCorrectly)
        {
            transform.position = originalPosition;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (placedCorrectly) return;

        if (other.gameObject.name == correctSlotName)
        {
            // Snap to correct slot
            isDragging = false;
            transform.position = other.transform.position;
            placedCorrectly = true;

            // Increment counter
            correctlyPlacedCount++;

            // Check if all 5 placed — start quiz with delay
            if (correctlyPlacedCount >= totalObjects)
            {
                StartCoroutine(StartQuizWithDelay());
            }

            ShowMessage("Correct!", Color.green);
        }
        else
        {
            transform.position = originalPosition;
            ShowMessage("Try Again!", Color.red);
        }
    }

    System.Collections.IEnumerator StartQuizWithDelay()
    {
        // Wait 2 seconds before starting quiz
        yield return new WaitForSeconds(2f);
        QuizManager.Instance.StartQuiz();
    }

    void ShowMessage(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        StopCoroutine("ClearMessageAfterDelay");
        StartCoroutine("ClearMessageAfterDelay");
    }

    System.Collections.IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        feedbackText.text = "";
    }

    public void ResetObject()
    {
        placedCorrectly = false;
        transform.position = originalPosition;
    }
}