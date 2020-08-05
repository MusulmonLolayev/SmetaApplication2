using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Smeta.Models;
using System.Data;
using System.Windows;
using System.Media;
using System.Globalization;

namespace Smeta.Methods
{
    public static class WriteXml
    {
        private static string DateTimeToString(DateTime dt)
        {
            string str = dt.Day + "." + dt.Month + "." + dt.Year;
            return str;
        }  
        private static XDocument GetXMLDocument(string path)
        {
            try
            {
                XDocument doc = XDocument.Load(path);
                return doc;
            }
            catch
            {
                //MessageBox.Show(exc.Message);
                return null;
            }
        }
        public static uint InsertMinZarplata(List<MinZarplata> newMinElement, byte key)
        {
            if (key == 0)
            {
                XDocument docMin = GetXMLDocument("Data\\MinZarplata.xml");

                if (docMin == null)
                {
                    XDocument doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "Yes"),
                    new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Zarplata' type='txt/css'"),
                    new XComment("Документ для минимум зарплаты"),
                    new XElement("MinZarplataDocument"));

                    doc.Save("Data\\MinZarplata.xml");

                    docMin = GetXMLDocument("Data\\MinZarplata.xml");
                }
                List<XElement> elems = docMin.Element("MinZarplataDocument").Elements().ToList<XElement>();
                uint max = 0, i;
                for (int j = 0; j < elems.Count; j++)
                {
                    i = uint.Parse(elems[j].Element("Id").Value.ToString());
                    if (max < i)
                        max = i;
                }

                //Yangi elementni kiritish
                foreach (MinZarplata ob in newMinElement)
                {
                    //MessageBox.Show("Ishladi...");
                    max++;
                    ob.Id = max;
                    XElement newElem = new XElement("MinZarplata",
                        new XElement("Id", ob.Id),
                        new XElement("Qiymat", ob.Qiymat),
                        new XElement("Sana", DateTimeToString(ob.Sana)),
                        new XElement("Asos", ob.Asos),
                        new XElement("Status", ob.Status.IsChecked == true ? 1.ToString() : 0.ToString())
                   );
                    docMin.Descendants("MinZarplataDocument").First().Add(newElem);
                }

