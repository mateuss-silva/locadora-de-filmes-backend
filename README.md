# Locadora de Filmes

- Imagens disponíveis no [**DockerHub**](https://hub.docker.com/r/mateussgt/locadora-de-filmes)

- [**Frontend**](https://github.com/mateuss-silva/locadora-de-filmes-frontend)

## Porta: 
 - **5050**
## Host
 - **localhost**
## Documentação
 - [**Swagger**](https://localhost:5050/swagger/index.html)
 - [**Github**](https://github.com/mateuss-silva/locadora-de-filmes-backend)

## Compose

```
version: "3.9"

services:
  api:
    image: mateussgt/locadora-de-filmes:api
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;https://+:5050;http://+:80
      - ASPNETCORE_Kestrel__Certificates__Default__Password=root
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
    volumes:
      - ~/.aspnet/https:/https:ro
    ports:
      - "5050:5050"
      - "80:80"
      - "443:443"
    depends_on:
      - "database"
  web:
    stdin_open: true
    image: mateussgt/locadora-de-filmes:web
    ports:
      - "3000:3000"
    depends_on:
      - "api"
  database:
    image: mateussgt/locadora-de-filmes:database
    environment:
      MYSQL_ROOT_PASSWORD: 'root'
    ports:
      - "3306:3306"
    volumes:
      - locadora-data:/data/db

volumes:
  locadora-data:
    driver: local

```
