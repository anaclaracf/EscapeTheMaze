using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource som;
    private AudioSource som_player;
    private GameObject[] som_quadro;
    private GameObject[] som_porta;

    public Slider volume_mus;
    public Slider volume_som;

    void Start()
    {
        som_player=GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        som_quadro=GameObject.FindGameObjectsWithTag("Picture");
        som_porta=GameObject.FindGameObjectsWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {

        som.volume=volume_mus.value;
        som_player.volume=volume_som.value;
        
        foreach(GameObject item in som_quadro){
            AudioSource item_2= item.GetComponent<AudioSource>();
            item_2.volume=volume_som.value;
        }

        foreach(GameObject item in som_porta){
            AudioSource item_2= item.GetComponent<AudioSource>();
            item_2.volume=volume_som.value;
        }

        
    }
}
