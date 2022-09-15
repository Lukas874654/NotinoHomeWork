using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

//nepoužívané using odebrat

namespace Notino.Homework
{
    //Třída by měla být v samostatném souboru
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Cesta by neměla být natvrdo (změna adresáře programu a musím upravovat kód)
            // -> Cestu k souborům dát do appsettings (popřípadě jiný způsob konfigurace aplikace) a nespoléhat se na Environment.CurrentDirectory
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

            //Validace, že aplikace má k zadaným cestám přístup (pokud nemá, tak se může rovnou ukončit a nežere zdroje)


            //try, catch je v tomto případě zbytečný
            try
            {
                //FileStream i StreamReader by měl být zabalen do using, aby si proces zbytečně nedržel zdroje, které už nepotřebuje
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                var reader = new StreamReader(sourceStream);
                //proměnná, která se využívá i mimo try blok, takže musí být definována o úroveň výš
                string input = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                //Exception skrývá kde skutečně došlo k vyjímce
                // -> Pokud ji chcu po zpracování znova vyhodit, tak zavolat pouze throw;
                throw new Exception(ex.Message);
            }

            //XDocument má vlastní metodu na načtení souboru, takže načítání přes FileStream je podle mě zbytečné, pokud vím, že soubor je opravdu xml
            //Pokud už používám string, tak aspoň validace, že není prázdný

            var xdoc = XDocument.Parse(input);
            //Root element může být null
            // -> Pokud není žadané, aby to padlo níže na null exception, tak by tu měla být validace, která ukončí proces

            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value

                //Načtení hodnot tímto způsobem bude vyhazovat chyby, kdy element není součástí xml
                // -> Pokud nemusí být součástí, pak použít ? ... tzn xdoc.Root.Element("text")?.Value
            };

            var serializedDoc = JsonConvert.SerializeObject(doc);

            //Znova FileStream a StreamWriter zabalit do using
            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);

            //Navíc pokud nestačí exception hlášky, tak implementovat v rámci kódu u jednotlivých situací kdy k nim může dojít log
        }
    }
}
