# 18 
# Leveraging Open-Source Frameworks


So far, we have learned the fundamentals of ASP.NET Core web API. ASP.NET Core is a very powerful framework that provides a comprehensive set of features to build web APIs. We also introduced some practices and patterns that can help us to build better web APIs. However, to implement a real-world web API, we still need some additional features, such as multi-tenancy, audit logging, and layered architecture. We can implement these features ourselves, but it will take a lot of time and effort. Fortunately, we do not need to reinvent the wheel. Many open-source frameworks and libraries can help us to build web API applications more efficiently. There is a Chinese saying: "_To do a good job, one must first sharpen one's tools._" We already have a good tool, ASP.NET Core, and now we will learn how to use other tools to build web APIs. 

In this chapter, we will introduce some popular open-source frameworks and libraries that can help us build web APIs efficiently following the best practices. These frameworks include the following: 

* ABP Framework 

* Clean architecture 

* Orchard Core 

* eShop 

* .NET Aspire 

We will explore their features, advantages, and practical use cases within the context of ASP.NET Core web API development. By the end of this chapter, you can choose whether you use these frameworks to start your web API project. Even if you do not use these frameworks, you can still learn some best practices from them. 

## How to choose a framework

In the context of software development, a framework refers to a pre-established, reusable structure that provides a foundation for building applications. It is a set of libraries, components, conventions, and tools that guide developers in creating software within a specific architectural style or pattern. A framework normally focuses on the architecture and design of the application. It does not implement the business logic. However, it may provide some common features that are required by most applications. Technically, ASP.NET Core is also a framework. In this chapter, the term _framework_ refers to a framework that is built on top of ASP.NET Core and provides additional features to build web APIs. They provide pre-built solutions and components, proven architectural patterns and best practices, and a collaborative community. Understanding and harnessing these frameworks can significantly enhance the productivity of developers. 

There are so many different open-source frameworks and libraries that it can be overwhelming to choose the right one. Choosing the right framework is a critical decision for any project. It is not only a technical decision but also related to the business. The following are some factors that you should consider when choosing a framework: 

* **Project requirements**: The first thing you should consider is whether the framework can meet the requirements of your project: 

    **Scalability**: Consider how your project will scale in the future. Some factors that you should consider include the number of users, the number of requests per second, concurrent requests, data volume, multi-tenancy, and so on. Check how the framework handles increasing traffic and data volume. 

    **Domain fit**: Consider what kind of application you are building. Some frameworks are designed for specific domains, such as e-commerce, **Content Management System** (**CMS**), or **Enterprise Resource Planning** (**ERP**). These frameworks may provide some domain-specific features that can boost your productivity. 

    **Extensibility**: Evaluate whether the framework can be extended to meet your future requirements and changes. A good framework should be flexible and extensible to adapt to evolving business requirements. 

* **Team skills**: Consider whether your team has the skills to use the framework and whether the framework is easy to learn: 

    **Technology stack**: Check whether your team and your organization are familiar with the technology stack of the framework and platform. A framework is often associated with a specific platform. Ensure that your team is familiar with the platform and the technology stack of the framework. 

    **Learning curve**: Assess the learning curve of the framework. A framework with a steep learning curve may require more time and effort to learn. If your team is not familiar with the framework, you may need to invest more time and effort into learning it. 

    **Development experience**: Evaluate the development experience of the framework and associated tools. Some teams may prefer convention over configuration, while others prioritize flexibility and configuration. Some frameworks may provide better tooling support, such as IDE integration, debugging, and testing. 

* **Community support and ecosystem**: It is important to consider the community support and ecosystem of the framework: 

    **Community support**: Check whether the framework has a large and active community. A large community can provide better support and more resources. You can check their GitHub repositories, forums, and Stack Overflow to see how active the community is. Normally, a framework with a large community has more contributors and stars on GitHub, as well as more questions and answers on Stack Overflow. You can also check their release history and GitHub issues to see how frequently the framework is updated and how quickly issues are resolved. 

    **Documentation**: Comprehensive, up-to-date, and high-quality documentation is crucial for effective adoption. Check whether the framework has well-maintained documentation. You can check their official website or GitHub wiki to see whether the documentation is easy to understand. 

    **Ecosystem**: Check whether the framework has a rich ecosystem. A rich ecosystem can provide more resources and tools that enhance the functionality and capabilities of the framework. Some frameworks may have a large number of plugins, extensions, and libraries, which means you can easily reuse existing components and solutions. 

    **Use cases and user reviews**: Investigate the use cases of the framework. A successful framework should have many real-world success stories. You can check their official website or GitHub repository to see how the framework is used in real-world projects. This can give you an indication of the maturity and stability of the framework. In addition, you can gather feedback and testimonials from other developers to see how they feel about the framework. 

* **Performance**: Performance is another important factor to consider when choosing a framework. Check the performance of the framework. A framework with poor performance may not be suitable for high-performance applications. You can check the benchmarks of the framework to see how it performs. When evaluating the performance, consider your project requirements and scenarios relevant to your project. Assess factors such as response time, throughput, latency, and memory usage. 

* **Security and compliance**: Evaluate the security and compliance of the framework. A framework with poor security may expose your application to security vulnerabilities. Check whether the framework has security features, such as authentication, authorization, and encryption. Check whether the framework is compliant with security standards, such as OWASP, GDPR, and HIPAA. 

