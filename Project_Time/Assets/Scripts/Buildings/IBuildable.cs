namespace ProjectTime.Buildings
{
    public interface IBuildable
    {
        BuildCost GetBuildCost();
        bool isBuildable();
        void BuildBuildable();
    }
}