using SportNugget.BusinessModels.Test;
using SportNugget.Data.Models;

namespace SportNugget.Business.Builders.Interfaces
{
    public interface ITestModelBuilder
    {
        TestModel Build(Test dataModel);
    }
}