* **Cost**: Check whether the framework is free to use. Some frameworks are free for non-commercial use but may require a license for commercial purposes. Some frameworks may have a free version that provides basic features only, but you need to pay for the full version. Some frameworks may charge a fee for business support. Other than the cost of the framework itself, you should also consider the cost of the infrastructure and resources required to run the framework, as well as the development effort and ongoing maintenance cost. 

* **Vendor lock-in**: Once you choose a framework, you may need to stick with it for a long time. It is important to consider whether the framework will lock you in. Some frameworks may have a specific architecture or design pattern, which may make it difficult to migrate to other frameworks. Consider factors such as the architecture, design pattern, and programming language of the framework. Also, you need to consider how the data is stored and whether it is easy to migrate the data to other platforms. Pay attention to any proprietary technology because it may lead to vendor lock-in. It is preferable to choose frameworks that adhere to open standards and use open-source technologies. 

Considering the preceding factors holistically can guide you in selecting a framework that aligns with your project requirements and team dynamics. However, as we have stated many times in this book, there is no silver bullet. There is no single perfect framework that can meet all your needs. You need to make trade-offs and choose the framework that best fits your requirements and constraints. 

Next, we will introduce some open-source frameworks that can help you build web APIs efficiently. The first one is ABP Framework, which is a comprehensive framework that provides a set of out-of-the-box features. 

## ABP Framework

ABP Framework is a comprehensive infrastructure for developing software solutions with modern architectures built on ASP.NET Core. This framework provides a robust foundation for creating applications that are reliable, secure, and scalable. It was originally developed based on the traditional ASP.NET MVC framework. Then it was rewritten on top of ASP.NET Core. It has been used in many real-world projects and has a large and active community. Currently, ABP Framework is maintained by Volosoft, a software company based in Turkey. 

Next, we will introduce the features of ABP Framework and how to use it to build web APIs. As their official documentation is very detailed, we will not repeat everything here. You can find more information about ABP Framework on their official website: https://abp.io. 

**Overview**

ABP Framework is a modular framework that provides a set of reusable components and services. It highly values **DDD**. It provides a set of DDD building blocks, such as aggregate roots, entities, repositories, and domain services. It also provides a set of base classes and interfaces that can be used to implement these building blocks. Here are some key features of ABP Framework: 

* **Microservice compatible**: ABP Framework is designed to build microservices. Each service is a separate application that can be deployed independently. They can use different databases. These services can communicate with each other using HTTP REST APIs or distributed events. APB Framework offers a built-in distributed event bus that enables services to integrate with RabbitMQ to publish and consume distributed events. 

* **Modular architecture**: ABP offers a modular architecture that enables developers to create reusable application modules, integrate with application lifecycle events, and define dependencies between core components of their system. This allows for greater flexibility and scalability, enabling businesses to create more efficient and effective applications. Many features of ABP Framework are implemented as modules and delivered as NuGet packages. You can easily add or remove modules to meet your requirements. 

* **DDD**: ABP Framework can help developers implement DDD-based layered architectures. It provides a startup template that implements the DDD architecture. You can use the predefined base classes, services, and interfaces to develop your application following the DDD principles. It also provides a detailed documentation that explains how to implement DDD-based architectures. 

* **Multi-tenancy support**: ABP Framework can help you build **Software-as-a-Service** (**SaaS**) applications. It provides multi-tenancy support out of the box. You can easily enable multi-tenancy in your application by adding a few lines of code. 

* **Authentication and authorization**: ABP Framework offers a rich set of authentication and authorization features. It can integrate with external identity providers, such as IdentityServer and OpenIddict. It also provides a built-in role-based permission system. You can easily define your own roles and permissions to control access to your application. 

* **Audit logging**: ABP Framework can automatically log all the operations and data changes performed by users and applications. It can also log the exceptions and errors that occur in your application. You can easily configure the audit logging for auditing purposes. 

* **HTTP APIs and dynamic proxies**: ABP Framework can automatically expose your application services as HTTP REST APIs and generate dynamic JavaScript proxies and C# proxies to consume these APIs. You do not need to write the controllers manually. Instead, you can focus on the business logic of your application. 

* **UI components**: ABP Framework is not only a backend framework but also provides a set of frontend UI components, supporting MVC, Razor Pages, Blazor, Angular, and React Native. It provides different themes and layouts that can be used to build web applications. The default theme is free. You can also purchase other themes from the ABP Commercial Marketplace. 

* **ABP CLI**: ABP Framework has a CLI tool that can help you create and manage your ABP applications. You can use the CLI tool to create a new application, add a new module, add a new page, and so on. It can also help you to upgrade your application to the latest version of ABP Framework. 

ABP Framework is a free and open-source framework. You can use it to develop commercial applications without any restrictions. Volosoft also provides ABP Commercial, which adds more benefits on top of the free version, such as additional UI components and themes, more application modules, and a CRUD page generator. ABP Commercial also offers premium support and consulting services.  

Some of the application modules included in ABP Commercial are as follows: 

* **Account management**: This module provides a set of features for managing user accounts, such as user registration, login, and password reset. 

* **CMS kit**: This module provides building blocks to create your own CMS. 

* **File management**: This module allows you to manage files in your application, including uploading, downloading, and organizing files in a hierarchical structure. 

* **Identity management**: This module provides out-of-the-box features for managing users, roles, and permissions. It also provides a UI for IdentityServer and OpenIddict. 

* **Payment management**: This module provides integration with popular payment gateways, such as Stripe and PayPal. 

