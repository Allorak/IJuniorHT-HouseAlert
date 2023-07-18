using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour
{
	[SerializeField] private UnityEvent _playerEntered;
	[SerializeField] private UnityEvent _playerExited;

    private void OnTriggerEnter2D(Collider2D other)
    {
		if(other.TryGetComponent(out Player player))
			_playerEntered?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out Player player))
			_playerExited?.Invoke();
    }
}
