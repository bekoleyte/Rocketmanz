using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPosition; //baslangic pozisyonu
    [SerializeField] Vector3 movementVector;
    float movementFactor; //hareket faktoru siniri
    [SerializeField] float period = 10f;

    void Start()
    {
        startingPosition = transform.position; //baslangic noktasina esitler pozisyonu
    }

    
    void Update()
    {
        if(period <= Mathf.Epsilon) {return;} // sifira esit veya kucukse mathfepsilion
        float cycles = Time.time / period; //periodu hesaplattik

        const float tau = Mathf.PI * 2; // sabit deger 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // -1 ile 1 arasi gezer
        //belli bir deger araliginda doner

        movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor; //factor degerimizi vectorlerle carptik offsete esitledik
        transform.position = startingPosition + offset;
    }
}
