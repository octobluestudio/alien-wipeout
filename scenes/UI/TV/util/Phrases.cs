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
                    new SpeechLine("Please Welcome our new contestant: Poulpinette!", TV.Mood.Amused),
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
            LevelOne.Event.Started.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Simple(new SpeechLine("Here we go!", TV.Mood.Amused)),
                SpeechLines.Simple(new SpeechLine("Let's hope they'll last longer than the previous contestant...", TV.Mood.Angry)),
                SpeechLines.Simple(new SpeechLine("And one more!", TV.Mood.Neutral)),
                SpeechLines.Simple(new SpeechLine("They know they'll end up dead, they're still going... What an inspiration!", TV.Mood.Impressed)),
            })
        },
        {
            LevelOne.Event.DodgedBoulder.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Oh! That was close!", TV.Mood.Impressed)),
                SpeechLines.Interrupt(new SpeechLine("Almost got a dinosaur death!", TV.Mood.Amused)),
                SpeechLines.Interrupt(new SpeechLine("That's not a ball! Keep away!", TV.Mood.Angry)),
                SpeechLines.Interrupt(new SpeechLine("Next time, rock, next time...", TV.Mood.Neutral)),
            })
        },
        {
            LevelOne.Event.DodgedWorm.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Oh snap! It almost took a bite!", TV.Mood.Impressed)),
                SpeechLines.Interrupt(new SpeechLine("There won't be a Dune remake tonight!", TV.Mood.Amused)),
                SpeechLines.Interrupt(new SpeechLine("What a beautiful move...", TV.Mood.Neutral)),
                SpeechLines.Interrupt(new SpeechLine("Did somebody feed the worms before the show?", TV.Mood.Angry)),
            })
        },
        {
            LevelOne.Event.DodgedGlove.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Two inches from a knockout!", TV.Mood.Angry)),
                SpeechLines.Interrupt(new SpeechLine("Did they really dodge it?", TV.Mood.Impressed)),
                SpeechLines.Interrupt(new SpeechLine("And one wasted glove! One!", TV.Mood.Neutral)),
                SpeechLines.Interrupt(new SpeechLine("Watch out for the next one!", TV.Mood.Amused)),
            })
        },
        {
            LevelOne.Event.Punched.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Hope they have a good dentist!", TV.Mood.Amused)),
                SpeechLines.Interrupt(new SpeechLine("That thing doesn't kill them?", TV.Mood.Angry)),
                SpeechLines.Interrupt(new SpeechLine("Call the medic.", TV.Mood.Neutral)),
                SpeechLines.Interrupt(new SpeechLine("What a jump!", TV.Mood.Impressed)),
            })
        },
        {
            LevelOne.Event.Smashed.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Oh no...", TV.Mood.Neutral)),
                SpeechLines.Interrupt(new SpeechLine("Who ordered squid pancakes?", TV.Mood.Amused)),
                SpeechLines.Interrupt(new SpeechLine("Who do you think is gonna clean after? eh?", TV.Mood.Angry)),
                SpeechLines.Interrupt(new SpeechLine("My psychic told me this would happen!", TV.Mood.Impressed)),
            })
        },
        {
            LevelOne.Event.Eaten.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("I think I'm gonna eat calamari tonight!", TV.Mood.Impressed)),
                SpeechLines.Interrupt(new SpeechLine("Isn't the worm the one supposed to be eaten?", TV.Mood.Amused)),
                SpeechLines.Interrupt(new SpeechLine("Hope you will find spice, friend.", TV.Mood.Neutral)),
                SpeechLines.Interrupt(new SpeechLine("We told you! Don't get eaten! It's not that difficult!", TV.Mood.Angry)),
            })
        },
        {
            LevelOne.Event.Fell.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Such a waste...", TV.Mood.Angry)),
                SpeechLines.Interrupt(new SpeechLine("Please, next time, don't die off camera...", TV.Mood.Neutral)),
                SpeechLines.Interrupt(new SpeechLine("Told you this was slippery...", TV.Mood.Amused)),
                SpeechLines.Interrupt(new SpeechLine("How could they miss it!?", TV.Mood.Impressed)),
            })
        },
        {
            LevelOne.Event.Win.ToString("G"),
            new SpeechLinesRandomizer(new SpeechLines[] {
                SpeechLines.Interrupt(new SpeechLine("Congratulations... till next time!", TV.Mood.Angry)),
                SpeechLines.Interrupt(new SpeechLine("I guess I was wrong all along. They did it!", TV.Mood.Amused)),
                SpeechLines.Interrupt(new SpeechLine("And they advance to the next lev... hum... phase.", TV.Mood.Neutral)),
                SpeechLines.Interrupt(new SpeechLine("Against all odds, here we are!", TV.Mood.Impressed)),
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
