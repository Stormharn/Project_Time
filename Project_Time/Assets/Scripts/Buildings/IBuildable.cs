namespace ProjectTime.Build
{
    public interface IBuildable
    {
        BuildCost GetBuildCost();
        bool isBuildable();
        void Build();
    }
}