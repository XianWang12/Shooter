using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);
    }

    public void PlaySFX(int index)
    {
        if (index != 0)
            if (sfx[index].isPlaying)
                return;

        if (index < sfx.Length)
        {
            sfx[index].pitch = Random.Range(0.8f, 1.2f);
            sfx[index].Play();
        }
    }

    public void StopSFX(int index) => sfx[index].Stop();

}
