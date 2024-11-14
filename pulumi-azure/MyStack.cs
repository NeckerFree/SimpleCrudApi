using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.ContainerInstance;
using Pulumi.AzureNative.ContainerInstance.Inputs;
using Pulumi.AzureNative.ContainerRegistry;
namespace pulumiazure
{


    class MyStack : Stack
    {
        public MyStack()
        {
            // 1. Create an Azure Resource Group
            var resourceGroup = new ResourceGroup("myResourceGroup");

            // 2. Create an Azure Container Registry (optional)
            var registry = new Registry("myRegistry", new RegistryArgs
            {
                ResourceGroupName = resourceGroup.Name,
                Sku = new Pulumi.AzureNative.ContainerRegistry.Inputs.SkuArgs
                {
                    Name = Pulumi.AzureNative.ContainerRegistry.SkuName.Basic,
                },
                AdminUserEnabled = true
            });

            var registryCredentials = Output.Tuple(resourceGroup.Name, registry.Name).Apply(t =>
                ListRegistryCredentials.InvokeAsync(new ListRegistryCredentialsArgs
                {
                    ResourceGroupName = t.Item1,
                    RegistryName = t.Item2,
                }));
            var  adminUsername = registryCredentials.Apply(c => c.Username);
            var adminPassword = registryCredentials.Apply(c => c.Passwords[0].Value);

            // 3. Create a container instance
            var container = new ContainerGroup("myContainerGroup", new ContainerGroupArgs
            {
                ResourceGroupName = resourceGroup.Name,
                OsType = "Linux",
                Containers =
            {
                new ContainerArgs
                {
                    Name = "myDotnetApp",
                    Image = Output.Format($"{registry.LoginServer}/my-dotnet-app:latest"),
                    Resources = new ResourceRequirementsArgs
                    {
                        Requests = new ResourceRequestsArgs
                        {
                            Cpu = 1,
                            MemoryInGB = 1.5,
                        },
                    },
                    EnvironmentVariables =
                    {
                        new EnvironmentVariableArgs
                        {
                            Name = "DOTNET_ENVIRONMENT",
                            Value = "Production"
                        }
                    }
                },
            },
                ImageRegistryCredentials =
            {
                new ImageRegistryCredentialArgs
                {
                    Server = registry.LoginServer,
                    Username = adminUsername,
                    Password = adminPassword,
                }
            },
                DnsConfig = new DnsConfigurationArgs
                {
                    NameServers = { "1.1.1.1" }
                },
                RestartPolicy = "Always",
            });

            // 4. Export the URL of the container
            this.Url = container.IpAddress.Apply(ip => $"http://{ip}");
        }

        [Output]
        public Output<string> Url { get; set; }
    }

}
