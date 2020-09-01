using System;
using System.Collections.Generic;
using Vano.Tools.Azure.Model;

namespace Vano.Tools.Azure
{
    public static class TemplateFactory
    {
        public static IEnumerable<Template> GetTemplates()
        {
            return new Template[]
            {
                /* Create ASP: Windows Server Farm */
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows ASP - PremiumV3 Small",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P1v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"app\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows ASP - PremiumV3 Medium",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P2v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"app\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows ASP - PremiumV3 Large",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P3v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"app\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                /* Create ASP: Windows Container Server Farm */
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows Container ASP - PremiumV3 Small",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P1v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"windows\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hyperv\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows Container ASP - PremiumV3 Medium",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P2v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"windows\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"hyperv\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows Container ASP - PremiumV3 Large",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P3v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"windows\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
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
                    Category = "Create ASP + Web App",
                    Name = "Create Linux ASP - PremiumV3 Small",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P1v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"linux\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"reserved\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Linux ASP - PremiumV3 Medium",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P2v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"linux\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
                        "        \"workerSizeId\": \"0\"," + Environment.NewLine +
                        "        \"numberOfWorkers\": \"1\"," + Environment.NewLine +
                        "        \"reserved\": true," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Linux ASP - PremiumV3 Large",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"sku\": {" + Environment.NewLine +
                        "        \"name\": \"P3v3\"," + Environment.NewLine +
                        "        \"tier\": \"PremiumV3\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"kind\": \"linux\"," + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<serverfarm>\"," + Environment.NewLine +
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
                    Category = "Create ASP + Web App",
                    Name = "Create Windows Web App",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>?stamp=<stamp>&api-version=<api-version>",
                    Body = 
                        "{" + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<sitename>\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "            ]" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows Container",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<sitename>\"," + Environment.NewLine +
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
                        "        \"serverFarmId\": \"/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Linux Container",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<sitename>\"," + Environment.NewLine +
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
                        "        \"serverFarmId\": \"/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },

                /* Create Web App Slots */
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows Web App Slot",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/slots/{slotname}?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<sitename>({slotname})\"," + Environment.NewLine +
                        "        \"siteConfig\": {" + Environment.NewLine +
                        "            \"appSettings\": [" + Environment.NewLine +
                        "                {" + Environment.NewLine +
                        "                }" + Environment.NewLine +
                        "            ]" + Environment.NewLine +
                        "        }," + Environment.NewLine +
                        "        \"serverFarmId\": \"/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Windows Container Slot",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/slots/{slotname}?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<sitename>({slotname})\"," + Environment.NewLine +
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
                        "        \"serverFarmId\": \"/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Create ASP + Web App",
                    Name = "Create Linux Container Slot",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/slots/{slotname}?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"location\": \"<location>\"," + Environment.NewLine +
                        "    \"tags\": {" + Environment.NewLine +
                        "        \"hidden-related:/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\": \"empty\"" + Environment.NewLine +
                        "    }," + Environment.NewLine +
                        "    \"properties\": {" + Environment.NewLine +
                        "        \"name\": \"<sitename>({slotname})\"," + Environment.NewLine +
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
                        "        \"serverFarmId\": \"/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/serverFarms/<serverfarm>\"," + Environment.NewLine +
                        "        \"hostingEnvironment\": \"\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },

                /* Common Tasks */
                new Template()
                {
                    Category = "Common Tasks",
                    Name = "List Web Apps",
                    Verb = "GET",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites?stamp=<stamp>&api-version=<api-version>",
                    Body = ""
                },
                new Template()
                {
                    Category = "Common Tasks",
                    Name = "Stop Web App",
                    Verb = "POST",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/stop?stamp=<stamp>&api-version=<api-version>",
                    Body = ""
                },
                new Template()
                {
                    Category = "Common Tasks",
                    Name = "Start Web App",
                    Verb = "Verb",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/start?stamp=<stamp>&api-version=<api-version>",
                    Body = ""
                },
                new Template()
                {
                    Category = "Common Tasks",
                    Name = "Get Site Config",
                    Verb = "GET",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/config?stamp=<stamp>&api-version=<api-version>",
                    Body = ""
                },
                new Template()
                {
                    Category = "Common Tasks",
                    Name = "Get App Settings",
                    Verb = "POST",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/config/appsettings/list?stamp=<stamp>&api-version=<api-version>",
                    Body = ""
                },
                new Template()
                {
                    Category = "Common Tasks",
                    Name = "Get Publishing credentials",
                    Verb = "POST",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/config/publishingcredentials/list?stamp=<stamp>&api-version=<api-version>",
                    Body = ""
                },
                /* Networking Tasks */
                new Template()
                {
                    Category = "Networking Tasks",
                    Name = "Get VNet details",
                    Verb = "GET",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Network/virtualNetworks/{vnetname}?stamp=<stamp>&api-version=2018-12-01",
                    Body = ""
                },
                new Template()
                {
                    Category = "Networking Tasks",
                    Name = "Create Swift connection",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/networkConfig/VirtualNetwork?stamp=<stamp>&api-version=2016-08-01",
                    Body =
                        "{" + Environment.NewLine +
                        "  \"id\": \"/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/config/virtualNetwork\"," + Environment.NewLine +
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
                    Category = "Networking Tasks",
                    Name = "Remove Swift connection",
                    Verb = "DELETE",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/networkConfig/VirtualNetwork?stamp=<stamp>&api-version=2016-08-01",
                    Body =
                        "{" + Environment.NewLine +
                        "  \"id\": \"/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/config/virtualNetwork\"," + Environment.NewLine +
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
                    Category = "Hyper-V Container Tasks",
                    Name = "Change Windows Container image",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/config/web?stamp=<stamp>&api-version=<api-version>",
                    Body =
                        "{" + Environment.NewLine +
                        "    \"properties\":{" + Environment.NewLine +
                        "        \"windowsFxVersion\":\"DOCKER|microsoft/iis\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "}"
                },
                new Template()
                {
                    Category = "Hyper-V Container Tasks",
                    Name = "Add SMB mapping to Windows Container [BYOS]",
                    Verb = "PUT",
                    Path = "/subscriptions/<subscription>/resourceGroups/<resourcegroup>/providers/Microsoft.Web/sites/<sitename>/config/AzureStorageAccounts?stamp=<stamp>&api-version=2019-08-01",
                    Body =
                        "{" + Environment.NewLine +
                        "  \"properties\": {" + Environment.NewLine +
                        "    \"<sharename>\": {" + Environment.NewLine +
                        "      \"type\": \"AzureFiles\"," + Environment.NewLine +
                        "      \"accountname\": \"<storageaccount-name>\"," + Environment.NewLine +
                        "      \"accesskey\": \"<storageaccount-accesskey>\"," + Environment.NewLine +
                        "      \"sharename\": \"<sharename>\"," + Environment.NewLine +
                        "      \"mountpath\": \"/<sharename>\"" + Environment.NewLine +
                        "    }" + Environment.NewLine +
                        "  }" + Environment.NewLine +
                        "}"
                }
            };
        }
    }
}
