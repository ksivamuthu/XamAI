namespace XamAI.ViewModel
{
    public class AzureCogntiveService : BaseViewModel
    {
        public int Id { get; set; }
        public CognitiveService Service { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public enum CognitiveService
    {
        Vision = 1,
        Face = 2,
        Speech = 3,
        Text = 4,
        Search = 5
    }
}