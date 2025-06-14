using NSwag;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Vano.Tools.Azure.Model;

namespace Vano.Tools.Azure
{
    public static class TemplateFactory
    {
        // For more info:
        // https://learn.microsoft.com/en-us/rest/api/appservice/
        // https://github.com/Azure/azure-rest-api-specs/blob/main/specification/web/resource-manager/readme.md
        public const string SwaggerBranch = "stable";
        public const string SwaggerApiVersion = "2024-11-01";
        public const string SwaggerEndpointBase = "https://raw.githubusercontent.com/Azure/azure-rest-api-specs/main/specification/web/resource-manager";
        public const string SwaggerEndpointWeb = SwaggerEndpointBase + "/Microsoft.Web/" + SwaggerBranch + "/" + SwaggerApiVersion + "/";
        
        public static async Task<IEnumerable<Template>> GetTemplates()
        {
            IEnumerable<Template> swagger = await GetTemplatesFromSwagger();
            IEnumerable<Template> builtIn = GetBuiltInTemplates();

            return swagger.Union(builtIn);
        }

        public static async Task<IEnumerable<Template>> GetTemplatesFromSwagger()
        {
            if (!File.Exists(Path.Combine(Path.GetTempPath(), "AppServicePlans.json")) ||
                !File.Exists(Path.Combine(Path.GetTempPath(), "WebApps.json")))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(address: new Uri(SwaggerEndpointWeb + "CommonDefinitions.json"), fileName: Path.Combine(Path.GetTempPath(), "CommonDefinitions.json"));
                    client.DownloadFile(address: new Uri(SwaggerEndpointWeb + "AppServicePlans.json"), fileName: Path.Combine(Path.GetTempPath(), "AppServicePlans.json"));                    
                    client.DownloadFile(address: new Uri(SwaggerEndpointWeb + "WebApps.json"), fileName: Path.Combine(Path.GetTempPath(), "WebApps.json"));
                }
            }

            IEnumerable<Template> webApps = await GetTemplatesFromSwagger(swaggerFilePath: Path.Combine(Path.GetTempPath(), "WebApps.json"));
            IEnumerable<Template> appServicePlans = await GetTemplatesFromSwagger(swaggerFilePath: Path.Combine(Path.GetTempPath(), "AppServicePlans.json"));

