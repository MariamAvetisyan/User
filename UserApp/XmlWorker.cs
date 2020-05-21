using System;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace UserApp
{
    public class XmlWorker
    {
        public static XmlDocument Document = new XmlDocument();

        public static void AppendToParentNode(XmlNode userNode)
        {
            Document.DocumentElement.AppendChild(userNode);
        }

        private static void AddAttribute(string attribute, string attributeValue, XmlNode userNode)
        {
            XmlAttribute UserAttribute = Document.CreateAttribute(attribute);
            UserAttribute.Value = attributeValue;
            userNode.Attributes.Append(UserAttribute);
        }

        public static void CreateDocument(string filePath)
        {
            XmlNode declarationNode = Document.CreateXmlDeclaration("1.0", "UTF-8", null);
            Document.AppendChild(declarationNode);
            XmlNode UsersNode = Document.CreateElement("Users");
            Document.AppendChild(UsersNode);
            Document.Save(filePath);
        }

        public static XmlNode CreateUserNode(User myUser)
        {

            XmlNode userNode = Document.CreateElement("User");

            AddAttribute("ID", myUser.UserId.ToString(), userNode);

            CreateElement("FirstName", myUser.FirstName, userNode);
            CreateElement("LastName", myUser.Lastname, userNode);
            CreateElement("EmailAddress", myUser.EmailAddress, userNode);
            CreateElement("PhoneNumber", myUser.PhoneNumber.ToString(), userNode);

            return userNode;
        }

        private static void CreateElement(string Value, string text, XmlNode userNode)
        {
            XmlNode elementNode = Document.CreateElement(Value);
            elementNode.AppendChild(Document.CreateTextNode(text));
            userNode.AppendChild(elementNode);
        }

        public static void EditUserById(int id, UserProperties element, string elementValue)
        {
            XmlNode UsersNode = Document.CreateElement("Users");
            XmlNode nodeToModify = Document.SelectSingleNode($"/Users/User[@ID='{id}']/{element}");
            nodeToModify.InnerText = elementValue;
        }

        internal static int GetLastUserId(string fileName)
        {
            Document.Load(fileName);
            XmlNodeList nodeList = Document.SelectNodes($"/Users/User");
            XmlNode lastNode = nodeList[nodeList.Count - 1];
            if (lastNode == null)
                return 0;

            return Convert.ToInt32(lastNode.Attributes[0].Value);
        }

        public static void LoadDocument(string filePath)
        {
            Document.Load(filePath);
        }

        public static void RemoveUserById(int id)
        {
            XmlNode UsersNode = Document.CreateElement("Users");
            XmlNode nodeToRemove = Document.SelectSingleNode($"/Users/User[@ID='{id}']");
            nodeToRemove.ParentNode.RemoveChild(nodeToRemove);
        }

        public static void SaveDocument(string filePath)
        {
            Document.Save(filePath);
        }


        public static void ShowUserById(string filePath, int Id)
        {
            Document.Load(filePath);

            foreach (XmlNode node in Document.DocumentElement)
            {
                if (node.Attributes[0].InnerText == Id.ToString())
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        Console.WriteLine(childNode.LocalName + " : " + childNode.InnerText);
                    }
                }
            }
        }

        public static bool IsValidId(string filePath, int id)
        {
            XDocument doc = XDocument.Load(filePath);
            bool isValidId = doc.Descendants()
                    .Where(x => (string)x.Attribute("ID") == id.ToString())
                    .FirstOrDefault() == null ? false : true;
            return isValidId;
        }

        public static bool IsValidElement(string filePath, int id, string Element)
        {
            XDocument doc = XDocument.Load(filePath);
            bool isValidId = doc.Descendants()
                    .Where(x => (string)x.Attribute("ID") == id.ToString())
                    .FirstOrDefault() == null ? false : true;
            return isValidId;
        }

        public static void RemoveAllUsers()
        {
            while (Document.DocumentElement.ChildNodes.Count > 0)
            {

                XmlNode nodeToRemove = Document.SelectSingleNode($"/Users/User");
                nodeToRemove.ParentNode.RemoveChild(nodeToRemove);
            }
        }

        public static void ShowAllUsers(string filePath)
        {
            Document.Load(filePath);
            if (Document.DocumentElement.HasChildNodes)
            {
                foreach (XmlNode nodes in Document.DocumentElement)
                {
                    Console.WriteLine(new string('*', 30));
                    Console.WriteLine("ID : " + nodes.Attributes[0].Value);
                    foreach (XmlNode node in nodes.ChildNodes)
                    {
                        Console.WriteLine(node.LocalName + " : " + node.InnerText);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no users to display");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
