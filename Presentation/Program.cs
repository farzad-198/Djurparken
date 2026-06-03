namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppRunner appRunner = new AppRunner();

            await appRunner.RunAsync();
        }
    }
}