using NSwag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Vano.Tools.Azure.Model;

namespace Vano.Tools.Azure
{
    public static class TemplateFactory
    {
        public static async Task<IEnumerable<Template>> GetTemplates()
        {
            List<Template> templates = new List<Template>();    

            IEnumerable<Template> swaggerTemplates = await GetTemplatesFromSwagger();
            templates.AddRange(swaggerTemplates);
            
            IEnumerable<Template> gitHubTemplates = await GetTemplatesFromGitHubRepo();
            templates.AddRange(gitHubTemplates);

            return templates;
        }

        #region Swagger

        // For more info:
        // https://learn.microsoft.com/en-us/rest/api/appservice/
        // https://github.com/Azure/azure-rest-api-specs/blob/main/specification/web/resource-manager/readme.md
        public static async Task<IEnumerable<Template>> GetTemplatesFromSwagger()
        {
            List<Template> templates = new List<Template>();

            string swaggerTemplatesFolder = Template.GetSwaggerTemplatesFolder();

            await DownloadTemplatesFromGitHubRepo(swaggerTemplatesFolder, owner: "Azure", repo: "azure-rest-api-specs", path: "specification/web/resource-manager/Microsoft.Web/stable/2024-11-01");

            foreach (string filePath in Directory.GetFiles(swaggerTemplatesFolder))
            {
                IEnumerable<Template> templatesFromSwaggerFile = await GetTemplatesFromSwagger(filePath);

                templates.AddRange(templatesFromSwaggerFile);
            }

            return templates;
        }

        public static async Task<IEnumerable<Template>> GetTemplatesFromSwagger(string swaggerFilePath)
        {
            List<Template> templates = new List<Template>();

            try
            {
                OpenApiDocument document = await OpenApiDocument.FromFileAsync(swaggerFilePath);
                foreach (KeyValuePair<string, OpenApiPathItem> pathItem in document.Paths)
                {
                    foreach (KeyValuePair<string, OpenApiOperation> operationItem in pathItem.Value)                
                    {
                        try
                        {
                            Template template = new Template()
                            {
                                Category = "Swagger [" + operationItem.Value.Tags.FirstOrDefault() + "]",
                                Name = operationItem.Value.OperationId,
                                Summary = operationItem.Value.Summary,
                                Verb = operationItem.Key.ToUpper(),
                                Path = pathItem.Key,
                                Body = operationItem.Value.Parameters.Where(p => p.Kind == OpenApiParameterKind.Body).FirstOrDefault()?.ToSampleJson()?.ToString(),
                            };

                            templates.Add(template);
                        }
                        catch
                        {
                            // DO NOTHING
                        }                    
                    }
                } 
            }
            catch
            {
                // DO NOTHING
            }

            return templates;
        }

        #endregion

        #region GitHub Templates

        public static async Task<IEnumerable<Template>> GetTemplatesFromGitHubRepo()
        {
            List<Template> templates = new List<Template>();

            string gitHubTemplatesFolder = Template.GetGitHubTemplatesFolder();
            await DownloadTemplatesFromGitHubRepo(gitHubTemplatesFolder, owner: "jvano", repo: "VisualARM", path: "Templates");

            foreach(string filePath in Directory.GetFiles(gitHubTemplatesFolder))
            {
                TemplateDocument doc = TemplateDocument.FromFile(filePath);

                templates.AddRange(doc.Templates);  
            }

            return templates;
        }

        public static async Task DownloadTemplatesFromGitHubRepo(string downloadFolder, string owner, string repo, string path)
        {
            IEnumerable<Tuple<string, string>> templateFiles = await GetTemplatesFilesFromGitHubRepo(owner, repo, path);
            if (templateFiles.Any())
            {
                using (HttpClient client = new HttpClient())
                {
                    foreach (Tuple<string, string> templateFile in templateFiles)
                    {
                        try
                        {
                            string name = templateFile.Item1 as string;
                            string downloadUrl = templateFile.Item2 as string;

                            Trace.WriteLine($"Downloading {name} from {downloadUrl}...");
                            byte[] fileBytes = await client.GetByteArrayAsync(downloadUrl);

                            string localFile = Path.Combine(downloadFolder, name);

                            // if file exists is overwritten.
                            File.WriteAllBytes(localFile, fileBytes); 
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message);
                        }
                    }
                }
            }
        }

        public static async Task<IEnumerable<Tuple<string, string>>> GetTemplatesFilesFromGitHubRepo(string owner, string repo, string path)
        {
            List<Tuple<string, string>> templateFiles = new List<Tuple<string, string>>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("VisualARM", "1.0"));

                string apiUrl = $"https://api.github.com/repos/{owner}/{repo}/contents/{path}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var items = JsonSerializer.Deserialize<JsonElement>(json);

                    foreach (var item in items.EnumerateArray())
                    {
                        string type = item.GetProperty("type").GetString();

                        if (type == "file")
                        {
                            string name = item.GetProperty("name").GetString();
                            string itemPath = item.GetProperty("path").GetString();
                            string downloadUrl = item.GetProperty("download_url").GetString();

                            templateFiles.Add(new Tuple<string, string>(name, downloadUrl));
                        }
                    }
                }
            }

            return templateFiles;
        }

        #endregion
    }
}
