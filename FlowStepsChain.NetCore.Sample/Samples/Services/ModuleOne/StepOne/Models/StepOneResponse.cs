namespace FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepOne.Models
{
    public class StepOneResponse
    {
        public StepOneResponse()
        {

        }
        public StepOneResponse(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
