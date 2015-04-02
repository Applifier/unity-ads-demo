using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Button))]
public class GetCoinsButton : MonoBehaviour 
{
	public string zoneID;

	public float rewardCooldown = 300f;
	public int rewardCoinAmount = 250;

	public bool interactable;
	
	private Button _button;

	private static DateTime _rewardCooldownTime;
	
#if UNITY_IOS || UNITY_ANDROID
	void Start ()
	{
		_button = GetComponent<Button>();
		_button.interactable = false;

		_rewardCooldownTime = GetRewardCooldownTime();
	}

	void Update ()
	{
		if (interactable) _button.interactable = IsReady();
	}

	private bool IsReady ()
	{
		if (DateTime.Compare(DateTime.UtcNow,_rewardCooldownTime) > 0)
		{
			return UnityAdsHelper.IsReady(zoneID);
		}
		else return false;
	}
	
	public void ShowAd ()
	{
		UnityAdsHelper.ShowAd(zoneID,null,RewardUserAndUpdateCooldownTime);
	}

	private void RewardUserAndUpdateCooldownTime ()
	{
		Debug.Log("Granting the user a reward...");

		Inventory.AddCoins(rewardCoinAmount);
		
		SetRewardCooldownTime(DateTime.UtcNow.AddSeconds(rewardCooldown));

		Debug.Log(string.Format("User was rewarded. Next rewarded ad is available in {0} seconds.",rewardCooldown));
	}
#else
	public void ShowAd ()
	{
		Debug.LogError("Failed to show ad. Unity Ads is not supported under the current build platform.");
	}
#endif

	public static DateTime GetRewardCooldownTime ()
	{
		if (object.Equals(_rewardCooldownTime,default(DateTime)))
		{
			if (PlayerPrefs.HasKey(Constants.KEY_REWARD_COOLDOWN_TIME))
			{
				_rewardCooldownTime = DateTime.Parse(PlayerPrefs.GetString(Constants.KEY_REWARD_COOLDOWN_TIME));
			}
			else _rewardCooldownTime = DateTime.UtcNow;
		}

		return _rewardCooldownTime;
	}
	
	public static void SetRewardCooldownTime (DateTime dateTime)
	{
		_rewardCooldownTime = dateTime;
		PlayerPrefs.SetString(Constants.KEY_REWARD_COOLDOWN_TIME,dateTime.ToString());
	}
	
	public static void ResetRewardCooldownTime ()
	{
		SetRewardCooldownTime(DateTime.UtcNow);
	}
}
