using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] public Text text;
    public float maxHealth = 100f;
    public float Hitpoints;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "" + Hitpoints;
        Hitpoints = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + Hitpoints;
    }
}
