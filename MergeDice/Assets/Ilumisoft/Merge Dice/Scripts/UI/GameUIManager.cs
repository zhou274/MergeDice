using Ilumisoft.MergeDice.Events;
using System;
using System.Collections;
using UnityEngine;
using StarkSDKSpace;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;

namespace Ilumisoft.MergeDice
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField]
        GameObject gameUI = null;

        [SerializeField]
        GameObject gameOverUI = null;

        [SerializeField]
        OverlayCanvas overlayCanvas = null;
        private StarkAdManager starkAdManager;

        public string clickid;



        private void OnEnable()
        {
            GameEvents<UIEventType>.OnTrigger += OnTriggerUI;
            
        }

        private void OnDisable()
        {
            GameEvents<UIEventType>.OnTrigger -= OnTriggerUI;
            
        }

        private void OnTriggerUI(UIEventType type)
        {
            switch (type)
            {
                case UIEventType.GameOver:
                    ShowGameOverUI();
                    break;
            }
        }

        void ShowGameOverUI()
        {
            StartCoroutine(ShowGameOverUICoroutine());
            ShowInterstitialAd("1lcaf5895d5l1293dc",
            () => {
                Debug.LogError("--插屏广告完成--");

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
        }
        void HideGameOverUI()
        {
            gameUI.gameObject.SetActive(true);
            gameOverUI.gameObject.SetActive(false);
        }
        IEnumerator ShowGameOverUICoroutine()
        {
            yield return overlayCanvas.FadeIn();
            gameUI.gameObject.SetActive(false);
            gameOverUI.gameObject.SetActive(true);
            yield return overlayCanvas.FadeOut();
        }
        /// <summary>
        /// 播放插屏广告
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="errorCallBack"></param>
        /// <param name="closeCallBack"></param>
        public void ShowInterstitialAd(string adId, System.Action closeCallBack, System.Action<int, string> errorCallBack)
        {
            starkAdManager = StarkSDK.API.GetStarkAdManager();
            if (starkAdManager != null)
            {
                var mInterstitialAd = starkAdManager.CreateInterstitialAd(adId, errorCallBack, closeCallBack);
                mInterstitialAd.Load();
                mInterstitialAd.Show();
            }
        }
    }
}