* **Language management**: This module is used to manage languages and localization resources in your application. 

You can find more information about ABP Commercial on their official website: https://commercial.abp.io/modules. 

**Getting started with ABP Framework** 

To use ABP Framework, you have various options: 

* **ABP CLI**: You can use the ABP CLI tool to create a new application. Use the following code to install the ABP CLI tool: 

      dotnet tool install -g Volo.Abp.Cli 

  When this book was written, the latest stable version of ABP Framework was 7.4. To use the preview version that supports .NET 8, you can use the following command to update the ABP CLI tool to the latest preview version: 

      dotnet tool update --global Volo.Abp.Cli --version 8.0.0-rc.3 

  Then you can use the following command to create a new web API application with a layered architecture: 

      abp new MyWebApiDemo -u none --mobile none --preview 

When you read the book, ABP Framework may have been updated to a newer version. Then you can omit the --preview option to use the latest stable version. Also, please note that there might be some breaking changes between different versions. If you find that the code in this book does not work, you can check the release notes and upgrade guides to see whether there are any breaking changes. You can find upgrade guides on the official website: https://docs.abp.io/en/abp/latest/Migration-Guides/Index. You can also check the GitHub repository and create an issue if you have any questions at https://github.com/abpframework/abp. 

By default, ABP CLI will create a web API application with a layered architecture. The default database provider is EF Core. You can customize the application following the documentation: https://docs.abp.io/en/abp/latest/CLI. 

* **ABP Suite**: If you purchase ABP Commercial, you can use the ABP Suite tool to create a new application. ABP Suite provides a graphical user interface that can help you create and manage your ABP applications. You can find more information about ABP Suite on their official website: https://commercial.abp.io/tools/suite. 

    ABP Framework is a modular framework. It has two types of modules: 

    * Framework modules: This type of module is provided by ABP Framework, including caching, event bus, emailing, serialization, validation, EF Core integration, and so on. These modules are commonly used in most applications. They do not have business logic. 

    * Application modules: These modules are used to implement the business logic for specific domains. For example, ABP provides a Blogging module that can be used to build a blogging application. These modules have their own entities, services, and repositories. 

    Using modules has the following benefits: 

   * We can encapsulate the business domain in a separate module. This can make the code more maintainable and reusable. You can even publish the module to the ABP Marketplace and share it with other developers. 

   * ABP Framework updates frequently. If we put all the code in a single module, sometimes it will be a bit difficult to upgrade the module if there are breaking changes. Separating the business logic into different modules can make it easier to upgrade the modules. 

As the official documentation of ABP Framework is very detailed, we will not repeat it here. You can play with the ABP CLI and explore the features of ABP Framework following their documentation: https://docs.abp.io/en/abp/latest, or check the source code on GitHub: https://github.com/abpframework/abp. 

**Summary of ABP Framework** 

ABP Framework is a powerful framework that can help you build web APIs efficiently. It provides a set of reusable components and services that can simplify the development of web APIs. It also provides a set of UI components that can be used to build web applications. It is a good choice if you want to build a web API application with a layered architecture. The documentation of ABP Framework is very detailed. They also provide quite a few samples, such as eShopOnAbp, EventHub, and BookStore. You can find the index of the samples at https://docs.abp.io/en/abp/latest/Samples/Index. Overall, ABP Framework is a high-quality, well-designed framework in the ASP.NET Core ecosystem. 

There are some challenges if you decide to use ABP Framework: 

* First, ABP Framework is a large framework with many features. It encapsulates many details and provides a lot of abstractions. In other words, if you want to take full advantage of ABP Framework, you need to follow its design. Especially if you want to apply DDD, you need to follow the DDD architecture of ABP Framework. However, if you are not familiar with DDD, it may make things more complicated. 

* ABP Framework is a modular framework. Many features are implemented as modules, which leads to a complex dependency graph. You need to understand the dependencies between modules to use them correctly. That means the learning curve of ABP Framework is a bit steep. 

* ABP Framework updates frequently. Sometimes it may break your application if you upgrade to the latest version. You need to pay attention to the release notes and upgrade guides when upgrading your application. 

In summary, ABP Framework is a great option if you want to build web API microservices that follow DDD patterns and a layered architecture. However, it is important to note that the advantages of ABP Framework can also be seen as disadvantages. To make the most of this framework, it is necessary to adhere to its design. Therefore, you need to carefully weigh the pros and cons of ABP Framework before using it. If you need some features such as multi-tenancy and audit logging, ABP Framework could be a great solution as these features would take a lot of time to implement from scratch. On the other hand, for smaller projects, ABP Framework may be overkill. It is highly recommended that you read their free e-book _Implementing Domain Driven Design_, which is a practical guide to applying DDD principles using ABP Framework. You can find the e-book at https://abp.io/books/implementing-domain-driven-design. 

If you feel that ABP Framework is too heavy, two popular repositories on GitHub provide a starting point for clean architecture: _Clean Architecture_ by Ardalis and _Clean Architecture_ by Jason Taylor. We will introduce these two repositories in the next section. 

## Clean architecture 

As we discussed in _Chapter 17_, clean architecture is not a specific framework or library. However, some frameworks can be used as a starting point to implement clean architecture. In this section, we will introduce two popular frameworks that can help you implement clean architecture. 

If you search for clean architecture on GitHub, you will find many repositories that implement clean architecture. Two popular repositories are worth mentioning: 

