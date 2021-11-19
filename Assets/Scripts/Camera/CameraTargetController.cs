using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTargetController : MonoBehaviour
{
    private float _previousY;

    private PlayerManager _simplePoolInstance;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.Instance != null)
		{
            _simplePoolInstance = PlayerManager.Instance;
        }
        
    }

	private void Update()
	{
        if (_simplePoolInstance.GetPlayersAmount() > 0)
        {
            if (_previousY > _simplePoolInstance.GetLowestPlayerY())
                //transform.DOMove(new Vector3(transform.position.x, _simplePoolInstance.GetLowestPlayerY() - 0.2f, transform.position.z), Time.deltaTime);
                transform.position =new Vector3(transform.position.x, _simplePoolInstance.GetLowestPlayerY() - 0.2f, transform.position.z);
            _previousY = _simplePoolInstance.GetPlayersAverageY();
        }
    }
}
