# HackerScrape

HackerScrape is a HackerNews scraper.

## Build

####Pre-requisites
You'll need .Net Core 2.1 installed.

I've included a `Makefile` for convenience. Please install `Make` for your platform before building. Alternatively just run the `Makefile` commands direclty.

After getting repo, navigate from the repo root to `./src/HackerScrape.CLI`.

### Windows
To build and run:

```
make run-win
```

To run directly again:

```
.\pub\hackernews.exe --posts 2
```

To run from a Docker container:

```
make make-docker
make run-docker
```

_(nb. I haven't tested the Docker container from Windows, only Mac)_

### Mac

```
make run-mac
```

To run directly:

```
./pub/hackernews --posts 2
```

To run from a Docker container:

```
make make-docker
make run-docker
```

### Notes
I've made the assumption that HN consistently uses 30 items per page. When you make a request
for more than 30 items, HackerScrape will issue parallel page requests to satisfy the number of items required,
e.g. for 50 items it will make two page requests (a balance between speed and robustness.)

Once the pages have been fetched and parsed, the CLI will order them by Rank and return the required number of items.

From the spec I wasn't sure if you wanted me to exclude the items that failed the stated rules (e.g. points must be >= 0)
or modify them to satisfy the rules. As it was more complicated to omit failed items but still be sure to return the number
of items required, I went with the latter approach to modify items to fit the rules.

* I've used the ScrapySharp library to help with HTML parsing and CSS selection.
* XUnit for unit tests (simple and well supported)
* Shouldly for test assertions (could have been FluentAssertions)

I've described the approach taken here rather than litter the code with comments. I'm not a fan of
exhaustive code commenting unless I'm using a 3rd party tool (e.g. Swagger) to produce API docs etc. 