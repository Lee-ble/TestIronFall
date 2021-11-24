using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTargetController : MonoBehaviour
{
    [SerializeField] bool getLowest;
    [SerializeField] float speed;
    private float _previousY = 10000f;
    private PlayerManager _simplePoolInstance;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.Instance != null)
		{
            _simplePoolInstance = PlayerManager.Instance;
        }
        
    }
	/*private void OnEnable()
	{
        transform.DOMoveY(0, speed).SetSpeedBased(true).SetEase(Ease.Linear);
	}
	*/
	private void Update()
	{
        if (_simplePoolInstance.GetPlayersAmount() > 0)
        {
            if (!getLowest)
            {
                if (_previousY > _simplePoolInstance.GetPlayersAverageY())
                {
                    transform.position = new Vector3(transform.position.x, _simplePoolInstance.GetPlayersAverageY(), transform.position.z);
                    _previousY = _simplePoolInstance.GetPlayersAverageY();
                }
            }
            else
            {
                if (_previousY > _simplePoolInstance.GetLowestPlayerY())
                {
                    transform.position = new Vector3(transform.position.x, _simplePoolInstance.GetLowestPlayerY(), transform.position.z);
                    _previousY = _simplePoolInstance.GetLowestPlayerY();
                }
            }
        }
    }

    private void CameraCoroutine()
	{

	}
}
