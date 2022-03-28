using SportNugget.BusinessModels.Test;
using SportNugget.ViewModels.Demos;

namespace SportNugget.Shared.ViewModelBuilders.Interfaces
{
    public interface ITestViewModelBuilder
    {
        TestViewModel Build(TestModel businessModel);
        List<TestViewModel> BuildMany(List<TestModel> businessModels);
    }
}
