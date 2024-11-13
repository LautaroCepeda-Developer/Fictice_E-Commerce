
# Fictice_E-Commerce Backend

This project is the backend of a fictitious ecommerce, I carried out this project to test my ability to organize a large and scalable project, in addition to testing my skills as a backend developer.

This project helped me apply many concepts such as dependency injection, SOLID principles, layered architecture, object-oriented programming, data validation, algorithms and data structure among many other things.

[!NOTE]
This project is intended solely as a portfolio project.
## Features

- User creation
- Multiple bank accounts per user
- Password hashing (BKDF2-HMAC-SHA512)
- Product publication by users
- Multiple product categories
- Multiple payment methods
- Models and data validation
- Automatic payment to sellers (with tax)
- Unique order number generation
- Unique transaction number generation
- Ban system with infraction limit
- Custom  response messages

### What I learned

I learned how to apply the principle of dependency injection, as well as knowing when to choose between scoped, transient and singletone.

Create a layered architecture composed of: 
- Controllers **-** Validators **-** Services **-** AutoMappers **-** DTOs **-** Repositories **-** Models

Which helped me to be able to separate responsibilities, efficiently maintain the large volume of project files, and also learn how to create scalable projects.

Learning about Data Transfer Objects (DTOs) helped me hide and protect sensitive information from entities.

I also learned how to perform custom validations on data sent and received through requests.

This project made me face real-world problems that would occur in an application that has financial or transactional processes (that is, in case a single thing fails, the process must be completely terminated without having made changes).
## Tech Stack
**Language:** C#

**Frameworks:** .NET 8 **-** ASP.NET Core

**Database:** PostgreSQL

**ORM:** Entity Framework

**NuGetPackages:** 
- *FluentValidation* to validate data and return custom error messages.
- *AutoMapper* to reduce the time spent mapping entities and DTOs, in addition to reducing repeated code.
- *Microsoft.EntityFrameworkCore.Design* & *Microsoft.EntityFrameworkCore.Tools* & *Npgsql.EntityFrameworkCore.PostgreSQL* to manage the entities and the database migrations in PostgreSQL through Entity Framework.
- *Microsoft.AspNetCore.Cryptography.KeyDerivation* & *System.Security.Cryptography.Cng* to hash the passwords.


## Running Tests

To test my endpoints, perform the tests on Postman collections using the batch request execution tool and the Postman API that allows you to write tests in javascript code.


## API Reference

### User API
#### GET methods
```
  # Get all users
  GET /Fictice_E-Commerce/UserAPI/User

  # Get all personal information of users
  GET /Fictice_E-Commerce/UserAPI/UserPersonalInformation

  # Get all bank accounts
  GET /Fictice_E-Commerce/UserAPI/UserBankAccount

  # Get user by id
  GET /Fictice_E-Commerce/UserAPI/User/{id}

  # Get user personal information by id
  GET /Fictice_E-Commerce/UserAPI/UserPersonalInformation/{id}

  # Get bank account by id
  GET /Fictice_E-Commerce/UserAPI/UserBankAccount/{id}

  # Get user by email
  GET /Fictice_E-Commerce/UserAPI/User/Email
  [FromHeader] email

  # Get user by nickname
  GET /Fictice_E-Commerce/UserAPI/User/Nickname
  [FromHeader] nickname
```

#### POST methods
```
  # Add user
  POST /Fictice_E-Commerce/UserAPI/User/
  body: {
    {
      "nickname": "string",
      "email": "string",
      "password": "string",
      "birthDate": "yyyy-MM-dd",
      "nationalIdentification": "string",
      "fullName": "string",
      "address": "string"
    }
  }

  # Login
  POST /Fictice_E-Commerce/UserAPI/User/Login
  [FromHeader] nicknameOrEmail
  [FromHeader] password

  # Add bank account
  POST /Fictice_E-Commerce/UserAPI/UserBankAccount
  body: {
    {
      "name": "string",
      "accountNumber": "string",
      "userId": int
    }
  }
```

