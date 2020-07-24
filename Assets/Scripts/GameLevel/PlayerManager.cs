using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform gun;

    [SerializeField]
    private GameObject[] mermiPrefab;

    [SerializeField]
    private Transform mermiPozisyon;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip topClick;

    float angle;

    float donusHizi = 5f;

    float ikiMermiArasiSure = 300f;

    float sonrakiAtis;

    public bool rotaDegisme = false;
    void Update()
    {
        if(rotaDegisme == true)
        {
            RotateDegistir();
        }
    }

    void RotateDegistir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ana sahne üzerindeki mouse pozisyonunu alır ve silahın pozisyonundan çıkartır
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gun.transform.position;

            // Radyanını buluruz                            // Radyanı dereceye dönüştürürüz.
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        }
        

        if(angle < 60 && angle > -60)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Yumuşak geçiş için kullanılır.
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, rotation, donusHizi * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time > sonrakiAtis)
                {
                    sonrakiAtis = Time.time + ikiMermiArasiSure / 1000;
                    StartCoroutine(MermiAt());
                }
            }
        } 
    }

    IEnumerator MermiAt()
    {
        yield return new WaitForSeconds(donusHizi * Time.deltaTime * 5);

        if (PlayerPrefs.GetInt("sesDurumu") == 1)
        {
            audioSource.PlayOneShot(topClick);
        }

        GameObject mermi = Instantiate(mermiPrefab[Random.Range(0, mermiPrefab.Length)], mermiPozisyon.position, mermiPozisyon.rotation) as GameObject;
    }
}
