namespace FlowStepsChain.NetCore.Sample.Samples.Services.ModuleOne.StepOne.Models
{
    public class StepOneRequest
    {
        public StepOneRequest()
        {

        }

        public StepOneRequest(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
