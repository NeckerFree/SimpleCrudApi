# SimpleCrudApi

docker build -t azure-crud:latest .
docker run -d -p 8080:8080 azure-crud

Azure region: EastUS


Push the Docker image to your container registry:

docker tag my-dotnet-app <your-registry-login-server>/my-dotnet-app:latest
docker push <your-registry-login-server>/my-dotnet-app:latest

pulumi preview
pulumi up

This approach will create 
- an Azure Resource Group, 
- Container Registry, 
- Container Instance 
and deploy your Dockerized .NET application.







