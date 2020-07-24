using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPaneli;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip butonClick;

    [SerializeField]
    private GameObject sesPaneli;

    bool sesPaneliAcikmi;

    AdmobManager admobManager;

    private void Awake()
    {
        admobManager = Object.FindObjectOfType<AdmobManager>();
    }
    void Start()
    {
        sesPaneliAcikmi = false;

        sesPaneli.GetComponent<RectTransform>().localPosition = new Vector3(0, -8, 0);

        menuPaneli.GetComponent<CanvasGroup>().DOFade(1, 1f);
        menuPaneli.GetComponent<RectTransform>().DOScale(1, 1f).SetEase(Ease.OutBack);

        admobManager.ShowBanner();
    }

    void Update()
    {
        
    }

    public void ikinciLeveleGec()
    {
        if(PlayerPrefs.GetInt("sesDurumu") == 1)
        {
            audioSource.PlayOneShot(butonClick);
        }

        SceneManager.LoadScene("ikinciMenuLevel");
        admobManager.ShowBanner();
        admobManager.GameOver();
    }

    public void AyarlariYap()
    {
        if (PlayerPrefs.GetInt("sesDurumu") == 1)
        {
            audioSource.PlayOneShot(butonClick);
        };

        if (!sesPaneliAcikmi)
        {
            sesPaneli.GetComponent<RectTransform>().DOLocalMoveY(-55, 0.5f);
            sesPaneliAcikmi = true;
        }
        else
        {
            sesPaneli.GetComponent<RectTransform>().DOLocalMoveY(-8, 0.5f);
            sesPaneliAcikmi = false;
        }
    }

    public void OyundanCik()
    {
        if (PlayerPrefs.GetInt("sesDurumu") == 1)
        {
            audioSource.PlayOneShot(butonClick);
        }
        Application.Quit();
    }
}
