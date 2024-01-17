using DNN.Module.Ecommerce.HealthInsuranceCompany.Services.Interfaces;
using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepOne.Models;

namespace FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepOne;

public class StepOneHandler : ICommand
{
    public async Task<(bool, object)> Execute(object input)
    {
        var model = input as StepOneRequest;
        StepOneResponse? result = null;
        try
        {
            if (model == null)
            {
                model = new StepOneRequest
                {
                    Id = 1,
                    Name = "Name Test User"
                };
            }
            result = new StepOneResponse
            {
                Id = model.Id,
                Name = model.Name,
                Age = 21
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return (true, result);
    }
}
