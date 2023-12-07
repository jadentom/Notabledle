
When publishing to docs

Extract from the wwwroot folder

https://www.linkedin.com/pulse/publish-blazor-any-webapp-github-pages-free-rikam-palkar/

1. First, add an empty file and give an extension of ".nojekyll":The .nojekyll file is a special file used in GitHub repositories, and its significance lies in its ability to control how GitHub Pages, the platform's static site hosting service, processes your repository's content. If you miss this file, you might get the error for loading BlazorWebAssebly.js.

2. Update <base> Tag:In the index.html file of your Blazor app, you'll find a <base> tag that sets the base URL for your app. Update this tag to reflect the correct base URL for your GitHub Pages repository. In your case, for a repository named "username.github.io", the <base> tag should look like this. Image 14. <Base> tag

