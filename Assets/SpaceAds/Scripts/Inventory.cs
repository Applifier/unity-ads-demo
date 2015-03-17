using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour 
{
	public static void AddCoins (int amount)
	{
		int totalAmount = GetCoins() + amount;
		SetCoins(totalAmount);

		Debug.Log(string.Format("Added {0} coins to the inventory. Total is now {1} coins.",amount,totalAmount));
	}

	public static int GetCoins ()
	{
		int amount = 0;

		if (PlayerPrefs.HasKey(Constants.KEY_COINCOUNT))
		{
			amount = PlayerPrefs.GetInt(Constants.KEY_COINCOUNT);
		}

		return amount;
	}

	public static void SetCoins (int amount)
	{
		PlayerPrefs.SetInt(Constants.KEY_COINCOUNT,amount);
		CoinCounter.Refresh();
	}
}
