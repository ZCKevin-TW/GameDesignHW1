using UnityEngine;
using Platformer.Mechanics;

public static class Vector2Extension
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
public class PlatformerJumpPad : MonoBehaviour
{
    public float Velocity = 20;

    void OnTriggerEnter2D(Collider2D other)
    {
        var rb = other.attachedRigidbody;
        if (rb == null) return;
        var player = rb.GetComponent<PlayerController>();
        if (player == null) return;
        AddVelocity(player);
    }

    void AddVelocity(PlayerController player)
    {
        /*
        Debug.Log(transform.position);
        Debug.Log(transform.eulerAngles);
        */
        var dir = Vector2.right.Rotate(transform.eulerAngles.z) * Velocity;
        Debug.Log(dir);
        Debug.Log(player.velocity);
        Debug.Log(player.velocity + Vector2.right.Rotate(transform.eulerAngles.z) * Velocity);
        player.Bounce(dir);
        //player.velocity = player.velocity + Vector2.right.Rotate(transform.eulerAngles.z) * Velocity;
        Debug.Log("New player velocity " + player.velocity);
    }
}
