using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Linq;


namespace Serialization//Вариант 14
{/*№ 14 Сериализация*/
    class Program
    {/*Задание
1. Из лабораторной №5 выберите класс с наследованием и/или
композицией/агрегацией для сериализации. Выполните
сериализацию/десериализацию объекта используя
a. бинарный,
b. SOAP,
c. JSON,
d. XML формат.
* Усложненное задание:
Для сериализации выберите класс-контейнер из лабораторной № 6.
При записи в xml формате используйте некоторые свойства класса как
атрибуты.
2. Создайте коллекцию (массив) объектов и выполните
сериализацию/десериализацию.
* Усложненное задание:
Создайте клиент и сервер на синхронных сокетах.
Нужно сериализованные данные(объект) отправить по сокету и
десериализовать на стороне клиента.
3. Используя XPath напишите два селектора для вашего XML документа.
4. Используя Linq to XML создайте новый xml- документ и напишите
несколько запросов.*/       
        static async Task Main(string[] args)
        {
            // Объект для сериализации
            Console.WriteLine("Объект для сериализации...\n");
            Mammals Tiger = new Mammals("Тигр", 3, 120);
           
            Console.WriteLine(Tiger.ToString());
            Console.WriteLine("Объект создан\n");

            /*1. Сериализация/десериализация объекта используя:*/
            Console.WriteLine("1. Сериализация/десериализация объекта используя:");

            //a. бинарный формат,
            Console.WriteLine("\na. бинарный формат,");

            // Создание объекта BinaryFormatter
            Console.WriteLine("Создание объекта BinaryFormatter...");           
            BinaryFormatter formatter = new BinaryFormatter();

            // Получение потока, куда будет записываться сериализованный объект
            Console.WriteLine("Получение потока, куда будет записываться сериализованный объект...");
            using (FileStream fs = new FileStream("mammals.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Tiger);

                Console.WriteLine("Объект сериализован.");
            }

            // Десериализация из файла mammals.dat
            Console.WriteLine("Десериализация из файла mammals.dat...");
            using (FileStream fs = new FileStream("mammals.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    Mammals newTiger = (Mammals)formatter.Deserialize(fs);
                    Console.WriteLine("Объект десериализован.");
                    Console.WriteLine(newTiger.ToString());
                }
                else
                {
                    Console.WriteLine("Файл пуст!");
                }
            }

            ////b. SOAP формат,
            //Console.WriteLine("\nb. SOAP формат,");

            //// объект для сериализации
            //Animals Whale = new Mammals("Кит", 23, 120000);

            //// создание SoapFormatter 
            //var SOAPFormatter = new SoapFormatter();

            //// создание потока (soap файл) 
            //using (var fs = new FileStream("whale.soap", FileMode.OpenOrCreate))
            //{
            //    // сериализация (сохранение объекта в поток) 
            //    SOAPFormatter.Serialize(fs, Whale.ToString());
            //    Console.WriteLine("Данные были сохранены в файл.");
            //}

            //// открытие потока (soap файл) 
            //using (var fs = new FileStream("whale.soap", FileMode.OpenOrCreate))
            //{
            //    if (fs.Length != 0)
            //    {
            //        // десериализация (создание объекта из потока) 
            //        var newWhale = SOAPFormatter.Deserialize(fs) as Mammals;
            //        Console.WriteLine("Объект десериализован.");
            //        Console.WriteLine(newWhale.ToString());
            //    }
            //    else
            //    {
            //        Console.WriteLine("soap файл пуст!");
            //    }
            //}

            //c. JSON формат,
            Console.WriteLine("\nc. JSON формат,");

            // объект для сериализации
            Animals Zebra = new Mammals() { Name = "Зебра", BodyLength = 2, Weight = 130 };

            var JSONFormatter = new DataContractJsonSerializer(typeof(Mammals));

            // сохранение данных
            using (FileStream fs = new FileStream("zebra.json", FileMode.OpenOrCreate))
            {
                // сериализация (сохранение объекта в поток) 
                JSONFormatter.WriteObject(fs, Zebra);
                Console.WriteLine("Данные были сохранены в файл.");
            }

            // чтение данных
            using (FileStream fs = new FileStream("zebra.json", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    // десериализация (создание объекта из потока) 
                    Mammals newZebra = (Mammals)JSONFormatter.ReadObject(fs);
                    Console.WriteLine("Объект десериализован.");
                    Console.WriteLine(newZebra.ToString());
                }
                else
                {
                    Console.WriteLine("json файл пуст!");
                }
            }

            //d. XML формат.
            Console.WriteLine("\nd. XML формат.");

            // объект для сериализации
            Animals Dolphin = new Mammals("Дельфин", 3, 120);

            // передача в конструктор типа класса
            XmlSerializer Xmlformatter = new XmlSerializer(typeof(Mammals));

            // получение потока, куда будет записываться сериализованный объект
            using (FileStream fs = new FileStream("dolphin.xml", FileMode.OpenOrCreate))
            {
                Xmlformatter.Serialize(fs, Dolphin);
                Console.WriteLine("Объект сериализован.");
            }

            // десериализация
            using (FileStream fs = new FileStream("dolphin.xml", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    Animals newDolphin = (Mammals)Xmlformatter.Deserialize(fs);

                    Console.WriteLine("Объект десериализован.");
                    Console.WriteLine(newDolphin.ToString());
                }
                else
                {
                    Console.WriteLine("xml файл пуст!");
                }
            }


            /*2. Создание коллекции (массива) объектов и выполнение сериализации / десериализации:*/
            Console.WriteLine("\n2. Создание коллекции (массива) объектов и выполнение сериализации / десериализации...");

            // массив для сериализации
            Animals[] animals = new Mammals[3];
            animals[0] = new Mammals("Лев", 2, 190);
            animals[1] = new Mammals("Львица", 2, 150);
            animals[2] = new Mammals("Львёнок", 1, 90);

            for (int i = 0; i < animals.Length; i++)
                Console.WriteLine(animals[i].ToString());
            Console.WriteLine();

            // сериализация
            Console.WriteLine("\nсериализация...");
            BinaryFormatter Arrayformatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("animals.dat", FileMode.OpenOrCreate))
            {
                // сериализация всего массива animals
                Arrayformatter.Serialize(fs, animals);

                Console.WriteLine("Объекты сериализованы.");
            }

            // десериализация
            Console.WriteLine("\nдесериализация...");
            using (FileStream fs = new FileStream("animals.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    Animals[] deserilizeMammals = (Animals[])Arrayformatter.Deserialize(fs);

                    foreach (Mammals m in deserilizeMammals)
                    {
                        Console.WriteLine(m.ToString());
                    }
                    Console.WriteLine("Объекты десериализованы.");
                }
                else
                {
                    Console.WriteLine("Файл пуст!");
                }
            }

            /*3. Два селектора для XML документа.*/
            Console.WriteLine("\n3. Два селектора для XML документа.\n");

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("dolphin.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            // выбор всех дочерних узлов
            Console.WriteLine("выбор всех дочерних узлов...");
            XmlNodeList childnodes1 = xRoot.SelectNodes("*");
            foreach (XmlNode n in childnodes1)
                Console.WriteLine(n.OuterXml);

            // выбор всех узллв <Name>:
            Console.WriteLine("\nвыбор всех узллв <Name>...");
            XmlNodeList childnodes2 = xRoot.SelectNodes("Name");
            foreach (XmlNode n in childnodes2)
                Console.WriteLine(n.OuterXml);

            /*4. Используя Linq to XML создайте новый xml-документ и напишите несколько запросов.*/
            Console.WriteLine("\n4. Создание нового xml-документа, используя Linq to XML и несколько запросов к нему.\n");

            XDocument xdoc = new XDocument(new XElement("phones",
                new XElement("phone",
                    new XAttribute("name", "iPhone 6"),
                    new XElement("company", "Apple"),
                    new XElement("price", "40000")),
                new XElement("phone",
                    new XAttribute("name", "Samsung Galaxy S5"),
                    new XElement("company", "Samsung"),
                    new XElement("price", "33000"))));
            xdoc.Save("phones.xml");

            XDocument xdoc2 = XDocument.Load("phones.xml");
            foreach (XElement phoneElement in xdoc2.Element("phones").Elements("phone"))
            {
                XAttribute nameAttribute = phoneElement.Attribute("name");
                XElement companyElement = phoneElement.Element("company");
                XElement priceElement = phoneElement.Element("price");

                if (nameAttribute != null && companyElement != null && priceElement != null)
                {
                    Console.WriteLine($"Смартфон: {nameAttribute.Value}");
                    Console.WriteLine($"Компания: {companyElement.Value}");
                    Console.WriteLine($"Цена: {priceElement.Value}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Создание на основании данных в xml объектов класса Phone...");
            XDocument xdoc3 = XDocument.Load("phones.xml");
            var items = from xe in xdoc3.Element("phones").Elements("phone")
                        where xe.Element("company").Value == "Samsung"
                        select new Phone
                        {
                            Name = xe.Attribute("name").Value,
                            Price = xe.Element("price").Value
                        };

            foreach (var item in items)
                Console.WriteLine($"{item.Name} - {item.Price}");
            Console.WriteLine();

            Console.WriteLine("\nИзменение содержимого простых элементов и атрибутов...\n");
            XDocument xdoc4 = XDocument.Load("phones.xml");
            XElement root = xdoc4.Element("phones");
            foreach (XElement xe in root.Elements("phone").ToList())
            {
                // изменяем название и цену
                if (xe.Attribute("name").Value == "Samsung Galaxy S5")
                {
                    xe.Attribute("name").Value = "Samsung Galaxy Note 4";
                    xe.Element("price").Value = "31000";
                }
                //если iphone - удаляем его
                else if (xe.Attribute("name").Value == "iPhone 6")
                {
                    xe.Remove();
                }
            }
            // добавляем новый элемент
            root.Add(new XElement("phone",
                        new XAttribute("name", "Nokia Lumia 930"),
                        new XElement("company", "Nokia"),
                        new XElement("price", "19500")));
            xdoc4.Save("pnones1.xml");
            // выводим xml-документ на консоль
            Console.WriteLine(xdoc4);

            Console.WriteLine("\nДобавление массива телефонов структуры Phones...\n");
            Phones[] phones = new Phones[] {
                new Phones("Iphone 6", "Apple", 34000),
                new Phones("Iphone SE", "Apple", 44000),
                new Phones("Iphone 7", "Apple", 55000),
                new Phones("Samsung Galaxy Ace", "Samsung", 7000),
                new Phones("Samsung Galaxy S10", "Samsung", 104000)
            };
            XDocument xdoc5 = new XDocument();
            XElement xphones = new XElement("phones");

            foreach (Phones i in phones)
            {
                XElement xphone = new XElement("phone");
                XAttribute xname = new XAttribute("name", i.name);
                XElement xcompany = new XElement("company", i.company);
                XElement xprice = new XElement("price", i.price);

                xphone.Add(xname, xcompany, xprice);
                xphones.Add(xphone);
            }
            xdoc5.Add(xphones);
            xdoc5.Save("phones.xml");
            // выводим xml-документ на консоль
            Console.WriteLine(xdoc5);


            Console.ReadKey();
        }
        class Phone
        {
            public string Name { get; set; }
            public string Price { get; set; }
        }
        struct Phones
        {
            public string name;
            public string company;
            public int price;


            public Phones(string name, string company, int price)
            {
                this.name = name;
                this.company = company;
                this.price = price;
            }
        }
    }   
    public interface IOrganism
    {
        void GetInfo();
    }
    [Serializable] [DataContract]
    public abstract class Animals //Животные
    {      
        public abstract string ToString();

        [DataMember]
        public string Name;
        [DataMember]
        public float BodyLength;
        [DataMember]
        public int Weight;       

        public Animals() { }    
        public Animals(string Name)
        {
            this.Name = Name;
            BodyLength = 0;
        }

        public Animals(string Name, float BodyLength)
        {
            this.Name = Name;
            this.BodyLength = BodyLength;
        }

        public int GetWeight()
        {
            return Weight;
        }

        static ref int Find(int number, int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == number)
                {
                    return ref numbers[i]; // возвращаем ссылку на адрес, а не само значение
                }
            }
            throw new IndexOutOfRangeException("число не найдено!");
        }
    }
    [Serializable] [DataContract]
    public class Mammals : Animals, IOrganism //Млекопитающие
    {
        [NonSerialized]
        private int Age;   

        public Mammals() { }            
        public Mammals(string Name) : base(Name)
        {
            BodyLength = 0;
        }
        public Mammals(string Name, float bodyLength, int weight) : base(Name)
        {
            BodyLength = bodyLength;
            Weight = weight;
        }

        public void GetInfo()
        {
            Console.WriteLine($"Название животного {Name} Длина тела: {BodyLength} Вес: {Weight}");
        }

        public new int GetWeight()
        {
            return Weight;
        }

        public override string ToString()
        {
            return string.Format("Млекопитающее: {0} \t Длина тела = {1}; Вес = {2}.", Name, BodyLength, Weight);
        }
    }

}