#### PUT methods
```
  # Update user by id
  PUT /Fictice_E-Commerce/UserAPI/User/{id}
  body: {
    {
      "nickname": "string",
      "email": "string",
      "password": "string"
    }
  }

  # Update user personal information by id
  PUT /Fictice_E-Commerce/UserAPI/UserPersonalInformation/{id}
  body: {
    {
      "nationalIdentification": "string",
      "fullName": "string",
      "address": "string"
    }
  }

  # Update bank account by id
  PUT /Fictice_E-Commerce/UserAPI/UserBankAccount/{id}
  body: {
    {
      "name": "string",
      "accountNumber": "string",
      "isMainAccount": bool,
      "userId": int
    }
  }

  # Update user by email
  PUT /Fictice_E-Commerce/UserAPI/User/Email
  [FromHeader] email
  body: {
    {
      "nickname": "string",
      "email": "string",
      "password": "string"
    }
  }
  
  # Update user by nickname
  PUT /Fictice_E-Commerce/UserAPI/User/Nickname
  [FromHeader] nickname
  body: {
    {
      "nickname": "string",
      "email": "string",
      "password": "string"
    }
  }
```

#### DELETE methods
```
  # Delete user by id
  DELETE /Fictice_E-Commerce/UserAPI/User/{id}

  # Delete bank account by id
  DELETE /Fictice_E-Commerce/UserAPI/UserBankAccount/{id}

  # Delete user by email
  DELETE /Fictice_E-Commerce/UserAPI/User/Email
  [FromHeader] email

  # Delete user by nickname
  DELETE /Fictice_E-Commerce/UserAPI/User/Nickname
  [FromHeader] nickname
```

### Public User API
#### GET methods
```
  # Get non-sensible data of all users
  GET /Fictice_E-Commerce/PublicUserAPI/PublicUser

  # Get non-sensible data from user by id
  GET /Fictice_E-Commerce/PublicUserAPI/PublicUser/{id}
```

### Ban API
#### GET methods
```
  # Get all banned users
  GET /Fictice_E-Commerce/BanAPI/BannedUser

  # Get all banned bank accounts
  GET /Fictice_E-Commerce/BanAPI/BannedBankAccount

  # Get banned user by id
  GET /Fictice_E-Commerce/BanAPI/BannedUser/{id}

  # Get banned bank account by id
  GET /Fictice_E-Commerce/BanAPI/BannedBankAccount/{id}

  # Get user infractions by id
  GET /Fictice_E-Commerce/BanAPI/Infraction/{id}
```
#### POST methods
```
  # Ban user by id
  POST /Fictice_E-Commerce/BanAPI/BannedUser/{id}
  body: {
    {
      "banMotive": "string",
      "banDateTimeEnd": "yyyy-MM-ddThh:mm:ss.mssZ"
    }
  }

  # Ban bank account by id
  POST /Fictice_E-Commerce/BanAPI/BannedBankAccount/{id}
  body: {
    {
      "banMotive": "string",
      "banDateTimeEnd": "yyyy-MM-ddThh:mm:ss.mssZ"
    }
  }
```

#### PUT methods
```
  # Updates ban info of a banned user by id
  PUT /Fictice_E-Commerce/BanAPI/BannedUser/{id}
  body: {
    {
      "banMotive": "string",
      "banDateTimeEnd": "yyyy-MM-ddThh:mm:ss.mssZ"
    }
  }

  # Updates ban info of a banned bank account by id
  PUT /Fictice_E-Commerce/BanAPI/BannedBankAccount/{id}
  body: {
    {
      "banMotive": "string",
      "banDateTimeEnd": "yyyy-MM-ddThh:mm:ss.mssZ"
    }
  }

  # Adds or remove infractions of a non-banned user
  PUT /Fictice_E-Commerce/BanAPI/Infractions/{id}?quantity=int
```

#### DELETE methods
```
  # Delete a banned user by id
  DELETE /Fictice_E-Commerce/BanAPI/BannedUser/{id}

  # Delete a banned bank account by id
  DELETE /Fictice_E-Commerce/BanAPI/BannedBankAccount/{id}
```

