using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 10f;

    public float Speed { get; private set; }

    private void Start()
    {
        Speed = minSpeed;
        speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
    }

    public void MoveTo(Vector3 position, ICommandListener listener)
    {
        StartCoroutine(MoveCoroutine(position, listener));
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition, ICommandListener listener)
    {
        Vector3 startPosition = gameObject.transform.position;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);

        while (gameObject.transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * Speed;
            float fractionOfJourney = distanceCovered / journeyLength;

            gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

            yield return null;
        }

        // Достигли целевой позиции
        listener.OnCommandCompleted();
    }

    private void OnSpeedSliderValueChanged(float value)
    {
        Speed = Mathf.Lerp(minSpeed, maxSpeed, value);
        Debug.Log(Speed);
    }
}