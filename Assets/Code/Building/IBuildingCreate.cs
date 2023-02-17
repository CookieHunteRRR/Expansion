using System;

namespace Code.Building
{
    public interface IBuildingCreate
    {
        event Action<int> OnBuildingCreate;
    }
}
