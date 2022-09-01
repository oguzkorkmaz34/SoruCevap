using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject soruPaneli;

    [SerializeField]
    GameObject dogruIcon, yanlýsIcon;

    [SerializeField] GameObject robot1, robot2, robot3;

    [SerializeField] GameObject dogruSonuc, yanlisSonuc;
    public bool soruCevaplansinmi;
    public string dogruCevap;

    SoruMAnager SoruManager;
    PlayerController playerController;

    int kalanHak;
    int dogruAdet;

    private void Awake()
    {
        SoruManager = Object.FindObjectOfType<SoruMAnager>();
        playerController=Object.FindObjectOfType<PlayerController>();
    }



    public void Start()
    {
        kalanHak = 3;
        dogruAdet = 0;
        StartCoroutine(OyunuAcRouitine());
    }

    IEnumerator OyunuAcRouitine()
    {
        yield return new WaitForSeconds(.1f);
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(320, 1f);

        yield return new WaitForSeconds(1.1f);
        SoruManager.SorularýYazdýr();
    }
    public void SonucuKontrolEt(string gelenCevap)
    {
        if (gelenCevap == dogruCevap)
        {
           dogruAdet++;
            if (dogruAdet>=15)
            {
                DogruSonucGoster();
            }
            else
            {
                SoruManager.SorularýYazdýr();
            }
            DogruÝcon();
        }
        else
        {
            YanlýsÝcon();
            StartCoroutine(OyuncuGeriGeldi());
        }

    }

    public void DogruÝcon()
    {
        dogruIcon.GetComponent<CanvasGroup>().DOFade(1, 3f);
        Invoke("DogruIconuKapat",.8f);
    }
    public void YanlýsÝcon()
    {
        yanlýsIcon.GetComponent<CanvasGroup>().DOFade(1, 3f);
        Invoke("YanlýsIconuKapat", .8f);
    }
    void DogruIconuKapat()
    {
        dogruIcon.GetComponent<CanvasGroup>().DOFade(0, 3f);

    }
    void YanlýsIconuKapat()
    {
        yanlýsIcon.GetComponent<CanvasGroup>().DOFade(0, 3f);

    }

    IEnumerator OyuncuGeriGeldi()
    {
        yield return new WaitForSeconds(1f);
        playerController.HataYapti();
        yield return new WaitForSeconds(1.4f);
        kalanHak--;
        HakAKaybet();
        if (kalanHak >0)
        {
            playerController.OyuncuGeriGel();
            yield return new WaitForSeconds(1.4f);
            SoruManager.SorularýYazdýr();
        }
        else
        {
            print("Oyun bitti");
            YanlýsSonucGoster();

        }
        

    }

    public void HakAKaybet()
    {
        if (kalanHak==2)
        {
            robot3.SetActive(false);
            robot2.SetActive(true);
            robot1.SetActive(true);
        }
        else if (kalanHak == 1)
        {
            robot3.SetActive(false);
            robot2.SetActive(false);
            robot1.SetActive(true);
        }
        else if (kalanHak == 0)
        {
            robot3.SetActive(false);
            robot2.SetActive(false);
            robot1.SetActive(false);

        }

    }

    public void DogruSonucGoster()
    {
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-320, 1f);
        dogruSonuc.GetComponent<CanvasGroup>().DOFade(1, .5f);
        dogruSonuc.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);

    }
    public void YanlýsSonucGoster()
    {
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-320, 1f);
        yanlisSonuc.GetComponent<CanvasGroup>().DOFade(1, .5f);
        yanlisSonuc.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);

    }

}
