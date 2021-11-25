using Cinemachine;
using UnityEngine;

public class CinemachineSetup : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float normalOrtho;
    [SerializeField] private float sixteenTenOrtho;
    void Start()
    {
        if ((Screen.height / Screen.width).Equals(16/10))
		{
            virtualCamera.m_Lens.OrthographicSize = sixteenTenOrtho;
		}
        else
		{
            virtualCamera.m_Lens.OrthographicSize = normalOrtho;
        }
    }
}
