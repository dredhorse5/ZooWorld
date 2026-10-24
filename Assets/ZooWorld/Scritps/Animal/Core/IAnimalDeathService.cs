namespace ZooWorld
{
    public interface IAnimalDeathService
    {
        bool Kill(Animal victim, Animal killer, bool wasEaten);
    }
}
