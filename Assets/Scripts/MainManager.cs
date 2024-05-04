using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//BEAT 0 - SIDE A
//BEAT 1 - SIDE B 
//BEAT 2 - SIDE C
//BEAT 3 - SIDE D 

[System.Serializable]
public class Beat
{
    public float sequenceEndTime;
    public int orbsCollected = 0;
    public int totalOrbsSpawned = 0;
    public AudioClip soundTrack;

    public float GetAccuracy()
    {
        return (float)orbsCollected / Mathf.Max(1, totalOrbsSpawned);
    }
}

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    [SerializeField] private List<Beat> beats;
    [SerializeField] private GameObject floorColliders;
    private PlayableDirector playableDirector;
    int currentBeat;

    private AudioSource baseTrackSource;

    [SerializeField] private AudioClip baseTrack;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        playableDirector = this.GetComponent<PlayableDirector>();  
    }

    private void Start()
    {
        playableDirector.time = 0;
        playableDirector.RebuildGraph();
        playableDirector.Evaluate();
        playableDirector.Play();

        CreateTrack(baseTrack);
    }

    // Update is called once per frame
    void Update()
    {
        if (playableDirector.time > beats[currentBeat].sequenceEndTime && playableDirector.state == PlayState.Playing)
        {
            playableDirector.Pause();
            OrbManager.Instance.SetState(false);

            if(currentBeat < 3) //END OF 0-2
            {
                //TODO: MAKE ARMS GLOW 
                
                //TODO: TELL USERS TO TOUCH THE GROUND 

                //SET ACTIVE COLLIDER ON THE FLOOR USERS TO TOUCH THE GROUND 
                floorColliders.gameObject.SetActive(true);
            }
            else if(currentBeat == 3) //END OF 3 
            {
                //END STATE OF EXPERIENCE
            }
        }
    }

    //CALLED BY FLOOR COLLIDERS
    public void GoToNextSequence()
    {
        CreateTrack(beats[currentBeat].soundTrack);
        currentBeat++;
        playableDirector.RebuildGraph();
        playableDirector.Evaluate();
        playableDirector.Play();
        floorColliders.gameObject.SetActive(false);
        OrbManager.Instance.SetState(true);
    }

    //CALLED BY ORB 
    public void CollectOrb()
    {
        beats[currentBeat].orbsCollected++;
    }

    //CALLED BY ORB MANAGER
    public void OrbSpawned()
    {
        beats[currentBeat].totalOrbsSpawned++;
    }

    public float GetCurrentAccuracy()
    {
        return beats[currentBeat].GetAccuracy();
    }

    public float GetTotalAccuracy()
    {
        float accuracy = 0f;

        int beatsSoFar = Mathf.Min(currentBeat + 1, beats.Count);

        for (int i = 0; i < beatsSoFar; i++)
        {
            accuracy += beats[i].GetAccuracy();
        }

        accuracy /= beatsSoFar;

        return accuracy;
    }

    private void CreateTrack(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("AudioClip is null in current beat!");
            return;
        }

        GameObject go = new()
        {
            name = clip.name
        };
        AudioSource source = go.AddComponent<AudioSource>();
        source.loop = true;
        source.clip = clip;
        if (baseTrackSource)
        {
            source.time = Mathf.Min(clip.length, baseTrackSource.time);
        }
        else
        {
            baseTrackSource = source;
        }
        source.Play();
    }
}
