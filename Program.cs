using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {
        string inputFile = "C:/Projet/ISAE/Source/SCENTIF_OTAO/oato_originaux/export_oatao_CONFERENCES_1963_XML.xml";
        string outputFile = "C:/Projet/ISAE/Source/SCENTIF_OTAO/oato_valide/conference.xml";

        XmlDocument doc = new XmlDocument();
        doc.Load(inputFile);

     

        XmlNode root = doc.DocumentElement;
        XmlNodeList eprintNodes = root.SelectNodes("//eprint");
        // Parcourir chaque noeud "data"
         foreach (XmlNode eprintNode in eprintNodes)
         {
             XmlNode recordNode = doc.CreateElement("record");
         

            XmlNodeList childNodes = eprintNode.ChildNodes;
            foreach (XmlNode childNode in childNodes)
            {
                if (childNode.Name.Equals("eprintid"))
                {
                    string eprintId = childNode.Value;
                    recordNode.InnerXml = $"<field id=\"eprint_id\">{eprintId}</field>";
                }
                if (childNode.Name.Equals("documents"))
                {
                    XmlNodeList docChildNodesDoc = childNode.ChildNodes;
                    foreach (XmlNode docChild in docChildNodesDoc) {
                        if (docChild.Name.Equals("document"))
                        {
                            XmlNodeList docChildNodes = docChild.ChildNodes;
                    foreach (XmlNode docChildNode in docChildNodes)
                    {
                        if (docChildNode.Name.Equals("files"))
                        {
                            XmlNodeList fileNodes = docChildNode.SelectNodes("file");
                          
                                    foreach (XmlNode fileNode in fileNodes)
                            {
                                XmlNode fieldLangue = fileNode.SelectSingleNode("language");
                                XmlNode filedSecurity = fileNode.SelectSingleNode("security");
                                XmlNode fieldDateEmbargo = fileNode.SelectSingleNode("date_embargo");
                                XmlNode DR = fileNode.SelectSingleNode("url");
                             

                                        if (fieldLangue != null )  
                                {
                                            XmlNode fileIdFieldNode = doc.CreateElement("field");
                                            XmlAttribute fileIdIdAttribute = doc.CreateAttribute("id");
                                            fileIdIdAttribute.Value = "language";
                                            fileIdFieldNode.Attributes.Append(fileIdIdAttribute);
                                            fileIdFieldNode.InnerText = fieldLangue.InnerText;
                                         
                                            recordNode.AppendChild(fileIdFieldNode);
                                        }
                                        if (filedSecurity != null ) {
                                            XmlNode filenameFieldNode = doc.CreateElement("field");
                                            XmlAttribute filenameIdAttribute = doc.CreateAttribute("id");
                                            filenameIdAttribute.Value = "security";
                                            filenameFieldNode.Attributes.Append(filenameIdAttribute);
                                            filenameFieldNode.InnerText = filedSecurity.InnerText;
                                        
                                            recordNode.AppendChild(filenameFieldNode);
                                        }
                                    
                                        if (fieldDateEmbargo != null) { 
                                    XmlNode mimeFieldNode = doc.CreateElement("field");
                                    XmlAttribute mimeIdAttribute = doc.CreateAttribute("id");
                                    mimeIdAttribute.Value = "date_embargo";
                                    mimeFieldNode.Attributes.Append(mimeIdAttribute);
                                    mimeFieldNode.InnerText = fieldDateEmbargo.InnerText;
                                            recordNode.AppendChild(mimeFieldNode);
                                        }

                                        if (DR != null)
                                        {
                                            XmlNode drFieldPathNode = doc.CreateElement("field_path");
                                            XmlAttribute drIdAttribute = doc.CreateAttribute("id");
                                            drIdAttribute.Value = "dr";
                                            drFieldPathNode.Attributes.Append(drIdAttribute);
                                            drFieldPathNode.InnerText = DR.InnerText;
                                            recordNode.AppendChild(drFieldPathNode);
                                            XmlNode drFieldNode = doc.CreateElement("field");
                                            drFieldNode.Attributes.Append(drIdAttribute);
                                            drFieldNode.InnerText = DR.InnerText;
                                            recordNode.AppendChild(drFieldNode);
                                        }

                                    }
                            }
                        
                        else
                        {
                            XmlNode fieldNode = doc.CreateElement("field");
                            XmlAttribute idAttribute = doc.CreateAttribute("id");
                            idAttribute.Value = docChildNode.Name;
                            fieldNode.Attributes.Append(idAttribute);
                            fieldNode.InnerText = docChildNode.InnerText;

                            recordNode.AppendChild(fieldNode);
                        }
                    }
                } }
                }
                else if (childNode.Name.Equals("affiliation"))
                {
                    XmlNodeList creatorNodes = childNode.SelectNodes("item");
                    foreach (XmlNode creatorNode in creatorNodes)
                    {
                        XmlNode name = creatorNode.SelectSingleNode("name");
                        XmlNode labo = creatorNode.SelectSingleNode("labo");
                        XmlNode equipe = creatorNode.SelectSingleNode("equipe");
                        XmlNode sous_equipe = creatorNode.SelectSingleNode("sous_equipe");
                        XmlNode authid = creatorNode.SelectSingleNode("authid");
                        if(authid != null)
                        {

                        string authid_value = authid.InnerText;
                        string[] authid_values = authid_value.Split(',');
                        int nombreValeurs_auth = authid_values.Length;
                        if(nombreValeurs_auth == 1)
                        {
                            XmlNode affiliationFieldNode = doc.CreateElement("field");
                            XmlAttribute affiliationIdAttribute = doc.CreateAttribute("id");
                            affiliationIdAttribute.Value = "affiliation_thez";
                            affiliationFieldNode.Attributes.Append(affiliationIdAttribute);


                            if (name != null)
                            {
                                affiliationFieldNode.InnerText = name.InnerText;
                                recordNode.AppendChild(affiliationFieldNode);
                            }
                            if (labo != null)
                            {
                                affiliationFieldNode.InnerText = labo.InnerText;
                                recordNode.AppendChild(affiliationFieldNode);
                            }
                            if (equipe != null)
                            {
                                affiliationFieldNode.InnerText = equipe.InnerText;
                                recordNode.AppendChild(affiliationFieldNode);
                            }
                            if (sous_equipe != null)
                            {
                                affiliationFieldNode.InnerText = sous_equipe.InnerText;
                                recordNode.AppendChild(affiliationFieldNode);
                            }
                        }
                        else
                        {
                            int i = 1;
                            XmlNode affiliationFieldNode1 = doc.CreateElement("field");
                            XmlAttribute affiliationIdAttribute1 = doc.CreateAttribute("id");
                            affiliationIdAttribute1.Value = "affiliation_thez1";
                            affiliationFieldNode1.Attributes.Append(affiliationIdAttribute1);


                            if (name != null)
                            {
                                affiliationFieldNode1.InnerText = name.InnerText;
                                recordNode.AppendChild(affiliationFieldNode1);
                            }
                            if (labo != null)
                            {
                                affiliationFieldNode1.InnerText = labo.InnerText;
                                recordNode.AppendChild(affiliationFieldNode1);
                            }
                            if (equipe != null)
                            {
                                affiliationFieldNode1.InnerText = equipe.InnerText;
                                recordNode.AppendChild(affiliationFieldNode1);
                            }
                            if (sous_equipe != null)
                            {
                                affiliationFieldNode1.InnerText = sous_equipe.InnerText;
                                recordNode.AppendChild(affiliationFieldNode1);
                            }
                            while (i < nombreValeurs_auth)
                            {
                                XmlNode affiliationFieldNode = doc.CreateElement("field");
                                XmlAttribute affiliationIdAttribute = doc.CreateAttribute("id");
                                affiliationIdAttribute.Value = "affiliation_thez";
                                affiliationFieldNode.Attributes.Append(affiliationIdAttribute);


                                if (name != null)
                                {
                                    affiliationFieldNode.InnerText = name.InnerText;
                                    recordNode.AppendChild(affiliationFieldNode);
                                }
                                if (labo != null)
                                {
                                    affiliationFieldNode.InnerText = labo.InnerText;
                                    recordNode.AppendChild(affiliationFieldNode);
                                }
                                if (equipe != null)
                                {
                                    affiliationFieldNode.InnerText = equipe.InnerText;
                                    recordNode.AppendChild(affiliationFieldNode);
                                }
                                if (sous_equipe != null)
                                {
                                    affiliationFieldNode.InnerText = sous_equipe.InnerText;
                                    recordNode.AppendChild(affiliationFieldNode);
                                }
                                i++;
                            }
                        }



                        }

                    }
                }
                else if (childNode.Name.Equals("creators"))
                {
                    XmlNodeList creatorNodes = childNode.SelectNodes("item");
                    foreach (XmlNode creatorNode in creatorNodes)
                    {
                       
                            XmlNode familyNode = creatorNode.SelectSingleNode("name/family");
                        XmlNode givenNode = creatorNode.SelectSingleNode("name/given");
                        XmlNode ppnNode = creatorNode.SelectSingleNode("ppn");

                       
                        XmlNode auteurFieldNode = doc.CreateElement("field");
                        XmlAttribute auteurIdAttribute = doc.CreateAttribute("id");


                        if (ppnNode != null)
                        {
                            auteurIdAttribute.Value = "auteur_ppn";
                            XmlAttribute auteurIdLinkAttribute = doc.CreateAttribute("idlink");
                            auteurIdLinkAttribute.Value = ppnNode.InnerText;
                            auteurFieldNode.Attributes.Append(auteurIdLinkAttribute);


                        }
                        else
                        {
                            auteurIdAttribute.Value = "auteur";
                        }
                        auteurFieldNode.Attributes.Append(auteurIdAttribute);
                            XmlNode auteur_nameFieldNode = doc.CreateElement("field");
                           
                            if (familyNode != null && givenNode != null)
                            {
                                auteurFieldNode.InnerText = familyNode.InnerText + " " + givenNode.InnerText;

                            }
                            else if (familyNode != null)
                            {
                                auteurFieldNode.InnerText = familyNode.InnerText;

                            }
                            else
                            {
                                auteurFieldNode.InnerText = givenNode.InnerText;

                            }
                            recordNode.AppendChild(auteurFieldNode);
                        
                        
                    }
                }

                else  if (childNode.HasChildNodes)
                {
                    XmlNode fieldNode = doc.CreateElement("field");
                    XmlAttribute idAttribute = doc.CreateAttribute("id");
                    idAttribute.Value = childNode.Name;
                    fieldNode.Attributes.Append(idAttribute);

                    foreach (XmlNode grandChildNode in childNode.ChildNodes)
                    {
                        XmlNode grandChildFieldNode = doc.CreateElement("field");
                        XmlAttribute grandChildIdAttribute = doc.CreateAttribute("id");
                        grandChildIdAttribute.Value = childNode.Name + "_" + grandChildNode.Name;
                        grandChildFieldNode.Attributes.Append(grandChildIdAttribute);
                        grandChildFieldNode.InnerText = grandChildNode.InnerText;

                        fieldNode.AppendChild(grandChildFieldNode);
                    }

                    recordNode.AppendChild(fieldNode);
                }
                else
                {
                    XmlNode fieldNode = doc.CreateElement("field");
                    XmlAttribute idAttribute = doc.CreateAttribute("id");
                    idAttribute.Value = childNode.Name;
                    fieldNode.Attributes.Append(idAttribute);
                    fieldNode.InnerText = childNode.InnerText;

                    recordNode.AppendChild(fieldNode);
                }
             }

             root.ReplaceChild(recordNode, eprintNode);
         }

         doc.Save(outputFile);

        XDocument xmlDoc = XDocument.Load(outputFile);
        var records = xmlDoc.Root.Elements("record");

        foreach (var record in records)
        {
            var duplicateFields = record.Elements("field")
                .GroupBy(x => new { Id = (string)x.Attribute("id"), Value = (string)x })
                .Where(g => g.Count() > 1)
                .SelectMany(g => g.Skip(1))
                .ToList(); // Convertir en liste une seule fois

            foreach (var field in duplicateFields)
            {
                field.Remove();
            }
        }
xmlDoc.Save(outputFile);
        Console.WriteLine("La transformation a été effectuée avec succès.");
    }
}
