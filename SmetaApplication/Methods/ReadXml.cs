using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Smeta.Models;
using System.Windows.Controls;
using System.Windows;
using System.IO;
using System.Globalization;

namespace Smeta.Methods
{
    class ReadXml
    {
        private static XDocument GetXMLDocument(string path)
        {
            try
            {
                path = Environment.CurrentDirectory + @"\" + path;
                //if (File.Exists(path))
                //    MessageBox.Show(path);
                XDocument doc = XDocument.Load(path);
                return doc;
            }
            catch
            {
                //MessageBox.Show(exc.Message);
                return null;
            }
        }
        public static double ToDuoubleNuqta(string str)
        {
            double dd;

            var numberStyle = NumberStyles.AllowLeadingWhite |
                 NumberStyles.AllowTrailingWhite |
                 NumberStyles.AllowLeadingSign |
                 NumberStyles.AllowDecimalPoint |
                 NumberStyles.AllowThousands |
                 NumberStyles.AllowExponent;
            var numberFormatInfo = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
            };
            if (!double.TryParse(str, numberStyle, numberFormatInfo, out dd))
            {
                numberFormatInfo.NumberDecimalSeparator = ",";
                if (!double.TryParse(str, numberStyle, numberFormatInfo, out dd))
                    dd = -1;
            }
            return dd;
        }
        private static DateTime ToDateTime(string str)
        {
            return DateTime.Parse(str);
            //DateTime dt;
            //var datetimeFormatInfo = new DateTimeFormatInfo()
            //{
            //    DateSeparator = "."
            //};
            //if (!DateTime.TryParse(str, datetimeFormatInfo, DateTimeStyles.AssumeUniversal, out dt))
            //    dt = DateTime.Now;
            //return dt;
        }
        public static List<MinZarplata> SelectMinZarplata(string where, string qiymat)
        {
            List<MinZarplata> list = new List<MinZarplata>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\MinZarplata.xml");

                if (doc == null)
                    return null;

                XElement elems = doc.Element("MinZarplataDocument");
                uint ii;
                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat) 
                        {
                            MinZarplata minzarplata = new MinZarplata();
                            CheckBox chec = new CheckBox();
                            minzarplata.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            minzarplata.Qiymat = ToDuoubleNuqta(elem.Element("Qiymat").Value);
                            minzarplata.Sana = ToDateTime(elem.Element("Sana").Value);
                            minzarplata.Asos = elem.Element("Asos").Value;
                            chec.IsChecked = elem.Element("Status").Value == "1" ? true : false;
                            minzarplata.Status = chec;

                            list.Add(minzarplata);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        MinZarplata minzarplata = new MinZarplata();
                        CheckBox chec = new CheckBox()
                        {
                            IsChecked = true
                        };
                        minzarplata.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        minzarplata.Qiymat = ToDuoubleNuqta(elem.Element("Qiymat").Value);
                        minzarplata.Sana = ToDateTime(elem.Element("Sana").Value);
                        minzarplata.Asos = elem.Element("Asos").Value;
                        chec.IsChecked = elem.Element("Status").Value == "1" ? true : false;
                        minzarplata.Status = chec;

                        list.Add(minzarplata);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }

        public static List<Lavozim> SelectLavozim(string where, string qiymat)
        {
          
            List<Lavozim> list = new List<Lavozim>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\Lavozim.xml");

                if (doc == null)
                    return null;

                XElement elems = doc.Element("LavozimDocument");
                uint ii;

                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            Lavozim lavozim = new Lavozim();

                            lavozim.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            lavozim.Nom = elem.Element("Nom").Value;
                            lavozim.tarif_raziryad = uint.TryParse(elem.Element("tarif_raziryad").Value, out ii) ? ii : 0;
                            lavozim.tarif_koefsent = ToDuoubleNuqta(elem.Element("tarif_koefsent").Value);
                            lavozim.tarif_oklad = ToDuoubleNuqta(elem.Element("tarif_oklad").Value);

                            list.Add(lavozim);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        Lavozim lavozim = new Lavozim();

                        lavozim.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        lavozim.Nom = elem.Element("Nom").Value;
                        lavozim.tarif_raziryad = uint.TryParse(elem.Element("tarif_raziryad").Value, out ii) ? ii : 0;
                        lavozim.tarif_koefsent = ToDuoubleNuqta(elem.Element("tarif_koefsent").Value);
                        lavozim.tarif_oklad = ToDuoubleNuqta(elem.Element("tarif_oklad").Value);

                        list.Add(lavozim);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            return list;
        }

