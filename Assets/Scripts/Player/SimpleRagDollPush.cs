using UnityEngine;

public class SimpleRagDollPush : MonoBehaviour
{
    [SerializeField] private Rigidbody dollRigidbody;
    [SerializeField] private float forceSidePower = 300f;
    [SerializeField] private float forceUpPower = 70f;

	private void Start()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.PlayerSwiped += OnSwipe;
		}
	}

	private void OnSwipe(int direction)
	{
		if (direction == -1)
		{
			dollRigidbody.AddForce(Vector3.left * forceSidePower, ForceMode.Impulse);
			dollRigidbody.AddForce(Vector3.left * forceUpPower, ForceMode.Impulse);
		}
		else if (direction == 1)
		{
			dollRigidbody.AddForce(Vector3.right * forceSidePower, ForceMode.Impulse);
			dollRigidbody.AddForce(Vector3.up * forceUpPower, ForceMode.Impulse);
		}
	}

	private void OnDestroy()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.PlayerSwiped -= OnSwipe;
		}
	}
}
