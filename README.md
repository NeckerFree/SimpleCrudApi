
# SimpleCrudApi
##Construir la imagen Docker local y ejecutarla
docker build . -t simple-dotnet-crud
docker run -d -p 8080:8080 simple-dotnet-crud

##Construir y publicar la imagen Docker en Docker Hub
docker login
docker build -t necker3000/simple-dotnet-crud:latest .
docker push necker3000/simple-dotnet-crud:latest

##Desplegar la imagen en Azure Container Instances
az login
Azure region: Canada Central
pulumi preview
pulumi up
pulumi destroy

docker run -p 8085:80 necker3000/simple-dotnet-crud:latest

Check who is using the port 80:
netstat -aon | findstr :80

Validate process using this port:
tasklist | findstr <PID>
tasklist | findstr 4

 pulumi stack init dev

 Remove-AzResourceGroup -Name rg-elio5ee89b82


