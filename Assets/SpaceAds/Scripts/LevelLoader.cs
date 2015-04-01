using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour 
{
	private static bool _isLoading;
	public static bool isLoading { get { return _isLoading; }}

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

	void OnLevelWasLoaded (int level)
	{
		if (_isLoading)
		{
			_isLoading = false;

			Debug.Log("Level was loaded.");
		}
	}

	public void LoadLevel (string name) 
	{
		if (!_isLoading) 
		{
			if (!string.IsNullOrEmpty(name))
			{
				_isLoading = true;
				
				Debug.Log(string.Format("Loading level '{0}'...",name));
				
				Application.LoadLevel(name);
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
				_isLoading = true;
				
				Debug.Log(string.Format("Loading level at index {0}...",index));
				
				Application.LoadLevel(index);
			}
			else Debug.LogError("Failed to load level. Index is out of bounds."); 
		}
		else Debug.LogError("Failed to load level. Next level is still loading.");
	}
}
