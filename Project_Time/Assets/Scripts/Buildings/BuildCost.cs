using UnityEngine;

[System.Serializable]
public class BuildCost
{
    [SerializeField] float wood;
    [SerializeField] float stone;
    [SerializeField] float steel;
    [SerializeField] float food;

    public float Wood { get => wood; }
    public float Stone { get => stone; }
    public float Steel { get => steel; }
    public float Food { get => food; }
}