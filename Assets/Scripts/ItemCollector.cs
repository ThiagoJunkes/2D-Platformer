using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int berrys = 0;
    [SerializeField] private TextMeshProUGUI berrysText;

    [SerializeField] private AudioSource collectionSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Strawberry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            berrys++;
            Debug.Log("Berrys: " + berrys);
            berrysText.text = "Berrys: " + berrys;
        }
    }
}
