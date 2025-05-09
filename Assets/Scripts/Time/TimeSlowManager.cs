using UnityEngine;

public class TimeSlowManager : MonoBehaviour
{
    [Range(0.01f, 1f)]
    [SerializeField] private float slowTimeScale = 0.2f;

    private float normalTimeScale = 1f;
    private bool isSlowed = false;

    public void EnableSlowTime()
    {
        if (isSlowed) return;

        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * slowTimeScale; // match physics update rate
        isSlowed = true;
    }

    public void DisableSlowTime()
    {
        if (!isSlowed) return;

        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f;
        isSlowed = false;
    }
}