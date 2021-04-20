using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using JsonFlatFileDataStore;
using EventHook;

namespace JobData
{
    public enum MouseClickType { LEFTCLICK = 1, RIGHTCLICK = 2, DOUBLECLICK = 3 };

    public class Config
    {
        private readonly string rootDir;
        private readonly string dataDir;
        private readonly string imgDir;
        private readonly string dictDir;

        public const string OBJECTS = "Objects";
        public const MouseClickType DEFMOUSE_CLICKTYPE = MouseClickType.LEFTCLICK;
        public string RootDir { get { return rootDir; } }
        public string DataDir { get { return dataDir; } }
        public string ImgDir { get { return imgDir; } }
        public string DictDir { get { return dictDir; } }

        public Config()
        {
            rootDir = AppDomain.CurrentDomain.BaseDirectory;
            dataDir = $"{rootDir}\\Data";
            imgDir = $"{rootDir}\\Img";
            dictDir = $"{rootDir}\\Dict";

            checkDIrectoryExists(dataDir);
            checkDIrectoryExists(imgDir);
            checkDIrectoryExists(dictDir);
        }

        private void checkDIrectoryExists(string dirPath) 
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }

    public class DataClass
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class StoreObject
    {
        public string ObjectName { get; }
        protected string dataDir;
        protected DataStore objects;

        public StoreObject(string pObjectName, string pDataDir)
        {
            ObjectName = pObjectName;
            dataDir = pDataDir;
            objects = new DataStore($"{dataDir}\\{ObjectName}.json");
        }

        public List<T> GetItems<T>() where T : DataClass
        {
            return objects.GetCollection<T>().GetItems()
                .Select(item => (T)item)
                .ToList();
        }

        public T GetItem<T>(int id) where T : DataClass
        {
            var collection = objects.GetCollection<T>();
            return (T)collection.AsQueryable().FirstOrDefault(o => o.Id == id);
        }

        public List<T> GetSortedItems<T>() where T : DataClass
        {
            List<T> objs = GetItems<T>();
            objs.Sort((o1, o2) => o1.Name.CompareTo(o2.Name));
            return objs;
        }

        public int GetCount<T>() where T : DataClass
        {
            var collection = objects.GetCollection<T>();
            return collection.Count;
        }

        public void Add<T>(T NewObj) where T : DataClass
        {
            var collection = objects.GetCollection<T>();
            T obj = (T)collection.AsQueryable().FirstOrDefault(o => o.Name == NewObj.Name);
            if (obj != null)
            {
                collection.ReplaceOne(obj.Id, NewObj);
            }
            else
            {
                if (!NewObj.Id.HasValue)
                {
                    NewObj.Id = collection.Count + 1;
                }
                collection.InsertOne(NewObj);
            }
        }

        public void Edit<T>(T NewObj) where T : DataClass
        {
            var collection = objects.GetCollection<T>();
            collection.ReplaceOne(NewObj.Id, NewObj);
        }

        public void Delete<T>(int id) where T : DataClass
        {
            var collection = objects.GetCollection<T>();
            collection.DeleteOne(id);
        }

        public void Delete<T>(string name) where T : DataClass
        {
            var collection = objects.GetCollection<T>();
            T obj = (T)collection.AsQueryable().FirstOrDefault(o => o.Name == name);
            if (obj != null)
            {
                collection.DeleteOne(obj.Id);
            }
        }

        public void DeleteAll<T>() where T : DataClass
        {
            var collection = objects.GetCollection<T>();
            collection.DeleteMany(o => true);
        }
    }

    public class ActObject : DataClass
    {
        public string ImageSrc { get; set; }
    }

    public class ActObjects : StoreObject
    {
        public ActObjects(string ObjectName, string DataDir) : base(ObjectName, DataDir)
        {
        }
    }

    public class Act
    {
        public Mouse.MousePoint ActPoint { get; set; }
        public MouseClickType ClickType { get; set; }
        public string KeyboardText { get; set; }
    }

    public class Operation : DataClass
    {
        public ActObject Actor { get; set; }
        public Act Action { get; set; }
    }
}
