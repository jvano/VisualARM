using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Vano.Tools.Azure.Model
{
    public class TemplateCategory
    {
        public string Name { get; set; }

        public IEnumerable<Template> Templates { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class TemplateDocument
    {
        public TemplateDocument()
        {
            this.Templates = new Template[] { };
        }

        public TemplateDocument(IEnumerable<Template> templates)
        {
            this.Templates = templates.ToArray();
        }

        public Template[] Templates { get; set; }

        public void AddTemplate(Template template)
        {
            List<Template> list = new List<Template>(this.Templates);
            list.Add(template);

            this.Templates = list.ToArray();
        }

        public static TemplateDocument FromFile()
        {
            string filePath = GetCustomTemplatesFilePath();

            return FromFile(filePath);
        }

        public static TemplateDocument FromFile(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TemplateDocument));
            if (!File.Exists(filePath))
            {
                return null;
            }

            using (FileStream stream = File.OpenRead(filePath))
            {
                return serializer.Deserialize(stream) as TemplateDocument;
            }
        }

        public void Save()
        {
            string filePath = GetCustomTemplatesFilePath();
            Save(filePath);
        }

        public void Save(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TemplateDocument));
            using (FileStream stream = File.OpenWrite(filePath))
            {
                serializer.Serialize(stream, this);
            }
        }

        public static string GetCustomTemplatesFilePath()
        {
            string customTemplatesFolder = Template.GetCustomTemplatesFolderPath();

            string filePath = Path.Combine(customTemplatesFolder, "CustomTemplates.xml");

            return filePath;
        }
    }

    public class Template
    {
        public Template()
        {
        }

        [XmlIgnore]
        public bool Custom { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Verb { get; set; }

        public string Path { get; set; }

        public string Body { get; set; }

        public override string ToString()
        {
            return !string.IsNullOrWhiteSpace(Summary) ?
                this.Name + " - " + this.Summary :
                this.Name;
        }

        public static string GetCustomTemplatesFolderPath()
        {
            string baseFolder = GetTemplatesRootFolderPath();
            string customTemplatesFolder = System.IO.Path.Combine(baseFolder, "Templates.Custom");
            if (!Directory.Exists(customTemplatesFolder))
            {
                Directory.CreateDirectory(customTemplatesFolder);
            }

            return customTemplatesFolder;
        }

        public static string GetGitHubTemplatesFolder()
        {
            string baseFolder = GetTemplatesRootFolderPath();
            string gitHubTemplatesFolder = System.IO.Path.Combine(baseFolder, "Templates.GitHub");
            if (!Directory.Exists(gitHubTemplatesFolder))
            {
                Directory.CreateDirectory(gitHubTemplatesFolder);
            }

            return gitHubTemplatesFolder;
        }

        public static string GetSwaggerTemplatesFolder()
        {
            string baseFolder = GetTemplatesRootFolderPath();
            string swaggerTemplatesFolder = System.IO.Path.Combine(baseFolder, "Templates.Swagger");
            if (!Directory.Exists(swaggerTemplatesFolder))
            {
                Directory.CreateDirectory(swaggerTemplatesFolder);
            }

            return swaggerTemplatesFolder;
        }

        public static string GetTemplatesRootFolderPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = System.IO.Path.Combine(appData, "VisualARM");
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            return appFolder;
        }
    }
}
