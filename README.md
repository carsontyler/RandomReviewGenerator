# Technical Assessment Exercise
##### Carson Tyler

### Requirements
- [.NET 8](https://dotnet.microsoft.com/en-us/download)
- Your favorite .NET 8 IDE

## Run the API

- Clone the repo and open in your IDE.
- Set the `RandomReviewGenerator` project as the startup project.
- Start the project with IIS Express (F5).
- This will launch the Swagger site for the API. The `/api/generate` endpoint can be can through this site if desired via "Try it out".
- To hit the API endpoint manually, open your preferred API platform and send a GET request to `https://localhost:44318/api/generate`
-- You may need to disable SSL Certificate Verification to successfully hit this endpoint.

## Run the site

- To run both the site and API, right click on the solution file and select "Properties".
- Select "Multiple startup projects" in the Startup Project tab.
- For both projects (RandomReviewGenerator and RandomReviewSite), select either "Start" or "Start without Debuggin" for the action.
- Start the app (F5).
- Both the Swagger site and website will run (`https://localhost:44385`). On the website, click the "Generate Review" button and a review will be generated. This can be run as many times as desired.

## Run unit tests
- To unit tests in your IDE, open the Test Explorer window and select `Run All Tests in View`

## Developer Notes
- I attempted to implement Docker but ran into a significant issue sending requests between the containers. I wasn't able to resolve this in a reasonable about of time but I've left in the code from the latest attempt; however, the containers are not in a working state. 
- I wasn't able to deploy to Azure as I've already used the free account. 