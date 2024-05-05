# University Tuition Management APIGateway w/ Docker
  The University API Gateway is a component of the university system that acts as a gateway for various microservices, handling requests from clients and routing them to the appropriate microservices. This README provides an overview of the API Gateway, including its design, assumptions, and encountered issues.

# Design
## Assumptions
- The API Gateway is designed to be a central entry point for the university system, providing a unified interface for clients to access the system's functionality. It is responsible for routing requests to the appropriate microservices based on the request URL and method.

- Gateway is implemented using Ocelot, an open-source API gateway framework for .NET. It runs as a standalone service, separate from the microservices it routes requests to. The gateway is deployed alongside the other microservices in the university system.

- The API Gateway uses routing rules defined in the ocelot.json configuration file to determine how to route incoming requests. Each route specifies the downstream path template, upstream path template, and the HTTP method to match. Requests are forwarded to the appropriate microservice based on these rules.


## Issues Encountered
- Authentication and Authorization: Initially, there were issues with setting up authentication and authorization using JWT tokens. The gateway was not properly validating tokens, leading to unauthorized access.

- Routing Configuration: There were issues with the routing configuration in the ocelot.json file. Incorrect routing rules led to requests being routed to the wrong microservices or not being routed at all. This was resolved by carefully reviewing and correcting the routing rules.

- Docker Configuration: Docker configuration for the API Gateway encountered issues during deployment. Misconfigurations in the docker-compose.yml file led to connectivity problems between the API Gateway and other microservices.

## Video
Youtube:


<a href="https://www.youtube.com/watch?v=08Uyy9V3qXU"><img src="https://img.youtube.com/vi/08Uyy9V3qXU/0.jpg" alt="vid" border="0" width="355" height="200" /></a>


Drive:


<a href="https://drive.google.com/file/d/1mynvLm74vLQi2rvZy-6aaqm8dus-dqUl/view?usp=sharing"><img src="https://i.ibb.co/82jQpBK/vid.png" alt="vid" border="0" width="355" height="200" /></a>


