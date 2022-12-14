# Chain stores
## Application with microservice architecture
Application is based on [YouTube video](https://www.youtube.com/watch?v=DgVjEo3OGBI), but with different scope and unit tests.
### Application diagram
![diagram](https://github.com/tevkr/chain-stores/blob/main/README%20images/diagram.png)
### How to start
First change your hosts file with adding a line
```
127.0.0.1 pr-8-rksp.com
```
Then run commands
```
kubectl apply -f k8s/local-pvc.yaml
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
kubectl apply -f k8s/mssql-plat-depl.yaml
kubectl apply -f k8s/rabbitmq-depl.yaml
kubectl apply -f k8s/stores-depl.yaml
kubectl apply -f k8s/stores-np-srv.yaml
kubectl apply -f k8s/employees-depl.yaml
kubectl apply -f k8s/products-depl.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.5.1/deploy/static/provider/cloud/deploy.yaml
kubectl apply -f k8s/ingress-srv.yaml
```
### Docker hub repositories
- [Store Service](https://hub.docker.com/repository/docker/nomxd/storeservice)
- [Product Service](https://hub.docker.com/repository/docker/nomxd/productservice)
- [Employee Service](https://hub.docker.com/repository/docker/nomxd/employeeservice)
### Postman collection
Get [Postman Collection](https://elements.getpostman.com/redirect?entityId=14852705-4191eab5-d312-4055-891f-7007c51f01ee&entityType=collection)