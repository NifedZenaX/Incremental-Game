using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapText : MonoBehaviour
{
    public float SpawnTime = 0.5f;
    public Text Text;

    private float _spawnTIme;

    private void OnEnable()
    {
        _spawnTIme = SpawnTime;
    }

    private void Update()
    {
        _spawnTIme -= Time.unscaledDeltaTime;
        if(_spawnTIme <= 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Text.CrossFadeAlpha(0f, 0.5f, false);
            if(Text.color.a == 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
