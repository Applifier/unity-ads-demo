using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinCounter : MonoBehaviour 
{
	public Text counterText;

	public static int _count;

	void Start ()
	{
		Refresh();
	}

	void Update ()
	{
		if (counterText != null) counterText.text = _count.ToString();
	}

	public static void Refresh ()
	{
		if (PlayerPrefs.HasKey(Constants.KEY_COINCOUNT))
		{
			_count = PlayerPrefs.GetInt(Constants.KEY_COINCOUNT);
		}
		else SpaceAdsDemo.Instance.ResetCoinCount();
	}
}
