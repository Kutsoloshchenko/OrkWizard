namespace OrkWizard
{
    public enum PlatformMovementType
    {
        Moving,
        ReverseOrderOnFinish
    }

    public enum Element
    {
        Fire,
        Ice,
        Acid,
        Physical
    }

    public enum Layers
    {
        Player = 6,
        Platform = 8,
        Wall = 9,
        Trap = 10,
        Enemy = 11
    }

    public enum ColliderType
    {
        Box,
        Sphere
    }

    public enum MovementType
    {
        Patrol,
        Pursue,
        RunAway,
        Defend,
        NoMovement
    }
}
