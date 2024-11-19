
# SimpleCrudApi

docker build . -t simple-dotnet-crud
docker run -d -p 8080:8080 simple-dotnet-crud

docker login
docker build . -t necker3000/simple-dotnet-crud:latest
docker push necker3000/simple-dotnet-crud:latest

Azure region: Canada Central
Push the Docker image to your container registry:

docker tag my-dotnet-app <your-registry-login-server>/my-dotnet-app:latest

pulumi preview
pulumi up
pulumi destroy