* _Clean Architecture_ by Ardalis 

* _Clean Architecture_ by Jason Taylor 

Both repositories provide a great starting point for developers looking to implement clean architecture. Both are based on ASP.NET Core and share similar concepts. Let's take a closer look at each of them. 

**Clean Architecture by Ardalis**

Ardalis (Steve Smith) is a software architect with over 20 years of experience. He has been awarded Microsoft MVP for 20 years. He shares his experience on his blog: https://ardalis.com. You can find his courses on Pluralsight: https://www.pluralsight.com/authors/steve-smith. He created quite a few popular open-source projects on GitHub. By the way, he is also the author of the book _Architecting Modern Web Applications with ASP.NET Core and Microsoft Azure_, which we introduced in _Chapter 17_. 

Ardalis has created a highly popular repository on GitHub at https://github.com/ardalis/CleanArchitecture, providing a starting point for clean architecture with ASP.NET Core. As of the writing of this book, the repository has earned over 14,000 stars on GitHub. 

This template consists of four projects: 

* **Core**: This project is the center of the clean architecture design. It is independent of other projects. This project contains the domain models, such as entities, aggregates, value objects, domain events, domain event handlers, and domain services,. It also uses MediatR to implement the mediator pattern. 

* **UseCases**: This is also referred to as the application layer. This project uses the CQRS pattern to implement the application logic. In this layer, you can define commands and queries using the repository pattern. It depends on the Core project. 

* **Infrastructure**: This project contains the implementation of the interfaces defined in the Core project. It depends on the Core project. For example, the template provides an implementation of the email service. Also, it integrates with EF Core to implement the repository pattern. If you want to use other database providers, you can use other ORM frameworks, such as Dapper, to replace EF Core. 

* **Web**: This is the entry point of the ASP.NET Core web project. It depends on other projects. It contains the controllers, views, and other web-related components. It also contains the configuration of the application. 

Besides the above projects, the template also provides a test folder, which contains the unit tests, integration tests, and functional tests. This gives you a good starting point to write tests for your application. 

This template depends on the following NuGet packages developed by Ardalis: 

* **ApiEndpoints**: https://github.com/ardalis/apiendpoints. This package simplifies the implementation of API endpoints. You can use folders to organize your endpoints for different resources. 

* **GuardClauses**: https://github.com/ardalis/guardclauses. This package checks invalid inputs and fails fast immediately. 

* **Result**: https://github.com/ardalis/result. This package provides a result abstraction that can be mapped to HTTP status codes. 

* **SharedKernel**: https://github.com/ardalis/Ardalis.SharedKernel. This package contains some useful base classes and interfaces, such as EntityBase, IAggregateRoot, ValueObject, ICommand, IQuery, and IRepository. 

* **SmartEnum**: https://github.com/ardalis/SmartEnum. This package provides a base class for quickly creating strongly typed enum alternatives in C#. 

* **Specification**: https://github.com/ardalis/specification. This package provides a base class to add specifications to your domain models. Specifications can be used to filter, sort, and query entities. 

Ardalis has created an impressive number of useful NuGet packages, and more information about them can be found on his GitHub page: https://github.com/ardalis. 

To use this clean architecture template, you have two options: 

* Clone the repository to your local machine and use it as a starting point for your application. 

* Install the template using the following command: 

      dotnet new install Ardalis.CleanArchitecture.Template 

  Then you can use the following command to create a new application: 

      dotnet new clean-arch -o Your.ProjectName 

Then you can open the solution in Visual Studio or VS Code and start developing your application following the clean architecture design. 

In summary, this template is a lightweight and flexible starting point to build your applications following clean architecture concepts. It does depend on the Ardalis NuGet packages, however, these packages are well-maintained and have a large number of downloads, so this should not be a problem. If you prefer not to use these packages, you can remove them and implement the features yourself. 

**Clean Architecture by Jason Taylor** 

Jason Taylor is a solution architect, trainer, mentor, and full-stack developer with 20 years of professional experience. He is also a Microsoft MVP. You can find his blog at https://jasontaylor.dev. He created a popular clean architecture template on GitHub: https://github.com/jasontaylordev/CleanArchitecture. At the time of writing this book, the repository has earned over 14,300 stars on GitHub. 

Similar to the template by Ardalis, this template also consists of four projects: 

* **Domain**: This project contains entities, enums, exceptions, interfaces, types and domain logic, and so on. It does not depend on any other projects. 

* **Application**: This project represents the application layer. It contains the application logic implemented using the CQRS pattern, which consists of commands, queries, and handlers. It depends on the Domain project. 

* **Infrastructure**: This project contains the implementation of the interfaces defined in the Application layer. These implementations include data access, external API integration, email service, file storage, and so on. It depends on the Domain and Application projects. 

* **WebUI**: This is the presentation layer of the application. It is implemented as a **Single Page Application** (**SPA**) using Angular or React. 

Similarly, it has a tests folder that contains unit tests and integration tests so you can start writing tests for your application. 

This template depends on the following popular NuGet packages: 

* **AutoMapper**: This package is used for object-to-object mapping. It can help you map between different types, such as between domain models and DTOs. 

* **MediatR**: This is used to implement the CQRS pattern. It provides a mediator to send commands and queries to their handlers. 

* **FluentValidation**: This package is used to validate the input data. It provides a fluent API to define the validation rules. 

* EntityFrameworkCore**: EF Core is used to access the database. 