        public static List<IshTuri> SelectIshTuri(string where, string qiymat)
        {
            List<IshTuri> list = new List<IshTuri>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\IshTuri.xml");
                if (doc == null)
                    return null;
                uint ii;
                XElement elems = doc.Element("IshTuriDocument");

                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            IshTuri ishturi = new IshTuri();
                            ishturi.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            ishturi.Nom = elem.Element("Nom").Value;
                            list.Add(ishturi);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        IshTuri ishturi = new IshTuri();
                        ishturi.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        ishturi.Nom = elem.Element("Nom").Value.ToString();
                        list.Add(ishturi);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }

        public static List<SostavBrigada> SelectSostavBrigada(string where, string qiymat)
        {
            List<SostavBrigada> list = new List<SostavBrigada>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\SostavBrigada.xml");
                if (doc == null)
                    return null;
                uint ii;
                XElement elems = doc.Element("SostavBrigadaDocument");

                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            SostavBrigada sostavbrigada = new SostavBrigada();

                            sostavbrigada.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            sostavbrigada.Son = uint.TryParse(elem.Element("Son").Value, out ii) ? ii : 0;
                            sostavbrigada.Koef = ToDuoubleNuqta(elem.Element("K").Value);
                            sostavbrigada.LavozimId = uint.TryParse(elem.Element("LavozimId").Value, out ii) ? ii : 0;
                            sostavbrigada.IshId = uint.TryParse(elem.Element("IshId").Value, out ii) ? ii : 0;

                            list.Add(sostavbrigada);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        SostavBrigada sostavbrigada = new SostavBrigada();

                        sostavbrigada.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        sostavbrigada.Son = uint.TryParse(elem.Element("Son").Value, out ii) ? ii : 0;
                        sostavbrigada.Koef = ToDuoubleNuqta(elem.Element("K").Value);
                        sostavbrigada.LavozimId = uint.TryParse(elem.Element("LavozimId").Value, out ii) ? ii : 0;
                        sostavbrigada.IshId = uint.TryParse(elem.Element("IshId").Value, out ii) ? ii : 0;

                        list.Add(sostavbrigada);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }           
            return list;
        }

        public static List<Izoh> SelectIzoh(string where, string qiymat)
        {
            List<Izoh> list = new List<Izoh>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\Izoh.xml");

                if (doc == null)
                    return null;

                XElement elems = doc.Element("IzohDocument");
                uint ii;
                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            Izoh izoh = new Izoh();

                            izoh.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            izoh.Nom = elem.Element("Nom").Value.ToString();
                            izoh.Koefsent = ToDuoubleNuqta(elem.Element("K").Value);
                            izoh.IshId = uint.TryParse(elem.Element("IshId").Value, out ii) ? ii : 0;

                            list.Add(izoh);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        Izoh izoh = new Izoh();

                        izoh.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        izoh.Nom = elem.Element("Nom").Value.ToString();
                        izoh.Koefsent = ToDuoubleNuqta(elem.Element("K").Value);
                        izoh.IshId = uint.TryParse(elem.Element("IshId").Value, out ii) ? ii : 0;

                        list.Add(izoh);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }

        public static List<PovishKoefsent> SelectPovishKoefsent(string where, string qiymat)
        {
            List<PovishKoefsent> list = new List<PovishKoefsent>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\Povish.xml");
                if (doc == null)
                    return null;

                XElement elems = doc.Element("PovishDocument");
                uint ii;
                DateTime dt;
                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {

                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            PovishKoefsent povish = new PovishKoefsent();
                            CheckBox chec = new CheckBox();

                            povish.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            povish.Sana = DateTime.TryParse(elem.Element("Sana").Value, out dt) ? dt : dt;
                            povish.Qiymat = ToDuoubleNuqta(elem.Element("Qiymat").Value);
                            povish.Izoh = elem.Element("Izoh").Value;
                            chec.IsChecked = elem.Element("Status").Value == "1" ? true : false;
                            povish.Status = chec;

                            list.Add(povish);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        PovishKoefsent povish = new PovishKoefsent();
                        CheckBox chec = new CheckBox();

                        povish.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        povish.Sana = DateTime.TryParse(elem.Element("Sana").Value, out dt) ? dt : dt;
                        povish.Qiymat = ToDuoubleNuqta(elem.Element("Qiymat").Value);
                        povish.Izoh = elem.Element("Izoh").Value;
                        chec.IsChecked = elem.Element("Status").Value == "1" ? true : false;
                        povish.Status = chec;

                        list.Add(povish);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }

        public static List<Ish> SelectIsh(string where, string qiymat)
        {
            List<Ish> list = new List<Ish>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\Ish.xml");
                uint ii;
                if (doc == null)
                    return null;

                XElement elems = doc.Element("IshDocument");

                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            Ish ish = new Ish();

                            ish.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            ish.Nom = elem.Element("Nom").Value;
                            //ish.Tarkibi = elem.Element("Tarkibi").ToString();
                            //ish.Izoh = elem.Element("Izoh").ToString();
                            ish.IshVaqti = ToDuoubleNuqta(elem.Element("IV").Value);
                            ish.IshTuriId = uint.TryParse(elem.Element("ITI").Value, out ii) ? ii : 0;
                            ish.PKL = uint.TryParse(elem.Element("PKL").Value, out ii) ? ii : 0;
                            ish.NomerNorma = elem.Element("NN").Value;
                            ish.KotegoriyaSlojnost = elem.Element("KS").Value;
                            ish.TarifStavka = ToDuoubleNuqta(elem.Element("TS").Value);
                            ish.OlchovBirligi = elem.Element("OB").Value;

                            list.Add(ish);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        Ish ish = new Ish();

                        ish.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        ish.Nom = elem.Element("Nom").Value;
                        //ish.Tarkibi = elem.Element("Tarkibi").ToString();
                        //ish.Izoh = elem.Element("Izoh").ToString();
                        ish.IshVaqti = ToDuoubleNuqta(elem.Element("IV").Value);
                        ish.IshTuriId = uint.TryParse(elem.Element("ITI").Value, out ii) ? ii : 0;
                        ish.PKL = uint.TryParse(elem.Element("PKL").Value, out ii) ? ii : 0;
                        ish.NomerNorma = elem.Element("NN").Value;
                        ish.KotegoriyaSlojnost = elem.Element("KS").Value;
                        ish.TarifStavka = ToDuoubleNuqta(elem.Element("TS").Value);
                        ish.OlchovBirligi = elem.Element("OB").Value;

                        list.Add(ish);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }

        public static List<Ish> SelectIshAll(string where, string qiymat)
        {
            List<Ish> list = new List<Ish>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\Ish.xml");
                if (doc == null)
                    return null;
                uint ii;
                XElement elems = doc.Element("IshDocument");

                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            Ish ish = new Ish();

                            ish.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            ish.Nom = elem.Element("Nom").Value;
                            ish.Tarkibi = elem.Element("Tarkibi").Value;
                            ish.Izoh = elem.Element("Izoh").Value;
                            ish.IshVaqti = ToDuoubleNuqta(elem.Element("IV").Value);
                            ish.IshTuriId = uint.TryParse(elem.Element("ITI").Value, out ii) ? ii : 0;
                            ish.PKL = uint.TryParse(elem.Element("PKL").Value, out ii) ? ii : 0;
                            ish.NomerNorma = elem.Element("NN").Value;
                            ish.KotegoriyaSlojnost = elem.Element("KS").Value;
                            ish.TarifStavka = ToDuoubleNuqta(elem.Element("TS").Value);
                            ish.OlchovBirligi = elem.Element("OB").Value;

                            list.Add(ish);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        Ish ish = new Ish();

                        ish.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        ish.Nom = elem.Element("Nom").Value;
                        ish.Tarkibi = elem.Element("Tarkibi").Value;
                        ish.Izoh = elem.Element("Izoh").Value;
                        ish.IshVaqti = ToDuoubleNuqta(elem.Element("IV").Value);
                        ish.IshTuriId = uint.TryParse(elem.Element("ITI").Value, out ii) ? ii : 0;
                        ish.PKL = uint.TryParse(elem.Element("PKL").Value, out ii) ? ii : 0;
                        ish.NomerNorma = elem.Element("NN").Value;
                        ish.KotegoriyaSlojnost = elem.Element("KS").Value;
                        ish.TarifStavka = ToDuoubleNuqta(elem.Element("TS").Value);
                        ish.OlchovBirligi = elem.Element("OB").Value;

                        list.Add(ish);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }

        public static List<Ish> SelectIshQuery(string qiymat1, string qiymat2, string qiymat3, int count)
        {
            List<Ish> list = new List<Ish>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\Ish.xml");
                if (doc == null)
                    return null;
                XElement elems = doc.Element("IshDocument");
                bool t = true;
                int i = 0;
                //MessageBox.Show(qiymat3);
                foreach (XElement elem in elems.Elements())
                {
                    t = true;
                    if (qiymat1 != "" && qiymat1 != "12" && elem.Element("ITI").Value != qiymat1)
                        t = false;
                    if (t && qiymat2 != "" && qiymat2 != "4" && elem.Element("PKL").Value != qiymat2)
                        t = false;
                    if (t && qiymat3 != "" && elem.Element("Nom").Value.ToLower().IndexOf(qiymat3) != 0)
                        t = false;

                    uint ii;
                    if (t)
                    {
                        Ish ish = new Ish();
                        ish.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        ish.Nom = elem.Element("Nom").Value;
                        //ish.Tarkibi = elem.Element("Tarkibi").ToString();
                        //ish.Izoh = elem.Element("Izoh").ToString();
                        ish.IshVaqti = ToDuoubleNuqta(elem.Element("IV").Value);
                        ish.IshTuriId = uint.TryParse(elem.Element("ITI").Value, out ii) ? ii : 0;
                        ish.PKL = uint.TryParse(elem.Element("PKL").Value, out ii) ? ii : 0;
                        ish.NomerNorma = elem.Element("NN").Value;
                        ish.KotegoriyaSlojnost = elem.Element("KS").Value;
                        ish.TarifStavka = ToDuoubleNuqta(elem.Element("TS").Value);
                        ish.OlchovBirligi = elem.Element("OB").Value;

                        list.Add(ish);
                        i++;
                        if (i >= count && count > 0)
                            break;
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }

        public static List<Dogovor> SelectDogovor(string where, string qiymat, int count)
        {
            List<Dogovor> list = new List<Dogovor>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\Dogovor.xml");

                if (doc == null)
                    return null;

                List<XElement> elems = doc.Element("DogovorDocument").Elements().ToList<XElement>();

                uint ii;
                int i = 0;
                if (where != null && qiymat != null)
                {
                    for (int j = elems.Count - 1; j >= 0; j--)
                    {
                        if (elems[j].Element(where).Value.ToString() == qiymat)
                        {
                            Dogovor dogovor = new Dogovor();

                            dogovor.Id = uint.TryParse(elems[j].Element("Id").Value, out ii) ? ii : 0;
                            dogovor.NomerDogovor = elems[j].Element("ND").Value;
                            dogovor.Sana = ToDateTime(elems[j].Element("S").Value);
                            dogovor.ObyektNom = elems[j].Element("ON").Value;
                            dogovor.KlentNom = elems[j].Element("KN").Value;
                            dogovor.Bajaruvchi = elems[j].Element("B").Value;
                            dogovor.IshTuriId = elems[j].Element("ITI").Value;
                            dogovor.PK = ToDuoubleNuqta(elems[j].Element("PK").Value);
                            dogovor.PKK = elems[j].Element("PKK").Value;
                            dogovor.PQ = ToDuoubleNuqta(elems[j].Element("PQ").Value);
                            i++;
                            list.Add(dogovor);
                        }

                        if (count != 0 && i >= count)
                            break;
                    }
                }
                else
                {
                    //MessageBox.Show(i.ToString());
                    for (int j = elems.Count - 1; j >= 0; j--)
                    {

                        Dogovor dogovor = new Dogovor();

                        dogovor.Id = uint.TryParse(elems[j].Element("Id").Value, out ii) ? ii : 0;
                        dogovor.NomerDogovor = elems[j].Element("ND").Value;
                        dogovor.Sana = ToDateTime(elems[j].Element("S").Value);
                        dogovor.ObyektNom = elems[j].Element("ON").Value;
                        dogovor.KlentNom = elems[j].Element("KN").Value;
                        dogovor.Bajaruvchi = elems[j].Element("B").Value;
                        dogovor.IshTuriId = elems[j].Element("ITI").Value;
                        dogovor.PK = ToDuoubleNuqta(elems[j].Element("PK").Value);
                        dogovor.PKK = elems[j].Element("PKK").Value;
                        dogovor.PQ = ToDuoubleNuqta(elems[j].Element("PQ").Value);

                        list.Add(dogovor);
                        i++;
                        if (count != 0 && i >= count)
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }
        public static List<Dogovor> DogovorFind(string qiymat, int count)
        {
            List<Dogovor> list = new List<Dogovor>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\Dogovor.xml");
                if (doc == null)
                    return null;
                List<XElement> elems = doc.Element("DogovorDocument").Elements().ToList<XElement>();

                uint ii;
                int i = 0;
                if (qiymat != null)
                {
                    qiymat = qiymat.ToLower();
                    string str = "";
                    for (int j = elems.Count - 1; j >= 0; j--)
                    {
                        str = SelectIshTuri("Id", elems[j].Element("ITI").Value.ToString())[0].Nom.ToLower();
                        if (elems[j].Element("ND").Value.ToString().ToLower().IndexOf(qiymat) >= 0 || elems[j].Element("ON").Value.ToString().ToLower().IndexOf(qiymat) >= 0 || elems[j].Element("KN").Value.ToString().IndexOf(qiymat) >= 0 || elems[j].Element("B").Value.ToString().IndexOf(qiymat) >= 0 || str.IndexOf(qiymat) >= 0)
                        {
                            Dogovor dogovor = new Dogovor();

                            dogovor.Id = uint.TryParse(elems[j].Element("Id").Value, out ii) ? ii : 0;
                            dogovor.NomerDogovor = elems[j].Element("ND").Value;
                            dogovor.Sana = ToDateTime(elems[j].Element("S").Value);
                            dogovor.ObyektNom = elems[j].Element("ON").Value;
                            dogovor.KlentNom = elems[j].Element("KN").Value;
                            dogovor.Bajaruvchi = elems[j].Element("B").Value;
                            dogovor.IshTuriId = elems[j].Element("ITI").Value;
                            dogovor.PK = ToDuoubleNuqta(elems[j].Element("PK").Value);
                            dogovor.PKK = elems[j].Element("PKK").Value;
                            dogovor.PQ = ToDuoubleNuqta(elems[j].Element("PQ").Value);

                            i++;
                            list.Add(dogovor);
                        }

                        if (count != 0 && i >= count)
                            break;
                    }
                }
                else
                {
                    //MessageBox.Show(i.ToString());
                    for (int j = elems.Count - 1; j >= 0; j--)
                    {
                        Dogovor dogovor = new Dogovor();

                        dogovor.Id = uint.TryParse(elems[j].Element("Id").Value, out ii) ? ii : 0;
                        dogovor.NomerDogovor = elems[j].Element("ND").Value;
                        dogovor.Sana = ToDateTime(elems[j].Element("S").Value);
                        dogovor.ObyektNom = elems[j].Element("ON").Value;
                        dogovor.KlentNom = elems[j].Element("KN").Value;
                        dogovor.Bajaruvchi = elems[j].Element("B").Value;
                        dogovor.IshTuriId = elems[j].Element("ITI").Value;
                        dogovor.PK = ToDuoubleNuqta(elems[j].Element("PK").Value);
                        dogovor.PKK = elems[j].Element("PKK").Value;
                        dogovor.PQ = ToDuoubleNuqta(elems[j].Element("PQ").Value);
                        list.Add(dogovor);
                        i++;
                        if (count != 0 && i >= count)
                            break;
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }
        
        public static List<Data> SelectData(string path)
        {
            List<Data> list = new List<Data>();
            try
            {
                XDocument doc = GetXMLDocument( @"\Data\Data\" + path + ".xml");
                uint ii;
                if (doc == null)
                    return null;
                //MessageBox.Show("Ka");
                XElement elems = doc.Element("DataDocument");

                foreach (XElement elem in elems.Elements())
                {
                    Data data = new Data();

                    data.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                    data.Hajm = ToDuoubleNuqta(elem.Element("Hajm").Value);
                    data.PovishKoefsent = elem.Element("PK").Value;
                    data.PovishQiymat = ToDuoubleNuqta(elem.Element("PQ").Value);
                    data.IshId = uint.TryParse(elem.Element("IID").Value, out ii) ? ii : 0;
                    data.DogovorId = uint.TryParse(elem.Element("DI").Value, out ii) ? ii : 0;
                    data.IV = ToDuoubleNuqta(elem.Element("IV").Value);
                    data.TS = ToDuoubleNuqta(elem.Element("TS").Value);

                    list.Add(data);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }
        public static uint MaxIdDogovor()
        {
            XDocument doc = GetXMLDocument("Data\\Dogovor.xml");

            uint maxId = 0, i;
            try
            {
                foreach (XElement elem in doc.Element("DogovorDocument").Elements())
                {
                    i = uint.Parse(elem.Element("Id").Value.ToString());
                    if (maxId < i)
                        maxId = i;
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return maxId;
        }

        public static void ReadInfo(ref int Width, ref int Height, ref byte yon, ref byte top1, ref byte top2)
        {
            try
            {
                XDocument doc = GetXMLDocument("info.xml");
                if (doc == null)
                    return;
                List<XElement> elem = doc.Element("Info").Elements().ToList<XElement>();
                byte bb;
                int ii;
                Width = int.TryParse(elem[0].Element("Width").Value, out ii) ? ii : 0;
                Height = int.TryParse(elem[0].Element("Height").Value, out ii) ? ii : 0;
                yon = byte.TryParse(elem[0].Element("yon").Value, out bb) ? bb : (byte) 0;
                top1 = byte.TryParse(elem[0].Element("top1").Value, out bb) ? bb : (byte) 0;
                top2 = byte.TryParse(elem[0].Element("top2").Value, out bb) ? bb : (byte) 0;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public static List<Izoh> SelectDalaKoyffetsent(string where, string qiymat)
        {
            List<Izoh> list = new List<Izoh>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\DalaIzoh.xml");

                if (doc == null)
                    return null;
                uint ii;
                XElement elems = doc.Element("IzohDocument");

                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            Izoh izoh = new Izoh();

                            izoh.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            izoh.Nom = elem.Element("Nom").Value;
                            izoh.Koefsent = ToDuoubleNuqta(elem.Element("Koy").Value);

                            list.Add(izoh);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        Izoh izoh = new Izoh();

                        izoh.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        izoh.Nom = elem.Element("Nom").Value;
                        izoh.Koefsent = ToDuoubleNuqta(elem.Element("Koy").Value);
                        list.Add(izoh);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }

        public static List<Izoh> SelectGeologiyaKoyffetsent(string where, string qiymat)
        {
            List<Izoh> list = new List<Izoh>();
            try
            {
                XDocument doc = GetXMLDocument("Data\\GeologiyaIzoh.xml");

                if (doc == null)
                    return null;
                uint ii;
                XElement elems = doc.Element("IzohDocument");

                if (where != null && qiymat != null)
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        if (elem.Element(where).Value.ToString() == qiymat)
                        {
                            Izoh izoh = new Izoh();

                            izoh.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                            izoh.Nom = elem.Element("Nom").Value;
                            izoh.Koefsent = ToDuoubleNuqta(elem.Element("Koy").Value);

                            list.Add(izoh);
                        }
                    }
                }
                else
                {
                    foreach (XElement elem in elems.Elements())
                    {
                        Izoh izoh = new Izoh();

                        izoh.Id = uint.TryParse(elem.Element("Id").Value, out ii) ? ii : 0;
                        izoh.Nom = elem.Element("Nom").Value;
                        izoh.Koefsent = ToDuoubleNuqta(elem.Element("Koy").Value);
                        list.Add(izoh);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            return list;
        }
    }
}
