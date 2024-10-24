using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ViewFactory : MonoBehaviour
    {
        [SerializeField] private GameObject background;
        [SerializeField] private RectTransform safeScreenArea;

        public string CurrentUrl => wv.Url;

        UniWebView wv;
        int openTabsCount = 1;

        public static ViewFactory Instance;
        private void Awake()
        {
            Instance = this;

            wv = gameObject.AddComponent<UniWebView>();
            wv.OnOrientationChanged += (view, orientation) =>
            {
                StartCoroutine(UpdateScreen());
            };

            wv.SetAcceptThirdPartyCookies(true);

            wv.SetAllowBackForwardNavigationGestures(true);
            wv.SetSupportMultipleWindows(true, true);
            wv.OnShouldClose += (view) => view.CanGoBack || openTabsCount > 1;
            wv.OnMultipleWindowOpened += (view, id) => openTabsCount++;
            wv.OnMultipleWindowClosed += (view, id) => openTabsCount--;
            wv.ReferenceRectTransform = safeScreenArea;
        }

        public void Show(string url)
        {
            background.SetActive(true);

            UniWebView.SetAllowJavaScriptOpenWindow(true);

            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;

            wv.Load(url);
            wv.Show();

            StartCoroutine(UpdateScreen());
        }

        private void UpdateWVFrame()
        {
            Rect _safeArea = Screen.safeArea;
            
            if (Screen.width < Screen.height)
            {
                float avg = (2 * _safeArea.yMax + Screen.height) / 3;

                wv.ReferenceRectTransform.anchorMax = new Vector2(1, avg / Screen.height);
            }
            else
            {
                wv.ReferenceRectTransform.anchorMax = Vector2.one;
            }

            wv.ReferenceRectTransform.anchorMin = Vector2.zero;

            wv.ReferenceRectTransform.offsetMin = Vector2.zero;
            wv.ReferenceRectTransform.offsetMax = Vector2.zero;
        }

        IEnumerator UpdateScreen()
        {
            yield return null;
            UpdateWVFrame();
            wv.UpdateFrame();
        }
    }
}
