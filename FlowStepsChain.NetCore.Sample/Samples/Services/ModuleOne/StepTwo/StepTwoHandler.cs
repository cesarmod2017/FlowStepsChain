using DNN.Module.Ecommerce.HealthInsuranceCompany.Services.Interfaces;
using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepOne.Models;
using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepTwo.Models;

namespace FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepTwo;

public class StepTwoHandler : ICommand
{
    public async Task<(bool, object)> Execute(object input)
    {
        var model = input as StepOneResponse;
        StepTwoResponse? result = null;
        try
        {
            result = new StepTwoResponse
            {
                Id = model.Id,
                Name = model.Name,
                Age = model.Age,
                Email = "email@server.com"
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return (true, result);
    }
}
