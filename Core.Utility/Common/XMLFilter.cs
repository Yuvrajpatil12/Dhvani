﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Utility
{
	public static class XMLFilter
	{
		private static readonly string _module = "Core.Utility.XMLFilter";

		public static string RemoveAllNamespaces(string xmlDocument)
		{
			XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

			return xmlDocumentWithoutNs.ToString();
		}

		private static XElement RemoveAllNamespaces(XElement xmlDocument)
		{
			if (!xmlDocument.HasElements)
			{
				XElement xElement = new XElement(xmlDocument.Name.LocalName);
				xElement.Value = xmlDocument.Value;
				foreach (XAttribute attribute in xmlDocument.Attributes())
					xElement.Add(attribute);
				return xElement;
			}
			return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
		}
	}
}
