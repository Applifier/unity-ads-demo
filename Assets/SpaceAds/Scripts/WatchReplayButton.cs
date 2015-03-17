using UnityEngine;
using System.Collections;

public class WatchReplayButton : MonoBehaviour 
{
	public void ShowEveryplayModal()
	{
#if EVERYPLAY_IPHONE || EVERYPLAY_ANDROID
		Everyplay.ShowSharingModal();
#endif
	}
}
