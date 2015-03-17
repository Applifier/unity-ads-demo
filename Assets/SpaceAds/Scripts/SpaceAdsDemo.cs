using UnityEngine;
using System.Collections;

public class SpaceAdsDemo : MonoBehaviour 
{
	public int initialCoinCount = 0;

	private static SpaceAdsDemo _instance;
	public static SpaceAdsDemo Instance { get { return GetInstance(); }}
	public static SpaceAdsDemo GetInstance ()
	{
		if (_instance == null)
		{
			GameObject gO = GameObject.Find("SpaceAdsDemo");
			if (gO == null) gO = new GameObject("SpaceAdsDemo");
			_instance = gO.AddComponent<SpaceAdsDemo>();
		}
		return _instance;
	}

	void Awake ()
	{
		if (_instance == null) _instance = this;
		else Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		if (Debug.isDebugBuild) ResetCoinCount();
	}

	public void ResetCoinCount ()
	{
		Inventory.SetCoins(initialCoinCount);
		Debug.Log(string.Format("Coin count reset to {0}.",initialCoinCount));
	}
}
