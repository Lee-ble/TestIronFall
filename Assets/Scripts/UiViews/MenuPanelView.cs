using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPanelView : MonoBehaviour
{
	[SerializeField] private Button playButton;
	[SerializeField] private Button shopButton;
	[SerializeField] private Button exitButton;

	[SerializeField] private GameObject shopPanel;
	[SerializeField] private GameObject playerShopObject;

	private void Start()
	{
		playButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
		shopButton.onClick.AddListener(() => { shopPanel.SetActive(true); gameObject.SetActive(false); playerShopObject.SetActive(true);  });
		exitButton.onClick.AddListener(() => { Application.Quit(); });
	}
}
