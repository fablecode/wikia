
![alt text](https://fablecode.visualstudio.com/_apis/public/build/definitions/81011f39-c070-4b51-8e1b-e56bfe2106c8/1/badge "Visual studio team services build status") 

# Wikia
Wikia is a C# library that makes it easy to access Wiki data.

## How?
Every wiki has its API accessible through URL: {wikidomain}/api/v1/.

For example:

1. [http://www.wikia.com/api/v1/](http://www.wikia.com/api/v1/)
2. [http://yugioh.wikia.com/api/v1/](http://yugioh.wikia.com/api/v1/)
3. [http://naruto.wikia.com/api/v1/](http://naruto.wikia.com/api/v1/)
4. [http://elderscrolls.wikia.com/api/v1/](http://elderscrolls.wikia.com/api/v1/)

For a quickstart, http://api.wikia.com/wiki/Quick_Start

For documentation, http://api.wikia.com/wiki/Documentation

## NuGet

    PM> Install-Package wikia

## Quickstart

```csharp

// wiki domain
string domainUrl = "http://yugioh.wikia.com";

// Article endpoint
IWikiArticle articles = new WikiArticle(domainUrl);

// Get Yugioh Wiki new articles
var result = articles.NewArticles();
```

## Endpoints

For a list of all endpoints, visit wiki api using {wikidomain}/api/v1/ format.

Example: For Yugioh Wiki Api endpoints, i'd use [http://yugioh.wikia.com/api/v1/](http://yugioh.wikia.com/api/v1/).

Notice the domain is "http://yugioh.wikia.com" and the suffix is "/api/v1/"

## License

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.
