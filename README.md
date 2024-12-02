
# HouseFinder API

HouseFinder API is a dotnet core api project that helps the interested party to find houses.
Functionality of the project is described below.


## Deployment

To deploy this project run

You can run this project in docker if you wish.
You have to install docker desktop first.
After installing docker do as instructed as below.

Clone the project from git.
```bash
git clone https://github.com/smuztabaali/HouseFinder.git
```
After clonig the project open cmd in the same path and type

```bash
docker-compose up -d
```
Dokcer Desktop must be open while writing this command.

If the project is successfully running in docker, go the link

```bash
http://localhost:8001/swagger/
```

Here you can do CRUD operation. Normally have to register first as user or admin,then login as user or admin depending on the function you want to perform. But I addedd [AllowAnynomous] here. So anyone can perform CRUD operation here.

**POST/api/UserAuth/register** Endpoint:

Sample Registration as admin,same can be done for the user.
```bash
{
  "id": 0,
  "userName": "muztaba",
  "email": "muztaba@gmail.com",
  "name": "Syed Muztaba Ali",
  "password": "admin",
  "role": "admin"
}
```
If successfull then the response should be like this:
```bash
{
  "statusCode": 200,
  "isSuccess": true,
  "errorMessage": null,
  "result": null
}
```

Then you can login.
Sample Login. 

```bash
{
  "userName": "muztaba",
  "password": "admin"
}
```
Response body should look like this:

```bash
{
  "statusCode": 200,
  "isSuccess": true,
  "errorMessage": null,
  "result": {
    "user": {
      "id": 1,
      "userName": "muztaba",
      "email": "muztaba@gmail.com",
      "name": "Syed Muztaba Ali",
      "password": "admin",
      "role": "admin"
    },
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyIxIiwibXV6dGFiYSJdLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE3MzMxNzY0MjEsImV4cCI6MTczMzI2MjgyMSwiaWF0IjoxNzMzMTc2NDIxfQ.5lxZNCJTCy-v0xE5OsePcXmKPV4OnwRM3GjkAvUsUQU"
  }
}
```
After Successfull login you should get get a Jwt Token. Use it to get authorization. 

Now let's see CRUD functionality.

**POST/api/v{version}/HouseFinderAPI** Endpoint:

You can create house information here.
Click on **Try it out**.
Something like this show up.

```bash
{
  "houseId": 0,
  "houseName": "string",
  "sqft": 0,
  "houseNoOrRoad": "string",
  "division": "string",
  "district": "string",
  "city": "string",
  "bedNumber": 0,
  "balconyNumber": 0,
  "rent": 0,
  "floorNumber": 0,
  "hasLift": true,
  "isAvailable": true,
  "contactNumber": "string"
}
```
**Version 1.0**.

Fill it up with some dummy data. I am providing two dummy data.
```bash
{
      "houseId": 0,
      "houseName": "Shawpno Villa",
      "sqft": 800,
      "houseNoOrRoad": "Ovijan 294,College Road",
      "division": "Dhaka",
      "district": "Gazipur",
      "city": "Tongi",
      "bedNumber": 2,
      "balconyNumber": 1,
      "rent": 8000,
      "floorNumber": 2,
      "hasLift": false,
      "isAvailable": true,
      "contactNumber": "01345678189"
}
```
```bash
{
      "houseId": 0,
      "houseName": "Ahmad Villa",
      "sqft": 1500,
      "houseNoOrRoad": "Sur 294,Sur Taranga Road",
      "division": "Dhaka",
      "district": "Dhaka",
      "city": "Mirpur",
      "bedNumber": 3,
      "balconyNumber": 3,
      "rent": 12000,
      "floorNumber": 2,
      "hasLift": true,
      "isAvailable": true,
      "contactNumber": "01598453781"
}
```
Execute the separately it. If everything is alright you should get a response like this.

```bash
{
  "statusCode": 201,
  "isSuccess": true,
  "errorMessage": null,
  "result": {
    "houseId": 1,
    "houseName": "Shawpno Villa",
    "sqft": 800,
    "houseNoOrRoad": "Ovijan 294,College Road",
    "division": "Dhaka",
    "district": "Gazipur",
    "city": "Tongi",
    "houseAddress": "Shawpno Villa, Ovijan 294,College Road, Tongi, Gazipur, Dhaka",
    "bedNumber": 2,
    "balconyNumber": 1,
    "rent": 8000,
    "floorNumber": 2,
    "hasLift": false,
    "isAvailable": true,
    "contactNumber": "01345678189",
    "createdAt": "2024-12-02T00:00:00+00:00",
    "lastUpdatedAt": "2024-12-02T00:00:00+00:00"
  }
}
```
```bash
{
  "statusCode": 201,
  "isSuccess": true,
  "errorMessage": null,
  "result": {
    "houseId": 2,
    "houseName": "Ahmad Villa",
    "sqft": 1500,
    "houseNoOrRoad": "Sur 294,Sur Taranga Road",
    "division": "Dhaka",
    "district": "Dhaka",
    "city": "Mirpur",
    "houseAddress": "Ahmad Villa, Sur 294,Sur Taranga Road, Mirpur, Dhaka, Dhaka",
    "bedNumber": 3,
    "balconyNumber": 3,
    "rent": 12000,
    "floorNumber": 2,
    "hasLift": true,
    "isAvailable": true,
    "contactNumber": "01598453781",
    "createdAt": "2024-12-02T00:00:00+00:00",
    "lastUpdatedAt": "2024-12-02T00:00:00+00:00"
  }
}
```

