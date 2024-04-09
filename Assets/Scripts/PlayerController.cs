using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;

public class PlayerController : MonoBehaviour
{
    //Voice Command
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    //Movement
    private float move = 0;
    private float speed = 3f;
    private float jumpHeight = 250f;

    private Rigidbody2D rigi;

    void Start(){
        rigi = GetComponent<Rigidbody2D>();

        actions.Add("right", Right);
        actions.Add("left", Left);
        actions.Add("jump", Jump);
        actions.Add("stop", Stop);
        actions.Add("attack", Attack);

        /*Open
        Talk
        Fire
        Turn
        Walk
        Run
        */

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void Update(){
        transform.position = new Vector3(transform.position.x + (move * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Right(){
        move = speed;
    }

    private void Left(){
        move = -speed;
    }

    private void Jump(){
        rigi.AddForce(transform.up * jumpHeight);
    }

    private void Stop(){
        move = 0;
    }

    private void Attack(){
        //Attack in front of the player. Set up facing direction, remove "Left" & "Right" with "Walk" & "Run"
    }
}
