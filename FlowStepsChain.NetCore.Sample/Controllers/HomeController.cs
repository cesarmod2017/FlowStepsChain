using FlowStepsChain.NetCore.Extensions;
using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne;
using FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepOne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowStepsChain.NetCore.Sample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IModuleOneHandlerServices moduleOneHandlerServices;
    public HomeController(ILogger<HomeController> logger, IModuleOneHandlerServices moduleOneHandlerServices)
    {
        _logger = logger;
        this.moduleOneHandlerServices = moduleOneHandlerServices;
    }

    public async Task<IActionResult> Index() => await this.FlowStep()
                    .AddStepAsync(moduleOneHandlerServices.StepOne)
                    .ExecuteViewAsync<StepOneResponse>(this, null);


    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(StepOneResponse model) => await this.FlowStep()
                   .WithModelState(ModelState)
                    .AddStepAsync(moduleOneHandlerServices.StepOne)
                    .AddStepAsync(moduleOneHandlerServices.StepTwo)
                   .ExecuteApiAsync(model);


}
