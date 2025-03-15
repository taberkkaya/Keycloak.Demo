# Keycloak.Demo

## 🚀 Technologies
- **Keycloak** - Identity and Access Management
- **.NET Core Web API**
- **Angular**

## 🐳 Setting Up Keycloak with Docker
To run Keycloak in a Docker container, use the following command:
```sh
docker run -d --name keycloak -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:25.0.2 start-dev
```
This will start Keycloak in development mode and expose it on port **8080**.

## 📑 Documentation
For API testing and reference, check out the following documentation:
- [📄 Postman Document](https://documenter.getpostman.com/view/42215598/2sAYkBs1jY#637289c4-8776-470d-8be9-38421e9556c4)
- [📘 Keycloak REST API Documentation](https://www.keycloak.org/docs-api/latest/rest-api/index.html)

## 📚 Resources
For additional learning materials, check out the following resources:

- 📺 [Taner Saydam's Udemy Profile](https://www.udemy.com/user/taner-saydam/?kw=taner+saydam&src=sac)
- 🐙 [Keycloak.Udemy Repository](https://github.com/TanerSaydam/Keycloak.Udemy)
