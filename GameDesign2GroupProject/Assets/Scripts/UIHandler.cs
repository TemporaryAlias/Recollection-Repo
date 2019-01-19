using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    [Header("Parent Objects")]
    public GameObject minigameUI;
    public GameObject platformerUI;

    [Space(5)]

    [Header("Minigame")]
    public Text minigameScoreText;

    [Space(5)]

    [Header("Platformer")]
    public Text healthText;

}
