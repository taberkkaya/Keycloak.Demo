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

### 🔑 Keycloak Admin Panel

Access the Keycloak Admin Console at: [http://localhost:8080/admin](http://localhost:8080/admin)

## 📸 
![image](https://github.com/user-attachments/assets/ef65a792-1a85-4598-b1fc-e146bf4d989f)
![image](https://github.com/user-attachments/assets/06aaf451-008b-4b8e-8929-e8c326bc14ca)

## 📑 Documentation

For API testing and reference, check out the following documentation:

- [📄 Postman Documentation](https://documenter.getpostman.com/view/42215598/2sAYkBs1jY#637289c4-8776-470d-8be9-38421e9556c4)
- [📘 Keycloak REST API Documentation](https://www.keycloak.org/docs-api/latest/rest-api/index.html)

## 📚 Resources

For additional learning materials, check out the following resources:

- 📺 [Taner Saydam's Udemy Profile](https://www.udemy.com/user/taner-saydam/?kw=taner+saydam&src=sac)
- 🐙 [Keycloak.Udemy Repository](https://github.com/TanerSaydam/Keycloak.Udemy)
