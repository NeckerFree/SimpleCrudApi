using PulumiAzure;
using System.Threading.Tasks;
using Deployment = Pulumi.Deployment;
namespace pulumiazure
{
    class Program
    {
        static Task<int> Main() => Deployment.RunAsync<AzAsStack>();
    }



}
