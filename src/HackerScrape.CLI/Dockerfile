FROM microsoft/dotnet:2.1-runtime
WORKDIR /app
COPY ./pub .
ENTRYPOINT ["dotnet", "HackerScrape.CLI.dll"]
CMD ["--posts", "10"]
#ENTRYPOINT /bin/sh