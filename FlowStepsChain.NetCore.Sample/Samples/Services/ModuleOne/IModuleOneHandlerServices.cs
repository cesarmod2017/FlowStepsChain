
namespace FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne
{
    public interface IModuleOneHandlerServices
    {
        Task<(bool, object)> StepOne(object input);
        Task<(bool, object)> StepTwo(object input);
    }
}