### Product API
#### GET methods
```
  # Get all products
  GET /Fictice_E-Commerce/ProductAPI/Product/{id}

  # Get all categories
  GET /Fictice_E-Commerce/ProductAPI/Category/{id}

  # Get product by id
  GET /Fictice_E-Commerce/ProductAPI/Product/{id}

  # Get category by id
  GET /Fictice_E-Commerce/ProductAPI/Category/{id}
```

#### POST methods
```
  # Add product
  POST /Fictice_E-Commerce/ProductAPI/Product
  body: {
    {
      "name": "string",
      "price": decimal,
      "quantity": int,
      "categoryId": int,
      "sellerId": int
    }
  }

  # Add category
  POST /Fictice_E-Commerce/ProductAPI/Category
  body: {
    {
      "name": "string"
    }
  }
```

#### PUT methods
```
  # Update product by id
  PUT /Fictice_E-Commerce/ProductAPI/Product/{id}
  body: {
    {
      "name": "string",
      "price": decimal,
      "quantity": int,
      "categoryId": int
    }
  }

  # Update category by id
  PUT /Fictice_E-Commerce/ProductAPI/Category/{id}
  body: {
    {
      "name": "string"
    }
  }
```

#### DELETE methods
```
  # Delete product by id
  DELETE /Fictice_E-Commerce/ProductAPI/Product/{id}

  # Delete category by id
  DELETE /Fictice_E-Commerce/ProductAPI/Category/{id}
```

### Sale API
#### GET methods
```
  # Get all sales
  GET /Fictice_E-Commerce/SaleAPI/Sale
  
  # Get all payments
  GET /Fictice_E-Commerce/SaleAPI/Payment

  # Get all payment methods
  GET /Fictice_E-Commerce/SaleAPI/PaymentMethod
  
  # Get sale by id
  GET /Fictice_E-Commerce/SaleAPI/Sale/{id}
  
  # Get payment by id
  GET /Fictice_E-Commerce/SaleAPI/Payment/{id}

  # Get payment method by id
  GET /Fictice_E-Commerce/SaleAPI/PaymentMethod/{id}
  
  # Get payment by transaction number
  GET /Fictice_E-Commerce/SaleAPI/Payment/TransactionNumber/{transactionNumber}
  
  # Get sale by order number
  GET /Fictice_E-Commerce/SaleAPI/Sale/OrderNumber/{orderNumber}
```

#### POST methods
```
  # Make a sale
  POST /Fictice_E-Commerce/SaleAPI/Sale/{paymentMethodId}
  body: {
    {
      "saleInsertDTO": {
        "orderNumber": "string", <--optional-->
        "saleValue": decimal,
        "buyerId": int
      },
      "products": [
        {
          "id": int,
          "quantity": int,
          "price": decimal #optional
        }
      ]
    }
  }

  # Add payment method
  POST /Fictice_E-Commerce/SaleAPI/PaymentMethod
  body: {
    {
      "name": "string"
    }
  }
```

#### PUT methods
```
  # Edit payment method by id
  PUT /Fictice_E-Commerce/SaleAPI/PaymentMethod/{id}
```

#### DELETE methods
```
  #Delete payment method by id
  DELETE /Fictice_E-Commerce/SaleAPI/PaymentMethod/{id}
```

### Company Data API
#### GET methods 
```
  # Get company data
  GET /Fictice_E-Commerce/CompanyDataAPI/CompanyData

  # Get tax
  GET /Fictice_E-Commerce/CompanyDataAPI/CompanyData/Tax
```
#### POST methods
```
  # Add company data
  POST /Fictice_E-Commerce/CompanyDataAPI/CompanyData
  body: {
    {
      "companyName": "string",
      "companyLegalAddres": "string",
      "empressBankAccount": "string",
      "tax": decimal
    }
  }
```

#### PUT methods
```
  # Updates the company data
  PUT /Fictice_E-Commerce/CompanyDataAPI/CompanyData
  body: {
    {
      "companyName": "string",
      "companyLegalAddres": "string",
      "empressBankAccount": "string",
      "tax": decimal
    }
  }
```
