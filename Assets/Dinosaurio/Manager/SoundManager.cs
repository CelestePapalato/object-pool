using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    List<string> nombres = new List<string>();
    [SerializeField]
    List<AudioClip> audioClips = new List<AudioClip>();

    Dictionary<string, AudioClip> listaDeSonidos = new Dictionary<string, AudioClip>();

    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < nombres.Count; i++)
        {
            listaDeSonidos.Add(nombres[i], audioClips[i]);
        }

        audioSource.clip = listaDeSonidos["perder"];
    }

    public void ReproducirSonido(string key)
    {
        if(key == "perder" && audioSource.clip)
        {
            // Si se quiere reproducir el sonido perder, debo usar un audio source
            // porque PlayClipAtPoint espera a que time scale sea mayor a cero, por
            // lo que no se reproduce nunca el sonido de perder
            audioSource.Play();
            return;
        }
        if (listaDeSonidos.ContainsKey(key))
        {
            AudioSource.PlayClipAtPoint(listaDeSonidos[key], Vector3.zero);
        }
    }
}