            return webApps.Union(appServicePlans);
        }

        public static async Task<IEnumerable<Template>> GetTemplatesFromSwagger(string swaggerFilePath)
        {
            List<Template> templates = new List<Template>();

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

            return templates;
        }

        public static IEnumerable<Template> GetBuiltInTemplates()
        {
            return new Template[]
            {
                /* Create ASP: Windows Server Farm */
                new Template()
                {
                    Category = "Visual ARM [Windows]",
                    Name = "Create Windows ASP - PremiumV3 Small",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P1v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"app\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows]",
                    Name = "Create Windows ASP - PremiumV3 Medium",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P2v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"app\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows]",
                    Name = "Create Windows ASP - PremiumV3 Large",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P3v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"app\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                /* Create ASP: Windows Container Server Farm */
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Create Windows Container ASP - PremiumV3 Small",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P1v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"windows\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hyperv\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Create Windows Container ASP - PremiumV3 Medium",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P2v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"windows\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hyperv\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Create Windows Container ASP - PremiumV3 Large",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P3v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"windows\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hyperv\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                /* Create ASP: Windows Container Server Farm */
                new Template()
                {
                    Category = "Visual ARM [Linux]",
                    Name = "Create Linux ASP - PremiumV3 Small",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P1v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"linux\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"reserved\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Linux]",
                    Name = "Create Linux ASP - PremiumV3 Medium",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P2v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"linux\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"reserved\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Linux]",
                    Name = "Create Linux ASP - PremiumV3 Large",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P3v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"linux\"," + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{serverFarmName}\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"reserved\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },

                /* Create Web App */
                new Template()
                {
                    Category = "Visual ARM [Windows]",
                    Name = "Create Windows Classic Web App",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}",
                    Body = 
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "            ]" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows]",
                    Name = "Create Windows Function App",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}\"," + Environment.NewLine +
                        "        \"kind\": \"functionapp\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "                { \"name\": \"FUNCTIONS_EXTENSION_VERSION\", \"value\": \"~4\" }" + Environment.NewLine +
                        "                { \"name\": \"FUNCTIONS_WORKER_RUNTIME\", \"value\": \"java\" }" + Environment.NewLine +
                        "                { \"name\": \"AzureWebJobsStorage\", \"value\": \"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net\" }" + Environment.NewLine +
                        "            ]," + Environment.NewLine +
                        "            \"javaVersion\": \"17\"" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Create Windows Container Web App",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                    \"name\": \"WEBSITES_ENABLE_APP_SERVICE_STORAGE\"," + Environment.NewLine +
                        "                    \"value\": \"true\"" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            ]," + Environment.NewLine +
                        "            \"appCommandLine\": \"\"," + Environment.NewLine +
                        "            \"windowsFxVersion\": \"DOCKER|mcr.microsoft.com/azure-app-service/windows/aspnet:4.7.2-windowsservercore-ltsc2019\"" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Create Windows Krypton Web App",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"windowsFxVersion\": \"ASPNET|8.0\"" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Linux]",
                    Name = "Create Linux Web App",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                    \"name\": \"WEBSITES_ENABLE_APP_SERVICE_STORAGE\"," + Environment.NewLine +
                        "                    \"value\": \"true\"" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            ]," + Environment.NewLine +
                        "            \"appCommandLine\": \"\"," + Environment.NewLine +
                        "            \"linuxFxVersion\": \"DOCKER|tomcat:9.0\"" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },

                /* Create Web App Slots */
                new Template()
                {
                    Category = "Visual ARM [Windows]",
                    Name = "Create Windows Classic Web App Slot",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/slots/{slotname}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}({slotname})\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            ]" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Create Windows Container Web App Slot",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/slots/{slotname}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}({slotname})\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                    \"name\": \"WEBSITES_ENABLE_APP_SERVICE_STORAGE\"," + Environment.NewLine +
                        "                    \"value\": \"true\"" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            ]," + Environment.NewLine +
                        "            \"appCommandLine\": \"\"," + Environment.NewLine +
                        "            \"windowsFxVersion\": \"DOCKER|mcr.microsoft.com/azure-app-service/windows/aspnet:4.7.2-windowsservercore-ltsc2019\"" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Create Windows Krypton Web App Slot",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/slots/{slotname}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}({slotname})\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"windowsFxVersion\": \"ASPNET|8.0\"" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Linux]",
                    Name = "Create Linux Web App Slot",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/slots/{slotname}",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"{location}\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"{name}({slotname})\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                    \"name\": \"WEBSITES_ENABLE_APP_SERVICE_STORAGE\"," + Environment.NewLine +
                        "                    \"value\": \"true\"" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            ]," + Environment.NewLine +
                        "            \"appCommandLine\": \"\"," + Environment.NewLine +
                        "            \"linuxFxVersion\": \"DOCKER|tomcat:9.0\"" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverFarms/{serverFarmName}\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },

                /* Common Tasks */
                new Template()
                {
                    Category = "Visual ARM [Common]",
                    Name = "List Web Apps",
                    Verb = "GET",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites",
                    Body = ""
                },
                new Template()
                {
                    Category = "Visual ARM [Common]",
                    Name = "Stop Web App",
                    Verb = "POST",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/stop",
                    Body = ""
                },
                new Template()
                {
                    Category = "Visual ARM [Common]",
                    Name = "Start Web App",
                    Verb = "Verb",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/start",
                    Body = ""
                },
                new Template()
                {
                    Category = "Visual ARM [Common]",
                    Name = "Get Site Config",
                    Verb = "GET",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/config",
                    Body = ""
                },
                new Template()
                {
                    Category = "Visual ARM [Common]",
                    Name = "Get App Settings",
                    Verb = "POST",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/config/appsettings/list",
                    Body = ""
                },
                new Template()
                {
                    Category = "Visual ARM [Common]",
                    Name = "Get Publishing credentials",
                    Verb = "POST",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/config/publishingcredentials/list",
                    Body = ""
                },
                new Template()
                {
                    Category = "Visual ARM [Common]",
                    Name = "Get BasicAuth publishing credentials",
                    Verb = "GET",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/basicPublishingCredentialsPolicies/scm",
                    Body = ""
                },
                new Template()
                {
                    Category = "Visual ARM [Common]",
                    Name = "Enable BasicAuth publishing credentials",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/basicPublishingCredentialsPolicies/scm",
                    Body =
                        "{" + Environment.NewLine +
                        "  \"name\": \"scm\"," + Environment.NewLine +
                        "  \"type\": \"Microsoft.Web/sites/basicPublishingCredentialsPolicies\"," + Environment.NewLine +
                        "  \"location\": \"{location}\"," + Environment.NewLine +
                        "  \"properties\": {" + Environment.NewLine +
                        "    \"allow\": true" + Environment.NewLine +
                        "  }" + Environment.NewLine +
                        "}"
                },
                /* Networking Tasks */
                new Template()
                {
                    Category = "Visual ARM [Networking]",
                    Name = "Get VNet details",
                    Verb = "GET",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Network/virtualNetworks/{vnetname}",
                    Body = ""
                },
                new Template()
                {
                    Category = "Visual ARM [Networking]",
                    Name = "Create Swift connection",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/networkConfig/VirtualNetwork",
                    Body =
                        "{" + Environment.NewLine +
                        "  \"id\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/config/virtualNetwork\"," + Environment.NewLine +
                        "  \"name\": \"virtualNetwork\"," + Environment.NewLine +
                        "  \"type\": \"Microsoft.Web/sites/config\"," + Environment.NewLine +
                        "  \"properties\": {" + Environment.NewLine +
                        "    \"subnetResourceId\": \"/subscriptions/{targetsubscription}/resourceGroups/{targetresourcegroup}/providers/Microsoft.Network/virtualNetworks/{targetvnet}/subnets/{targetsubnet}\"," + Environment.NewLine +
                        "    \"swiftSupported\": true" + Environment.NewLine +
                        "  }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Networking]",
                    Name = "Remove Swift connection",
                    Verb = "DELETE",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/networkConfig/VirtualNetwork",
                    Body =
                        "{" + Environment.NewLine +
                        "  \"id\": \"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/config/virtualNetwork\"," + Environment.NewLine +
                        "  \"name\": \"virtualNetwork\"," + Environment.NewLine +
                        "  \"type\": \"Microsoft.Web/sites/config\"," + Environment.NewLine +
                        "  \"properties\": {" + Environment.NewLine +
                        "    \"subnetResourceId\": \"/subscriptions/{targetsubscription}/resourceGroups/{targetresourcegroup}/providers/Microsoft.Network/virtualNetworks/{targetvnet}/subnets/{targetsubnet}\"," + Environment.NewLine +
                        "    \"swiftSupported\": true" + Environment.NewLine +
                        "  }" + Environment.NewLine +
                        "}"
                },
                /* Hyper-V Container Tasks */
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Change Windows Container image",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/config/web",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"properties\":{" + Environment.NewLine +
                        "        \"windowsFxVersion\":\"DOCKER|microsoft/iis\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Visual ARM [Windows Container]",
                    Name = "Add SMB mapping to Windows Container [BYOS]",
                    Verb = "PUT",
                    Path = "/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{name}/config/AzureStorageAccounts",
                    Body =
                        "{" + Environment.NewLine +
                        "  \"properties\": {" + Environment.NewLine +
                        "    \"{sharename}\": {" + Environment.NewLine +
                        "      \"type\": \"AzureFiles\"," + Environment.NewLine +
                        "      \"accountname\": \"{storageaccount-name}\"," + Environment.NewLine +
                        "      \"accesskey\": \"{storageaccount-accesskey}\"," + Environment.NewLine +
                        "      \"sharename\": \"{sharename}\"," + Environment.NewLine +
                        "      \"mountpath\": \"/{sharename}\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "  }" + Environment.NewLine +
                        "}"
                }
            };
        }
    }
}
