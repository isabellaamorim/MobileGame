using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;


public class BannerADS : MonoBehaviour
{
       
    [SerializeField] private string _adUnitId = "ca-app-pub-3512518258135079/2923670132";
    BannerView _bannerView;


    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
     
        // Inicializa o SDK do Google Mobile Ads
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            CreateBannerView();
            LoadAd();
        });
    }

    public void CreateBannerView()
    {
        Debug.Log("Criando banner view");

        // Se j� houver um banner, destr�i o antigo
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Cria um banner 320x50 no topo da tela
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
        ListenToAdEvents();
    }

    [Obsolete]
    public void LoadAd()
    {
        // Cria uma inst�ncia de uma banner view primeiro
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        // Cria nossa solicita��o usada para carregar o an�ncio
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        // Envia a solicita��o para carregar o an�ncio
        Debug.Log("Carregando an�ncio de banner.");
        _bannerView.LoadAd(adRequest);
    }

    private void ListenToAdEvents()
    {
        // Chamado quando um an�ncio � carregado na banner view
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view carregou um an�ncio com resposta: "
                + _bannerView.GetResponseInfo());
        };
        // Chamado quando um an�ncio falha ao carregar na banner view
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view falhou ao carregar um an�ncio com erro: "
                + error);
        };
        // Chamado quando � estimado que o an�ncio ganhou dinheiro
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Banner view pagou {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Chamado quando uma impress�o � registrada para um an�ncio
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view registrou uma impress�o.");
        };
        // Chamado quando um clique � registrado para um an�ncio
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view foi clicado.");
        };
        // Chamado quando um an�ncio abre conte�do em tela cheia
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view abriu conte�do em tela cheia.");
        };
        // Chamado quando o conte�do em tela cheia de um an�ncio � fechado
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view fechou o conte�do em tela cheia.");
        };
    }

    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destruindo an�ncio de banner.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
