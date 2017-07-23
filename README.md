
# Wikia

![alt text](https://fablecode.visualstudio.com/_apis/public/build/definitions/81011f39-c070-4b51-8e1b-e56bfe2106c8/1/badge "Visual studio team services build status") 

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

For a list of all wikia endpoints, you should use {wikidomain}/api/v1/ format.

Example: For Yugioh Wiki Api endpoints, i'd use [Yugioh Wikia](http://yugioh.wikia.com/api/v1/)

## License

MIT License

Copyright (c) 2017 Dennis Poulton

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

