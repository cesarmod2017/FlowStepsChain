namespace FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepTwo.Models
{
    public class StepTwoResponse
    {
        public StepTwoResponse()
        {

        }
        public StepTwoResponse(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }
}
