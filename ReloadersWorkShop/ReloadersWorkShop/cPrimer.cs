﻿//============================================================================*
// cPrimer.cs
//
// Copyright © 2013-2014, Kevin S. Beebe
// All Rights Reserved
//============================================================================*

//============================================================================*
// .Net Using Statements
//============================================================================*

using System;
using System.Xml;

//============================================================================*
// NameSpace
//============================================================================*

namespace ReloadersWorkShop
	{
	//============================================================================*
	// cPrimer class
	//============================================================================*

	[Serializable]
	public class cPrimer : cSupply
		{
		//============================================================================*
		// Public Enumerations
		//============================================================================*

		public enum ePrimerSize
			{
			Small = 0,
			Large,
			}

		//============================================================================*
		// Private Data Members
		//============================================================================*

		private string m_strModel = "";
		private ePrimerSize m_eSize = ePrimerSize.Small;

		private bool m_fStandard = true;
		private bool m_fMagnum = false;

		private bool m_fBenchRest = false;
		private bool m_fMilitary = false;

		//============================================================================*
		// cPrimer() - Constructor
		//============================================================================*

		public cPrimer(bool fIdentity = false)
			: base(cSupply.eSupplyTypes.Primers, fIdentity)
			{
			}

		//============================================================================*
		// cPrimer() - Copy Constructor
		//============================================================================*

		public cPrimer(cPrimer Primer)
			: base(Primer)
			{
			Copy(Primer, false);
			}

		//============================================================================*
		// BenchRest Property
		//============================================================================*

		public bool BenchRest
			{
			get
				{
				return (m_fBenchRest);
				}
			set
				{
				m_fBenchRest = value;
				}
			}

		//============================================================================*
		// Comparer()
		//============================================================================*

		public static int Comparer(cPrimer Primer1, cPrimer Primer2)
			{
			if (Primer1 == null)
				{
				if (Primer2 != null)
					return (-1);
				else
					return (0);
				}
			else
				{
				if (Primer2 == null)
					return (1);
				}

			return (Primer1.CompareTo(Primer2));
			}

		//============================================================================*
		// CompareTo()
		//============================================================================*

		public override int CompareTo(Object obj)
			{
			if (obj == null)
				return (1);

			//----------------------------------------------------------------------------*
			// Base Class
			//----------------------------------------------------------------------------*

			cSupply Supply = (cSupply) obj;

			int rc = base.CompareTo(Supply);

			//----------------------------------------------------------------------------*
			// Model
			//----------------------------------------------------------------------------*

			if (rc == 0)
				{
				cPrimer Primer = (cPrimer) Supply;

				rc = cDataFiles.ComparePartNumbers(m_strModel, Primer.m_strModel);
				}

			//----------------------------------------------------------------------------*
			// Return results
			//----------------------------------------------------------------------------*

			return (rc);
			}

		//============================================================================*
		// Copy()
		//============================================================================*

		public void Copy(cPrimer Primer, bool fCopyBase = true)
			{
			if (fCopyBase)
				base.Copy(Primer);

			m_strModel = Primer.m_strModel;
			m_eSize = Primer.m_eSize;
			m_fStandard = Primer.m_fStandard;
			m_fMagnum = Primer.m_fMagnum;
			m_fMilitary = Primer.m_fMilitary;
			m_fBenchRest = Primer.m_fBenchRest;
			}

		//============================================================================*
		// CSVLine Property
		//============================================================================*

		public override string CSVLine
			{
			get
				{
				string strLine = base.CSVLine;

				strLine += m_strModel;
				strLine += ",";

				strLine += m_eSize == ePrimerSize.Small ? "Small" : "Large";
				strLine += ",";

				strLine += m_fStandard ? "Yes," : "-,";
				strLine += m_fMagnum ? "Yes," : "-,";
				strLine += m_fBenchRest ? "Yes," : "-,";
				strLine += m_fMilitary ? "Yes," : "-";

				return (strLine);
				}
			}

		//============================================================================*
		// CSVLineHeader Property
		//============================================================================*

		public static string CSVLineHeader
			{
			get
				{
				string strLine = cSupply.CSVSupplyLineHeader;

				strLine += "Model,Size,Standard,Magnum,Bench Rest,Military";

				return (strLine);
				}
			}

		//============================================================================*
		// Export() - XML Document
		//============================================================================*

		public override void Export(XmlDocument XMLDocument, XmlElement XMLParentElement)
			{
			XmlElement XMLThisElement = XMLDocument.CreateElement("Primer");
			XMLParentElement.AppendChild(XMLThisElement);

			base.Export(XMLDocument, XMLThisElement);

			// Model

			XmlElement XMLElement = XMLDocument.CreateElement("Model");
			XmlText XMLTextElement = XMLDocument.CreateTextNode(m_strModel);
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Size

			XMLElement = XMLDocument.CreateElement("Size");
			XMLTextElement = XMLDocument.CreateTextNode(m_eSize == ePrimerSize.Small ? "Small" : "Large");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Standard

			XMLElement = XMLDocument.CreateElement("Standard");
			XMLTextElement = XMLDocument.CreateTextNode(m_fStandard ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Magnum

			XMLElement = XMLDocument.CreateElement("Magnum");
			XMLTextElement = XMLDocument.CreateTextNode(m_fMagnum ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Bench Rest

			XMLElement = XMLDocument.CreateElement("BenchRest");
			XMLTextElement = XMLDocument.CreateTextNode(m_fBenchRest ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Military

			XMLElement = XMLDocument.CreateElement("Military");
			XMLTextElement = XMLDocument.CreateTextNode(m_fMilitary ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);
			}

		//============================================================================*
		// ExportName Property
		//============================================================================*

		public string ExportName
			{
			get
				{
				return ("Primer");
				}
			}

		//============================================================================*
		// Import()
		//============================================================================*

		public override bool Import(XmlDocument XMLDocument, XmlNode XMLThisNode, cDataFiles DataFiles)
			{
			base.Import(XMLDocument, XMLThisNode, DataFiles);

			XmlNode XMLNode = XMLThisNode.FirstChild;

			while (XMLNode != null)
				{
				switch (XMLNode.Name)
					{
					case "Model":
						m_strModel = XMLNode.FirstChild.Value;
						break;
					case "Size":
						m_eSize = XMLNode.FirstChild.Value == "Small" ? ePrimerSize.Small : ePrimerSize.Large;
						break;
					case "Standard":
						m_fStandard = XMLNode.FirstChild.Value == "Yes";
						break;
					case "Magnum":
						m_fMagnum = XMLNode.FirstChild.Value == "Yes";
						break;
					case "BenchRest":
						m_fBenchRest = XMLNode.FirstChild.Value == "Yes";
						break;
					case "Military":
						m_fMilitary = XMLNode.FirstChild.Value == "Yes";
						break;
					default:
						break;
					}

				XMLNode = XMLNode.NextSibling;
				}

			return (Validate());
			}

		//============================================================================*
		// Magnum Property
		//============================================================================*

		public bool Magnum
			{
			get
				{
				return (m_fMagnum);
				}
			set
				{
				m_fMagnum = value;
				}
			}

		//============================================================================*
		// Military Property
		//============================================================================*

		public bool Military
			{
			get
				{
				return (m_fMilitary);
				}
			set
				{
				m_fMilitary = value;
				}
			}

		//============================================================================*
		// Model Property
		//============================================================================*

		public string Model
			{
			get
				{
				return (m_strModel);
				}
			set
				{
				m_strModel = value;
				}
			}

		//============================================================================*
		// ResolveIdentities()
		//============================================================================*

		public override bool ResolveIdentities(cDataFiles DataFiles)
			{
			return(base.ResolveIdentities(DataFiles));
			}

		//============================================================================*
		// SortSizeString Property
		//============================================================================*

		public string ShortSizeString
			{
			get
				{
				string strSizeString = "";

				switch (m_eSize)
					{
					case ePrimerSize.Small:
						strSizeString = "Small";
						break;

					case ePrimerSize.Large:
						strSizeString = "Large";
						break;
					}

				return (strSizeString);
				}
			}

		//============================================================================*
		// Size Property
		//============================================================================*

		public ePrimerSize Size
			{
			get
				{
				return (m_eSize);
				}
			set
				{
				m_eSize = value;
				}
			}

		//============================================================================*
		// SizeString Property
		//============================================================================*

		public string SizeString
			{
			get
				{
				string strSizeString = ShortSizeString;

				strSizeString += " ";

				switch (FirearmType)
					{
					case cFirearm.eFireArmType.Handgun:
						strSizeString += "Pistol";
						break;

					case cFirearm.eFireArmType.Rifle:
						strSizeString += "Rifle";
						break;

					case cFirearm.eFireArmType.Shotgun:
						strSizeString += "Shotgun";
						break;
					}

				return (strSizeString);
				}
			}

		//============================================================================*
		// Standard Property
		//============================================================================*

		public bool Standard
			{
			get
				{
				return (m_fStandard);
				}
			set
				{
				m_fStandard = value;
				}
			}

		//============================================================================*
		// ToShortString()
		//============================================================================*

		public string ToShortString()
			{
			return (String.Format("{0} {1}", (Manufacturer != null ? Manufacturer.ToString() : ""), (m_strModel != null ? m_strModel : "")));
			}

		//============================================================================*
		// ToString
		//============================================================================*

		public override string ToString()
			{
			string strString = String.Format("{0} {1} - {2} {3}", Manufacturer, m_strModel, m_eSize == ePrimerSize.Small ? "Small" : "Large", FirearmType == cFirearm.eFireArmType.Handgun ? "Pistol" : "Rifle");

			if (!m_fStandard && m_fMagnum)
				strString += " Magnum";

			strString = ToCrossUseString(strString);

			return (strString);
			}

		//============================================================================*
		// Validate()
		//============================================================================*

		public override bool Validate()
			{
			bool fOK = base.Validate();

			if (fOK)
				fOK = !String.IsNullOrEmpty(m_strModel);

			if (Identity)
				return (fOK);

			if (fOK && !m_fStandard && !m_fMagnum && !m_fMilitary && !m_fBenchRest)
				fOK = false;

			return (fOK);
			}
		}
	}
