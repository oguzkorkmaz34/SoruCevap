using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private bool HareketliMi;
    Vector3 hangiYon;
    Quaternion donusYonu;
    Animator anim;

    private void Awake()
    {
         anim=GetComponent<Animator>();   
    }


   

    public void HareketEt(Vector3 hedefPos, float gecikmeZamani = 0.25f)
    {
        if (!HareketliMi)
        {
            StartCoroutine(HaraketRoutine(hedefPos, gecikmeZamani));
        }

    }

    IEnumerator HaraketRoutine(Vector3 hedefPos, float gecikmeZamani)
    {
        HareketliMi = true;
        hangiYon = new Vector3(hedefPos.x - transform.position.x, transform.position.y, hedefPos.z - this.transform.position.z);
        donusYonu = Quaternion.LookRotation(hangiYon);
        transform.DORotateQuaternion(donusYonu, .2f);
        anim.SetBool("HareketEtsinMi", true);

        yield return new WaitForSeconds(0.2f);
        this.transform.DOMove(hedefPos, gecikmeZamani);

        while (Vector3.Distance(hedefPos, this.transform.position) > 0.01f)
        {
            yield return null;
        }
        anim.SetBool("HareketEtsinMi", false);
        donusYonu =Quaternion.LookRotation(Vector3.zero);
        transform.DORotateQuaternion(donusYonu, .2f);
        this.transform.position = hedefPos;
       

        HareketliMi = false;
    }

    public void HataYapti()
    {
        anim.SetBool("hataYapti", true);
    }

    public void OyuncuGeriGel()
    {
        this.transform.position = Vector3.zero;
        anim.SetBool("hataYapti", false);

    }

}
