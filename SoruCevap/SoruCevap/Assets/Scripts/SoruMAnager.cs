using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class SoruMAnager : MonoBehaviour
{
    [SerializeField]
    List<SorularItem> sorularList;

    [SerializeField]
    TMP_Text sorutxt;

    [SerializeField]
    GameObject cevapPrefab;

    [SerializeField]
    Transform cevapContainer;


    int kacinciSoru;
    int cevapAdet;

    string[] secenekler = { "A)", "B)", "C)" };

    GameManager gameManager;
    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
    }


    public void Start()
    {
        sorularList = sorularList.OrderBy(i => Random.value).ToList();
    }

    public void SorularýYazdýr()
    {
        cevapAdet = 0;
        sorutxt.text = sorularList[kacinciSoru].soru;

        sorutxt.GetComponent<CanvasGroup>().alpha = 0f;
        sorutxt.GetComponent<RectTransform>().localScale = Vector3.zero;
        CevaplarýOlustur();
    }

    void CevaplarýOlustur()
    {
        GameObject[] silinecekCevaplar = GameObject.FindGameObjectsWithTag("CevapTAg");

        if (silinecekCevaplar.Length >= 0)
        {
            for (int i = 0; i < silinecekCevaplar.Length; i++)
            {
                DestroyImmediate(silinecekCevaplar[i]);
            }

        }

        for (int i = 0; i < 3; i++)
        {
            GameObject cevapObje = Instantiate(cevapPrefab);
            cevapObje.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = secenekler[i];
            cevapObje.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = sorularList[kacinciSoru].cevaplar[i].ToString();
            cevapObje.transform.SetParent(cevapContainer);

            cevapObje.GetComponent<Transform>().localScale = Vector3.zero;
        }
        gameManager.dogruCevap = sorularList[kacinciSoru].dogruCevap;
        StartCoroutine(CevaplarýAcRoutine());
    }

    IEnumerator CevaplarýAcRoutine()
    {
        yield return new WaitForSeconds(.5f);

        sorutxt.GetComponent<CanvasGroup>().DOFade(1, .3f);
        sorutxt.GetComponent<RectTransform>().DOScale(1, .3f);


        yield return new WaitForSeconds(.4f);

        while (cevapAdet < 3)
        {
            cevapContainer.GetChild(cevapAdet).DOScale(1, .2f);
            yield return new WaitForSeconds(.2f);

            cevapAdet++;

        }
        gameManager.soruCevaplansinmi = true;
    }


}
