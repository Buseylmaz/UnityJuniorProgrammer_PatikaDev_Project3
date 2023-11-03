using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    /// <summary>
    /// Bu kod blogunda amaç arka planýn belli bir konuma gelince tekrardan baþlangýç posizyonuna gelmesi.
    /// 
    /// Belli konum dedigimiz ise baþlangýç posizyonundan 60 birim kaymasý 
    /// 
    /// </summary>

    Vector3 startPos;

    float repeatWidth;

    void Start()
    {
        startPos = transform.position;


        //Arka planý birkaç saniyede bir tekrarlýyoruz, ancak geçiþ oldukça garip görünüyor.
        //Bazý yeni deðiþkenlerle arka plan döngüsünü kusursuz ve sorunsuz hale getirmemiz gerekiyor.
        //Arka Plana Box Collider bileþeni ekleyin ve box collider’inin geniþliðini 2'ye bölünerek alýn
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        
    }

    
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
