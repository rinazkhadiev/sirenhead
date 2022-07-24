using UnityEngine;

public class AnalyticsEventManager : MonoBehaviour
{
    public void OnEvent(string Event)
    {
	AppMetrica.Instance.ReportEvent(Event);
    }
}
