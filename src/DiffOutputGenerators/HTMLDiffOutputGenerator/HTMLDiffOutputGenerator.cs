using System;
using System.Collections.Generic;
using System.Text;
using textdiffcore;

namespace textdiffcore.DiffOutputGenerators
{
    public class HTMLDiffOutputGenerator : IDiffOutputGenerator
    {
        public string AttributeName {get;set;}
        public string AddAttributeValue {get;set;}
        public string RemoveAttributeValue {get;set;}
        public string EqualAttributeValue {get;set;}
        public string TagType {get; set;}



        public HTMLDiffOutputGenerator(string tagType = "span", string attributeNae = "", string addAttributeValue = "", string removeAttributeValue = "", string equalAttributeValue = "")
        {
            TagType = tagType;
            AttributeName = attributeNae;
            AddAttributeValue = addAttributeValue;
            RemoveAttributeValue = removeAttributeValue;
            EqualAttributeValue = equalAttributeValue;
        }

        public string GenerateOutput(List<Diffrence> diffrences)
        {
            StringBuilder sb = new();
            for (int i = 0; i < diffrences.Count; i++)
            {
                sb.AppendLine(GenerateHTMLElement(diffrences[i], i));
            }

            return sb.ToString();
        }
         
        public string GenerateOutput(Diffrence diffrence)
        {            
            return GenerateHTMLElement(diffrence);
        }

		private string GetAttributeValue(Diffrence d)
        {
            switch (d.action)
            {
                case TextDiffAction.Add:return AddAttributeValue;
                case TextDiffAction.Remove:return RemoveAttributeValue;
                case TextDiffAction.Equal:return EqualAttributeValue;
                default: return "";
            }
        }
        private string GenerateHTMLElement(Diffrence d, int? index = null)
        {
            if (string.IsNullOrEmpty(GetAttributeValue(d)))
            {
                return d.value;
            }
            else
            {
                var indexAttribute = index == null ? null : $"index=\"{index.Value}\"";
                return $"<{this.TagType} {this.AttributeName}=\"{this.GetAttributeValue(d)}\" {indexAttribute}>\r\n{d.value}\r\n</{this.TagType}>";
            }
        }

    }
}
