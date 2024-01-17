using Microsoft.AspNetCore.Mvc;

namespace FlowStepsChain.NetCore.Extensions
{
    public static class FlowStepsControllerExtensions
    {
        public static FlowStepsChain FlowStep(this ControllerBase controller)
        {
            return new FlowStepsChain();
        }

        public static FlowStepsChain FlowStep(this Controller controller)
        {
            return new FlowStepsChain();
        }
    }
}
