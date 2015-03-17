using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour 
{
	public AnimationClip easeOutClip;

	private bool _isLoading;
	public bool isLoading { get { return _isLoading; }}

	private static LevelLoader _instance;
	public static LevelLoader Instance { get { return GetInstance(); }}
	public static LevelLoader GetInstance ()
	{
		if (_instance == null)
		{
			GameObject gO = GameObject.Find("LevelLoader");
			if (gO == null) gO = new GameObject("LevelLoader");
			_instance = gO.AddComponent<LevelLoader>();
		}
		return _instance;
	}
	
	void Awake ()
	{
		if (_instance == null) _instance = this;
		else Destroy(gameObject);		
	}

	public void LoadLevel (string name) 
	{
		if (!_isLoading) 
		{
			if (!string.IsNullOrEmpty(name))
			{
				StartCoroutine(EaseOutAndLoadLevel(name));
			}
			else Debug.LogError("Failed to load level. Name cannot be null or empty.");
		}
		else Debug.LogError("Failed to load level. Next level is still loading.");
	}
	
	public void LoadLevel (int index) 
	{
		if (!_isLoading)
		{
			if (index >= 0 && index < Application.levelCount)
			{
				StartCoroutine(EaseOutAndLoadLevel(index));
			}
			else Debug.LogError("Failed to load level. Index is out of bounds."); 
		}
		else Debug.LogError("Failed to load level. Next level is still loading.");
	}
	
	private IEnumerator EaseOutAndLoadLevel (string name)
	{
		_isLoading = true;
		
		Debug.Log(string.Format("Loading level '{0}'...",name));
		
		if (easeOutClip != null)
		{
			// TODO: Play ease out AnimationClip and yield while playing.
		}
		else Debug.LogWarning("Missing ease out animation clip for level loading. Continuing to load level now...");
		
		Application.LoadLevel(name);
		
		yield break;
	}

	private IEnumerator EaseOutAndLoadLevel (int index)
	{
		_isLoading = true;
		
		Debug.Log(string.Format("Loading level at index {0}...",index));
		
		if (easeOutClip != null)
		{
			// TODO: Play ease out AnimationClip and yield while playing.
		}
		else Debug.LogWarning("Missing ease out animation clip for level loading. Continuing to load level now...");

		Application.LoadLevel(index);
		
		yield break;
	}
}
