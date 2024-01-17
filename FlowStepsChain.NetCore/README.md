# FlowStepChain

FlowStepChain é uma biblioteca para o ASP.NET Core que simplifica o gerenciamento e a execução de sequências de funções em suas Controllers. Com o FlowStepChain, você pode facilmente configurar várias etapas a serem executadas em sequência, tratando erros e retornando resultados de maneira consistente.

## Características

-   Definição simples e encadeada de etapas a serem executadas em sequência.
-   Suporte a funções síncronas e assíncronas.
-   Verificação opcional e automática da validade do ModelState antes da execução das etapas.
-   Configuração de redirecionamento personalizado para erros e tratamento de exceções.
-   Flexibilidade para retornar resultados em IActionResult, ViewModel ou Razor Pages.
-   Facilita a reutilização e a manutenção do código em suas Controllers.

## Instalação

Para instalar o FlowStepChain, adicione-o como uma dependência do seu projeto ASP.NET Core usando o NuGet ou o gerenciador de pacotes de sua preferência.

## Exemplo de uso

 ```

public class MyController : Controller {     

	[HttpPost("create")]
	public async Task<IActionResult> Exemplo1([FromBody] UserResponse model)
	{         
		return await this.FlowStep()
                                .AddStepAsync(userCommandService.UserValidate)
                                .AddStepAsync(userCommandService.UserCreate)
                                .AddStep(userServices.MapearParaViewModel)
                                .ExecuteViewAsync<UserResponse>(this, model);
					  
	} 

    public async Task<IActionResult> Exemplo2(UserResponse model) => await this.FlowStep()
                                .AddStepAsync(userServices.ValidarModel)
                                .AddStepAsync(userServices.CriarRegistro)
                                .AddStep(userServices.MapearParaViewModel)
                                .ExecuteViewAsync<UserResponse>(this, model);
}

```

## Exemplo de uso ExecuteViewAsync

O ExecuteViewAsync irá processar a sequência e irá retornar uma View ou Redirecionar para uma página de Erro

```
    return await this.FlowStep()
                                .AddStepAsync(userCommandService.UserValidate)
                                .AddStepAsync(userCommandService.UserCreate)
                                .AddStep(userServices.MapearParaViewModel)
                                .ExecuteViewAsync<UserResponse>(this, model);
```


## Exemplo de uso ExecuteResultAsync

O ExecuteResultAsync irá processar a sequência e irá retornar um objeto

```
    var objResult = await this.FlowStep()
                            .AddStepAsync(userCommandService.UserValidate)
                            .AddStepAsync(userCommandService.UserCreate)
                            .ExecuteResultAsync<UserResponse>(model);
```

## Exemplo de uso ExecuteTypedAsync

O ExecuteTypedAsync irá processar a sequência e irá retornar dois retornos (bool, object)

```
    var objResult = await this.FlowStep()
                            .AddStepAsync(userCommandService.UserValidate)
                            .AddStepAsync(userCommandService.UserCreate)
                            .ExecuteTypedAsync<UserResponse>(model);

	// objResult.Item1 to tipo bool
    // objResult.Item2 do tipo class
```

## Mapper ToMap

O mapper serve para mapear 2 objetos semelhantes de forma simples abaixo segue os 2 exemplos de como utilizar

```
 var obj1 = new DataDTO();
 var obj2 = new DataDTO();

 No exemplo a classe obj1 irá passar todos os valores para a classe obj2

 Através de uma extensão
    obj1.ToMap(obj2);
 Acesso direto ao Mapper
    Mapper.ToMap(obj1, obj2);
```


## Mapper GetChanges

O GetChanges irá retornar uma lista dos campos que sofreram alterações com as propriedades Field, OldValue e CurrentValeu

```
    Acesso direto ao Mapper
    var resultDiff = Mapper.GetChanges(obj1, obj2);
    Através de uma extensão
    var resultDiff = obj1.GetChanges(obj2);
```


## Documentação

Para obter informações detalhadas sobre como configurar e usar o FlowStepChain, consulte a documentação e os exemplos disponíveis no repositório do projeto.

## Licença

FlowStepChain é distribuído sob a [licença MIT](https://chat.openai.com/c/LICENSE.md).