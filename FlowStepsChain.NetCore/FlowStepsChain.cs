using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlowStepsChain.NetCore
{
    public class FlowStepsChain
    {
        private readonly List<Func<object, Task<(bool, object)>>> _functions = new List<Func<object, Task<(bool, object)>>>();
        private string ErrorAction = "Message";
        private string ErrorController = "Error";
        private ModelStateDictionary _modelState;

        public FlowStepsChain WithModelState(ModelStateDictionary modelState)
        {
            _modelState = modelState;
            return this;
        }

        public FlowStepsChain SetErrorRoute(string action, string controller)
        {
            ErrorAction = action;
            ErrorController = controller;
            return this;
        }

        public FlowStepsChain AddStep(Func<object, (bool, object)> step)
        {
            _functions.Add(input => Task.FromResult(step(input)));
            return this;
        }

        public FlowStepsChain AddStepAsync(Func<object, Task<(bool, object)>> step)
        {
            _functions.Add(step);
            return this;
        }

        public async Task<IActionResult> ExecuteApiAsync(object input)
        {
            object result = input;

            foreach (var step in _functions)
            {
                var (success, nextResult) = await step(result);

                if (!success)
                {
                    return new BadRequestObjectResult(nextResult);
                }

                result = nextResult;
            }

            return new OkObjectResult(result);
        }

        public async Task<T> ExecuteResultAsync<T>(object input)
        {
            object result = input;

            foreach (var step in _functions)
            {
                var (success, nextResult) = await step(result);

                if (!success)
                {
                    return default(T);
                }

                result = nextResult;
            }

            return (T)result;
        }
        public async Task<(bool, T)> ExecuteTypedAsync<T>(object input)
        {
            object result = input;

            foreach (var step in _functions)
            {
                var (success, nextResult) = await step(result);

                if (!success)
                {
                    return (false, default(T));
                }

                result = nextResult;
            }

            return (true, (T)result);
        }
        public async Task<IActionResult> ExecuteViewAsync<T>(Controller controller, object input, string? viewName = null)
        {
            if (_modelState != null && !_modelState.IsValid)
            {
                return controller.BadRequest(_modelState);
            }

            var (success, result) = await ExecuteTypedAsync<T>(input);


            if (success)
            {
                if (string.IsNullOrEmpty(viewName))
                    return controller.View((T)result);
                else
                    return controller.View(viewName, (T)result);
            }
            else
            {
                // Você pode personalizar a resposta de erro aqui, se necessário
                return controller.RedirectToAction(ErrorAction, ErrorController);
            }
        }
    }
}
