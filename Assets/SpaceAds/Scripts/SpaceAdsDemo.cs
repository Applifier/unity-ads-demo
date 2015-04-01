using System;
using UnityEngine;
using System.Collections;

public class SpaceAdsDemo : MonoBehaviour 
{
	public static Action<int> OnCoinsAddedAction;
	public static Action OnCoinCountUpdatedAction;

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
		if (Debug.isDebugBuild) 
		{
			Inventory.ResetCoinCount();
			GetCoinsButton.ResetRewardCooldownTime();
		}
	}

	public static void OnCoinsAdded (int amount)
	{
		if (!object.ReferenceEquals(OnCoinsAddedAction,null)) 
		{
			OnCoinsAddedAction(amount);
		}
	}

	public static void OnCoinCountUpdated ()
	{
		if (!object.ReferenceEquals(OnCoinCountUpdatedAction,null)) 
		{
			OnCoinCountUpdatedAction();
		}
	}
}