**GET/api/v{version}/HouseFinderAPI** Endpoint:

Returns all the houses stored in the database.
 If you Execute this enpoint you should get response like this

 ```bash
 {
  "statusCode": 200,
  "isSuccess": true,
  "errorMessage": null,
  "result": [
    {
      "houseId": 1,
      "houseName": "Shawpno Villa",
      "sqft": 800,
      "houseNoOrRoad": "Ovijan 294,College Road",
      "division": "Dhaka",
      "district": "Gazipur",
      "city": "Tongi",
      "houseAddress": "Shawpno Villa, Ovijan 294,College Road, Tongi, Gazipur, Dhaka",
      "bedNumber": 2,
      "balconyNumber": 1,
      "rent": 8000,
      "floorNumber": 2,
      "hasLift": false,
      "isAvailable": true,
      "contactNumber": "01598453781"
    },
    {
      "houseId": 2,
      "houseName": "Ahmad Villa",
      "sqft": 1500,
      "houseNoOrRoad": "Sur 294,Sur Taranga Road",
      "division": "Dhaka",
      "district": "Dhaka",
      "city": "Mirpur",
      "houseAddress": "Ahmad Villa, Sur 294,Sur Taranga Road, Mirpur, Dhaka, Dhaka",
      "bedNumber": 3,
      "balconyNumber": 3,
      "rent": 12000,
      "floorNumber": 2,
      "hasLift": true,
      "isAvailable": true,
      "contactNumber": "01598453781"
    }
  ]
}
 ```

 you can also search by bed room number,city,district or division.

 **GET/api/v{version}/HouseFinderAPI/{id}** Endpoint:

Returns only the selected houses by id.
Insert id and version(1.0).
If it exist should get a response like this
```bash
{
  "statusCode": 200,
  "isSuccess": true,
  "errorMessage": null,
  "result": {
    "houseId": 1,
    "houseName": "Shawpno Villa",
    "sqft": 800,
    "houseNoOrRoad": "Ovijan 294,College Road",
    "division": "Dhaka",
    "district": "Gazipur",
    "city": "Tongi",
    "houseAddress": "Shawpno Villa, Ovijan 294,College Road, Tongi, Gazipur, Dhaka",
    "bedNumber": 2,
    "balconyNumber": 1,
    "rent": 8000,
    "floorNumber": 2,
    "hasLift": false,
    "isAvailable": true
  }
}
```

If the selected id doesn't exist then response should be like:
```bash
{
  "statusCode": 404,
  "isSuccess": false,
  "errorMessage": [
    "No data found."
  ],
  "result": null
}
```

**PUT/api/v{version}/HouseFinderAPI/{id}** Endpoint:

A particula house's information can be updated by using this endpoint.
Provide the id number and version(1.0).
Let's change something in house with id 1. Let's change rent from 8000 to 12000.

Change like this in the request body.
```bash
{
      "houseId": 1,
      "houseName": "Shawpno Villa",
      "sqft": 800,
      "houseNoOrRoad": "Ovijan 294,College Road",
      "division": "Dhaka",
      "district": "Gazipur",
      "city": "Tongi",
      "houseAddress": "Shawpno Villa, Ovijan 294,College Road, Tongi, Gazipur, Dhaka",
      "bedNumber": 2,
      "balconyNumber": 1,
      "rent": 12000,
      "floorNumber": 2,
      "hasLift": false,
      "isAvailable": true,
      "contactNumber": "01345678189"
    }
```
Execute it.

It should return **204** StatusCode.

Then if we go the  **GET/api/v{version}/HouseFinderAPI/{id}** endpoint and provide the id and version. In this case id will be 1 and version will be 1.0, we will get a response like this:

```bash
{
  "statusCode": 200,
  "isSuccess": true,
  "errorMessage": null,
  "result": {
    "houseId": 1,
    "houseName": "Shawpno Villa",
    "sqft": 800,
    "houseNoOrRoad": "Ovijan 294,College Road",
    "division": "Dhaka",
    "district": "Gazipur",
    "city": "Tongi",
    "houseAddress": "Shawpno Villa, Ovijan 294,College Road, Tongi, Gazipur, Dhaka",
    "bedNumber": 2,
    "balconyNumber": 1,
    "rent": 12000,
    "floorNumber": 2,
    "hasLift": false,
    "isAvailable": true,
    "contactNumber": "01345678189"
  }
}
```

rent filed has been updated.

**DELETE/api/v{version}/HouseFinderAPI/{id}** Endpoint:

Provide the id and version(1.0).
The house data associated with the id 1 will be deleted from the record.

If successfull you will get a response like this:

```bash
{
  "statusCode": 204,
  "isSuccess": true,
  "errorMessage": null,
  "result": null
}
```

If the id doesn't exist, then the response body should look like this:
```bash
{
  "statusCode": 404,
  "isSuccess": false,
  "errorMessage": [
    "Data doesn't exist."
  ],
  "result": null
}
```






