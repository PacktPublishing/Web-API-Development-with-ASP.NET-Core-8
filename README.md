# Web API Development with ASP.NET Core 8

<a href="https://www.packtpub.com/product/web-api-development-with-aspnet-core-8/9781804610954"><img src="https://content.packt.com/B18971/cover_image_small.jpg" alt="" height="256px" align="right"></a>

This is the code repository for [Web API Development with ASP.NET Core 8](https://www.packtpub.com/product/web-api-development-with-aspnet-core-8/9781804610954), published by Packt.

**Learn techniques, patterns, and tools for building high-performance, robust, and scalable web APIs**

## What is this book about?
Web API applications have become increasingly significant in recent years, fueled by the ever-accelerating pace of technological advancements. However, with this rapid evolution comes a pressing challenge: the need to create web API applications that are not only functional but also adaptable, maintainable, and scalable to meet the demands of users and businesses alike. This book will help you address this challenge head-on, equipping you with the knowledge and skills required to develop web API applications from scratch.
	
This book covers the following exciting features:
* Build a strong foundation in web API fundamentals
* Explore the ASP.NET Core 8 framework and other industry-standard libraries and tools for high-performance, scalable web APIs
* Apply essential software design patterns such as MVC, dependency injection, and the repository pattern
* Use Entity Framework Core for database operations and complex query creation
* Implement robust security measures to protect against malicious attacks and data breaches
* Deploy your application to the cloud using Azure and leverage Azure DevOps to implement CI/CD

If you feel this book is for you, get your [copy](https://www.amazon.com/dp/180461095X) today!

<a href="https://www.packtpub.com/?utm_source=github&utm_medium=banner&utm_campaign=GitHubBanner"><img src="https://raw.githubusercontent.com/PacktPublishing/GitHub/master/GitHub.png" 
alt="https://www.packtpub.com/" border="5" /></a>


## Instructions and Navigations
All of the code is organized into folders. For example, Chapter02.

The code will look like the following:
```
namespace MyFirstApi.Models;

public class Post
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
```

**Following is what you need for this book:**

This book is for developers who want to learn how to build web APIs with ASP.NET Core and create flexible, maintainable, scalable applications with .NET platform. Basic knowledge of C#, .NET, and Git will assist with understanding the concepts more easily.

With the following software and hardware list you can run all code files present in the book (Chapter 1-18).

## Software and Hardware List

| Chapter  | Software required                        | OS required                   |
| -------- | -----------------------------------------| ------------------------------|
| 1-18     | Visual Studio 2022 Community Edition     | Windows, macOS, or Linux      |
| 1-18     | Azure, Azure DevOps, GitHub              | Windows, macOS, or Linux      |
| 1-18     |Seq, Prometheus, Grafana, Jaeger          | Windows, Docker/Linux         |

### Errata
* Page 20, *Some other popular frameworks include XML-RPC, **SOAP PRC**, JSON-RPC, and gRPC.*  _it should be_  *Some other popular frameworks include XML-RPC, **SOAP RPC**, JSON-RPC, and gRPC.*
* Page 39, Command for creating a web API project says
`dotnet new webapi -n MyFirstApi -controllers
cd MyFirstApi
code .`
_it should be_
`dotnet new webapi -n MyFirstApi -controllers
cd MyFirstApi code`
The two commands should be placed in two separate lines.
* Page 68, Second code snippet says `var posts = await _postService.GetAllPosts();` _it should be_ `var posts = await _postsService.GetAllPosts();`

## Related products <Other books you may enjoy>
* Architecting ASP.NET Core Applications [[Packt]](https://www.packtpub.com/product/architecting-aspnet-core-applications-third-edition/9781805123385) [[Amazon]](https://www.amazon.com/dp/1805123386)

* ASP.NET 8 Best Practices [[Packt]](https://www.packtpub.com/product/aspnet-8-best-practices/9781837632121) [[Amazon]](https://www.amazon.com/dp/183763212X)

## Get to Know the Author
**Xiaodi Yan**
is a seasoned software engineer with a proven track record in the IT industry. Since 2015, he has been awarded Microsoft MVP, showcasing his dedication to and expertise in .NET, AI, DevOps, and cloud computing. He is also a Microsoft Certified Trainer (MCT), Azure Solutions Architect Expert, and LinkedIn Learning instructor. Xiaodi often presents at conferences and user groups, leveraging his extensive experience to engage and inspire audiences. Based in Wellington, New Zealand, he spearheads the Wellington .NET User Group, fostering a vibrant community of like-minded professionals.
Connect with Xiaodi on LinkedIn to stay updated on his latest insights.
