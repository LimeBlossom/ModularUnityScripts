using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceCommandSystem : MonoBehaviour
{
    [SerializeField] private VoiceCommand[] commands;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, GameEvent[]> gameEvents = new Dictionary<string, GameEvent[]>();

    void Start()
    {
        for(int i = 0; i < commands.Length; i++)
        {
            foreach(string phrase in commands[0].phrases)
            {
                gameEvents.Add(phrase, commands[0].eventsToCall);
            }
        }

        ///* Computer Commands */
        //actions.Add("computer its go time", ItsGoTime);
        //actions.Add("computer next song", NextSong);
        //actions.Add("computer stop the music", StopMusic);
        //actions.Add("computer take me to the studio", TeleportStudio);

        ///* Eyeball Cam Commands */
        //actions.Add("irene stay", Stay);
        //actions.Add("irene follow", Follow);
        //actions.Add("irene studio position", IreneStudioPosition);

        ///* ChatBot Commands */
        //actions.Add("chat bot", ChatBot);
        //chatBotActions.Add("socials", Socials);
        //chatBotActions.Add("nya", Nya);
        //chatBotActions.Add("say hello", Hello);

        ///* Frankie Commands */
        //actions.Add("frankie", FrankieGrunt);
        //actions.Add("franky", FrankieGrunt);
        //actions.Add("how you doing frankie?", FrankieGrunt);
        //actions.Add("isn't that right frankie?", FrankieGrunt);
        //actions.Add("say hello frankie", FrankieGrunt);
        //actions.Add("what do you think frankie?", FrankieGrunt);
        //actions.Add("isn't that funny frankie?", FrankieGrunt);
        //actions.Add("isn't it frankie?", FrankieGrunt);
     
        ///* Facial Expression Commands */
        //// Laughing Face
        //faceExpressionActions.Add("haha", FunnyFace);
        //faceExpressionActions.Add("funny", FunnyFace);
        //faceExpressionActions.Add("hilarious", FunnyFace);

        //// Angry Face
        //faceExpressionActions.Add("what the hell", AngryFace);
        //faceExpressionActions.Add("what the fuck", AngryFace);
        //faceExpressionActions.Add("bullshit", AngryFace);
        //faceExpressionActions.Add("angry", AngryFace);

        keywordRecognizer = new KeywordRecognizer(gameEvents.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += PhraseRecognized;
        keywordRecognizer.Start();
    }

    private void PhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        foreach(GameEvent gameEvent in gameEvents[speech.text])
        {
            gameEvent.Raise();
        }
    }
}
