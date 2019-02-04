using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{

    [SerializeField]
    public float delayTime = 10f;
    public int loadThisScene = 2;

    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed > delayTime)
        {
            SceneManager.LoadScene(loadThisScene);
        }
    }
}
