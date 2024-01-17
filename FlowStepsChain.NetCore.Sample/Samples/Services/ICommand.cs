namespace DNN.Module.Ecommerce.HealthInsuranceCompany.Services.Interfaces;

public interface ICommand
{
    Task<(bool, object)> Execute(object input);
}
