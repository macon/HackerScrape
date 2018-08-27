build-run: build run 

build: cleanup restore build

make-docker: cleanup publish-fdd docker-build

run-win: cleanup restore publish-win run-win-exe
 
run-mac: cleanup restore publish-mac run-mac-exe 

cleanup:
	dotnet clean

restore:
	dotnet restore

build:
	dotnet build

publish-fdd:
	dotnet publish -o ./pub -c Release
	
publish-win:
	dotnet publish -o pub -r win10-x64 --self-contained
	
run-win-exe:
	./pub/HackerScrape.CLI.exe --posts 25

run-mac-exe:
	./pub/HackerScrape.CLI --posts 25

publish-mac:
	dotnet publish -o pub -r osx-x64 --self-contained

docker-build:
	docker build -t hackerscrape:1.0 .

docker-run:
	docker run -ti hackerscrape:1.0 --posts 20
        
run:
	dotnet run -p HackerScrape.CLI.csproj