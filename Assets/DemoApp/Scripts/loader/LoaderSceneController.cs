using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace DemoApplication
{
  public class LoaderSceneController : MonoBehaviour
	{
    SpriteRenderer background;
		SpriteRenderer coins;
		SpriteRenderer spriteadsLogo;
		SpriteRenderer unityadslogo;
		SpriteRenderer startButton;
		GUIText coinsCounter;


    float worldScreenHeight;
    float worldScreenWidth;
    float additionalScale = 0.5f;
    	
    void Awake () {
    	Screen.orientation = ScreenOrientation.AutoRotation;
    	Screen.autorotateToLandscapeLeft = true;
    	Screen.autorotateToLandscapeRight = true;
    	Screen.autorotateToPortrait = false;
    	Screen.autorotateToPortraitUpsideDown = false;
    }

    void Start () {
			coins = GameObject.Find(@"coins").GetComponent<SpriteRenderer>();
			background = GameObject.Find(@"background").GetComponent<SpriteRenderer>();
			spriteadsLogo = GameObject.Find(@"spaceads-logo").GetComponent<SpriteRenderer>();
			coinsCounter = GameObject.Find(@"coinsCounter").GetComponent<GUIText>();
			unityadslogo = GameObject.Find(@"unityadslogo").GetComponent<SpriteRenderer>();
			startButton = GameObject.Find(@"startButton").GetComponent<SpriteRenderer>();

			worldScreenHeight = Camera.main.orthographicSize * 2;
			worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
			coins.transform.position = new Vector3(worldScreenWidth/2 - coins.bounds.size.x, worldScreenHeight/2 - coins.bounds.size.y, coins.transform.position.z);
			unityadslogo.transform.position = new Vector3(0, worldScreenHeight/2 - unityadslogo.bounds.size.y, unityadslogo.transform.position.z);
			spriteadsLogo.transform.position = new Vector3(0, unityadslogo.transform.position.y - unityadslogo.bounds.size.y, spriteadsLogo.transform.position.z);
			startButton.transform.position = new Vector3(0, spriteadsLogo.transform.position.y - spriteadsLogo.bounds.size.y*2, startButton.transform.position.z);
			SharedData.coinsCount = 0;
			coinsCounter.text = SharedData.coinsCount.ToString();
			coinsCounter.transform.position = new Vector3(1 - coinsCounter.GetScreenRect().width/Screen.width - 0.06f,
			                                              0.98f, 
			                                              0);
    }
    
    void Update () {
      if (additionalScale > 0.1f) {
				additionalScale -= 0.05f * Time.deltaTime;
				float x = background.sprite.bounds.size.x;
				float y = background.sprite.bounds.size.y;
				float aspect = (float)x/(float)y;
				float finalHeight = (float)worldScreenWidth/(float)aspect;
				background.transform.localScale = new Vector3(additionalScale + worldScreenWidth/x, 
				                                              additionalScale + finalHeight/y, 
				                                              1);
			}
    }
	}
}

