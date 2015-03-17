using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_IOS || UNITY_ANDROID
using UnityEngine.Advertisements;
#endif

[RequireComponent (typeof (Button))]
public class GetCoinsButton : MonoBehaviour 
{
	public string zoneID;

	public float rewardCooldown = 300f;
	public int rewardCoinAmount = 250;
	public RewardNotice rewardNotice;
	
	private static float _timeUntilInteractable = 0f;

	private Button _button;

#if UNITY_IOS || UNITY_ANDROID
	void Start ()
	{
		_button = GetComponent<Button>();
		_button.interactable = false;
	}

	void Update ()
	{
		_button.interactable = IsReady();
	}

	private bool IsReady ()
	{
		if (Time.time > _timeUntilInteractable)
		{
			if (string.IsNullOrEmpty(zoneID)) zoneID = null;
			
			return Advertisement.isReady(zoneID);
		}
		
		return false;
	}
	
	public void ShowAd ()
	{
		if (string.IsNullOrEmpty(zoneID)) zoneID = null;
		
		if (Advertisement.isReady(zoneID))
		{
			ShowOptions options = new ShowOptions();
			options.resultCallback = HandleShowResultWithReward;
			options.pause = true;
			
			Advertisement.Show(zoneID,options);
		}
		else 
		{
			Debug.LogWarning(string.Format("Unable to show ad. The ad placement zone ($0) is not ready.",
			                               zoneID == null ? "default" : zoneID));
		}
	}
	
	private void HandleShowResultWithReward (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown. Granting the user a reward...");

			Inventory.AddCoins(rewardCoinAmount);

			rewardNotice.SetAmount(rewardCoinAmount);
			rewardNotice.Show();

			_timeUntilInteractable = Time.time + rewardCooldown;

			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
#else
	public void ShowAd ()
	{
		Debug.LogError("Failed to show ad. Unity Ads is not supported under the current build platform.");
	}
#endif
}
