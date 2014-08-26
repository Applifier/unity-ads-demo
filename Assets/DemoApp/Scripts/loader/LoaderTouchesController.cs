using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

namespace DemoApplication
{
	public class LoaderTouchesController : MonoBehaviour {
		bool loadLevelAnimationStarted = false;
		bool getMoreCoinsAnimationPlayed = false;
		SpriteRenderer getMoreCoinsRenderer;
		SpriteRenderer[] objects;
		GUIText coinsCounter;
		bool adsShowing = false;
    bool showedStartAd = false;

#if UNITY_EDITOR
		string gameId = @"14850";
#elif UNITY_ANDROID
    string gameId = @"14851";
#elif UNITY_IOS
    string gameId = @"14850";
#else 
    string gameId = @"14850";
#endif

    void Start () {
      Advertisement.Initialize(gameId);
			objects = GameObject.FindObjectsOfType<SpriteRenderer>();
			coinsCounter = GameObject.Find(@"coinsCounter").GetComponent<GUIText>();
			getMoreCoinsRenderer = GameObject.Find(@"getMoreCoins").GetComponent<SpriteRenderer>();
		}

		void Update () {
			if (Advertisement.isReady(@"incentivizedZone")) {
				if (!getMoreCoinsAnimationPlayed) {
					GameObject.Find(@"getMoreCoins").animation.Play();
					getMoreCoinsAnimationPlayed = true;
				}
			} else {
				getMoreCoinsAnimationPlayed = false;
				if (getMoreCoinsRenderer.color.a != 0)
				getMoreCoinsRenderer.color = new Color(getMoreCoinsRenderer.color.r, getMoreCoinsRenderer.color.g, getMoreCoinsRenderer.color.b, 0);
			}

			if(Advertisement.isReady("gameStartZone") && !showedStartAd) {
        showedStartAd = true;
				Advertisement.Show(@"gameStartZone", new ShowOptions {pause = true, resultCallback = null});
      }

			if (Input.GetMouseButtonDown (0) && !Advertisement.isShowing) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					if (hit.collider.name == @"startButton") {
						loadLevelAnimationStarted = true;
					}

					if (hit.collider.name == @"getMoreCoins" && !adsShowing) {
						adsShowing = true;
						Advertisement.Show(@"incentivizedZone", new ShowOptions {
							pause = true,
							resultCallback = result => {
								adsShowing = false;
								if (result == ShowResult.Finished) {
									SharedData.coinsCount += 10;
									GUIText guiText = GameObject.Find(@"coinsCounter").GetComponent<GUIText>();
									guiText.text = SharedData.coinsCount.ToString();
									guiText.transform.position = new Vector3(1 - guiText.GetScreenRect().width/Screen.width - 0.06f,
									                                         0.98f, 
									                                         0);
								}
							}
						});
					}
				}
			}
			if (loadLevelAnimationStarted) {
				coinsCounter.color = new Color(coinsCounter.color.r,coinsCounter.color.g,coinsCounter.color.b, coinsCounter.color.a - Time.deltaTime);
				foreach (SpriteRenderer renderer in objects) {
					if (renderer.animation)
					renderer.animation.Stop();
					renderer.color = new Color(renderer.color.r,renderer.color.g,renderer.color.b, renderer.color.a - Time.deltaTime);
					if (renderer.name == @"coins" && renderer.color.a < 0.01f) Application.LoadLevel("DemoAppLevel");
				}
			}
		}
	}
}
