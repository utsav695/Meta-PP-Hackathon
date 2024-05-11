using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public static OrbManager Instance { get; private set; }

    [SerializeField] private GameObject leftHand; //white
    [SerializeField] private GameObject rightHand; //black
    [SerializeField] private GameObject orbPrefabYellow;
    [SerializeField] private GameObject orbPrefabWhite;

    [Tooltip("seconds between which instantiation")]
    [SerializeField] public float OrbSpawnRate = 1;
    private float timer = 0;
    // Update is called once per frame
    public bool EmitOrbs = true;

    private void Awake()
    {
        Instance = this;
    }

    public void SetState(bool state)
    {
        EmitOrbs = state;
    }

    void Update()
    {
        if (EmitOrbs)
        {
            if (timer < OrbSpawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Instantiate(orbPrefabWhite, leftHand.transform.position, Quaternion.identity);
                Instantiate(orbPrefabYellow, rightHand.transform.position, Quaternion.identity);

                timer = 0;

                MainManager.Instance.OrbSpawned();
            }
        } 
      
    }
}
