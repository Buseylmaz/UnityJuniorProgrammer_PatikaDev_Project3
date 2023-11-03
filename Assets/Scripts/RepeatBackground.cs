using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    /// <summary>
    /// Bu kod blogunda ama� arka plan�n belli bir konuma gelince tekrardan ba�lang�� posizyonuna gelmesi.
    /// 
    /// Belli konum dedigimiz ise ba�lang�� posizyonundan 60 birim kaymas� 
    /// 
    /// </summary>

    Vector3 startPos;

    float repeatWidth;

    void Start()
    {
        startPos = transform.position;


        //Arka plan� birka� saniyede bir tekrarl�yoruz, ancak ge�i� olduk�a garip g�r�n�yor.
        //Baz� yeni de�i�kenlerle arka plan d�ng�s�n� kusursuz ve sorunsuz hale getirmemiz gerekiyor.
        //Arka Plana Box Collider bile�eni ekleyin ve box collider�inin geni�li�ini 2'ye b�l�nerek al�n
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
