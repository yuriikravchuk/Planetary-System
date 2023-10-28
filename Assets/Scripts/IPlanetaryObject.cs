public interface IPlanetaryObject
{
    public enum MassClassEnum
    {
        Asteroidan,
        Mercurian,
        Subterran,
        Terran,
        Superterran,
        Neptunian,
        Jovian
    }

    MassClassEnum MassClass { get; }
    double Mass { get; }
}
