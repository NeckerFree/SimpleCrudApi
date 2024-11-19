using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

namespace PulumiAzure
{
    class AzAsStack : Stack
    {
        public AzAsStack()
        {
            // Replace these with your specific values
            var dockerImage = "necker3000/simple-dotnet-crud:latest"; // Replace with your Docker Hub image
            var webAppName = "az-wa-elio";
            var resourceGroupName = "rg-elio";
            var appServicePlanName = "asp-elio";

            // Create an Azure Resource Group
            var resourceGroup = new ResourceGroup(resourceGroupName, new ResourceGroupArgs
            {
                Location = "Canada Central"
            }

                );

            // Create an Azure App Service Plan (Free Tier)
            var appServicePlan = new AppServicePlan(appServicePlanName, new AppServicePlanArgs
            {
                ResourceGroupName = resourceGroup.Name,
                Location = resourceGroup.Location,
                Kind = "Linux",
                Reserved = true, // Required for Linux-based apps
                Sku = new SkuDescriptionArgs
                {
                    //Name = "F1", // Free Tier
                    //Tier = "Free",
                    Name = "B1", // Basic Tier
                    Tier = "Basic",

                }
            });

            // Create an Azure Web App configured to use a Docker Hub image
            var webApp = new WebApp(webAppName, new WebAppArgs
            {
                ResourceGroupName = resourceGroup.Name,
                Location = resourceGroup.Location,
                ServerFarmId = appServicePlan.Id,
                SiteConfig = new SiteConfigArgs
                {
                    AppSettings =
                {
                    new NameValuePairArgs { Name = "DOCKER_REGISTRY_SERVER_URL", Value = "https://index.docker.io" },
                    new NameValuePairArgs { Name = "DOCKER_REGISTRY_SERVER_USERNAME", Value = "necker3000" },
                    new NameValuePairArgs { Name = "DOCKER_REGISTRY_SERVER_PASSWORD", Value = "e2n0c0m2dh$" },
                    new NameValuePairArgs { Name = "WEBSITES_PORT", Value = "80" } // Port your container listens on
                    , new NameValuePairArgs { Name = "DOCKER_ENABLE_CI_LOGGING", Value = "true" }
                },
                    LinuxFxVersion = $"DOCKER|{dockerImage}"
                }
            });

            this.Url = Output.Format($"https://{webApp.DefaultHostName}");
        }


        // Export the Web App's URL
        [Output]
        public Output<string> Url { get; set; }
    }
}