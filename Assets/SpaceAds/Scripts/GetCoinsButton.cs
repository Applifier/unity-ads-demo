using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

[RequireComponent (typeof (Button))]
public class GetCoinsButton : MonoBehaviour 
{
	public string zoneID;

	public float rewardCooldown = 300f;
	public int rewardCoinAmount = 250;

	public bool interactable;
	
	private Button _button;

	private static DateTime _rewardCooldownTime;
	
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
			return Advertisement.IsReady(zoneID);
		}
		else return false;
	}
	
	public void ShowAd ()
	{
		Advertisement.Show(zoneID);
	}

	private void RewardUserAndUpdateCooldownTime ()
	{
		Debug.Log("Granting the user a reward...");

		Inventory.AddCoins(rewardCoinAmount);
		
		SetRewardCooldownTime(DateTime.UtcNow.AddSeconds(rewardCooldown));

		Debug.Log(string.Format("User was rewarded. Next rewarded ad is available in {0} seconds.",rewardCooldown));
	}

	//--- Reward Cooldown Methods

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
