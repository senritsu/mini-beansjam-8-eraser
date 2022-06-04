using System.Collections;
using System.Collections.Generic;
using Droni;
using UnityEngine;

public class DialogTest : MonoBehaviour
{

    [TextArea]
    public string voiceLine_text;
    public AudioClip voiceLine_audio;

    public Color voiceLine_color;

    public float offset;

    private void Start()
    {
        DialogMaster.master.PlayDialogueColor(voiceLine_text, voiceLine_audio, voiceLine_color, offset);
    }

}