* **xUnit, Moq, and Shouldly**: These packages are used to write tests. Shouldly is similar to FluentAssertions, which we introduced in _Chapter 9_. 

These packages are mature and well maintained by the community, so you can use them with confidence. This template provides a Todo management application as an example. It also has a basic role-based authorization system. You can use this template as a starting point to build your own applications. 

To use this template, you can install the template using the following command: 

    dotnet new install Clean.Architecture.Solution.Template 

Then you can use the following command to create a new application: 

    dotnet new ca-sln -cf None -o YourProjectName 

Other than that, the template also provides commands to create commands and queries. For example, you can create a new command using the following command: 

    dotnet new ca-usecase --name CreateTodoList --feature-name TodoLists --usecase-type command --return-type int 

Use the following command to create a new query: 

    dotnet new ca-usecase -n GetTodos -fn TodoLists -ut query -rt TodosVm 

These commands can help you quickly create commands and queries for the CQRS pattern. 

As its name suggests, this template is a clean template that follows the clean architecture design. It uses mature and popular NuGet packages. So, it is a good option if you do not want to depend on too many dependencies. 

**Summary of clean architecture templates**

It can be difficult to say which template is superior. Both Ardalis and Jason Taylor are experienced solution architects and Microsoft MVPs. Both templates are lightweight, well designed, and maintained. They have many similarities and share the same concepts. For example, both templates use the MediatR package to implement the CQRS pattern. You can use either of these templates to build your applications following the clean architecture design. You can even build your own template based on these templates because they are open-source. 

It can be difficult to determine which template is superior. Both Ardalis and Jason Taylor are highly experienced solution architects and Microsoft MVPs, and both templates are well crafted and kept up to date. There are many similarities between the two, as they both utilize the MediatR package to implement the CQRS pattern and share the same concepts. If you find ABP Framework too cumbersome, you can use either of these templates to build applications following the clean architecture design. Furthermore, you can even create your own template based on these templates since they are open-source. 

Next, we will introduce another open-source framework, Orchard Core, which is a CMS framework built on top of ASP.NET Core. 

## Orchard Core 

Orchard Core is a free, open-source, modular, multi-tenant application framework built with ASP.NET Core. Similar to ABP Framework, Orchard was originally built on top of ASP.NET MVC, then it was refactored and rewritten with ASP.NET Core. It is a mature framework that can be used to speed up the development of CMS applications. 

Compared to ABP Framework, Orchard Core is more focused on CMS applications. It provides a set of reusable components for building CMS applications, such as content management, media management, localization, multi-tenancy, workflow, and security. It also provides a rich set of UI themes so you can easily build a website in minutes. It is a good choice if you want to build a CMS application using ASP.NET Core. 

You can find more information about Orchard Core on their official website: https://www.orchardcore.net. The source code is hosted on GitHub: https://github.com/OrchardCMS/OrchardCore. The documentation is available at https://docs.orchardcore.net. At the time of writing this book, the repository has earned nearly 7,000 stars on GitHub. 

Orchard Core consists of two parts: 

* **Orchard Core Framework**: This is the core framework of Orchard Core. It is used to build Orchard modules. If you only need to develop a web API application, you can use this framework. 

* **Orchard Core CMS**: This is a CMS application built on top of the Orchard Core Framework. It provides a complete CMS solution including liquid templates, live previews, and so on. These components would be useful if you want to build a CMS application. However, if you are developing a web API application, you do not need to use this part. 

Orchard Core supports a few ways to create a CMS application: 

* **Full CMS**: In this way, you can use the Orchard Core theme and templates to spin up a CMS application in minutes. You just need to customize the content and theme to meet your requirements. 

* **Decoupled CMS**: In this mode, you need to create a separate frontend application and use the Orchard Core as the backend. You can use Razor Pages or MVC to implement the frontend application. 

* **Headless CMS**: This is similar to the decoupled CMS. Orchard Core only manages the content and provides a REST API or GraphQL API to access the content. You can use any frontend framework to build the frontend application. 

To use the Orchard Core framework, you just need to clone the repository to your local machine and open the solution in Visual Studio or VS Code. Then you can start developing your application. Alternatively, you can create a new ASP.NET Core application and add the Orchard Core NuGet packages to your application. This approach needs a bit more effort because you need to configure the application manually. You can find more information about how to use the Orchard Core framework in the documentation: https://docs.orchardcore.net/en/latest/docs/getting-started/. 

Again, Orchard Core is primarily designed for CMS applications. If your application requires complex business logic, then this framework may not be the best fit. However, if you are looking to create a CMS application or website for a small business, Orchard Core can help you build it quickly and efficiently. Next, we will explore eShop, which is a reference application published by Microsoft to demonstrate how to build cloud-native microservice applications using ASP.NET Core. 

## eShop 

eShop is a reference application that demonstrates how to build a cloud-native microservice-based application using .NET Core and Docker, and optionally Azure, Kubernetes, and Visual Studio. Technically, it is not a framework or library. However, it is a great example of how to utilize the latest technologies to build a microservice-based application. 

You can find the source code of eShopOnContainers on GitHub: https://github.com/dotnet/eShop. It is a full-functioning e-commerce application that can be used to sell products online. The application implements some basic features of an e-commerce application: 

* Product catalog 

* Filtering and searching 

* Shopping basket management 

* Ordering and checkout 

* Account management 

* Orders management 

To demonstrate the microservice architecture, the application has been split into multiple microservices: 

