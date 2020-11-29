using Godot;
using Godot.Collections;
using System;

public class ImpactLocator : Node2D
{
    private PackedScene AlertArrow;

    private Dictionary<string, AlertArrow> markers;

    public override void _Ready()
    {
        this.AlertArrow = ResourceLoader.Load<PackedScene>("res://scenes/UI/AlertArrow.tscn");

        this.markers = new Dictionary<string, AlertArrow>();
    }

    public void RegisterBoulder(Boulder boulder) {
        boulder.Connect(nameof(Boulder.ImminentImpact), this, "OnImminentImpact");
        boulder.Connect(nameof(Boulder.Impact), this, "OnImpact");
    }

    public void OnImminentImpact(string identifier, Vector2 position)
    {
        var marker = (AlertArrow)this.AlertArrow.Instance();
        marker.GlobalPosition = position;

        this.markers.Add(identifier, marker);
        this.AddChild(marker);
    }

    public void OnImpact(string identifier)
    {
        if (this.markers.ContainsKey(identifier))
        {
            this.markers[identifier].QueueFree();
            this.markers.Remove(identifier);
        }
    }
}
