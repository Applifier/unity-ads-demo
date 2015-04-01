using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour 
{
	public static int initialCoinCount = 0;

	public static void AddCoins (int amount)
	{
		int totalAmount = GetCoinCount() + amount;
		SetCoinCount(totalAmount);

		SpaceAdsDemo.OnCoinsAdded(amount);

		Debug.Log(string.Format("Added {0} coins to the inventory. Total is now {1} coins.",amount,totalAmount));
	}

	public static int GetCoinCount ()
	{
		if (PlayerPrefs.HasKey(Constants.KEY_COINCOUNT))
		{
			return PlayerPrefs.GetInt(Constants.KEY_COINCOUNT);
		} 
		else return initialCoinCount;
	}

	public static void SetCoinCount (int amount)
	{
		PlayerPrefs.SetInt(Constants.KEY_COINCOUNT,amount);

		SpaceAdsDemo.OnCoinCountUpdated();
	}

	public static void ResetCoinCount ()
	{
		SetCoinCount(initialCoinCount);

		Debug.Log(string.Format("Coin count reset to {0}.",initialCoinCount));
	}
}