* **Identity microservice**: This microservice is used to manage the user accounts. It is implemented using ASP.NET Core Identity and SQL Server database. 

* **Catalog microservice**: This microservice is used to manage the product catalog. It uses EF Core to implement CRUD operations on the SQL Server database. 

* **Ordering microservice**: This microservice is used to manage the orders. It is an example of DDD-based architecture. 

* **Basket microservice**: This microservice is an example of a CRUD microservice using Redis Cache. 

* **Payment microservice**: This microservice is used to process the payment. 

* **Admin microservice**: This microservice is used to manage the application. 

There are some other components in this application, such as mobile BFF, Web App, Blazor App, and Mobile App. 

The eShop application is a showcase of Microsoft technologies, so you can find many cool features from Microsoft in this application. The latest version of eShop also uses OpenAI to generate product and category descriptions. Microsoft has published a free e-book that explains how to architect and develop cloud-native .NET applications for Azure. We have referenced this book in _Chapter 17_. You can find the e-book _.NET Microservices: Architecture for Containerized .NET Applications_ at https://learn.microsoft.com/en-us/dotnet/architecture/microservices/. It is highly recommended that you read this book to extend your knowledge of cloud-native applications. 

Besides eShop, Microsoft has also published a few other reference applications based on the eShop application: 

* **eShopOnContainers**: This is the original eShop application; now it is deprecated. But you can find its repository at https://github.com/dotnet-architecture/eShopOnContainers/tree/dev. Note that the repository has been archived and is no longer maintained. 

* **eShopOnWeb**: https://github.com/dotnet-architecture/eShopOnWeb. This is a monolithic ASP.NET Core MVC application. 

* **eShopOnBlazor**: https://github.com/dotnet-architecture/eShopOnBlazor. This sample application uses Blazor to implement the frontend. It is useful for developers who want to migrate their existing ASP.NET Web Forms applications to Blazor. 

* **eShopOnDapr**: https://github.com/dotnet-architecture/eShopOnDapr. This sample application is powered by Dapr. **Dapr** is an open-source, event-driven distributed application runtime that helps developers build resilient, stateless, and stateful microservice applications that run on the cloud and edge. Using Dapr significantly simplifies the original eShopOnContainers application. As Dapr is beyond the scope of this book, we will not discuss it further here. For more information about Dapr, please visit https://dapr.io. 

The eShop sample application is an excellent example of how to construct a microservice-based application using ASP.NET Core and Azure. It provides many useful features and best practices that can be leveraged for learning purposes. However, Microsoft does not recommend using this application in a production environment. Therefore, it is best suited for educational purposes. 

As we mentioned earlier, eShop uses many cool features from Microsoft as a showcase. One latest update is .NET Aspire, which is a new framework for building cloud-native applications. We will introduce .NET Aspire in the next section. 

## .NET Aspire 

Microsoft unveiled .NET Aspire at the .NET Conf 2023 in November 2023. This new framework is designed to facilitate the development of cloud-native distributed applications with built-in observability. Unlike frameworks such as ABP Framework or Clean Architecture, .NET Aspire does not prioritize DDD or layered architecture. Instead, it focuses on microservice orchestration, cloud-native development, and observability. So, why did Microsoft create this? 

Developers building microservice applications with ASP.NET Core must use a variety of tools, cloud services, and frameworks. Cloud providers offer a range of features, including hosting, caching, logging, and monitoring, but developers must still integrate these features into their applications, akin to building with Lego blocks. Examples include the following: 

* Containerization 

* Monitoring 

* Logging 

* Tracing 

* Caching 

* Messaging 

* Authentication 

* Authorization 

* Service discovery 

* Configuration 

* Resilience and retry 

This list can be very long. The development process can be lengthy and labor-intensive. Before developers can begin to address the business's needs, they must first complete a long list of tasks. This requires a significant amount of time and effort. 

.NET Aspire aims to simplify this process by providing a comprehensive toolset that can be used to build cloud-native applications with enhanced orchestration and observability: 

* .NET Aspire is a cloud-ready stack for building observable, resilient, and configurable cloud-native applications. 

* .NET Aspire provides a set of components that can be used to build cloud-native applications, including telemetry, resilience, configuration, health checks, and composition. It also provides many components for integrating with cloud services, such as storage, database, messaging, caching, and security. 

* .NET Aspire makes service discovery much easier. It provides a built-in service discovery mechanism that can automatically discover services using named endpoints. 

At the time of writing this book, .NET Aspire is still in the early stages of development. To use it, you need to install Visual Studio 2022 Preview and install the .NET Aspire workload: 

