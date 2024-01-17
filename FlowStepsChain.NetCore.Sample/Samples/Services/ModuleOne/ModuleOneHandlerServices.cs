using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepOne;
using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepTwo;

namespace FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne
{
    public class ModuleOneHandlerServices : IModuleOneHandlerServices
    {
        private readonly StepOneHandler stepOneHandler;
        private readonly StepTwoHandler stepTwoHandler;

        public ModuleOneHandlerServices(StepOneHandler stepOneHandler, StepTwoHandler stepTwoHandler)
        {
            this.stepOneHandler = stepOneHandler;
            this.stepTwoHandler = stepTwoHandler;
        }

        public async Task<(bool, object)> StepOne(object input) => await stepOneHandler.Execute(input);

        public async Task<(bool, object)> StepTwo(object input) => await stepTwoHandler.Execute(input);
    }
}
