using UnityEngine;

public class Sun : MonoBehaviour
{
    public int Id { get; set; }
    public float Mass { get; set; }
    public float astronomicalUnit;
    public float scale;
    public float dt;    
    public float gravitationalConstantReal;

    void Start()
    {
        dt = 5000;
        astronomicalUnit = 1.49597871e11f;
        scale = 12f;
        gravitationalConstantReal = 6.6743e-11f;
    }
}