![Fig_18.1](https://github.com/PacktPublishing/Web-API-Development-with-ASP.NET-Core-8/blob/main/samples/chapter18/Aspire/images_/B18971_18_01.PNG)

Figure 18.1 – Installing the .NET Aspire workload 

Alternatively, if you use .NET CLI, you can install the .NET Aspire workload using the following command: 

    dotnet workload update 

    dotnet workload install aspire 

To check whether the installation is successful, you can use the following command: 

    dotnet workload list 

You should see the following output: 

    Installed Workload Id      Manifest Version                     Installation Source 

    ----------------------------------------------------------------------------------- 

    aspire                     8.0.0-preview.1.23557.2/8.0.100      VS 17.9.34321.82 

 

Use `dotnet workload search` to find additional workloads to install. 

When you read this book, the version number may be different. Also, you need to have Docker Desktop installed on your machine. 

Next, let us create a new .NET Aspire application and see how it works. 

**Creating a new .NET Aspire application**

There are two options for creating a new .NET Aspire application: 

* **.NET Aspire Application**: This option creates the minimal .NET Aspire application that includes the orchestration and shared services only. You can add other services and frontend applications later. 

* **.NET Aspire Starter Application**: This option creates a full-stack application that includes the backend application and the frontend application. 

To demonstrate the features of .NET Aspire, we will create a .NET Aspire Starter Application: 

1. You can use the following command to create a new .NET Aspire starter application: 

       dotnet new aspire-starter --use-redis-cache -n AspireDemo 

2. The --use-redis-cache option is used to enable Redis cache. 

3. You can also use Visual Studio 2022 Preview to create a new .NET Aspire application: 

![Fig:18.2](https://github.com/PacktPublishing/Web-API-Development-with-ASP.NET-Core-8/blob/main/samples/chapter18/Aspire/images_/B18971_18_01.PNG)

Figure 18.2 – Creating a new .NET Aspire application using Visual Studio 2022 Preview 

4. Choose the **.NET Aspire Starter Application** template and click the **Next** button. Then you can follow the wizard to create a new .NET Aspire starter application. 

5. The starter application is pre-configured to quickly get you started with .NET Aspire. The created sample application includes the following projects: 

       AspireDemo.AppHost: This project orchestrates different projects and services. It is the entry point of the application. 

       AspireDemo.ServiceDefaults: This is a shared project that manages configurations for resilience, service discovery, and observability. 

       AspireDemo.ApiService: This project is a minimal API project to provide a REST API. It is similar to the default ASP.NET Core Web API template. 

       AspireDemo.Web: This project is a Blazor application that consumes the REST API provided by the AspireDemo.ApiService project. 

    Both AspireDemo.ApiService and AspireDemo.Web projects depend on the AspireDemo.ServiceDefaults project. 

6. Next, we can run the AspireDemo.AppHost project. Navigate to the AspireDemo.AppHost folder and use the following command to run the project: 

       dotnet run 

    You should see the following output: 

       info: Aspire.Dashboard.DashboardWebApplication[0] 

             Now listening on: http://localhost:15162 

       info: Aspire.Dashboard.DashboardWebApplication[0] 

             OTLP server running at: http://localhost:16284 

       info: Microsoft.Hosting.Lifetime[0] 

             Application started. Press Ctrl+C to shut down. 

       info: Microsoft.Hosting.Lifetime[0] 

             Hosting environment: Development 

       info: Microsoft.Hosting.Lifetime[0] 

7. The port number may be different when you run your application. The output shows a dashboard URL. In this case, it is http://localhost:15162. Open the URL in your browser, and you should see the following dashboard: 

![Fig:18.3](https://github.com/PacktPublishing/Web-API-Development-with-ASP.NET-Core-8/blob/main/samples/chapter18/Aspire/images_/B18971_18_03.PNG)

Figure 18.3 – The .NET Aspire dashboard 

8. In the dashboard, you can find the links for the API and web applications. Click the web application link, and you will see a Blazor application that shows the weather forecast. 

9. Click the **Logs** menu. Here you can check the logs of the applications: 

![Fig:18.4](https://github.com/PacktPublishing/Web-API-Development-with-ASP.NET-Core-8/blob/main/samples/chapter18/Aspire/images_/B18971_18_04.PNG)

Figure 18.4 – The logs of the .NET Aspire application 

10. Click the **Traces** menu. Here you can check the traces of the applications: 

![Fig:18.5](https://github.com/PacktPublishing/Web-API-Development-with-ASP.NET-Core-8/blob/main/samples/chapter18/Aspire/images_/B18971_18_05.PNG)

Figure 18.5 – The traces of the .NET Aspire application 

You can also check the metrics of the applications: 

![Fig:18.6](https://github.com/PacktPublishing/Web-API-Development-with-ASP.NET-Core-8/blob/main/samples/chapter18/Aspire/images_/B18971_18_06.PNG)

Figure 18.6 – The metrics of the .NET Aspire application 

The dashboard is designed to provide visibility into the logs, traces, and metrics of applications. This allows users to quickly identify and address any issues that may arise, as well as gain insights into the performance of their applications. 

At first glance, the Blazor application and web API appear to be similar to the applications we have previously discussed. However, there is some magic at work beneath the surface: 

* The start project is AspireDemo.AppHost, but why can it run the Blazor application and the web API? 

* In the Program.cs file in the AspireDemo.Web project, WeatherApiClient uses http://apiservice as the base address. However, it does not seem to be a valid URL. How does it find the API service? 

* You can see that Docker started a Redis container. Where is the Redis cache configured and used in the application? 

* There is no configuration for logging, metrics, and tracing in the AspireDemo.ApiService project. How does it work? 

Let us try to answer these questions in the next section. 

Understanding the .NET Aspire application 

First, check the AspireDemo.AppHost project because it is the entry point of the application. Open the Program.cs file and you will find the following code: 

````
var builder = DistributedApplication.CreateBuilder(args); 

var cache = builder.AddRedisContainer("cache"); 

var apiservice = builder.AddProject<Projects.AspireDemo_ApiService>("apiservice");  

builder.AddProject<Projects.AspireDemo_Web>("webfrontend") 

    .WithReference(cache) 

    .WithReference(apiservice); 

builder.Build().Run();
```` 

The preceding code is similar to the Program.cs file of the ASP.NET Core Web API template. However, the type of the builder is IDistributedApplicationBuilder, while the type of the builder in the ASP.NET Core Web API template is IHostBuilder or IWebHostBuilder. IDistributedApplicationBuilder is a new type introduced by .NET Aspire. It is used to orchestrate various projects and services. 

IDistributedApplicationBuilder adds the projects using the AddProject method. The AddProject method is an extension method to add a project to the application. In the preceding code, it adds two projects: AspireDemo.ApiService and AspireDemo.Web. So, when the AspireDemo.AppHost project starts, it can run both the Blazor application and the web API. 

When the AspireDemo.AppHost project adds a project, it can specify the name of the project. For example, the preceding code names the API project as apiservice and the Blazor project as webfrontend. These names are used to identify the projects. Then it can call the WithReference method to service discovery information as environment variables from the referenced project. For example, the AspireDemo.Web project has a reference to the AspireDemo.ApiService project. That is why WeatherApiClient uses http://apiservice as the base address. The AspireDemo.Web project does not need to know the actual URL of the API service. It can automatically discover the API service using the name of the project. This is called service discovery. 

IDistributedApplicationBuilder also adds a Redis container named cache using the AddRedisContainer method, which is one of the built-in components of .NET Aspire. .NET Aspire supports many components out of the box, including the following: 

* Azure Cosmos DB 

* Azure Key Vault 

* Azure Service Bus 

* PostgreSQL 

* RabbitMQ 

* Redis 

* Redis Distributed Caching 

* SQL Server 

You can find the full list of supported components at https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/components-overview. 

These components are provided as NuGet packages. When your application needs these cloud-native services, you can add the corresponding NuGet packages to your application. This simplifies and standardizes the configuration of your application. For example, the AspireDemo.Web project has a reference to the cache component. So, you can find the following code in the Program.cs file of the AspireDemo.Web project: 

    builder.AddRedisOutputCache("cache"); 

The preceding code configures the Redis cache as the output cache, but it does not need to know where the Redis cache is. It just references the cache component. The AspireDemo.AppHost project will automatically configure the Redis cache and pass the configuration to the AspireDemo.Web project. This is much easier than configuring the Redis cache manually for the AspireDemo.Web project. 

Next, let us open the Extensions.cs file in the AspireDemo.ServiceDefaults project. You will find an AddServiceDefaults method, which is used to configure the default services, including OpenTelemetry, health checks, service discovery, resilience policies, and so on. If you need more advanced configurations, you can add your own configuration code here. 

The AddServiceDefaults method is called in the Program.cs file in both the AspireDemo.ApiService and AspireDemo.Web projects. So, both projects can use the default configurations provided by the AspireDemo.ServiceDefaults project. That is why they can have logging, metrics, and tracing without any configuration. 

So far, we have explored the Aspire starter application and learned how it works. .NET Aspire is still in the early stages of development. As Microsoft stated, “_.NET Aspire is an opinionated, cloud ready stack for building observable, production ready, distributed applications._” It is believed that .NET Aspire will continue to evolve and become a powerful framework for building cloud-native applications. It is worth keeping an eye on this framework. 

To learn more about .NET Aspire, you can check the official documentation at https://learn.microsoft.com/en-us/dotnet/aspire/. As this framework is new, the documentation may not be complete. If you can find any issues, you can report them at https://github.com/dotnet/aspire. 

## Summary

In this chapter, we have introduced several popular frameworks that can help you build ASP.NET Core Web API applications. These frameworks are all open-source, well-supported, and have a large community. Let us summarize these frameworks. 

ABP Framework is designed to help you build applications following DDD and Clean Architecture. It provides many built-in modules that can be used to build applications quickly. If you appreciate DDD and layered architecture, ABP Framework is a solid choice. However, you need to consider the learning curve. If your project is small, ABP Framework may be overkill. 

Clean Architecture by Ardalis and Clean Architecture by Jason Taylor are two popular templates that can be used to build applications following Clean Architecture. They are lightweight and flexible. If you prefer fewer dependencies, these templates are good options. However, they do not provide many built-in features. You need to implement them yourself. 

Orchard Core is designed to quickly build CMS applications. It is also modular and provides many built-in features. If you want to build a CMS application, Orchard Core is a suitable choice. 

eShop is a reference application that demonstrates how to build a microservice-based application using .NET Core and microservices. If you want to learn more about microservices and cloud-native applications, eShop serves as a valuable example. 

.NET Aspire is a new framework that focuses on microservice orchestration, cloud-native development, and observability. It does not prioritize DDD or layered architecture. You can use any architecture you like. But .NET Aspire provides many built-in components that can help you build cloud-native applications quickly. 

It is important to note that these frameworks are not mutually exclusive. You can combine them to build your applications. For example, you can use .NET Aspire to orchestrate your microservices and use ABP Framework or Clean Architecture templates to build your microservices. The possibilities are endless. You can choose the frameworks that best suit your needs. 

It is time to wrap up this book. We hope you have enjoyed reading this book and have gained valuable insights into ASP.NET Core. ASP.NET Core is a great framework for building web APIs. However, the more we learn, the more we realize that there is still much to discover. We hope this book has provided you with a solid foundation and inspired you to continue learning. As the Chinese proverb goes, "_The sea of learning knows no bounds; only those who are willing to learn will be able to reach the other shore._" We wish you all the best in your future endeavors and continued success in your learning journey. Happy coding! 
