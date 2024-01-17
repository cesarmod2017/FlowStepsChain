# FlowStepChain

FlowStepChain is a library for ASP.NET Core that simplifies the management and execution of function sequences in your Controllers. With FlowStepChain, you can easily configure multiple steps to be executed in sequence, handling errors and returning results consistently.

## Features

- Simple and chained definition of steps to be executed in sequence.
- Support for synchronous and asynchronous functions.
- Optional and automatic verification of ModelState validity before step execution.
- Custom redirection configuration for errors and exception handling.
- Flexibility to return results in IActionResult, ViewModel, or Razor Pages.
- Facilitates code reuse and maintenance in your Controllers.

## Installation

To install FlowStepChain, add it as a dependency to your ASP.NET Core project using NuGet or your preferred package manager.

## Installation Nuget

Package link on Nuget Org.

```
https://www.nuget.org/packages/FlowStepsChain.NetCore
```

Command to install the package via terminal

```
dotnet add package FlowStepsChain.NetCore --version 1.0.5
```

## Usage Example

 ```
public class MyController : Controller {     

    [HttpPost("create")]
    public async Task<IActionResult> Example1([FromBody] UserResponse model)
    {         
        return await this.FlowStep()
                            .AddStepAsync(userCommandService.UserValidate)
                            .AddStepAsync(userCommandService.UserCreate)
                            .AddStep(userServices.MapToViewModel)
                            .ExecuteViewAsync<UserResponse>(this, model);
                  
    } 

    public async Task<IActionResult> Example2(UserResponse model) => await this.FlowStep()
                            .AddStepAsync(userServices.ValidateModel)
                            .AddStepAsync(userServices.CreateRecord)
                            .AddStep(userServices.MapToViewModel)
                            .ExecuteViewAsync<UserResponse>(this, model);
}
```


## ExecuteViewAsync Usage Example

ExecuteViewAsync will process the sequence and will return a View or Redirect to an Error page

 ```
    return await this.FlowStep()
                            .AddStepAsync(userCommandService.UserValidate)
                            .AddStepAsync(userCommandService.UserCreate)
                            .AddStep(userServices.MapToViewModel)
                            .ExecuteViewAsync<UserResponse>(this, model);
```


## ExecuteResultAsync Usage Example

ExecuteResultAsync will process the sequence and will return an object

```
    var objResult = await this.FlowStep()
                        .AddStepAsync(userCommandService.UserValidate)
                        .AddStepAsync(userCommandService.UserCreate)
                        .ExecuteResultAsync<UserResponse>(model);
```

## ExecuteTypedAsync Usage Example

ExecuteTypedAsync will process the sequence and will return two outputs (bool, object)

```
    var objResult = await this.FlowStep()
                        .AddStepAsync(userCommandService.UserValidate)
                        .AddStepAsync(userCommandService.UserCreate)
                        .ExecuteTypedAsync<UserResponse>(model);

    // objResult.Item1 is of type bool
    // objResult.Item2 is of class type
```

## Mapper ToMap

The mapper is used to map two similar objects in a simple way. Below are two examples of how to use it

```
 var obj1 = new DataDTO();
 var obj2 = new DataDTO();

 In the example, the obj1 class will pass all values to the obj2 class

 Through an extension
    obj1.ToMap(obj2);
 Direct access to the Mapper
    Mapper.ToMap(obj1, obj2);
```

## Mapper GetChanges

GetChanges will return a list of fields that have changed with the properties Field, OldValue, and CurrentValue

```
    Direct access to the Mapper
    var resultDiff = Mapper.GetChanges(obj1, obj2);
    Through an extension
    var resultDiff = obj1.GetChanges(obj2);
```

## Documentation

For detailed information on how to configure and use FlowStepChain, see the documentation and examples available in the project repository.

## License

FlowStepChain is distributed under the MIT License.