                docMin.Save("Data\\MinZarplata.xml");
                return max;
            }
            else
            {
                //XDocument docMin = new XDocument("Data\\MinZarplata.xml");
                uint max = 0;
                XDocument doc = new XDocument(
                   new XDeclaration("1.0", "utf-8", "Yes"),
                   new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Zarplata' type='txt/css'"),
                   new XComment("Документ для минимум зарплаты"),
                   new XElement("MinZarplataDocument"));

                foreach (MinZarplata ob in newMinElement)
                {
                    //max++;
                    XElement newElem = new XElement("MinZarplata",
                       new XElement("Id", ob.Id),
                       new XElement("Qiymat", ob.Qiymat),
                       new XElement("Sana", DateTimeToString(ob.Sana)),
                       new XElement("Asos", ob.Asos),
                       new XElement("Status", ob.Status.IsChecked == true ? 1.ToString() : 0.ToString())
                  );
                    doc.Descendants("MinZarplataDocument").First().Add(newElem);
                }
                doc.Save("Data\\MinZarplata.xml");
                return max;
            }
        }
        public static void UpDataMinZarplata(MinZarplata eting)
        {
            XDocument docMin = GetXMLDocument("Data\\MinZarplata.xml");
            if (docMin == null)
                return;
            List<XElement> elems = docMin.Element("MinZarplataDocument").Elements().ToList<XElement>();

            for(int i = 0; i < elems.Count; i++)
            {
                if(elems[i].Element("Id").Value.ToString() == eting.Id.ToString())
                {
                    elems[i].SetElementValue("Qiymat", eting.Qiymat);
                    elems[i].SetElementValue("Sana", DateTimeToString(eting.Sana));
                    elems[i].SetElementValue("Asos", eting.Asos);
                    elems[i].SetElementValue("Status", eting.Status.IsChecked == true ? 1.ToString() : 0.ToString());
                }
                else
                    if (eting.Status.IsChecked == true)
                {
                    elems[i].SetElementValue("Status", 0);
                }
            }
            

            docMin.Save("Data\\MinZarplata.xml");
        }
        public static void DeleteMinZarplata(string Id)
        {
            XDocument docMin = GetXMLDocument("Data\\MinZarplata.xml");
            if (docMin == null)
                return;
            List<XElement> elems = docMin.Element("MinZarplataDocument").Elements().ToList<XElement>();

            List<XElement> elem = new List<XElement>();
            elem = elems.Where(x => x.Element("Id").Value.ToString() != Id).Select(x => x).ToList<XElement>();

            docMin = new XDocument(
                    new XDeclaration("1.0", "utf-8", "Yes"),
                    new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Zarplata' type='txt/css'"),
                    new XComment("Документ для минимум зарплаты"),
                    new XElement("MinZarplataDocument"));

            docMin.Descendants("MinZarplataDocument").First().Add(elem);

            docMin.Save("Data\\MinZarplata.xml");
        }

        public static uint InsertLavozim(List<Lavozim> newLavozimelement, byte key)
        {
            
            if (key == 0)
            {
                XDocument doclavozim = GetXMLDocument("Data\\Lavozim.xml");
                if (doclavozim == null)
                {
                    doclavozim = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Zarplata' type='txt/css'"),
                        new XComment("Документ для должонсть"),
                        new XElement("LavozimDocument"));
                }

                uint maxId = 0, i;

                foreach (XElement elem in doclavozim.Element("LavozimDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (Lavozim ob in newLavozimelement)
                {
                    maxId++;
                    ob.Id = maxId;
                    XElement newElem = new XElement("Lavozim",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("tarif_raziryad", ob.tarif_raziryad),
                        new XElement("tarif_koefsent", ob.tarif_koefsent),
                        new XElement("tarif_oklad", ob.tarif_oklad)
                        );

                    doclavozim.Descendants("LavozimDocument").First().Add(newElem);
                }
                doclavozim.Save("Data\\Lavozim.xml");
                return maxId;                
            }
            else
            {
                uint MaxId = 0;
                XDocument doclavozim = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Zarplata' type='txt/css'"),
                        new XComment("Документ для должонсть"),
                        new XElement("LavozimDocument"));

                foreach (Lavozim ob in newLavozimelement)
                {
                    MaxId++;
                    ob.Id = MaxId;
                    XElement newElem = new XElement("Lavozim",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("tarif_raziryad", ob.tarif_raziryad),
                        new XElement("tarif_koefsent", ob.tarif_koefsent),
                        new XElement("tarif_oklad", ob.tarif_oklad)
                        );

                    doclavozim.Descendants("LavozimDocument").First().Add(newElem);
                }

                doclavozim.Save("Data\\Lavozim.xml");
                return MaxId;
            }
        }
        public static void UpDataLavozim(Lavozim uplavozim)
        {
            XDocument doclavozim = GetXMLDocument("Data\\Lavozim.xml");
            if (doclavozim == null)
                return;

            List<XElement> elems = new List<XElement>();
            elems = doclavozim.Element("LavozimDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == uplavozim.Id.ToString())
                {
                    elems[i].SetElementValue("Nom", uplavozim.Nom);
                    elems[i].SetElementValue("tarif_raziryad", uplavozim.tarif_raziryad);
                    elems[i].SetElementValue("tarif_koefsent", uplavozim.tarif_koefsent);
                    elems[i].SetElementValue("tarif_oklad", uplavozim.tarif_oklad);
                    break;
                }
            }

            doclavozim.Save("Data\\Lavozim.xml");
        }
        public static void DeleteLavozim(string dellavozimId)
        {
            XDocument doc = GetXMLDocument("Data\\Lavozim.xml");
            if (doc == null)
                return;

            List<XElement> elems = doc.Element("LavozimDocument").Elements().ToList<XElement>();

            List<XElement> editing = new List<XElement>();

            editing = elems.Where(x => x.Element("Id").Value.ToString() != dellavozimId).Select(x => x).ToList<XElement>();

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для должонсть"),
                        new XElement("LavozimDocument"));
            doc.Descendants("LavozimDocument").First().Add(editing);
            doc.Save("Data\\Lavozim.xml");
        }

        private static void InsertIshTuri(List<IshTuri> newIshTuri, byte key)
        {
            if (key == 0)
            {
                XDocument docIshturi = GetXMLDocument("Data\\IshTuri.xml");
                if (docIshturi == null)
                {
                    docIshturi = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Zarplata' type='txt/css'"),
                        new XComment("Документ для тип работу"),
                        new XElement("IshTuriDocument"));
                    docIshturi.Save("Data\\IshTuri.xml");
                }

                uint maxId = 0, i;

                foreach (XElement elem in docIshturi.Element("IshTuriDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (IshTuri ob in newIshTuri)
                {
                    maxId++;
                    ob.Id = maxId;
                    XElement newElem = new XElement("IshTuri",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom)
                        );

                    docIshturi.Descendants("IshTuriDocument").First().Add(newElem);
                }

                docIshturi.Save("Data\\IshTuri.xml");
            }
            else
            {
                XDocument docIshturi = new XDocument(
                    new XDeclaration("1.0", "utf-8", "Yes"),
                    new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Zarplata' type='txt/css'"),
                    new XComment("Документ для тип работу"),
                    new XElement("IshTuriDocument"));

                foreach (IshTuri ob in newIshTuri)
                {
                    XElement newElem = new XElement("IshTuri",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom)
                        );

                    docIshturi.Descendants("IshTuriDocument").First().Add(newElem);
                }

                docIshturi.Save("Data\\IshTuri.xml");
            }
        }

        public static void InsertSostavBrigada(List<SostavBrigada> newSostav, byte key)
        {
            if (key == 0)
            {
                XDocument doc3 = GetXMLDocument("Data\\SostavBrigada.xml");
                if (doc3 == null)
                {
                    doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для состав бригады"),
                        new XElement("SostavBrigadaDocument"));
                    doc3.Save("Data\\SostavBrigada.xml");
                }

                uint maxId = 0, i;

                foreach (XElement elem in doc3.Element("SostavBrigadaDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (SostavBrigada ob in newSostav)
                {
                    maxId++;
                    ob.Id = maxId;

                    XElement newelem = new XElement("SostavBrigada",
                        new XElement("Id", ob.Id),
                        new XElement("Son", ob.Son),
                        new XElement("K", ob.Koef),
                        new XElement("LavozimId", ob.LavozimId),
                        new XElement("IshId", ob.IshId)
                        );

                    doc3.Descendants("SostavBrigadaDocument").First().Add(newelem);
                }

                doc3.Save("Data\\SostavBrigada.xml");
            }
            else
            {
                XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для состав бригады"),
                        new XElement("SostavBrigadaDocument"));



                foreach (SostavBrigada ob in newSostav)
                {
                    XElement newelem = new XElement("SostavBrigada",
                        new XElement("Id", ob.Id),
                        new XElement("Son", ob.Son),
                        new XElement("K", ob.Koef),
                        new XElement("LavozimId", ob.LavozimId),
                        new XElement("IshId", ob.IshId)
                        );

                    doc3.Descendants("SostavBrigadaDocument").First().Add(newelem);
                }

                doc3.Save("Data\\SostavBrigada.xml");
            }
        }
        public static void updataSostavBrigada(SostavBrigada updata)
        {
            XDocument doc = GetXMLDocument("Data\\SostavBrigada.xml");
            if (doc == null)
                return;

            List<XElement> elems = new List<XElement>();
            elems = doc.Element("SostavBrigadaDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == updata.Id.ToString())
                {
                    elems[i].SetElementValue("Son", updata.Son);
                    elems[i].SetElementValue("K", updata.Koef);
                    elems[i].SetElementValue("IshId", updata.IshId);
                    elems[i].SetElementValue("LavozimId", updata.LavozimId);
                    break;
                }
            }

            doc.Save("Data\\SostavBrigada.xml");
        }
        public static void DeleteSostavBrigada(string nodes, string Id)
        {
            XDocument doc = GetXMLDocument("Data\\SostavBrigada.xml");
            if (doc == null)
                return;

            List<XElement> elems = new List<XElement>();
            elems = doc.Element("SostavBrigadaDocument").Elements().ToList<XElement>();
            elems = elems.Where(x => x.Element(nodes).Value.ToString() != Id).Select(x => x).ToList<XElement>();

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для состав бригады"),
                        new XElement("SostavBrigadaDocument"));

            doc.Descendants("SostavBrigadaDocument").First().Add(elems);

            doc.Save("Data\\SostavBrigada.xml");
        }

        public static void InsertIzoh(List<Izoh> newIzoh, byte key)
        {
            if (key == 0)
            {
                XDocument doc3 = GetXMLDocument("Data\\Izoh.xml");
                if (doc3 == null)
                {
                    doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для примачания"),
                        new XElement("IzohDocument"));
                    doc3.Save("Data\\Izoh.xml");
                }

                uint maxId = 0, i;

                foreach (XElement elem in doc3.Element("IzohDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (Izoh ob in newIzoh)
                {
                    maxId++;
                    ob.Id = maxId;

                    XElement newelem = new XElement("Izoh",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("K", ob.Koefsent),
                        new XElement("IshId", ob.IshId),
                        new XElement("IT", ob.IT)
                        );

                    doc3.Descendants("IzohDocument").First().Add(newelem);
                }

                doc3.Save("Data\\Izoh.xml");
            }
            else
            {
                XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для примачания"),
                        new XElement("IzohDocument"));



                foreach (Izoh ob in newIzoh)
                {
                    XElement newelem = new XElement("Izoh",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("K", ob.Koefsent),
                        new XElement("IshId", ob.IshId),
                        new XElement("IT", ob.IT)
                        );

                    doc3.Descendants("IzohDocument").First().Add(newelem);
                }

                doc3.Save("Data\\Izoh.xml");
            }
        }
        public static void UpdataIzoh(Izoh updataizoh)
        {
            XDocument doc = GetXMLDocument("Data\\Izoh.xml");
            if (doc == null)
                return;

            List<XElement> elems = new List<XElement>();
            elems = doc.Element("IzohDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == updataizoh.Id.ToString())
                {
                    elems[i].SetElementValue("Nom", updataizoh.Nom);
                    elems[i].SetElementValue("K", updataizoh.Koefsent);
                    elems[i].SetElementValue("IshId", updataizoh.IshId);
                    break;
                }
            }

            doc.Save("Data\\Izoh.xml");
        }
        public static void DeleteIzoh(string nodes,string Id)
        {
            XDocument doc = GetXMLDocument("Data\\Izoh.xml");
            if (doc == null)
                return;

            List<XElement> elems = new List<XElement>();
            elems = doc.Element("IzohDocument").Elements().ToList<XElement>();
            elems = elems.Where(x => x.Element(nodes).Value != Id).Select(x => x).ToList<XElement>();

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для примачания"),
                        new XElement("IzohDocument"));

            doc.Descendants("IzohDocument").First().Add(elems);

            doc.Save("Data\\Izoh.xml");
        }

        public static void InsertPovishKoefsent(List<PovishKoefsent> newPovish, byte key)
        {
            if (key == 0)
            {
                XDocument doc3 = GetXMLDocument("Data\\Povish.xml");
                if (doc3 == null)
                {
                    doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для повишивый коефесент"),
                        new XElement("PovishDocument"));
                }

                uint maxId = 0, i;

                foreach (XElement elem in doc3.Element("PovishDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (PovishKoefsent ob in newPovish)
                {
                    maxId++;
                    ob.Id = maxId;

                    XElement newelem = new XElement("Izoh",
                        new XElement("Id", ob.Id),
                        new XElement("Sana", DateTimeToString(ob.Sana)),
                        new XElement("Qiymat", ob.Qiymat),
                        new XElement("Izoh", ob.Izoh),
                        new XElement("Status", ob.Status.IsChecked == true ? 1 : 0)
                        );

                    doc3.Descendants("PovishDocument").First().Add(newelem);
                }

                doc3.Save("Data\\Povish.xml");
            }
            else
            {
                XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для повишивый коефесент"),
                        new XElement("PovishDocument"));



                foreach (PovishKoefsent ob in newPovish)
                {
                    XElement newelem = new XElement("Izoh",
                        new XElement("Id", ob.Id),
                        new XElement("Sana", DateTimeToString(ob.Sana)),
                        new XElement("Qiymat", ob.Qiymat),
                        new XElement("Izoh", ob.Izoh),
                        new XElement("Status", ob.Status.IsChecked == true ? 1 : 0)
                        );

                    doc3.Descendants("PovishDocument").First().Add(newelem);
                }

                doc3.Save("Data\\Povish.xml");
            }
        }
        public static void UpdataPovishKeofsent(List<PovishKoefsent> updata)
        {
            XDocument doclavozim = GetXMLDocument("Data\\Povish.xml");
            if (doclavozim == null)
                return;

            List<XElement> elems = new List<XElement>();
            elems = doclavozim.Element("PovishDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                elems[i].SetElementValue("Id", updata[i].Id);
                elems[i].SetElementValue("Sana",  DateTimeToString(updata[i].Sana));
                elems[i].SetElementValue("Qiymat", updata[i].Qiymat);
                elems[i].SetElementValue("Izoh", updata[i].Izoh);
                elems[i].SetElementValue("Status", updata[i].Status.IsChecked == true ? "1" : "0");
            }

            doclavozim.Save("Data\\Povish.xml");
        }
        public static void UpdataPovishKeofsent1(PovishKoefsent updata)
        {
            XDocument doclavozim = GetXMLDocument("Data\\Povish.xml");
            if (doclavozim == null)
                return;

            List<XElement> elems = new List<XElement>();
            elems = doclavozim.Element("PovishDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == updata.Id.ToString())
                {
                    elems[i].SetElementValue("Id", updata.Id);
                    elems[i].SetElementValue("Sana", DateTimeToString(updata.Sana));
                    elems[i].SetElementValue("Qiymat", updata.Qiymat);
                    elems[i].SetElementValue("Izoh", updata.Izoh);
                    elems[i].SetElementValue("Status", updata.Status.IsChecked == true ? "1" : "0");
                    break;
                }
                
            }

            doclavozim.Save("Data\\Povish.xml");
        }
        public static void DeletePovishKoefsent(string Id)
        {
            XDocument docMin = GetXMLDocument("Data\\Povish.xml");
            if (docMin == null)
                return;
            List<XElement> elems = docMin.Element("PovishDocument").Elements().ToList<XElement>();

            List<XElement> elem = new List<XElement>();
            elem = elems.Where(x => x.Element("Id").Value.ToString() != Id).Select(x => x).ToList<XElement>();

            docMin = new XDocument(
                    new XDeclaration("1.0", "utf-8", "Yes"),
                    new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Zarplata' type='txt/css'"),
                    new XComment("Документ для повишивый коефесент"),
                    new XElement("PovishDocument"));

            docMin.Descendants("PovishDocument").First().Add(elem);

            docMin.Save("Data\\Povish.xml");
        }

        public static uint InsertIsh(List<Ish> newIsh, byte key)
        {
            if (key == 0)
            {
                XDocument doc3 = GetXMLDocument("Data\\Ish.xml");
                if (doc3 == null)
                {
                    doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для работу"),
                        new XElement("IshDocument"));
                    doc3.Save("Data\\Ish.xml");
                }

                uint maxId = 0, i;

                foreach (XElement elem in doc3.Element("IshDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (Ish ob in newIsh)
                {
                    maxId++;
                    ob.Id = maxId;

                    XElement newelem = new XElement("Ish",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("Tarkibi", ob.Tarkibi),
                        new XElement("Izoh", ob.Izoh),
                        new XElement("IV", ob.IshVaqti),
                        new XElement("ITI", ob.IshTuriId),
                        new XElement("PKL", ob.PKL),
                        new XElement("NN", ob.NomerNorma),
                        new XElement("KS", ob.KotegoriyaSlojnost),
                        new XElement("TS", ob.TarifStavka),
                        new XElement("OB", ob.OlchovBirligi)
                        );

                    doc3.Descendants("IshDocument").First().Add(newelem);
                }

                doc3.Save("Data\\Ish.xml");
                return maxId;
            }
            else
            {
                XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для работу"),
                        new XElement("IshDocument"));

                uint maxId = 0;

                foreach (Ish ob in newIsh)
                {
                    //maxId++;
                    //ob.Id = maxId;
                    XElement newelem = new XElement("Ish",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("Tarkibi", ob.Tarkibi),
                        new XElement("Izoh", ob.Izoh),
                        new XElement("IV", ob.IshVaqti),
                        new XElement("ITI", ob.IshTuriId),
                        new XElement("PKL", ob.PKL),
                        new XElement("NN", ob.NomerNorma),
                        new XElement("KS", ob.KotegoriyaSlojnost),
                        new XElement("TS", ob.TarifStavka),
                        new XElement("OB", ob.OlchovBirligi)
                        );

                    doc3.Descendants("IshDocument").First().Add(newelem);
                }

                doc3.Save("Data\\Ish.xml");
                return maxId;
            }
        }
        public static void UpdataIsh(Ish updataish)
        {
            XDocument doc = GetXMLDocument("Data\\Ish.xml");
            if (doc == null)
                return;

            List<XElement> elems = new List<XElement>();
            elems = doc.Element("IshDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == updataish.Id.ToString())
                {
                    elems[i].SetElementValue("Nom", updataish.Nom);
                    elems[i].SetElementValue("Tarkibi",updataish.Tarkibi);
                    elems[i].SetElementValue("Izoh", updataish.Izoh);
                    elems[i].SetElementValue("IV", updataish.IshVaqti);
                    elems[i].SetElementValue("ITI", updataish.IshTuriId);
                    elems[i].SetElementValue("PKL", updataish.PKL);
                    elems[i].SetElementValue("NN", updataish.NomerNorma);
                    elems[i].SetElementValue("KS", updataish.KotegoriyaSlojnost);
                    elems[i].SetElementValue("TS", updataish.TarifStavka);
                    elems[i].SetElementValue("OB", updataish.OlchovBirligi);
                    break;
                }
            }

            doc.Save("Data\\Ish.xml");
        }
        public static void DeleteIsh(string Id)
        {
            XDocument doc = GetXMLDocument("Data\\Ish.xml");

            if (doc == null)
                return;

            List<XElement> list = doc.Element("IshDocument").Elements().ToList<XElement>();

            list = list.Where(x => x.Element("Id").Value != Id).Select(x => x).ToList<XElement>();

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для работу"),
                        new XElement("IshDocument"));

            doc.Descendants("IshDocument").First().Add(list);

            doc.Save("Data\\Ish.xml");

        }

        public static uint InsertDogovor(List<Dogovor> newDogovor, byte key)
        {
            if (key == 0)
            {
                XDocument doc3 = GetXMLDocument("Data\\Dogovor.xml");
                if (doc3 == null)
                {
                    doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для договор"),
                        new XElement("DogovorDocument"));
                    doc3.Save("Data\\Dogovor.xml");
                }

                uint maxId = 0, i;

                foreach (XElement elem in doc3.Element("DogovorDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (Dogovor ob in newDogovor)
                {
                    maxId++;
                    ob.Id = maxId;

                    XElement newelem = new XElement("Dogovor",
                        new XElement("Id", ob.Id),
                        new XElement("ND", ob.NomerDogovor),
                        new XElement("S", DateTimeToString(ob.Sana)),
                        new XElement("ON", ob.ObyektNom),
                        new XElement("KN", ob.KlentNom),
                        new XElement("B", ob.Bajaruvchi),
                        new XElement("ITI", ob.IshTuriId),
                        new XElement("PK", ob.PK),
                        new XElement("PKK", ob.PKK),
                        new XElement("PQ", ob.PQ)
                        );

                    doc3.Descendants("DogovorDocument").First().Add(newelem);
                }

                doc3.Save("Data\\Dogovor.xml");
                return maxId;
            }
            else
            {
                XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для договор"),
                        new XElement("DogovorDocument"));

                uint maxId = 0;

                foreach (Dogovor ob in newDogovor)
                {
                    maxId++;
                    ob.Id = maxId;
                    XElement newelem = new XElement("Dogovor",
                         new XElement("Id", ob.Id),
                        new XElement("ND", ob.NomerDogovor),
                        new XElement("S", DateTimeToString(ob.Sana)),
                        new XElement("ON", ob.ObyektNom),
                        new XElement("KN", ob.KlentNom),
                        new XElement("B", ob.Bajaruvchi),
                        new XElement("ITI", ob.IshTuriId),
                        new XElement("PK", ob.PK),
                        new XElement("PKK", ob.PKK),
                        new XElement("PQ", ob.PQ)
                         );

                    doc3.Descendants("DogovorDocument").First().Add(newelem);
                }

                doc3.Save("Data\\Dogovor.xml");
                return maxId;
            }
        }
        public static void InsertData(List<Data> newData, string path)
        {
            XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для дату"),
                        new XElement("DataDocument"));



            foreach (Data ob in newData)
            {
                XElement newelem = new XElement("Data",
                    new XElement("Id", ob.Id),
                    new XElement("Hajm", ob.Hajm),
                    new XElement("PK", ob.PovishKoefsent),
                    new XElement("PQ", ob.PovishQiymat),
                    new XElement("IID", ob.IshId),
                    new XElement("DI", ob.DogovorId),
                    new XElement("IV", ob.IV),
                    new XElement("TS", ob.TS)
                    );

                doc3.Descendants("DataDocument").First().Add(newelem);
            }

            doc3.Save(Environment.CurrentDirectory + @"\Data\Data\" + path +  ".xml");
        }
        public static void UpDataDogovor(Dogovor updogovor)
        {
            XDocument doc = GetXMLDocument("Data\\Dogovor.xml");
            if (doc == null)
                return;

            List<XElement> elems = doc.Element("DogovorDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == updogovor.Id.ToString())
                {
                    elems[i].SetElementValue("ND", updogovor.NomerDogovor);
                    elems[i].SetElementValue("S", DateTimeToString(updogovor.Sana));
                    elems[i].SetElementValue("ON", updogovor.ObyektNom);
                    elems[i].SetElementValue("KN", updogovor.KlentNom);
                    elems[i].SetElementValue("B", updogovor.Bajaruvchi);
                    elems[i].SetElementValue("ITI", updogovor.IshTuriId);
                    elems[i].SetElementValue("PK", updogovor.PK);
                    elems[i].SetElementValue("PKK", updogovor.PKK);
                    elems[i].SetElementValue("PQ", updogovor.PQ);
                    break;
                }
            }

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для договор"),
                        new XElement("DogovorDocument"));
            doc.Descendants("DogovorDocument").First().Add(elems);
            doc.Save("Data\\Dogovor.xml");
        }
        public static void UpDataDogovor1(Dogovor updogovor)
        {
            XDocument doc = GetXMLDocument("Data\\Dogovor.xml");
            if (doc == null)
                return;

            List<XElement> elems = doc.Element("DogovorDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == updogovor.Id.ToString())
                {
                    elems[i].SetElementValue("ND", updogovor.NomerDogovor);
                    elems[i].SetElementValue("S", DateTimeToString(updogovor.Sana));
                    elems[i].SetElementValue("ON", updogovor.ObyektNom);
                    elems[i].SetElementValue("KN", updogovor.KlentNom);
                    elems[i].SetElementValue("B", updogovor.Bajaruvchi);
                    elems[i].SetElementValue("ITI", updogovor.IshTuriId);
                    //elems[i].SetElementValue("PK", updogovor.PK);
                    //elems[i].SetElementValue("PKK", updogovor.PKK);
                    //elems[i].SetElementValue("PQ", updogovor.PQ);
                    break;
                }
            }

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для договор"),
                        new XElement("DogovorDocument"));
            doc.Descendants("DogovorDocument").First().Add(elems);
            doc.Save("Data\\Dogovor.xml");
        }
        public static void DeleteDogovor(string deldogvorId)
        {
            XDocument doc = GetXMLDocument("Data\\Dogovor.xml");
            if (doc == null)
                return;

            List<XElement> elems = doc.Element("DogovorDocument").Elements().ToList<XElement>();

            List<XElement> editing = new List<XElement>();

            foreach (XElement ob in elems)
            {
                if(deldogvorId != ob.Element("Id").Value.ToString())
                {
                    editing.Add(ob);
                }
            }

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для договор"),
                        new XElement("DogovorDocument"));
            doc.Descendants("DogovorDocument").First().Add(editing);
            doc.Save("Data\\Dogovor.xml");
        }
        public static void InsertDogovorDelete(Dogovor ob)
        {
            XDocument doc;
            try
            {
                doc = XDocument.Load(Environment.CurrentDirectory + @"\Data\savat\Dogovor.xml");
            }
            catch
            {
                doc = null;
            }

            //try
            //{
            //    doccopy = GetXMLDocument(@"\Data\Data\" + ob.Id.ToString() + ".xml");
            //    if (doccopy != null)
            //        doccopy.Save(Environment.CurrentDirectory + @"\Data\Savat\" + ob.Id.ToString() + ".xml");
            //    else
            //        MessageBox.Show("Null");
            //}
            //catch(Exception exc)
            //{
            //    MessageBox.Show(exc.ToString());  
            //}
            if (doc == null)
            {
                doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Dogovor' type='txt/css'"),
                        new XComment("Документ для договор"),
                        new XElement("DogovorDocument"));
            }

            XElement newelem = new XElement("Dogovor",
                        new XElement("Id", ob.Id),
                        new XElement("ND", ob.NomerDogovor),
                        new XElement("S", DateTimeToString(ob.Sana)),
                        new XElement("ON", ob.ObyektNom),
                        new XElement("KN", ob.KlentNom),
                        new XElement("B", ob.Bajaruvchi),
                        new XElement("ITI", ob.IshTuriId),
                        new XElement("PK", ob.PK),
                        new XElement("PKK", ob.PKK),
                        new XElement("PQ", ob.PQ)
                        );
            doc.Descendants("DogovorDocument").First().Add(newelem);
            doc.Save(Environment.CurrentDirectory + @"\Data\savat\Dogovor.xml");
        }
        
        public static void InsertInfo(int Width = 1252, int Height = 606, byte yon = 1, byte top1 = 1, byte top2 = 1)
        {
            XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Document for info window"),
                        new XElement("Info"));



            XElement newelem = new XElement("Data",
                   new XElement("Width", Width),
                   new XElement("Height", Height),
                   new XElement("yon", yon),
                   new XElement("top1", top1),
                   new XElement("top2", top2)
                   );

            doc3.Descendants("Info").First().Add(newelem);
            doc3.Save("info.xml");
        }

        public static uint InsertDalaKoyffetsent(List<Izoh> newIzoh, byte key)
        {
            if (key == 0)
            {
                XDocument doc3 = GetXMLDocument("Data\\DalaIzoh.xml");
                if (doc3 == null)
                {
                    doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Dala izohlari jadvali' type='txt/css'"),
                        new XComment("Документ для примечание"),
                        new XElement("IzohDocument"));
                }

                uint maxId = 0, i;

                foreach (XElement elem in doc3.Element("IzohDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (Izoh ob in newIzoh)
                {
                    maxId++;
                    ob.Id = maxId;

                    XElement newelem = new XElement("Izoh",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("Koy", ob.Koefsent)
                        );

                    doc3.Descendants("IzohDocument").First().Add(newelem);
                }

                doc3.Save("Data\\DalaIzoh.xml");
                return maxId;
            }
            else
            {
                XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Dala izohlari jadvali' type='txt/css'"),
                        new XComment("Документ для примечание"),
                        new XElement("IzohDocument"));

                uint maxId = 0;

                foreach (Izoh ob in newIzoh)
                {
                    maxId++;
                    ob.Id = maxId;
                    XElement newelem = new XElement("Izoh",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("Koy", ob.Koefsent)
                        );

                    doc3.Descendants("IzohDocument").First().Add(newelem);
                }

                doc3.Save("Data\\DalaIzoh.xml");
                return maxId;
            }
        }
        public static void UpDataDalaKoyffetsent(Izoh upIzoh)
        {
            XDocument doc = GetXMLDocument("Data\\DalaIzoh.xml");
            if (doc == null)
                return;

            List<XElement> elems = doc.Element("IzohDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == upIzoh.Id.ToString())
                {
                    elems[i].Element("Nom").Value = upIzoh.Nom;
                    elems[i].SetElementValue("Koy", upIzoh.Koefsent);
                    break;
                }
            }
            
            doc.Save("Data\\DalaIzoh.xml");
        }
        public static void DeleteDalaKoyffetsent(string deldogvorId)
        {
            XDocument doc = GetXMLDocument("Data\\DalaIzoh.xml");
            if (doc == null)
                return;

            List<XElement> elems = doc.Element("IzohDocument").Elements().ToList<XElement>();

            List<XElement> editing = new List<XElement>();

            foreach (XElement ob in elems)
            {
                if (deldogvorId != ob.Element("Id").Value.ToString())
                {
                    editing.Add(ob);
                }
            }

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для примечание"),
                        new XElement("IzohDocument"));
            doc.Descendants("IzohDocument").First().Add(editing);
            doc.Save("Data\\DalaIzoh.xml");
        }

        public static uint InsertGeologiyaKoyffetsent(List<Izoh> newIzoh, byte key)
        {
            if (key == 0)
            {
                XDocument doc3 = GetXMLDocument("Data\\GeologiyaIzoh.xml");
                if (doc3 == null)
                {
                    doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Dala izohlari jadvali' type='txt/css'"),
                        new XComment("Документ для примечание"),
                        new XElement("IzohDocument"));
                }

                uint maxId = 0, i;

                foreach (XElement elem in doc3.Element("IzohDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }

                foreach (Izoh ob in newIzoh)
                {
                    maxId++;
                    ob.Id = maxId;

                    XElement newelem = new XElement("Izoh",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("Koy", ob.Koefsent)
                        );

                    doc3.Descendants("IzohDocument").First().Add(newelem);
                }

                doc3.Save("Data\\GeologiyaIzoh.xml");
                return maxId;
            }
            else
            {
                XDocument doc3 = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Dala izohlari jadvali' type='txt/css'"),
                        new XComment("Документ для примечание"),
                        new XElement("IzohDocument"));

                uint maxId = 0;

                foreach (Izoh ob in newIzoh)
                {
                    maxId++;
                    ob.Id = maxId;
                    XElement newelem = new XElement("Izoh",
                        new XElement("Id", ob.Id),
                        new XElement("Nom", ob.Nom),
                        new XElement("Koy", ob.Koefsent)
                        );

                    doc3.Descendants("IzohDocument").First().Add(newelem);
                }

                doc3.Save("Data\\GeologiyaIzoh.xml");
                return maxId;
            }
        }
        public static void UpDataGeologiyaKoyffetsent(Izoh upIzoh)
        {
            XDocument doc = GetXMLDocument("Data\\GeologiyaIzoh.xml");
            if (doc == null)
                return;

            List<XElement> elems = doc.Element("IzohDocument").Elements().ToList<XElement>();

            for (int i = 0; i < elems.Count; i++)
            {
                if (elems[i].Element("Id").Value.ToString() == upIzoh.Id.ToString())
                {
                    elems[i].Element("Nom").Value = upIzoh.Nom;
                    elems[i].SetElementValue("Koy", upIzoh.Koefsent);
                    break;
                }
            }

            doc.Save("Data\\GeologiyaIzoh.xml");
        }
        public static void DeleteGeologiyaKoyffetsent(string deldogvorId)
        {
            XDocument doc = GetXMLDocument("Data\\GeologiyaIzoh.xml");
            if (doc == null)
                return;

            List<XElement> elems = doc.Element("IzohDocument").Elements().ToList<XElement>();

            List<XElement> editing = new List<XElement>();

            foreach (XElement ob in elems)
            {
                if (deldogvorId != ob.Element("Id").Value.ToString())
                {
                    editing.Add(ob);
                }
            }

            doc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "Yes"),
                        new XProcessingInstruction("xml-stylesheet", "href='Style.css' title='Sostav  brigada' type='txt/css'"),
                        new XComment("Документ для примечание"),
                        new XElement("IzohDocument"));
            doc.Descendants("IzohDocument").First().Add(editing);
            doc.Save("Data\\GeologiyaIzoh.xml");
        }

    }
}
