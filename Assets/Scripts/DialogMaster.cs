using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Droni
{
public class DialogMaster : MonoBehaviour
{
    //Game-object Names
    private const string DialogTextName = "DialogText";
    private const string DialogAudioName = "DialogAudio";

    //Global Instance
    public static DialogMaster master;

    [Tooltip("After a text was fully printed it will stay the time the idleTime dictates.")]
    public float idleTime = 5.0F;
    
    //Components
    private TextMeshProUGUI dialog_text;
    private AudioSource dialog_audio;
    private Queue<char> character_queue = new Queue<char>();

    //Timing variables
    private float talk_time = 1F;
    private float char_pause = 0.1F;
    
    //Update Time Control Variables
    private float timer = 0F;
    private bool running = false;

    //Component Search
    #region Setup
    private void Awake()
    {
        if (master)
        {
            Debug.LogErrorFormat("{0} exists twice", GetType().Name);
            return;
        }

        GameObject go = GameObject.Find(DialogTextName);

        if (!go)
        {
            Debug.LogErrorFormat("{0} could not be found", DialogTextName);
            return;
        }

        dialog_text = go.GetComponent<TextMeshProUGUI>();

        if (!dialog_text)
        {
            Debug.LogErrorFormat("{0} does not contain an object of Type TextMeshPro", go.name);
            return;
        }
        
        go = GameObject.Find(DialogAudioName);

        if (!go)
        {
            Debug.LogErrorFormat("{0} could not be found", DialogAudioName);
            return;
        }

        dialog_audio = go.GetComponent<AudioSource>();

        if (!dialog_audio)
        {
            Debug.LogErrorFormat("{0} does not contain an object of Type TextMeshPro", go.name);
            return;
        }

        dialog_audio.playOnAwake = false;
        dialog_text.text = "";
        dialog_text.color = Color.white;
        master = this;
    }
    #endregion

    // Core Logic
    #region QueuePrint
    private void Update()
    {
        if (!running) return;
        if (character_queue.Count == 0 && timer > idleTime)
        {
            timer = 0.0F;
            dialog_text.text = "";
            running = false;
        }

        if (character_queue.Count > 0 && timer > char_pause)
        {
            dialog_text.text += character_queue.Dequeue();
            timer = 0.0F;
        }

        timer += Time.deltaTime;
    }
    #endregion
    
    public void PlayDialogueColor(string text, AudioClip audio, Color color, float offset = 0)
    {
        dialog_text.color = color;
        character_queue = new Queue<char>(text.ToCharArray());
        dialog_audio.clip = audio;
        talk_time = audio.length + offset;
        char_pause = talk_time / character_queue.Count;
        dialog_text.text = "";
        dialog_audio.Play();
        timer = 0.0F;
        running = true;
    }

    public void PrintDialog(string text, Color color, float time = 5.0F)
    {
        dialog_text.color = color;
        character_queue = new Queue<char>(text.ToCharArray());
        talk_time = time;
        char_pause = talk_time / character_queue.Count;
        dialog_text.text = "";
        timer = 0.0F;
        running = true;
    }
    

}
}
