using System;
using System.Collections.Generic;

public class Phrases
{
    public const string FillBlank = "FillBlank";
    public const string Greetings = "Greetings";

    private static readonly Dictionary<string, SpeechLinesRandomizer> Available = new Dictionary<string, SpeechLinesRandomizer>() {
        {
            Greetings,
            new SpeechLinesRandomizer(new SpeechLines[] {
                new SpeechLines(new SpeechLine[] {
                    new SpeechLine("Hello Terrans! Welcome to this new Episode of Alien Wipeout!", TV.Mood.Impressed),
                    new SpeechLine("Please Welcome our new contestant: Poulpinette!", TV.Mood.Amused)
                })
            })
        },
        {
            FillBlank,
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Simple(new SpeechLine("What a beautiful day...", TV.Mood.Neutral)),
                SpeechLines.Simple(new SpeechLine("What a crap show...", TV.Mood.Angry)),
                SpeechLines.Simple(new SpeechLine("Hope they'll die soon!", TV.Mood.Amused)),
                SpeechLines.Simple(new SpeechLine("I'm too old for this s**t!!!", TV.Mood.Angry)),
                SpeechLines.Simple(new SpeechLine("Does the buzzer really exist...?", TV.Mood.Amused)),
                new SpeechLines(new SpeechLine[] {
                    (new SpeechLine("Whoa...", TV.Mood.Impressed, 0.5f)),
                    (new SpeechLine("Oh, no, still the same boring stuff...", TV.Mood.Angry)),
                }, 0.1f, 0.1f, false),
            })
        },
        {
            EarthWorld.Event.Started.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Simple(new SpeechLine("Here we go!", TV.Mood.Amused)),
                SpeechLines.Simple(new SpeechLine("Let's hope they'll last longer than the previous contestant...", TV.Mood.Angry)),
                SpeechLines.Simple(new SpeechLine("And one more!", TV.Mood.Neutral)),
            })
        },
        {
            EarthWorld.Event.DodgedBoulder.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Oh! That was close!", TV.Mood.Impressed))
            })
        },
        {
            EarthWorld.Event.DodgedWorm.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Oh snap! It almost took a bite!", TV.Mood.Impressed))
            })
        },
        {
            EarthWorld.Event.DodgedGlove.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Two inches from a knockout!", TV.Mood.Angry))
            })
        },
        {
            EarthWorld.Event.Punched.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("I think I'm gonna eat calamari tonight...", TV.Mood.Amused))
            })
        },
        {
            EarthWorld.Event.Smashed.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Oh no...", TV.Mood.Amused))
            })
        },
        {
            EarthWorld.Event.Eaten.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("I think I'm gonna eat calamari tonight...", TV.Mood.Neutral))
            })
        },
        {
            EarthWorld.Event.Fell.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("What a waste...", TV.Mood.Angry))
            })
        }
    };

    public static SpeechLines Random(string category)
    {
        if (!Available.ContainsKey(category))
        {
            return null;
        }

        return Available[category].Random();
    }
}
