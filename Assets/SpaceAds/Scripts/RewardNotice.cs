using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RewardNotice : MonoBehaviour 
{
	public Text text;
	public Animator animator;

	void Awake ()
	{
		SpaceAdsDemo.OnCoinsAddedAction += SetAmountAndShow;
	}

	void OnDestroy ()
	{
		SpaceAdsDemo.OnCoinsAddedAction -= SetAmountAndShow;
	}

	public void SetAmountAndShow (int amount)
	{
		string notice = text.text;
		text.text = string.Format(notice,amount);

		animator.Play("RewardNoticeVisible");
	}
}
