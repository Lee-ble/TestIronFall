using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    private PlayerManager _simplePoolInstance;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.Instance != null)
		{
            _simplePoolInstance = PlayerManager.Instance;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (_simplePoolInstance.GetPlayersAmount() > 0)
		{
            transform.position = new Vector3(transform.position.x, _simplePoolInstance.GetPlayersAverageY(), transform.position.z);
		}
    }
}
