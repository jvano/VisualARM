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
            XmlSerializer serializer = new XmlSerializer(typeof(TemplateDocument));

            string filePath = GetTemplatesFilePath();

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
            XmlSerializer serializer = new XmlSerializer(typeof(TemplateDocument));

            string filePath = GetTemplatesFilePath();

            using (FileStream stream = File.OpenWrite(filePath))
            {
                serializer.Serialize(stream, this);
            }
        }

        private static string GetTemplatesFilePath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appData, "AzureResourceManagerClient");
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            string filePath = Path.Combine(appFolder, "Templates.txt");

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

        public string Verb { get; set; }

        public string Path { get; set; }

        public string Body { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
