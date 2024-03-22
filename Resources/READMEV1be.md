# Flowy
<img src="https://raw.githubusercontent.com/rmacellaro/flowy-solution/master/Documentation/logo.jpg" width="100" height="100">

it is a dotnet project that interfaces the Camunda 8 Self-Managed workflow engine, the system is made up of two projects:
- a dotnet webapi api that exposes an api rest service which interfaces the camunda workflow engine (this project).
- a frontend project developed with Angular that exposes the [flowy-app](https://github.com/rmacellaro/flowy-app) web interface.

## Flowy Solution
This project deals with interfacing the features of [Camunda 8](https://camunda.com/) and completing them with additional features, this dotnet solution is made up of 3 projects:

![Swagger Documentation](https://raw.githubusercontent.com/rmacellaro/flowy-solution/master/Documentation/swagger.jpg)

### Flowy.Camunda

This dotnet library works as a client and connects to [Camunda 8 self-managed](https://docs.camunda.io/docs/self-managed/about-self-managed/), for the local development phase it is necessary to use a local distribution of camunda, you can refer to this guide to run a doker instance with everything what is needed to start development.

In order to interface with the Camunda bees it is necessary to register flowy as a client on the keycloak access manager.

![Keycloak Documentation](https://raw.githubusercontent.com/rmacellaro/flowy-solution/master/Documentation/keycloak.jpg)

### Flowy.core

It is a library that interfaces with a local mysql database for storing the data necessary for everything related to the features exposed by Flowy, it mainly concerns modeling features, such as saving drafts of process in bpmn 2 (xml) format or form interfaces in json format.

### Flowy.api
It is the rest api project to expose the functionality of the system, it consists of self-generated swagger documentation.
