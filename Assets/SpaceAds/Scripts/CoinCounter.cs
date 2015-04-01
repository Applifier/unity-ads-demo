using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinCounter : MonoBehaviour 
{
	public Text counterText;

	void Awake()
	{
		SpaceAdsDemo.OnCoinCountUpdatedAction += Refresh;
	}

	void OnDestroy ()
	{
		SpaceAdsDemo.OnCoinCountUpdatedAction -= Refresh;
	}

	void Start ()
	{
		Refresh();
	}

	private void Refresh ()
	{
		if (!object.ReferenceEquals(counterText,null))
		{
			counterText.text = Inventory.GetCoinCount().ToString();
		}
	}
}
