using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinCounter : MonoBehaviour 
{
	public Text counterText;

	void Start ()
	{
		OnCoinCountUpdated();

		SpaceAdsDemo.OnCoinCountUpdated += OnCoinCountUpdated;
	}

	void OnDestroy ()
	{
		SpaceAdsDemo.OnCoinCountUpdated -= OnCoinCountUpdated;
	}

	private void OnCoinCountUpdated ()
	{
		if (object.ReferenceEquals(counterText,null)) return;

		counterText.text = Inventory.GetCoinCount().ToString();
	}
}
