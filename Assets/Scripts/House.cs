using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void Start()
    {
        _alarm = GetComponentInChildren<Alarm>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_alarm is not null)
            _alarm.TurnOn();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(_alarm is not null)
            _alarm.TurnOff();
    }
}
