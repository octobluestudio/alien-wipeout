using Godot;
using System.Collections.Generic;

public class LevelAudio : Node
{
    public bool MuteMusic = false;
    private float DefaultVolumeDb = -20;

    private List<AudioStreamPlayer> MuteControl = new List<AudioStreamPlayer>();

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionReleased("mute_music"))
        {
            this.Mute();
        }
    }

    public void Reset()
    {
        this.MuteControl = new List<AudioStreamPlayer>();
    }

    public void AddPlayerToMuteControl(AudioStreamPlayer player)
    {
        this.MuteControl.Add(player);
        this.MutePlayer(player);
    }

    private void Mute()
    {
        this.MuteMusic = !this.MuteMusic;

        foreach (AudioStreamPlayer player in this.MuteControl)
        {
            this.MutePlayer(player);
        }
    }

    private void MutePlayer(AudioStreamPlayer player)
    {
        player.VolumeDb = (this.MuteMusic) ? -80 : this.DefaultVolumeDb;
    }
}
